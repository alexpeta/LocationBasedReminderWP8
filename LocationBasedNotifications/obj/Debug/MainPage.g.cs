﻿#pragma checksum "C:\Users\Alex\documents\visual studio 2012\Projects\LocationBasedNotifications\LocationBasedNotifications\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CD21BF5A359300C4E28B87DA96355A84"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace LocationBasedNotifications {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Button ViewLocationsButton;
        
        internal System.Windows.Controls.Button ReminderPivotButton;
        
        internal System.Windows.Controls.Button ViewMapButton;
        
        internal System.Windows.Controls.Button LoadTestData;
        
        internal System.Windows.Controls.Button UpdateTileViaAgent;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/LocationBasedNotifications;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.ViewLocationsButton = ((System.Windows.Controls.Button)(this.FindName("ViewLocationsButton")));
            this.ReminderPivotButton = ((System.Windows.Controls.Button)(this.FindName("ReminderPivotButton")));
            this.ViewMapButton = ((System.Windows.Controls.Button)(this.FindName("ViewMapButton")));
            this.LoadTestData = ((System.Windows.Controls.Button)(this.FindName("LoadTestData")));
            this.UpdateTileViaAgent = ((System.Windows.Controls.Button)(this.FindName("UpdateTileViaAgent")));
        }
    }
}

