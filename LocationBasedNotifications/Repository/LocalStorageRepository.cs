using LocationBasedNotifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Repository
{
    public class LocalStorageRepository : IRepository
    {
        #region Private Members
        private ReminderDataContext _db;
        #endregion Private Members

        #region Constructors
        public LocalStorageRepository()
        {
            _db = App.LocalDB;
        }
        #endregion Constructors

        #region IRepository
        public IEnumerable<Location> GetLocationsList()
        {
            List<Location> result = null;

            if (_db.Locations != null)
            {
                result = _db.Locations.ToList();
            }

            return result;
        }
        public bool AddLocation(Location itemToAdd)
        {
            if (itemToAdd == null)
            {
                throw new ArgumentNullException("location cant be null");
            }

            try
            {
                _db.Locations.InsertOnSubmit(itemToAdd);
                _db.SubmitChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool DeleteLocation(Location itemToRemove)
        {
            if (itemToRemove == null)
            {
                throw new ArgumentNullException("location cant be null");
            }

            try
            {
                if (_db.Reminders != null)
                {
                    IEnumerable<Reminder> remindersWithSelectedLocation = _db.Reminders.Where(r => r.LocationId == itemToRemove.LocationId);
                    _db.Reminders.DeleteAllOnSubmit(remindersWithSelectedLocation);
                }

                _db.Locations.DeleteOnSubmit(itemToRemove);
                _db.SubmitChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public Location GetLocationById(int id)
        {
            Location result = _db.Locations.FirstOrDefault(l => l.LocationId == id);

            return result;
        }
        public IEnumerable<Reminder> GetRemindersList()
        {
            List<Reminder> result = null;

            if (_db.Locations != null)
            {
                result = _db.Reminders.ToList();
            }

            return result;
        }
        public ReminderStatus GetStatusById(int id)
        {
            ReminderStatus result = null;
            if (_db.ReminderStatuses != null)
            {
                result = _db.ReminderStatuses.SingleOrDefault(s => s.ReminderStatusId == id);
            }
            return result;
        }
        public void AddReminder(Reminder reminder)
        {
            if (reminder != null)
            {
                _db.Reminders.InsertOnSubmit(reminder);
                _db.SubmitChanges();
            }
        }
        public IEnumerable<ReminderStatus> GetReminderStatusesList()
        {
            List<ReminderStatus> result = null;

            if (_db.Locations != null)
            {
                result = _db.ReminderStatuses.ToList();
            }

            return result;
        }
        public IEnumerable<Reminder> GetRemindersByStatusId(int statusId)
        {
            List<Reminder> result = null;

            if (_db.Locations != null)
            {
                result = _db.Reminders.Where(r => r.ReminderStatusId == statusId).ToList();
            }

            return result;
        }
        public bool DeleteReminder(Reminder reminder)
        {
            if (reminder == null)
            {
                throw new ArgumentNullException("reminder");
            }

            try
            {
                _db.Reminders.DeleteOnSubmit(reminder);
                _db.SubmitChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        #endregion IRepository




    }
}
