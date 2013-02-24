using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Model
{
    public class Location : BaseModel
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
                RaiseNotifyPropertyChanged("Name");
            }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set 
            {
                if (_latitude == value)
                {
                    return;
                }
                _latitude = value;
                RaiseNotifyPropertyChanged("Latitude");
            }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set 
            {
                if (_longitude == value)
                {
                    return;
                }
                _longitude = value;
                RaiseNotifyPropertyChanged("Longitude");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set 
            {
                if (_description == value)
                {
                    return;
                }
                _description = value;
                RaiseNotifyPropertyChanged("Description");
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


    }
}
