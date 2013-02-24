using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    [Table]
    public class Location : BaseModel
    {
        #region Properties
        private int _locationId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }
        
        private string _name;

        [Column]
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

        private double _latitude;

        [Column]
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

        private double _longitude;

        [Column]
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

        private string _description;

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

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
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


    }
}
