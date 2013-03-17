using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    [Table(Name="Reminders")]
    public class Reminder : BaseModel
    {
        #region Private Members
        private int _reminderId;
        private int _accurecy;
        private string _name;
        private double _distanceToLocation;
        private int _locationId;
        private int _reminderStatusId;
        private EntityRef<ReminderStatus> _status;
        private EntityRef<Location> _location;
        private Binary _version;
        #endregion Private Members

        #region Public Properties
        [Column(IsVersion = true)]
        public Binary Version 
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ReminderId
        {
            get { return _reminderId; }
            set { _reminderId = value; }
        }
        
        [Column]
        public int Accurecy
        {
            get { return _accurecy; }
            set 
            {
                if (_accurecy == value)
                {
                    return;
                }

                NotifyPropertyChanging("Accurecy");
                _accurecy = value;
                NotifyPropertyChanged("Accurecy");
            }
        }

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

        public double DistanceToLocation
        {
            get { return _distanceToLocation; }
            set 
            {
                if (_distanceToLocation == value)
                {
                    return;
                }

                NotifyPropertyChanging("DistanceToLocation");
                _distanceToLocation = value;
                NotifyPropertyChanged("DistanceToLocation");
            }
        }

        [Column]
        public int LocationId
        {
            get { return _locationId; }
            set 
            {
                if (_locationId == value)
                {
                    return;
                }
                NotifyPropertyChanging("LocationId");
                _locationId = value;
                NotifyPropertyChanged("LocationId");
            }
        }

        [Column]
        public int ReminderStatusId
        {
            get
            {
                return _reminderStatusId;
            }
            set
            {
                if (_reminderStatusId == value)
                {
                    return;
                }
                NotifyPropertyChanging("ReminderStatusId");
                _reminderStatusId = value;
                NotifyPropertyChanged("ReminderStatusId");
            }
        }

        [Association(Storage = "_status", ThisKey = "ReminderStatusId", OtherKey = "ReminderStatusId", IsForeignKey = true, DeleteOnNull = true, DeleteRule = "CASCADE")]
        public ReminderStatus Status
        {
            get { return _status.Entity; }
            set { _status.Entity = value; }
        }

        [Association(Storage = "_location", ThisKey = "LocationId", OtherKey = "LocationId", IsForeignKey = true, DeleteOnNull = true, DeleteRule="CASCADE")]
        public Location Location
        {
            get { return _location.Entity; }
            set { _location.Entity = value; }
        }


        #endregion Public Properties

        #region Constructors
        public Reminder()
        {
        }
        #endregion Constructors

        #region Overrides
        public override string ToString()
        {
            string result = null;
            if(this.Name != null)
            {
                result = Name;
            }
            return result;
        }

        #endregion Overrides

        #region Public Methods
        public Reminder DeepCopy()
        {
            Reminder copy = new Reminder();
            copy.ReminderId = ReminderId;
            copy.Accurecy = Accurecy;
            copy.Name = Name;
            copy.DistanceToLocation = DistanceToLocation;
            copy.LocationId = LocationId;
            copy.Location = Location;
            copy.Status = Status;
            copy.ReminderStatusId = ReminderStatusId;

            return copy;
        }
        #endregion Public Methods
    }
}
