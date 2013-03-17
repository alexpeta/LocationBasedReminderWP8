using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
        protected override async void OnInvoke(ScheduledTask task)
        {
            //1.Get current geo position.
            //2.update WP8 live tile with info.
            //Geoposition geoposition = await TryGetGeoCoordinate();
            //if (geoposition != null)
            //{

            //}

            UpdateLiveTile();


            NotifyComplete();
        }

        private async Task<Geoposition> TryGetGeoCoordinate()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 100;

            Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(1), timeout: TimeSpan.FromSeconds(30));
            return geoposition;
        }

        private async Task<List<Reminder>> GetActiveRemindersAsync()
        {

            return null;
        }

        private void UpdateLiveTile()
        {
            FlipTileData primaryTileData = new FlipTileData();
            primaryTileData.Count = 69;
            primaryTileData.BackContent = "updated via agent!";

            ShellTile primaryTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("MainPage"));
            if (primaryTile != null)
            {
                primaryTile.Update(primaryTileData);
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