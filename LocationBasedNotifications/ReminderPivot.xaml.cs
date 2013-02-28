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
using LocationBasedNotifications.Contracts;

namespace LocationBasedNotifications
{
    public partial class ReminderPivot : PhoneApplicationPage
    {
        #region Reminder Members
        private ReminderViewModel _model;
        #endregion Reminder Members

        #region Constructors
        public ReminderPivot()
        {
            InitializeComponent();
            _model = new ReminderViewModel();
            this.DataContext = _model;
        }
        #endregion Constructors
    }
}