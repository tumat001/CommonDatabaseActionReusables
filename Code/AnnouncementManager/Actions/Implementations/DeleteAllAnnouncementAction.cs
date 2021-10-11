using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AnnouncementManager.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.AnnouncementManager.Actions
{
    public class DeleteAllAnnouncementAction : AbstractAction<AnnouncementDatabasePathConfig>
    {

        internal DeleteAllAnnouncementAction(AnnouncementDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Deletes all announcements.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no announcment was deleted.</returns>
        public bool DeleteAllAnnouncements()
        {

            bool isSuccessful = true;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}]",
                        databasePathConfig.AnnouncementTableName);

                    command.ExecuteNonQuery();

                }
            }

            return isSuccessful;

        }


        /// <summary>
        /// Deletes all announcements.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <returns>True if the delete operation was successful, even if no announcment was deleted.</returns>
        public bool TryDeleteAllAnnouncements()
        {
            try
            {
                return DeleteAllAnnouncements();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}