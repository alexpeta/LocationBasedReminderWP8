using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LocationBasedNotifications.Contracts;
using LocationBasedNotifications.Repository;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;

namespace LocationBasedNotifications
{
    public partial class Map : PhoneApplicationPage
    {
        #region Private Members

        #endregion Private Members

        #region Constructors
        
        public Map()
        {
            InitializeComponent();
            CreateAppBar();
        }
        #endregion Constructors

        #region Private Methods
        private void CreateAppBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.IsMenuEnabled = true;

            ApplicationBarIconButton editLocationButton = new ApplicationBarIconButton();
            editLocationButton.IconUri = new Uri("/Assets/edit.png", UriKind.Relative);
            editLocationButton.Text = "Edit Location";
            editLocationButton.IsEnabled = true;
            editLocationButton.Click += EditLocationButton_Click;
            ApplicationBar.Buttons.Add(editLocationButton);

        }
        private void EditLocationButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddPushpin()
        {

        }

        #endregion Private Methods

        #region Overrides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //int passedParameter = 0;
            //try
            //{
            //    string stringParam = NavigationContext.QueryString["locationID"];
            //    int.TryParse(stringParam,out passedParameter);

            //    if (_repository != null)
            //    {
            //        _model = _repository.GetLocationById(passedParameter);
            //        GeoCoordinate coordonates = new GeoCoordinate(_model.Latitude, _model.Longitude);
            //        MyMap.Center = coordonates;
            //        MyMap.ZoomLevel = 10;
            //    }
            //}
            //catch (Exception)
            //{
            //    passedParameter = 0;
            //}
                        
            base.OnNavigatedTo(e);
        }
        #endregion Overrides
    }
}