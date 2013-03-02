using LocationBasedNotifications.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace LocationBasedNotifications
{
    public class ReminderViewModel : ViewModelBase
    {
        #region Private Members
        private ObservableCollection<Reminder> _activeReminders;
        private ObservableCollection<Reminder>  _inactiveReminders;
        private ObservableCollection<ReminderStatus> _statuses;

        private Reminder _selectedReminder;
        #endregion Private Members

        #region Public Properties
        public ObservableCollection<Reminder> ActiveReminders
        {
            get { return _activeReminders; }
            set { _activeReminders = value; }
        }
        public ObservableCollection<Reminder> InctiveReminders
        {
            get { return _inactiveReminders; }
            set { _inactiveReminders = value; }
        }
        public ObservableCollection<ReminderStatus> Statuses
        {
            get { return _statuses; }
            set { _statuses = value; }
        }
        public Reminder SelectedReminder
        {
            get
            {
                return _selectedReminder;
            }
            set
            {
                if (_selectedReminder == value)
                {
                    return;
                }
                NotifyPropertyChanging("SelectedReminder");
                _selectedReminder = value;
                NotifyPropertyChanged("SelectedReminder");
            }
        }
        #endregion Public Properties

        #region Constructors
        public ReminderViewModel() 
            : base()
        {
            ActiveReminders = new ObservableCollection<Reminder>();
            InctiveReminders = new ObservableCollection<Reminder>();
            Statuses = new ObservableCollection<ReminderStatus>();
            SelectedReminder = null;

            InsertDummyData();
            LoadDataAsyncFromRepository();
        }
        #endregion Constructors

        #region Public Methods
        public void UpdateRemindersDistance(Geocoordinate currentGeocoordinate)
        {
            if (ActiveReminders != null)
            {
                Location currentLocation = new Location();
                currentLocation.Longitude = currentGeocoordinate.Longitude;
                currentLocation.Latitude = currentGeocoordinate.Latitude;

                foreach (var reminder in ActiveReminders)
                {
                    DistanceHelper distanceHelper = new DistanceHelper(currentLocation, reminder.Location);
                    reminder.DistanceToLocation = distanceHelper.Distance;
                }
            }
        }
        public void DeactivateSelectedItem()
        {            
            if (SelectedReminder != null)
            {
                ActiveReminders.Remove(SelectedReminder);

                Reminder copyOfSelectedReminder = SelectedReminder.DeepCopy();
                copyOfSelectedReminder.Status = Statuses.FirstOrDefault(s => string.Equals(s.Value, "Inactive", StringComparison.CurrentCultureIgnoreCase));
                InctiveReminders.Add(copyOfSelectedReminder);
               
                SelectedReminder = null;
            }
        }
        public void ActivateSelectedItem()
        {
            if (SelectedReminder != null)
            {
                InctiveReminders.Remove(SelectedReminder);

                Reminder copyOfSelectedReminder = SelectedReminder.DeepCopy();
                copyOfSelectedReminder.Status = Statuses.FirstOrDefault(s => string.Equals(s.Value, "Active", StringComparison.CurrentCultureIgnoreCase));
                ActiveReminders.Add(copyOfSelectedReminder);

                SelectedReminder = null;
            }
        }

        #endregion Public Methods

        #region Private Methods
        private void InsertDummyData()
        {
            Location location = new Location();
            location.Name = "Home";
            base.Repository.AddLocation(location);
            base.Repository.Save();



            ReminderStatus active = base.Repository.GetStatusById(1);
            ReminderStatus inactive = base.Repository.GetStatusById(2);

            Reminder reminder = new Reminder();
            reminder.Status = active;
            reminder.ReminderStatusId = active.ReminderStatusId;
            reminder.Location = location;
            reminder.Name = "My first Reminder";

            Reminder second = new Reminder();
            second.Status = inactive;
            second.ReminderStatusId = inactive.ReminderStatusId;
            second.Location = location;
            second.Name = "Second inactive reminder";


            base.Repository.AddReminder(reminder);
            base.Repository.AddReminder(second);

            base.Repository.Save();
        }

        private async void LoadDataAsyncFromRepository()
        {
            if (base.Repository != null)
            {
                Statuses = new ObservableCollection<ReminderStatus>(base.Repository.GetReminderStatusesList());

                //load visible active sync
                IEnumerable<Reminder> activeList = Repository.GetRemindersByStatusId(1);
                if (activeList != null)
                {
                    foreach (var reminder in activeList)
                    {
                        ActiveReminders.Add(reminder);
                    }
                }

                //load inactive async
                IEnumerable<Reminder> temporaryInactiveList = null;

                Action getInactiveRemindersAction = () =>
                    {
                        temporaryInactiveList = Repository.GetRemindersByStatusId(2);
                    };

                await Task.Factory.StartNew(getInactiveRemindersAction);

                Action refreshUI = () =>
                    {
                        if (temporaryInactiveList != null)
                        {
                            foreach (var reminder in temporaryInactiveList)
                            {
                                InctiveReminders.Add(reminder);
                            }
                        }
                    };

                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(refreshUI);
            }

        }
        #endregion Private Methods


    }
}
