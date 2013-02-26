using LocationBasedNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Contracts
{
    public interface IRepository<T>
        where T : BaseModel
    {
        T GetItemById(int id);
        IEnumerable<T> GetInMemoryItems();
        bool AddItem(T itemToAdd);
        bool RemoveItem(T itemToRemove);
        void Save(IEnumerable<T> list);
    }
}
