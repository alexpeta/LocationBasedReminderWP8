﻿#pragma checksum "C:\Users\Alex\documents\visual studio 2012\Projects\LocationBasedNotifications\LocationBasedNotifications\ReminderPivot.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D04289C57A598C72B787B1F42B99B69"
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
    
    
    public partial class ReminderPivot : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot RemindersPivotHolder;
        
        internal System.Windows.Controls.ListBox ActiveRemindersListBox;
        
        internal System.Windows.Controls.ListBox InactiveRemindersListBox;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LocationBasedNotifications;component/ReminderPivot.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.RemindersPivotHolder = ((Microsoft.Phone.Controls.Pivot)(this.FindName("RemindersPivotHolder")));
            this.ActiveRemindersListBox = ((System.Windows.Controls.ListBox)(this.FindName("ActiveRemindersListBox")));
            this.InactiveRemindersListBox = ((System.Windows.Controls.ListBox)(this.FindName("InactiveRemindersListBox")));
        }
    }
}

