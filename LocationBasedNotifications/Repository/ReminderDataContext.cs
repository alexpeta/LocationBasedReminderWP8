using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Repository
{
    public class ReminderDataContext : DataContext
    {
        #region Constructors
        public ReminderDataContext(string connectionString)
            : base(connectionString)
        {
        }
        #endregion Constructors

        #region Public Properties
        public Table<Location> Locations;
        public Table<ReminderStatus> ReminderStatuses;
        #endregion Public Properties

    }
}
