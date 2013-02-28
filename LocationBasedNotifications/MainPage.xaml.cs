using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LocationBasedNotifications.Resources;
using Windows.Storage;
using Microsoft.Phone.Scheduler;
using LocationBasedNotifications.Repository;

namespace LocationBasedNotifications
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }   

        #region Button Handlers
        private void ViewLocationsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MyLocations.xaml", UriKind.Relative));
        }
        private void ManageReminderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ReminderPivot.xaml", UriKind.Relative));
        }
        private void ViewMapButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Map.xaml", UriKind.Relative));
        }
        #endregion Button Handlers



    }
}