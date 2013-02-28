﻿using System;
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
        private LocationViewModel _model;
        #endregion Private Members

        #region Constructors
        public MyLocations()
        {
            InitializeComponent();
            _model = new LocationViewModel();
            CreateAppBar();
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
            addBarButton.Click += CreateLocationButton_Click;
            ApplicationBar.Buttons.Add(addBarButton);

            ApplicationBarIconButton deleteLocationBarButton = new ApplicationBarIconButton();
            deleteLocationBarButton.IconUri = new Uri("/Assets/delete.png", UriKind.Relative);
            deleteLocationBarButton.Text = "Delete";
            deleteLocationBarButton.IsEnabled = false;
            deleteLocationBarButton.Click += DeleteLocationBarButton_Click;
            ApplicationBar.Buttons.Add(deleteLocationBarButton);

            ApplicationBarIconButton cancelSelectionBarButton = new ApplicationBarIconButton();
            cancelSelectionBarButton.IconUri = new Uri("/Assets/cancel.png", UriKind.Relative);
            cancelSelectionBarButton.Text = "Cancel Selection";
            cancelSelectionBarButton.IsEnabled = false;
            cancelSelectionBarButton.Click += CancelSelectionBarButton_Click;
            ApplicationBar.Buttons.Add(cancelSelectionBarButton);

            ApplicationBarIconButton backBarButton = new ApplicationBarIconButton();
            backBarButton.IconUri = new Uri("/Assets/back.png", UriKind.Relative);
            backBarButton.Text = "Back";
            backBarButton.IsEnabled = true;
            backBarButton.Click += BackToMainScreenBarButton_Click;
            ApplicationBar.Buttons.Add(backBarButton);

        }
        #endregion Private Methods

        #region Private Event Handlers
        private void OnSelectedLocationChanged(object sender, RoutedEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            if (listbox != null)
            {
                Location selectedLocation = listbox.SelectedItem as Location;
                if (selectedLocation != null)
                {
                    _model.MyLocation = selectedLocation;

                    foreach (var button in ApplicationBar.Buttons)
                    {
                        ApplicationBarIconButton castedButton = button as ApplicationBarIconButton;
                        if (castedButton != null)
                        {
                            if (!string.Equals(castedButton.Text, "New Location", StringComparison.CurrentCultureIgnoreCase) &&
                                !string.Equals(castedButton.Text, "Back", StringComparison.CurrentCultureIgnoreCase))
                            {
                                castedButton.IsEnabled = _model.MyLocation != null;
                            }
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
                _model.RemoveLocation();
                _model.MyLocation = null;
            }
        }
        private void CancelSelectionBarButton_Click(object sender, EventArgs e)
        {
            _model.MyLocation = null;
        }
        private void CreateLocationButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateLocation.xaml", UriKind.Relative));
        }
        private void BackToMainScreenBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        #endregion Private Event Handlers
    }
}