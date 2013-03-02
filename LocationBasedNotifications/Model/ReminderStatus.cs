using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    [Table(Name="ReminderStatuses")]
    public class ReminderStatus : BaseModel
    {
        private int _reminderStatusId;
        private string _value;
        

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ReminderStatusId
        {
            get { return _reminderStatusId; }
            set { _reminderStatusId = value; }
        }
        
        [Column]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        
        [Column(IsVersion = true)]
        private Binary _version;
        
        #region Constructors
        public ReminderStatus() : this(string.Empty)
        {
                
        }
        public ReminderStatus(string value)
        {
            Value = value;
        }
        #endregion Constructors

        #region Overrides
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            if (!Object.ReferenceEquals(this, obj))
            {
                return false;
            }

            ReminderStatus other = obj as ReminderStatus;

            return this.ReminderStatusId == other.ReminderStatusId &&
                this.Value == other.Value;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 23 + ReminderStatusId.GetHashCode();
                if (Value != null)
                {
                    hash = hash * 23 + Value.GetHashCode();
                }
            }
            return hash;
        }

        #endregion Overrides

    }
}
