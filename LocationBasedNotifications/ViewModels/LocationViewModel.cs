using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    public class LocationViewModel : ViewModelBase
    {
        #region Private Members
        private ObservableCollection<Location> _myLocations;
        private Location _myLocation;
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
        #endregion Properties

        #region Constructors
        public LocationViewModel()
        {
            MyLocations = new ObservableCollection<Location>();
            MyLocation = new Location();
        }
        #endregion Constructors

        #region Private Methods
        #endregion Private Methods


        #region Public Methods
        public void LoadLocationForEdit(int locationId)
        {
            MyLocation = base.Repository.GetLocationById(locationId);
        }
        public void PopulateModelWithStorageData()
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
        public void SaveMyLocation()
        {
            if (MyLocation != null)
            {
                base.Repository.AddLocation(MyLocation);
                base.Repository.Save();
            }
        }
        public void RemoveLocation()
        {
            if (MyLocation != null)
            {
                base.Repository.DeleteLocation(MyLocation);
                base.Repository.Save();
                _myLocations.Remove(MyLocation);
            }
        }
        #endregion Public Methods
    }
}
