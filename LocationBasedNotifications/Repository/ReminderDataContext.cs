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
        public ReminderDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Location> Locations;

    }
}
