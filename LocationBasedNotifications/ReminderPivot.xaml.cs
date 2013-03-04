using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LocationBasedNotifications.Repository;
using LocationBasedNotifications.Contracts;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using Windows.Devices.Geolocation;
using LocationBasedNotifications.Helpers;

namespace LocationBasedNotifications
{
    public partial class ReminderPivot : PhoneApplicationPage
    {
        #region Reminder Members
        private ReminderViewModel _model;
        #endregion Reminder Members

        #region Constructors
        public ReminderPivot()
        {
            InitializeComponent();
            CreateAppBar();
            _model = new ReminderViewModel();
            this.DataContext = _model;
        }
        #endregion Constructors


        #region Private Methods
        private void CreateAppBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.IsMenuEnabled = true;

            ApplicationBarIconButton addBarButton = new ApplicationBarIconButton();
            addBarButton.IconUri = new Uri("/Assets/new.png", UriKind.Relative);
            addBarButton.Text = "New Reminder";
            addBarButton.IsEnabled = true;
            addBarButton.Click += CreateReminderButton_Click;
            ApplicationBar.Buttons.Add(addBarButton);

        }

        #region Click Event Handlers
        private void CreateReminderButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateReminder.xaml", UriKind.Relative));
        }
        #endregion Click Event Handlers

        private void ViewMap_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int castedReminderId = 0;
                try
                {
                    castedReminderId = Convert.ToInt32(button.Tag);
                }
                catch (Exception)
                {
                    castedReminderId = -1;
                }

                NavigationService.Navigate(new Uri(string.Format("/Map.xaml?locationId={0}", castedReminderId.ToString()), UriKind.Relative));

            }
        }
        #endregion  Private Methods
    }
}