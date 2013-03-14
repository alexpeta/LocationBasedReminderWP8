using LocationBasedNotifications.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LocationBasedNotifications
{
    public class CreateReminderViewModel : ViewModelBase
    {
        #region Constructors
        private ObservableCollection<Location> _locations;
        private Location _selectedLocation;
        private string _name;
        private int _accurecy;
        private bool _isBusy;
        #endregion Constructors

        #region Public Properties
        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }
        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                if (_selectedLocation == value)
                {
                    return;
                }

                NotifyPropertyChanging("SelectedLocation");
                _selectedLocation = value;
                NotifyPropertyChanged("SelectedLocation");
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public int Accurecy
        {
            get { return _accurecy; }
            set
            {
                if (_accurecy == value)
                {
                    return;
                }

                NotifyPropertyChanging("Accurecy");
                _accurecy = value;
                NotifyPropertyChanged("Accurecy");
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
        public CreateReminderViewModel()
        {
            Locations = new ObservableCollection<Location>();
            IsBusy = false;
            LoadLocationsAsync();
        }
        #endregion Constructors

        #region Private Methods
        public void SaveReminder()
        {
            ReminderStatus status = base.Repository.GetStatusById((int)ReminderStatusTypes.Active);

            Reminder reminderToSave = new Reminder();
            reminderToSave.Accurecy = this.Accurecy;
            reminderToSave.Location = this.SelectedLocation;
            reminderToSave.Name = this.Name;
            reminderToSave.DistanceToLocation = 0D;
            if (this.SelectedLocation != null)
            {
                reminderToSave.LocationId = this.SelectedLocation.LocationId;
                reminderToSave.Location = this.SelectedLocation;
            }
            if (status != null)
            {
                reminderToSave.Status = status;
                reminderToSave.ReminderStatusId = status.ReminderStatusId;
            }

            base.Repository.AddReminder(reminderToSave);
            
    
        }
        private async void LoadLocationsAsync()
        {
            IsBusy = true;  

            IEnumerable<Location> locations = null;
            await Task.Factory.StartNew(() =>
            {
                locations = base.Repository.GetLocationsList();
            });

            Action refreshUIAction = () =>
                {
                    if (locations != null)
                    {
                        foreach (Location loc in locations)
                        {
                            this.Locations.Add(loc);
                        }
                    }
                    IsBusy = false;
                };

            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(refreshUIAction);

        }
        #endregion Private Methods



    }
}
