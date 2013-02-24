using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    public abstract class BaseModel : INotifyPropertyChanged,INotifyPropertyChanging
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this,new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void NotifyPropertyChanging(string propertyName)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged

        protected virtual bool IsValid()
        {
            throw new NotImplementedException();
        }


    }
}
