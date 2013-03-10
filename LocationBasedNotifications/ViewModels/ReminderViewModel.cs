using LocationBasedNotifications.Logic;
using LocationBasedNotifications.ViewModels;
using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace LocationBasedNotifications
{
    public class ReminderViewModel : ViewModelBase
    {
        #region Private Members
        private ObservableCollection<Reminder> _activeReminders;
        private ObservableCollection<Reminder>  _inactiveReminders;
        private ObservableCollection<ReminderStatus> _statuses;
        private ICommand _activationCommand;
        private ICommand _deleteCommand;
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
        public ICommand ActivationCommand
        {
            get
            {
                return _activationCommand;
            }
            set
            {
                _activationCommand = value;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand;
            }
            set
            {
                _deleteCommand = value;
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

            ActivationCommand = new DelegateCommand(this.OnActivationCommand);
            DeleteCommand = new DelegateCommand(this.OnDeleteCommand);

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
                    reminder.DistanceToLocation = DistanceHelper.CalculateDistanceInKilometers(currentLocation, reminder.Location);
                }
            }
        }
        public void OnActivationCommand(object reminderId)
        {
            int castedReminderId = 0;
            try
            {
                castedReminderId = Convert.ToInt32(reminderId);
            }
            catch (Exception)
            {
                castedReminderId = -1;
            }

            if (castedReminderId != 0 && castedReminderId != -1)
            {
                if (ActiveReminders.Any(r => r.ReminderId == castedReminderId))
                {
                    Reminder reminder = ActiveReminders.FirstOrDefault(r=>r.ReminderId == castedReminderId);
                    this.DeactivateSelectedItem(reminder);
                }
                else if (InctiveReminders.Any(r => r.ReminderId == castedReminderId))
                {
                    Reminder reminder = InctiveReminders.FirstOrDefault(r => r.ReminderId == castedReminderId);
                    this.ActivateSelectedItem(reminder);
                }                     
            }
        }
        public void OnDeleteCommand(object reminderId)
        {
            MessageBoxResult deleteAnswer = MessageBox.Show("Are you sure you want to delete this reminder?", "Delete Reminder", MessageBoxButton.OKCancel);
            if (deleteAnswer == MessageBoxResult.OK)
            {
                int castedReminderId = 0;
                try
                {
                    castedReminderId = Convert.ToInt32(reminderId);
                }
                catch (Exception)
                {
                    castedReminderId = -1;
                }

                if (castedReminderId != 0 && castedReminderId != -1)
                {
                    Reminder reminder = InctiveReminders.FirstOrDefault(r => r.ReminderId == castedReminderId);
                    if (reminder != null)
                    {
                        if(base.Repository.DeleteReminder(reminder))
                        {
                            InctiveReminders.Remove(reminder);
                        }
                    }
                }
            }
        }
        #endregion Public Methods

        #region Private Methods
        private async void LoadDataAsyncFromRepository()
        {
            if (base.Repository != null)
            {
                IEnumerable<ReminderStatus> localStatusList = base.Repository.GetReminderStatusesList();

                if (localStatusList != null)
                {
                    foreach (ReminderStatus status in localStatusList)
                    {
                        Statuses.Add(status);
                    }
                }

                //load visible active sync
                IEnumerable<Reminder> activeList = Repository.GetRemindersByStatusId((int)ReminderStatusTypes.Active);
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
                        temporaryInactiveList = Repository.GetRemindersByStatusId((int)ReminderStatusTypes.Inactive);
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
        private void DeactivateSelectedItem(Reminder reminder)
        {
            if (reminder != null)
            {
                ActiveReminders.Remove(reminder);

                Reminder copyOfSelectedReminder = reminder.DeepCopy();
                copyOfSelectedReminder.Status = Statuses.FirstOrDefault(s => string.Equals(s.Value, "Inactive", StringComparison.CurrentCultureIgnoreCase));
                InctiveReminders.Add(copyOfSelectedReminder);

                if (!ActiveReminders.Any())
                {
                    RemoveBackgroundAgent();
                }
            }
        }
        private void ActivateSelectedItem(Reminder reminder)
        {
            if (reminder != null)
            {
                InctiveReminders.Remove(reminder);

                Reminder copyOfSelectedReminder = reminder.DeepCopy();
                copyOfSelectedReminder.Status = Statuses.FirstOrDefault(s => string.Equals(s.Value, "Active", StringComparison.CurrentCultureIgnoreCase));

                if (!ActiveReminders.Any())
                {
                    StartBackgroundAgent();
                }
                
                ActiveReminders.Add(copyOfSelectedReminder);
            }
        }        
        private void StartBackgroundAgent()
        {
            PeriodicTask periodicTask = null;

            periodicTask = ScheduledActionService.Find(Constants.BackgroundAgentName) as PeriodicTask;
            if (periodicTask != null)
            {
                ScheduledActionService.Remove(Constants.BackgroundAgentName);
            }

            periodicTask = new PeriodicTask(Constants.BackgroundAgentName);
            periodicTask.Description = "This is Lockscreen image provider app.";
            periodicTask.ExpirationTime = DateTime.Now.AddDays(14);

            ScheduledActionService.Add(periodicTask);

            ScheduledActionService.LaunchForTest(Constants.BackgroundAgentName, TimeSpan.FromSeconds(10));
        }
        private void RemoveBackgroundAgent()
        {
            PeriodicTask periodicTask = null;

            periodicTask = ScheduledActionService.Find(Constants.BackgroundAgentName) as PeriodicTask;
            if (periodicTask != null)
            {
                ScheduledActionService.Remove(Constants.BackgroundAgentName);
            }
        }
        #endregion Private Methods
    }
}
