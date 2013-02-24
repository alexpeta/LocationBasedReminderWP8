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
        private void CreateRemindersButtton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateLocation.xaml", UriKind.Relative));
        }

        private void ViewLocationsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MyLocations.xaml", UriKind.Relative));
        }
        #endregion Button Handlers

    }
}