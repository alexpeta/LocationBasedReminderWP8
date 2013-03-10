using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    public class CreateReminderViewModel : ViewModelBase
    {

        #region Constructors
        private Reminder _reminder;
        private ObservableCollection<Location> _locations;
        #endregion Constructors

        #region Public Properties
        public Reminder Reminder
        {
            get { return _reminder; }
            set { _reminder = value; }
        }
        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }
        #endregion Public Properties

        #region Constructors
        public CreateReminderViewModel()
        {

        }
        #endregion Constructors

    }
}
