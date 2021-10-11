using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AnnouncementManager.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;

namespace CommonDatabaseActionReusables.AnnouncementManager.Actions
{
    public class CreateAnnouncementAction : AbstractAction<AnnouncementDatabasePathConfig>
    {
        internal CreateAnnouncementAction(AnnouncementDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Creates an announcement with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The announcement is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// The <paramref name="builder"/>'s <see cref="Announcement.Builder.DateTimeCreated"/> and <see cref="Announcement.Builder.DateTimeLastModified"/> is set to the current time.
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>The Id corresponding to the created announcement.</returns>
        public int CreateAnnouncement(Announcement.Builder builder)
        {
            var inputConstraintsChecker = new AntiSQLInjectionInputConstraint();
            inputConstraintsChecker.SatisfiesConstraint(builder.Title);

            //

            int announcementId = -1;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("INSERT INTO [{0}] ({1}, {2}, {3}, {4}, {5}) VALUES (@Title, @Content, @MainImage, @DateTimeCreated, @DateTimeLastModified); SELECT SCOPE_IDENTITY()",
                        databasePathConfig.AnnouncementTableName,
                        databasePathConfig.TitleColumnName, databasePathConfig.ContentColumnName, databasePathConfig.MainImageColumnName,
                        databasePathConfig.DateTimeCreatedColumnName, databasePathConfig.DateTimeLastModifiedColumnName);
                    
                    command.Parameters.Add(new SqlParameter("Title", builder.Title));


                    command.Parameters.Add(new SqlParameter("Content", GetParamOrDbNullIfParamIsNull(builder.Content)));
                    command.Parameters.Add(new SqlParameter("MainImage", GetParamOrDbNullIfParamIsNull(builder.MainImage)));

                    var dateTimeNow = DateTime.Now;
                    builder.DateTimeCreated = dateTimeNow;
                    builder.DateTimeLastModified = dateTimeNow;

                    command.Parameters.Add(new SqlParameter("DateTimeCreated", builder.DateTimeCreated));
                    command.Parameters.Add(new SqlParameter("DateTimeLastModified", builder.DateTimeLastModified));


                    announcementId = int.Parse(command.ExecuteScalar().ToString());
                }
            }

            return announcementId;
        }


        /// <summary>
        /// Creates an announcement with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The announcement is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// The <paramref name="builder"/>'s <see cref="Announcement.Builder.DateTimeCreated"/> and <see cref="Announcement.Builder.DateTimeLastModified"/> is set to the current time.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>The Id corresponding to the created announcement. -1 is returned when an exception occurred.</returns>
        public int TryCreateAnnouncement(Announcement.Builder builder)
        {
            try
            {
                return CreateAnnouncement(builder);
            }
            catch (Exception)
            {
                return -1;
            }
        }


    }
}