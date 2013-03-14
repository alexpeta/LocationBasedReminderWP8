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
            this.DataContext = _viewModel;
        }
        #endregion Constructors

        #region Private Methods
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveReminder();
            NavigationService.Navigate(new Uri("/ReminderPivot.xaml", UriKind.Relative));
        }
        #endregion Private Methods
    }
}