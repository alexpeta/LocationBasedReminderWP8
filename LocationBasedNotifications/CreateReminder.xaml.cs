using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LocationBasedNotifications
{
    public partial class CreateReminder : PhoneApplicationPage
    {
        #region Private Members
        private CreateReminderViewModel _viewModel = null;
        #endregion Private Members

        #region Constructors
        public CreateReminder()
        {
            InitializeComponent();

            _viewModel = new CreateReminderViewModel();
        }
        #endregion Constructors
    }
}