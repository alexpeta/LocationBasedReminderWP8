using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Logic
{
    public sealed class DistanceHelper
    {
        #region Private Members
        //represents the radius of the Earth in KM
        private static readonly int R = 6371;
        #endregion Private Members

        #region Constructors
        private DistanceHelper()
        {
        }
        #endregion Constructors
        
        #region Statics
        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
        #endregion Statics

        #region Private Methods
        private static double CalculateDistance(Location start, Location end)
        {
            double dLat = DistanceHelper.DegreesToRadians(end.Latitude - start.Latitude);
            double dLon = DistanceHelper.DegreesToRadians(end.Longitude - start.Longitude);

            double lat1 = DistanceHelper.DegreesToRadians(start.Latitude);
            double lat2 = DistanceHelper.DegreesToRadians(end.Latitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }
        #endregion Private Methods

        #region Public Methods
        public static double CalculateDistanceInKilometers(Location start, Location end)
        {
            return CalculateDistance(start,end);
        }
        public static double CalculateDistanceInMeters(Location start, Location end)
        {
            return CalculateDistance(start,end)*1000;
        }
        


        #endregion
    }
}
