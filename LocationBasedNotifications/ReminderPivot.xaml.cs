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
        public ReminderPivot()
        {
            InitializeComponent();
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