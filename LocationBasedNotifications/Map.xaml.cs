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

namespace LocationBasedNotifications
{
    public partial class Map : PhoneApplicationPage
    {
        #region Private Members
        private Location _model;
        private IRepository _repository;
        #endregion Private Members

        #region Constructors
        public Map() : this(new LocalStorageRepository())
        {
        }

        public Map(IRepository repository)
        {
            InitializeComponent();
            _model = new Location();
            _repository = repository;
        }
        #endregion Constructors

        #region Overrides
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int passedParameter = 0;
            try
            {
                string stringParam = NavigationContext.QueryString["locationID"];
                int.TryParse(stringParam,out passedParameter);

                if (_repository != null)
                {
                    _model = _repository.GetItemById(passedParameter);
                    GeoCoordinate coordonates = new GeoCoordinate(_model.Latitude, _model.Longitude);
                    MyMap.Center = coordonates;
                    MyMap.ZoomLevel = 10;
                }
            }
            catch (Exception)
            {
                passedParameter = 0;
            }
                        
            base.OnNavigatedTo(e);
        }
        #endregion Overrides
    }
}