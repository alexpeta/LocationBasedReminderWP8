using LocationBasedNotifications.Contracts;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Repository
{
    public class IsolatedStorageRepository : IRepository
    {
        #region IIsolatedStorageRepository
        public IEnumerable<Location> GetLocationsList()
        {
            throw new NotImplementedException();
        }
        public bool AddLocation(Location itemToAdd)
        {
            throw new NotImplementedException();
        }
        public bool DeleteLocation(Location itemToRemove)
        {
            throw new NotImplementedException();
        }
        public Location GetLocationById(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Reminder> GetRemindersList()
        {
            throw new NotImplementedException();
        }
        public ReminderStatus GetStatusById(int id)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
        public void AddReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ReminderStatus> GetReminderStatusesList()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Reminder> GetRemindersByStatusId(int statusId)
        {
            throw new NotImplementedException();
        }
        public bool DeleteReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }
        public void UpdateReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }
        public Reminder GetReminderById(int reminderId)
        {
            throw new NotImplementedException();
        }
        #endregion IIsolatedStorageRepository

    }
}
