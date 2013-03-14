using LocationBasedNotifications.Contracts;
using LocationBasedNotifications.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private IRepository _repository;

        #region Public Properties
        protected IRepository Repository
        {
            get
            {
                return _repository;
            }
            set
            {
                _repository = value;
            }
        }
        #endregion Public Properties

        #region Constructors
        public ViewModelBase() : this(new LocalStorageRepository())
        {
        }
        public ViewModelBase(IRepository repository)
        {
            _repository = repository;
        }
        #endregion Constructors

        #region INotifyProperty
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
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
        #endregion INotifyProperty

    }
}
