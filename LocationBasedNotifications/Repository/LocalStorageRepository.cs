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
            var results = from d in _db.Locations
                          select d;

            return results;
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

        public void Save(IEnumerable<Location> list)
        {
            throw new NotImplementedException();
        }
        public Location GetItemById(int id)
        {
            Location result = _db.Locations.FirstOrDefault(l => l.LocationId == id);

            return result;
        }
        #endregion IRepository



    }
}
