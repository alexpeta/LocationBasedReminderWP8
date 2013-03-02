using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace LocationBasedNotifications.Helpers
{
    public sealed class HelperMethods
    {
        #region Constructors
        private HelperMethods()
	    {
	    }
        #endregion Constructors

        #region Statics
        public static async Task<Geocoordinate> GetCurrentLocation()
        {
            Geocoordinate result = null;
            bool agreement = false;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<bool>(Constants.LocationAgreement, out agreement) == false)
            {
                MessageBoxResult answer = MessageBox.Show("Allow the app to use your current position?", "User Validation", MessageBoxButton.OKCancel);
                if (answer == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings[Constants.LocationAgreement] = agreement = true;
                }
            }

            if (agreement)
            {
                Geolocator locator = new Geolocator();
                locator.DesiredAccuracyInMeters = Constants.AccurecyInMeters;

                IAsyncOperation<Geoposition> currentAsyncOperation = locator.GetGeopositionAsync(TimeSpan.FromMinutes(Constants.PositionMaximumAge), TimeSpan.FromSeconds(Constants.Timeout));

                Geoposition currentPosition = await currentAsyncOperation;
                if (currentPosition != null && currentPosition.Coordinate != null)
                {
                    result = currentPosition.Coordinate;
                }
            }            

            return result;
        }
        #endregion Statics
    }
}
