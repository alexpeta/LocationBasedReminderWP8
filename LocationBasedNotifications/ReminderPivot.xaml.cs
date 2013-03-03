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
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using Windows.Devices.Geolocation;
using LocationBasedNotifications.Helpers;

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
            CreateAppBar();
            _model = new ReminderViewModel();
            this.DataContext = _model;
        }
        #endregion Constructors


        #region Private Methods
        private void CreateAppBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.IsMenuEnabled = true;

            ApplicationBarIconButton addBarButton = new ApplicationBarIconButton();
            addBarButton.IconUri = new Uri("/Assets/new.png", UriKind.Relative);
            addBarButton.Text = "New Location";
            addBarButton.IsEnabled = true;
            addBarButton.Click += CreateReminderButton_Click;
            ApplicationBar.Buttons.Add(addBarButton);

            ApplicationBarIconButton deleteLocationBarButton = new ApplicationBarIconButton();
            deleteLocationBarButton.IconUri = new Uri("/Assets/delete.png", UriKind.Relative);
            deleteLocationBarButton.Text = "Delete";
            deleteLocationBarButton.IsEnabled = false;
            deleteLocationBarButton.Click += DeleteReminderButton_Click;
            ApplicationBar.Buttons.Add(deleteLocationBarButton);
        }

        #region Click Event Handlers
        private void OnSelectedReminderChanged(object sender, RoutedEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            if (listbox != null)
            {
                Reminder selectedReminder = listbox.SelectedItem as Reminder;
                if (selectedReminder != null)
                {
                    _model.SelectedReminder = selectedReminder;

                    foreach (var button in ApplicationBar.Buttons)
                    {
                        ApplicationBarIconButton castedButton = button as ApplicationBarIconButton;
                        if (castedButton != null)
                        {
                            if (!string.Equals(castedButton.Text, "New Location", StringComparison.CurrentCultureIgnoreCase))
                            {
                                castedButton.IsEnabled = _model.SelectedReminder != null;
                            }
                        }
                    }
                }
            }
        }
        private void OnSelectedPivotItemChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;
            if (pivot != null)
            {
                PivotItem selectedItem = pivot.SelectedItem as PivotItem;
                if (selectedItem != null)
                {
                    if (string.Equals(selectedItem.Header.ToString(), "Active", StringComparison.CurrentCultureIgnoreCase))
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
        private void DeleteReminderButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void CreateReminderButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateReminder.xaml", UriKind.Relative));
        }
        #endregion Click Event Handlers

        #endregion  Private Methods
    }
}