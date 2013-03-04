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
using LocationBasedNotifications.Helpers;

namespace LocationBasedNotifications
{
    public partial class CreateEditLocation : PhoneApplicationPage
    {
        #region Private Members
        private LocationViewModel _model;
        #endregion Private Members

        #region Constructors
        public CreateEditLocation()
        {
            InitializeComponent();
            _model = new LocationViewModel();
            this.DataContext = _model;
        }
        #endregion Constructors

        #region Button Handlers
        private void SaveLocationButton_Click(object sender, RoutedEventArgs e)
        {
            _model.SaveMyLocation();
            NavigationService.Navigate(new Uri(string.Format("/MyLocations.xaml?locationID={0}", _model.MyLocation.LocationId), UriKind.Relative));
        }
        #endregion Button Handlers


        #region Overrides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int locationId =0;
            try
            {
                locationId = int.Parse(NavigationContext.QueryString["locationId"]);
            }
            catch (Exception)
            {
                locationId = -1;
            }

            if (locationId != 0 && locationId != -1)
            {
                PageName.Text = "Edit Location";
                _model.LoadLocationForEdit(locationId);
            }
            else
            {
                PageName.Text = "Add Location";
            }

            base.OnNavigatedTo(e);
        }
        #endregion Overrides
    }
}