using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Windows.Devices.Geolocation;
using Windows.Foundation;



namespace LocationBasedNotifications
{
    public partial class CreateReminder : PhoneApplicationPage
    {
        public CreateReminder()
        {
            InitializeComponent();
        }

        #region Button Handlers
        private async void GetMyLocationButton_Click(object sender, RoutedEventArgs e)
        {
            bool agreement = false;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<bool>(Constants.LocationAgreement, out agreement) == false)
            {
                MessageBoxResult result = MessageBox.Show("Allow the app to use your current position?", "User Validation", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings[Constants.LocationAgreement] = agreement = true;
                }
            }

            if (agreement)
            {
                Geolocator locator = new Geolocator();
                locator.DesiredAccuracyInMeters = Constants.AccurecyInMeters;
                
                IAsyncOperation<Geoposition> currentAsyncOperation = locator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10));

                Geoposition currentPosition = await currentAsyncOperation;
                if (currentPosition != null && currentPosition.Coordinate != null)
                {
                    LatitudeCoordinate.Text = currentPosition.Coordinate.Latitude.ToString("0.00");
                    LongitudeCoordinate.Text = currentPosition.Coordinate.Longitude.ToString("0.00");
                }
            }            
        }
        #endregion Button Handlers
    }
}