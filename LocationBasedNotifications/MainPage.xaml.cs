using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LocationBasedNotifications.Resources;
using Windows.Storage;
using Microsoft.Phone.Scheduler;
using LocationBasedNotifications.Repository;
using LocationBasedNotifications.Contracts;

namespace LocationBasedNotifications
{
    public partial class MainPage : PhoneApplicationPage
    {

        private bool loaded = false;
        private IRepository _repository;


        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _repository = new LocalStorageRepository();
        }   

        #region Button Handlers
        private void ViewLocationsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MyLocations.xaml", UriKind.Relative));
        }
        private void ManageReminderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ReminderPivot.xaml", UriKind.Relative));
        }
        private void ViewMapButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Map.xaml", UriKind.Relative));
        }


        private void LoadTestDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                Location gym = new Location();
                gym.Name = "Gym";
                gym.Longitude = 026.0823D;
                gym.Latitude = 44.4298D;
                _repository.AddLocation(gym);

                Location home = new Location();
                home.Name = "Home";
                home.Longitude = 024.8690D;
                home.Latitude = 43.7461D;
                _repository.AddLocation(home);


                Location work = new Location();
                work.Name = "Work";
                work.Longitude = 026.0808D;
                work.Latitude = 44.4294D;
                _repository.AddLocation(work);


                ReminderStatus active = _repository.GetStatusById(1);
                ReminderStatus inactive = _repository.GetStatusById(2);

                Reminder reminder = new Reminder();
                reminder.Status = inactive;
                reminder.ReminderStatusId = active.ReminderStatusId;
                reminder.Location = gym;
                reminder.Name = "Drink protein";

                Reminder second = new Reminder();
                second.Status = inactive;
                second.ReminderStatusId = inactive.ReminderStatusId;
                second.Location = home;
                second.Name = "Call Raluca";


                _repository.AddReminder(reminder);
                _repository.AddReminder(second);

                _repository.Save();
                loaded = true;
            }
        }

        #endregion Button Handlers


    }
}