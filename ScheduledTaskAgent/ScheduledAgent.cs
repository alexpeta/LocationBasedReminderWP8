using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Device.Location;

namespace ScheduledTaskAgent
{
    public class ScheduledAgent : Microsoft.Phone.Scheduler.ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            //ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
            if (TryGetGeoCoordinate())
            {
                NotifyComplete();
            }
            else
            {
 
            }
        }

        private bool TryGetGeoCoordinate()
        {
            bool result = false;
            //try
            //{
                GeoCoordinateWatcher locationWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                locationWatcher.MovementThreshold = 200;
                locationWatcher.PositionChanged += OnPositionChanged;
                result = locationWatcher.TryStart(true, TimeSpan.FromSeconds(10));
            //}
            //catch (Exception)
            //{                
            //    throw;
            //}
            return result;

        }

        private void OnPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            GeoCoordinateWatcher raiser = sender as GeoCoordinateWatcher;
            if (raiser != null)
            {
                raiser.Stop();
                SendToastReminder();
            }
        }

        private void SendToastReminder()
        {
            ShellToast toast = new ShellToast();
            toast.Title = "LBR";
            toast.Content = "You have arrived!!";
            toast.NavigationUri = new Uri("/ReminderPivot.xaml", UriKind.Relative);
            toast.Show();
        }
    }
}