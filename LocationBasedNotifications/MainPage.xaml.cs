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
using ScheduledTaskAgent;

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
                gym.Description = string.Empty;
                _repository.AddLocation(gym);

                Location home = new Location();
                home.Name = "MyHome";
                home.Longitude = 024.8690D;
                home.Latitude = 43.7461D;
                home.Description = string.Empty;
                _repository.AddLocation(home);


                Location work = new Location();
                work.Name = "Work";
                work.Longitude = 026.0808D;
                work.Latitude = 44.4294D;
                work.Description = string.Empty;
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


                loaded = true;
            }
        }

        #endregion Button Handlers

        private void LiveTileButton_Click_1(object sender, RoutedEventArgs e)
        {
            IconicTileData oIcontile = new IconicTileData();
            oIcontile.Title = "Hello Iconic Tile!!";
            oIcontile.Count = 7;

            oIcontile.IconImage = new Uri("Assets/Tiles/202x202.png", UriKind.Relative);
            oIcontile.SmallIconImage = new Uri("Assets/Tiles/110x110.png", UriKind.Relative);

            oIcontile.WideContent1 = "windows phone 8 Live tile";
            oIcontile.WideContent2 = "Icon tile";
            oIcontile.WideContent3 = "All about Live tiles By WmDev";

            oIcontile.BackgroundColor = System.Windows.Media.Colors.Orange;

            // find the tile object for the application tile that using "Iconic" contains string in it.
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Location".ToString()));

            if (TileToFind != null && TileToFind.NavigationUri.ToString().Contains("Location"))
            {
                TileToFind.Delete();
                ShellTile.Create(new Uri("/MainPage.xaml?id=Iconic", UriKind.Relative), oIcontile, true);
            }
            else
            {
                ShellTile.Create(new Uri("/MainPage.xaml?id=Iconic", UriKind.Relative), oIcontile, true);
            }
        }

        private void FlipTileButton_Click_1(object sender, RoutedEventArgs e)
        {

            // find the tile object for the application tile that using "flip" contains string in it.
            ShellTile oTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Location".ToString()));


            if (oTile != null && oTile.NavigationUri.ToString().Contains("Location"))
            {
                FlipTileData oFliptile = new FlipTileData();
                oFliptile.Title = "Hello WP8!!";
                oFliptile.Count = 11;
                oFliptile.BackTitle = "Updated Flip Tile";

                oFliptile.BackContent = "back of tile";
                oFliptile.WideBackContent = "back of the wide tile";

                oFliptile.BackBackgroundImage = new Uri("/Assets/Tiles/A336.png", UriKind.Relative);
                oFliptile.WideBackBackgroundImage = new Uri("/Assets/Tiles/A691.png", UriKind.Relative);
                oTile.Update(oFliptile);
                MessageBox.Show("Flip Tile Data successfully update.");
            }
            else
            {
                // once it is created flip tile
                Uri tileUri = new Uri("/MainPage.xaml?tile=flip", UriKind.Relative);
                ShellTileData tileData = this.CreateFlipTileData();
                ShellTile.Create(tileUri, tileData, true);
            }
        }

        private ShellTileData CreateFlipTileData()
        {
            return new FlipTileData()
            {
                Title = "Hi Flip Tile",
                BackTitle = "This is WP8 flip tile",
                BackContent = "Live Tile Demo",
                WideBackContent = "Hello Nokia Lumia 920",
                Count = 99,
                SmallBackgroundImage = new Uri("/Assets/Tiles/A159.png", UriKind.Relative),
                BackgroundImage = new Uri("/Assets/Tiles/A336.png", UriKind.Relative),
                WideBackgroundImage = new Uri("/Assets/Tiles/A691.png", UriKind.Relative),
            };
        }


        PeriodicTask periodicTask;
        string periodicTaskName = "PeriodicAgent";
        public bool agentsAreEnabled = true;

        private void UpdateTileViaAgent_Click_1(object sender, RoutedEventArgs e)
        {
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;
            if (periodicTask != null)
            {
                ScheduledActionService.Remove(periodicTaskName);
            }


            periodicTask = new PeriodicTask(periodicTaskName);
            periodicTask.Description = "This is Lockscreen image provider app.";
            periodicTask.ExpirationTime = DateTime.Now.AddDays(14);

            ScheduledActionService.Add(periodicTask);

            // If debugging is enabled, use LaunchForTest to launch the agent in one minute.

            ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(10));

        }


    }
}