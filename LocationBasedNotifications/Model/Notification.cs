using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Model
{
    public class Notification :  BaseModel
    {
        #region Properties
        private Location _location;
        public Location MyLocation
        {
            get { return _location; }
            set { _location = value; }
        }
        private string _reason;
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        #endregion Properties

        #region Constructors
        public Notification(Location location, string reason)
        {
            MyLocation = location;
            Reason = reason;
        }
        #endregion Constructors


    }
}
