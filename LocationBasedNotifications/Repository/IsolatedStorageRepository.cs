using LocationBasedNotifications.Contracts;
using LocationBasedNotifications.Model;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Repository
{
    public class IsolatedStorageRepository : IIsolatedStorageRepository<Location>
    {
        #region IIsolatedStorageRepository
        public IEnumerable<Location> GetInMemoryLocations()
        {
            IEnumerable<Location> result = null;
            try
            {
                IsolatedStorageSettings.ApplicationSettings.TryGetValue<IEnumerable<Location>>(Constants.LBNLocations, out result);
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        public bool AddItem(Location itemToAdd)
        {
            if (itemToAdd == null)
            {
                throw new ArgumentException("location can't be null");
            }

            IEnumerable<Location> result = null;
            try
            {
                IsolatedStorageSettings.ApplicationSettings.TryGetValue<IEnumerable<Location>>(Constants.LBNLocations, out result);
            }
            catch (Exception)
            {
                result = new List<Location>();
            }

            List<Location> aux;
            if (result != null)
            {
                aux = result.ToList();
            }
            else
            {
                aux = new List<Location>();
            }
            
            aux.Add(itemToAdd);
            this.Save(aux.AsEnumerable<Location>());
            return true;
        }

        public bool RemoveItem(Location itemToRemove)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<Location> list)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Any(s => string.Equals(s.Key, Constants.LBNLocations, StringComparison.CurrentCultureIgnoreCase)))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(Constants.LBNLocations);
            }
            IsolatedStorageSettings.ApplicationSettings[Constants.LBNLocations] = list;
        }
        #endregion IIsolatedStorageRepository



    }
}
