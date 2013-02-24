﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications
{
    public sealed class Constants
    {
        public readonly static uint AccurecyInMeters = 100;
        public readonly static int PositionMaximumAge = 5; // minutes
        public readonly static int Timeout = 10; //seconds
        public readonly static string LocationAgreement = "useLocationAgreement";
        public readonly static string LBNLocations = "LBNLocations";
        public readonly static string DBConnectionString = "Data Source=isostore:/LocationBasedReminders.sdf";

        private Constants()
        {
        }
    }
}
