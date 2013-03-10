using LocationBasedNotifications.Helpers;
using LocationBasedNotifications.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace LocationBasedNotifications
{
    public class LocationViewModel : ViewModelBase
    {
        #region Private Members
        private ObservableCollection<Location> _myLocations;
        private Location _myLocation;
        private ICommand _getGeolocationCommand;
        #endregion

        #region Properties
        public ObservableCollection<Location> MyLocations
        {
            get { return _myLocations; }
            set { _myLocations = value; }
        }
        public Location MyLocation
        {
            get { return _myLocation; }
            set
            {
                if (_myLocation == value)
                {
                    return;
                }

                NotifyPropertyChanging("MyLocation");
                _myLocation = value;
                NotifyPropertyChanged("MyLocation");
            }
        }
        public ICommand GetGeolocationCommand
        {
            get
            {
                return _getGeolocationCommand;
            }
            set
            {
                _getGeolocationCommand = value;
            }
        }
        #endregion Properties

        #region Constructors
        public LocationViewModel()
        {
            MyLocations = new ObservableCollection<Location>();
            MyLocation = new Location();
            GetGeolocationCommand = new DelegateCommand(OnGetGeolocationCommand);

            PopulateModelWithStorageData();
        }

        #endregion Constructors

        #region Private Methods
        private async void OnGetGeolocationCommand(object obj)
        {
            Geocoordinate currentPosition = await HelperMethods.GetCurrentLocation();
            if (currentPosition != null)
            {
                MyLocation.Latitude = currentPosition.Latitude;
                MyLocation.Longitude = currentPosition.Longitude;
            }
        }
        private void PopulateModelWithStorageData()
        {
            IEnumerable<Location> locations = base.Repository.GetLocationsList();
            if (locations != null)
            {
                foreach (var location in locations)
                {
                    MyLocations.Add(location);
                }
            }
        }
        #endregion Private Methods


        #region Public Methods
        public void LoadLocationForEdit(int locationId)
        {
            MyLocation = base.Repository.GetLocationById(locationId);
        }
        public void SaveMyLocation()
        {
            base.Repository.AddLocation(MyLocation);
        }
        public void RemoveLocation()
        {
            base.Repository.DeleteLocation(MyLocation);
            _myLocations.Remove(MyLocation);
        }
        #endregion Public Methods

        
    }
}
