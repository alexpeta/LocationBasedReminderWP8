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
using Microsoft.Phone.Maps.Toolkit;
using LocationBasedNotifications.Model;

namespace LocationBasedNotifications
{
    public partial class Map : PhoneApplicationPage
    {
        #region Private Members
        private MapViewModel _model;
        #endregion Private Members

        #region Public Properties
        public MapViewModel Model
        {
            get { return _model; }
            set { _model = value; }
        }
        #endregion Public Properties

        #region Constructors
        public Map()
        {
            InitializeComponent();
            CreateAppBar();
            Model = new MapViewModel();
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
            string param = string.Empty;
            if (Model.Location != null)
            {
                param = string.Format("?locationId={0}", Model.Location.LocationId);
            }
            NavigationService.Navigate(new Uri(string.Format("/CreateEditLocation.xaml{0}",param), UriKind.Relative));
        }

        private void AddPushPin()
        {
            MapOverlay MyOverlay = new MapOverlay();
            MyOverlay.GeoCoordinate = Model.Location.GeoCoordinate;
            MyOverlay.Content = Model.Location.GetSimplePushpin(Colors.Purple);
            
            MapLayer MyLayer = new MapLayer();
          
            MyLayer.Add(MyOverlay);
            MyMap.Layers.Add(MyLayer);
        }


        #endregion Private Methods

        #region Overrides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int passedParameter = 0;
            try
            {
                string stringParam = NavigationContext.QueryString["locationId"];
                int.TryParse(stringParam, out passedParameter);
            }
            catch (Exception)
            {
                passedParameter = -1;
            }

            if (passedParameter != 0 && passedParameter != -1)
            {
                Model.MapMode = MapMode.Edit;
                Model.LoadLocationById(passedParameter);
                MyMap.Center = Model.Location.GeoCoordinate;
                MyMap.ZoomLevel = Constants.MapZoomLevel;
                AddPushPin();
            }
            else
            {
                Model.MapMode = MapMode.Create;
                MyMap.ZoomLevel = Constants.MapZoomLevel;
            }


            base.OnNavigatedTo(e);
        }
        #endregion Overrides
    }
}