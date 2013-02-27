using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LocationBasedNotifications.Repository;

namespace LocationBasedNotifications
{
    public partial class ReminderPivot : PhoneApplicationPage
    {


        #region Constructors
        public ReminderPivot()
        {
            InitializeComponent();
        }
        #endregion Constructors


        private void LoadTestReminder()
        {
            //ReminderStatus active = new ReminderStatus();
            //active.ReminderStatusId = 1;
            //active.Value = "Active";
            //_localDB.ReminderStatuses.InsertOnSubmit(active);

            //Location location = new Location();
            //location.Name = "Home";
            //location.LocationId = 1;


            //Reminder reminder = new Reminder();
            //reminder.ReminderId = 1;
            //reminder.Status = active;
            //reminder.Location = location;


            //_localDB.Reminders.InsertOnSubmit(reminder);
        }



        #region Overrides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            List<ReminderStatus> statuses = null;

            ReminderDataContext db = App.LocalDB;
            if (db != null)
            {
                if(db.ReminderStatuses != null)
                {
                    statuses = db.ReminderStatuses.ToList();
                }                  
            }
            
            if(statuses != null)
            {
                List<PivotItem> pivotItems = new List<PivotItem>();
                foreach (var status in statuses)
                {
                    PivotItem item = new PivotItem();
                    item.Header = status.Value;
                    RemindersPivotHolder.Items.Add(item);
                }
            }


            
            base.OnNavigatedTo(e);
        }
        #endregion Overrides
    }
}