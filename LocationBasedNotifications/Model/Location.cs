using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Model
{
    public class Location
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _latitude;
        public int Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private int _longitude;
        public int Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion Properties

        #region Constructors
        public Location(string name, int latitude,int longitude, string description)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
        }
        #endregion Constructors
    }
}
