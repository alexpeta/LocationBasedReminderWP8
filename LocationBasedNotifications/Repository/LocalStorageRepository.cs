using LocationBasedNotifications.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Repository
{
    public class LocalStorageRepository : IRepository<Location>
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
        public IEnumerable<Location> GetInMemoryItems()
        {
            List<Location> result = null;

            if (_db.Locations != null)
            {
                result = _db.Locations.ToList();
            }

            return result;
        }

        public bool AddItem(Location itemToAdd)
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

        public bool RemoveItem(Location itemToRemove)
        {
            if (itemToRemove == null)
            {
                throw new ArgumentNullException("location cant be null");
            }

            try
            {
                _db.Locations.DeleteOnSubmit(itemToRemove);
                _db.SubmitChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Location GetItemById(int id)
        {
            Location result = _db.Locations.FirstOrDefault(l => l.LocationId == id);

            return result;
        }

        public void Save(IEnumerable<Location> list)
        {
            throw new NotImplementedException();
        }
        #endregion IRepository

        public List<Reminder> GetRemindersList()
        {
            List<Reminder> result = null;

            if (_db.Locations != null)
            {
                result = _db.Reminders.ToList();
            }

            return result;
        }

    }
}
