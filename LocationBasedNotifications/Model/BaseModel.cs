using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Model
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseNotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged

        protected virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
