﻿using LocationBasedNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationBasedNotifications.Contracts
{
    public interface IRepository
    {
        #region Location Methods
        Location GetLocationById(int id);
        IEnumerable<Location> GetLocationsList();
        bool AddLocation(Location itemToAdd);
        bool DeleteLocation(Location itemToDelete);
        #endregion Location Methods

        #region Reminder Methods
        IEnumerable<Reminder> GetRemindersList();
        IEnumerable<Reminder> GetRemindersByStatusId(int statusId);
        void AddReminder(Reminder reminder);
        bool DeleteReminder(Reminder reminder);
        #endregion Reminder Methods

        #region Status Methods
        ReminderStatus GetStatusById(int id);
        IEnumerable<ReminderStatus> GetReminderStatusesList();
        #endregion Status Methods
    }
}
