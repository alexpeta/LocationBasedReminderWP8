﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    public class LocationsViewModel : BaseModel
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

                NotifyPropertyChanging("SelectedItem");
                _selectedItem = value;                
                NotifyPropertyChanged("SelectedItem");

            }
        }
        #endregion Properties

        #region Constructors
        public LocationsViewModel()
        {
            MyLocations = new ObservableCollection<Location>();
            SelectedItem = null;
        }
        #endregion Constructors
    }
}