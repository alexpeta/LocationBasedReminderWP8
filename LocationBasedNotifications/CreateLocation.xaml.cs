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
using LocationBasedNotifications.Contracts;
using LocationBasedNotifications.Repository;

namespace LocationBasedNotifications
{
    public partial class CreateReminder : PhoneApplicationPage
    {
        #region Private Members
        private LocationViewModel _model;
        #endregion Private Members

        #region Constructors
        public CreateReminder()
        {
            InitializeComponent();
            _model = new LocationViewModel();
            this.DataContext = _model;
        }
        #endregion Constructors

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

                    _model.MyLocation.Latitude = currentPosition.Coordinate.Latitude;
                    _model.MyLocation.Longitude = currentPosition.Coordinate.Longitude;

                }
            }            
        }
        private void CreateLocationButton_Click(object sender, RoutedEventArgs e)
        {
            _model.SaveMyLocation();
            NavigationService.Navigate(new Uri(string.Format("/MyLocations.xaml?locationID={0}", _model.MyLocation.LocationId), UriKind.Relative));
        }
        #endregion Button Handlers
    }
}