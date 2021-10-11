using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AnnouncementManager.Configs;
using CommonDatabaseActionReusables.AnnouncementManager.Exceptions;
using CommonDatabaseActionReusables.AnnouncementManager;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;

namespace CommonDatabaseActionReusables.AnnouncementManager.Actions
{
    public class GetAllAnnouncementAction : AbstractAction<AnnouncementDatabasePathConfig>
    {

        internal GetAllAnnouncementAction(AnnouncementDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyList<Announcement> GetAllAnnouncementsAsList()
        {
            return new AdvancedGetAnnouncementsAction(databasePathConfig).AdvancedGetAnnouncementsAsList(new AdvancedGetParameters());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/>. Returns an empty list if an exception occurs.</returns>
        public IReadOnlyList<Announcement> TryGetAllAnnouncementsAsList()
        {
            try
            {
                return GetAllAnnouncementsAsList();
            }
            catch (Exception)
            {
                return new List<Announcement>();
            }
        }


    }
}