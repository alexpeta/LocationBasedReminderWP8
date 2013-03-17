using LocationBasedNotifications.Logic;
using LocationBasedNotifications.ViewModels;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool _hasData;
        private bool _isBusy;
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
        public bool HasData
        {
            get { return _hasData; }
            set 
            {
                NotifyPropertyChanging("HasData");
                _hasData = value;
                NotifyPropertyChanged("HasData");
            }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                {
                    return;
                }

                NotifyPropertyChanging("IsBusy");
                _isBusy = value;
                NotifyPropertyChanged("IsBusy");
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
            HasData = false;

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
                Location currentLocation = new Location
                {
                    Longitude = currentGeocoordinate.Longitude,
                    Latitude = currentGeocoordinate.Latitude
                };

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
                Reminder reminder = base.Repository.GetReminderById(castedReminderId);
                if (reminder != null)
                {
                    if (reminder.Status != null)
                    {
                        ReminderStatus status = null;
                        if (string.Equals(reminder.Status.Value, "Active", StringComparison.CurrentCultureIgnoreCase))
                        {
                            status = base.Repository.GetStatusById((int)ReminderStatusTypes.Inactive);
                        }
                        else
                        {
                            status = base.Repository.GetStatusById((int)ReminderStatusTypes.Active);
                        }

                        reminder.Status = status;
                        reminder.ReminderStatusId = status.ReminderStatusId;
                        base.Repository.UpdateReminder(reminder);
                    }
                    LoadDataAsyncFromRepository();
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
        private void ClearReminersLists()
        {
            ActiveReminders.Clear();
            InctiveReminders.Clear();
        }
        private async void LoadDataAsyncFromRepository()
        {
            IsBusy = true;
            ClearReminersLists();
            if (base.Repository != null)
            {
                //load visible active sync
                IEnumerable<ReminderStatus> localStatusList = null;
                IEnumerable<Reminder> activeList = null;
                IEnumerable<Reminder> temporaryInactiveList = null;

                await Task.Factory.StartNew(() =>
                    {
                        localStatusList = base.Repository.GetReminderStatusesList();
                    });

                if (localStatusList != null)
                {
                    foreach (ReminderStatus status in localStatusList)
                    {
                        Statuses.Add(status);
                    }
                }

                await Task.Factory.StartNew(() =>
                    {
                        activeList = Repository.GetRemindersByStatusId((int)ReminderStatusTypes.Active);
                    });

                foreach (var item in activeList)
                {
                    ActiveReminders.Add(item);
                }

                if (ActiveReminders.Any())
                {
                    HasData = true;
                }

                await Task.Factory.StartNew(() =>
                    {
                        temporaryInactiveList = Repository.GetRemindersByStatusId((int)ReminderStatusTypes.Inactive);
                    });

                Action refreshUIWithInactiveReminders = () =>
                    {
                        if (temporaryInactiveList != null)
                        {
                            foreach (var reminder in temporaryInactiveList)
                            {
                                InctiveReminders.Add(reminder);
                            }
                        }
                        IsBusy = false;
                        
                        RefreshLiveTile();

                        if (ActiveReminders.Any())
                        {
                            //StartBackgroundAgentAsync();
                        }
                        else
                        {
                            //RemoveBackgroundAgentAsync();
                        }
                    };

                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(refreshUIWithInactiveReminders);
            }

        }    
        private void StartBackgroundAgentAsync(bool forceRestart = false)
        {
            PeriodicTask periodicTask = null;
            periodicTask = ScheduledActionService.Find(Constants.BackgroundAgentName) as PeriodicTask;

            if (periodicTask == null || forceRestart)
            {
                //ScheduledActionService.Remove(Constants.BackgroundAgentName);

                periodicTask = new PeriodicTask(Constants.BackgroundAgentName);
                periodicTask.Description = "Location Based Reminders Agent";
                periodicTask.ExpirationTime = DateTime.Now.AddDays(14);

                ScheduledActionService.Add(periodicTask);

                //TODO : this is for dev only
                ScheduledActionService.LaunchForTest(Constants.BackgroundAgentName, TimeSpan.FromSeconds(Constants.Timeout));
            }
        }
        private  void RemoveBackgroundAgentAsync()
        {
            PeriodicTask periodicTask = null;

            periodicTask = ScheduledActionService.Find(Constants.BackgroundAgentName) as PeriodicTask;
            if (periodicTask != null)
            {
                ScheduledActionService.Remove(Constants.BackgroundAgentName);
            }
        }
        private void RefreshLiveTile()
        {
            FlipTileData tileData = new FlipTileData()
            {
                Title = "LocationBasedReminder",
                SmallBackgroundImage = new Uri("/Assets/Tiles/A159.png", UriKind.Relative),
                BackgroundImage = new Uri("/Assets/Tiles/A336.png", UriKind.Relative),
                WideBackgroundImage = new Uri("/Assets/Tiles/A691.png", UriKind.Relative),
            };
            
            tileData.BackTitle = "Back Tile";
            tileData.BackContent = string.Empty;
            tileData.WideBackContent = string.Empty;
            tileData.Count = ActiveReminders.Count;
            
            ShellTile liveTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("MainPage"));
            if (liveTile == null)
            {
                Uri tileUri = new Uri("/MainPage.xaml", UriKind.Relative);               
                ShellTile.Create(tileUri, tileData, true);
            }
            else
            {
                liveTile.Update(tileData);
            }
        }
        #endregion Private Methods
    }
}
