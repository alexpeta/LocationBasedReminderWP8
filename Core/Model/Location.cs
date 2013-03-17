using Microsoft.Phone.Maps.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LocationBasedNotifications
{
    [Table(Name="Locations")]
    public class Location : BaseModel
    {
        #region Private Members
        private int _locationId;
        private string _name;
        private double _latitude;
        private double _longitude;
        private string _description;
        #endregion Private Members

        #region Properties
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }
        
        [Column(CanBeNull=false)]
        public string Name
        {
            get { return _name; }
            set 
            {
                if (_name == value)
                {
                    return;
                }

                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        
        [Column(CanBeNull = false)]
        public double Latitude
        {
            get { return _latitude; }
            set 
            {
                if (_latitude == value)
                {
                    return;
                }

                NotifyPropertyChanging("Latitude");
                _latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }
        
        [Column(CanBeNull = false)]
        public double Longitude
        {
            get { return _longitude; }
            set 
            {
                if (_longitude == value)
                {
                    return;
                }

                NotifyPropertyChanging("Longitude");
                _longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }
        
        [Column]
        public string Description
        {
            get { return _description; }
            set 
            {
                if (_description == value)
                {
                    return;
                }

                NotifyPropertyChanging("Description");
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        //[Association(Storage = "_reminder", ThisKey = "_reminderId", OtherKey = "ReminderId", IsForeignKey = true, DeleteOnNull = true)]
        //public Reminder Reminder
        //{
        //    get
        //    {
        //        return _reminder.Entity;
        //    }
        //    set
        //    {
        //        _reminder.Entity = value;
        //    }
        //}


        public GeoCoordinate GeoCoordinate
        {
            get 
            {
                return new GeoCoordinate(Latitude, Longitude);
            }
        }
        #endregion Properties

        #region Constructors
        public Location() : this(string.Empty,0D,0D,string.Empty)
        {
        }
        public Location(string name, double latitude, double longitude, string description)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
        }
        #endregion Constructors

        #region Public Methods
        public Pushpin GetSimplePushpin(Color backgroundColor)
        {
            Pushpin locationPushpin = new Pushpin();
            locationPushpin.Background = new SolidColorBrush(backgroundColor);
            locationPushpin.Content = Name;
            //locationPushpin.Tag = "locationPushpin";

            return locationPushpin;
        }
        public Location DeepCopy()
        {
            Location result = new Location();
            
            result.Longitude = Longitude;
            result.Latitude = Latitude;
            result.Name = Name;
            result.Description = Description;
            result.LocationId = LocationId;

            return result;
        }
        #endregion Public Methods
    }
}
