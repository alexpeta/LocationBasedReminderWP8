using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LocationBasedNotifications.Repository;
using LocationBasedNotifications.Contracts;

namespace LocationBasedNotifications
{
    public partial class MyLocations : PhoneApplicationPage
    {
        #region Private Members
        private IRepository<Location> _repository;
        private LocationsViewModel _model;
        #endregion Private Members

        #region Constructors
        public MyLocations() : this(new LocalStorageRepository())
        {
        }
        public MyLocations(IRepository<Location> repository)
        {
            InitializeComponent();
            _repository = repository;
            _model = new LocationsViewModel();
            _model.PropertyChanged += OnEnableAppBarButtons;

            PopulateModelWithStorageData();

            CreateAppBar();
            this.DataContext = _model;
        }
        #endregion Constructors

        #region Private Methods
        private void CreateAppBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Mode = ApplicationBarMode.Minimized;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.IsMenuEnabled = true;
            
            ApplicationBarIconButton addBarButton = new ApplicationBarIconButton();
            addBarButton.IconUri = new Uri("/Images/new.png", UriKind.Relative);
            addBarButton.Text = "New Location";
            addBarButton.IsEnabled = true;
            addBarButton.Click += CreateLocationButton_Click;
            ApplicationBar.Buttons.Add(addBarButton);

            ApplicationBarIconButton deleteLocationBarButton = new ApplicationBarIconButton();
            deleteLocationBarButton.IconUri = new Uri("/Images/delete.png", UriKind.Relative);
            deleteLocationBarButton.Text = "Delete";
            deleteLocationBarButton.IsEnabled = false;
            deleteLocationBarButton.Click += DeleteLocationBarButton_Click;
            ApplicationBar.Buttons.Add(deleteLocationBarButton);

            ApplicationBarIconButton cancelSelectionBarButton = new ApplicationBarIconButton();
            cancelSelectionBarButton.IconUri = new Uri("/Images/cancel.png", UriKind.Relative);
            cancelSelectionBarButton.Text = "Cancel Selection";
            cancelSelectionBarButton.IsEnabled = false;
            cancelSelectionBarButton.Click += CancelSelectionBarButton_Click;
            ApplicationBar.Buttons.Add(cancelSelectionBarButton);

        }

        #region Private Event Handlers
        private void OnSelectedLocationChanged(object sender, RoutedEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            if (listbox != null)
            {
                Location selectedLocation = listbox.SelectedItem as Location;
                if (selectedLocation != null)
                {
                    _model.SelectedItem = selectedLocation;
                }
            }
        }
        private void OnEnableAppBarButtons(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                foreach (var button in ApplicationBar.Buttons)
                {
                    ApplicationBarIconButton castedButton = button as ApplicationBarIconButton;
                    if (castedButton != null)
                    {
                        if (castedButton.Text != "New Location")
                        {
                            castedButton.IsEnabled = _model.SelectedItem != null;
                        }
                    }
                }
            }
        }
        private void DeleteLocationBarButton_Click(object sender, EventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Delete selected location?", "Delete location", MessageBoxButton.OKCancel);
            if (answer == MessageBoxResult.OK)
            {
                if (_model.SelectedItem != null)
                {
                    _repository.RemoveItem(_model.SelectedItem);
                    _model.MyLocations.Remove(_model.SelectedItem);
                    _model.SelectedItem = null;
                }
            }
        }
        private void CancelSelectionBarButton_Click(object sender, EventArgs e)
        {
            _model.SelectedItem = null;
            //MyLocationsListBox.SelectedItem = null;
        }
        private void CreateLocationButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateLocation.xaml", UriKind.Relative));
        }
        #endregion Public Event Handlers

        private void PopulateModelWithStorageData()
        {
            IEnumerable<Location> locations = _repository.GetInMemoryLocations();
            if (locations != null)
            {
                foreach (var location in locations)
                {
                    _model.MyLocations.Add(location);
                }
            }
        }
        #endregion Private Methods

    }
}