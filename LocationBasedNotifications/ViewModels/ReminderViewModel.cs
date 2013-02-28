using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    public class ReminderViewModel : ViewModelBase
    {
        #region Private Members
        private ObservableCollection<Reminder> _activeReminders;
        private ObservableCollection<Reminder>  _inactiveReminders;
        private ObservableCollection<Reminder> _allReminders;

        private ObservableCollection<ReminderStatus> _statuses;
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
        public ObservableCollection<Reminder> AllReminders
        {
            get { return _allReminders; }
            set { _allReminders = value; }
        }

        public ObservableCollection<ReminderStatus> Statuses
        {
            get { return _statuses; }
            set { _statuses = value; }
        }

        #endregion Public Properties

        #region Constructors
        public ReminderViewModel() 
            : base()
        {
            ActiveReminders = new ObservableCollection<Reminder>();
            InctiveReminders = new ObservableCollection<Reminder>();
            AllReminders = new ObservableCollection<Reminder>();
            Statuses = new ObservableCollection<ReminderStatus>();

            InsertDummyData();
            LoadDataFromRepository();
        }
        #endregion Constructors

        #region Private Methods
        private void InsertDummyData()
        {
            Location location = new Location();
            location.Name = "Home";
            base.Repository.AddLocation(location);
            base.Repository.Save();



            ReminderStatus active = base.Repository.GetStatusById(2);
            ReminderStatus inactive = base.Repository.GetStatusById(3);

            Reminder reminder = new Reminder();
            reminder.Status = active;
            reminder.Location = location;
            reminder.Name = "My first Reminder";

            Reminder second = new Reminder();
            second.Status = inactive;
            second.Location = location;
            second.Name = "Second inactive reminder";


            base.Repository.AddReminder(reminder);
            base.Repository.AddReminder(second);

            base.Repository.Save();
        }

        private void LoadDataFromRepository()
        {
            if (base.Repository != null)
            {
                Statuses = new ObservableCollection<ReminderStatus>(base.Repository.GetReminderStatusesList());

                AllReminders = new ObservableCollection<Reminder>(base.Repository.GetRemindersList());

                IEnumerable<Reminder> activeList = AllReminders.Where(r => r.Status != null && r.Status.ReminderStatusId == 2);
                if (activeList != null)
                {
                    foreach (var reminder in activeList)
                    {
                        ActiveReminders.Add(reminder);
                    }
                }

                IEnumerable<Reminder> inactiveList = AllReminders.Where(r => r.Status != null && r.Status.ReminderStatusId == 3);
                if (inactiveList != null)
                {
                    foreach (var reminder in inactiveList)
                    {
                        InctiveReminders.Add(reminder);
                    }
                }

                //throwing ex
                /*
                Action action = () =>
                    {
                        Thread.Sleep(2000);
                        
                        IEnumerable<Reminder> activeList = AllReminders.Where(r => r.Status != null && r.Status.ReminderStatusId == 2);
                        if (activeList != null)
                        {
                            foreach (var reminder in activeList)
                            {
                                ActiveReminders.Add(reminder);
                            }
                        }
 
                        IEnumerable<Reminder> inactiveList = AllReminders.Where(r => r.Status != null && r.Status.ReminderStatusId == 3);
                        if (inactiveList != null)
                        {
                            foreach (var reminder in inactiveList)
                            {
                                InctiveReminders.Add(reminder);
                            }
                        }

                    };

                Task t = Task.Factory.StartNew(action);
                 */

            }

        }
        #endregion Private Methods
    }
}
