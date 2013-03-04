using LocationBasedNotifications.Contracts;
using LocationBasedNotifications.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationBasedNotifications.Model;
namespace LocationBasedNotifications
{
    public class MapViewModel :  ViewModelBase
    {
        #region Private Members
        private Location _location;
        private MapMode _mapMode;
        #endregion Private Members

        #region Public Properties
        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public MapMode MapMode
        {
            get { return _mapMode; }
            set { _mapMode = value; }
        }
        #endregion Public Properties

        #region Constructors     
        public MapViewModel() : this(null,MapMode.Edit)
        {
        }
        public MapViewModel(Location location, MapMode mapMode)
        {
            Location = location;
            MapMode = mapMode;
        }
        #endregion Constructors

        #region Public Methods
        public void LoadLocationById(int locationId)
        {
           Location =  base.Repository.GetLocationById(locationId);
        }
        #endregion Public Methods

    }
}
