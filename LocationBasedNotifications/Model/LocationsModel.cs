using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Model
{
    public class LocationsModel : INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Location> _myLocations;
        public ObservableCollection<Location> MyLocations
        {
            get { return _myLocations; }
            set { _myLocations = value; }
        }

        private Location _selectedItem;
        public Location SelectedItem
        {
            get { return _selectedItem; }
            set 
            {
                if (_selectedItem == value)
                {
                    return;
                }

                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
        #endregion Properties

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged

        #region Constructors
        public LocationsModel()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue<ObservableCollection<Location>>(Constants.LBNLocations, out _myLocations))
            {
                _myLocations = new ObservableCollection<Location>();
            }
            SelectedItem = null;
        }
        #endregion Constructors
    }
}
