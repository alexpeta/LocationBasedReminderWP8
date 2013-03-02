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

        public bool AddLocation(Location itemToAdd)
        {

            //OBSOLETE REFACTOR THIS!!

            //if (itemToAdd == null)
            //{
            //    throw new ArgumentException("location can't be null");
            //}

            //IEnumerable<Location> result = null;
            //try
            //{
            //    IsolatedStorageSettings.ApplicationSettings.TryGetValue<IEnumerable<Location>>(Constants.LBNLocations, out result);
            //}
            //catch (Exception)
            //{
            //    result = new List<Location>();
            //}

            //List<Location> aux;
            //if (result != null)
            //{
            //    aux = result.ToList();
            //}
            //else
            //{
            //    aux = new List<Location>();
            //}
            
            //aux.Add(itemToAdd);
            //this.Save(aux.AsEnumerable<Location>());
            return true;
        }

        public bool RemoveLocation(Location itemToRemove)
        {
            throw new NotImplementedException();
        }

        //public void Save(IEnumerable<Location> list)
        //{
        //    if (IsolatedStorageSettings.ApplicationSettings.Any(s => string.Equals(s.Key, Constants.LBNLocations, StringComparison.CurrentCultureIgnoreCase)))
        //    {
        //        IsolatedStorageSettings.ApplicationSettings.Remove(Constants.LBNLocations);
        //    }
        //    IsolatedStorageSettings.ApplicationSettings[Constants.LBNLocations] = list;
        //}
        public Location GetLocationById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion IIsolatedStorageRepository





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
    }
}
