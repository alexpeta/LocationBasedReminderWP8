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
        public ReminderStatus() : this(0,string.Empty)
        {
                
        }
        public ReminderStatus(int reminderId,string value)
        {
            ReminderStatusId = reminderId;
            Value = value;
        }
        #endregion Constructors

    }
}
