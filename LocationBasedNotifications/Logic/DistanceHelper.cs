using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Logic
{
    public class DistanceHelper
    {
        #region Private Members
        //represents the radius of the Earth in KM
        private readonly int R = 6371;
        private Location _start;
        private Location _end;
        private double _distance;
        #endregion Private Members

        #region Public Properties
        public Location Start
        {
            get { return _start; }
            set { _start = value; }
        }
        public Location End
        {
            get { return _end; }
            set { _end = value; }
        }
        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
            }
        }
        #endregion Public Properties

        #region Constructors
        public DistanceHelper(Location start, Location end)
        {
            Start = start;
            End = end;

            CalculateDistance();
        }
        #endregion Constructors
        
        #region Statics
        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
        #endregion Statics

        #region Private Methods
        private void CalculateDistance()
        {
            double dLat = DistanceHelper.DegreesToRadians(End.Latitude - Start.Latitude);
            double dLon = DistanceHelper.DegreesToRadians(End.Longitude - Start.Longitude);

            double lat1 = DistanceHelper.DegreesToRadians(Start.Latitude);
            double lat2 = DistanceHelper.DegreesToRadians(End.Latitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            this.Distance = R * c;
        }
        #endregion Private Methods
    }
}
