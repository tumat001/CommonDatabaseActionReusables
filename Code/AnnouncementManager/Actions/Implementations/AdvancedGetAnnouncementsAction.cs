using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AnnouncementManager.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;

namespace CommonDatabaseActionReusables.AnnouncementManager.Actions
{
    public class AdvancedGetAnnouncementsAction : AbstractAction<AnnouncementDatabasePathConfig>
    {

        internal AdvancedGetAnnouncementsAction(AnnouncementDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>.</returns>
        public IReadOnlyList<Announcement> AdvancedGetAnnouncementsAsList(AdvancedGetParameters adGetParameter)
        {

            var list = new List<Announcement>();
            var builder = new Announcement.Builder();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT * FROM (SELECT [{0}], [{1}], [{2}], [{3}], [{4}], [{5}] FROM [{6}] ORDER BY {0} {7} {8}) T {9} ORDER BY {5} DESC",
                        databasePathConfig.IdColumnName, databasePathConfig.TitleColumnName, databasePathConfig.ContentColumnName, databasePathConfig.MainImageColumnName,
                        databasePathConfig.DateTimeCreatedColumnName, databasePathConfig.DateTimeLastModifiedColumnName, databasePathConfig.AnnouncementTableName,
                        adGetParameter.GetSQLStatementFromOffset(),
                        adGetParameter.GetSQLStatementFromFetch(),
                        adGetParameter.GetSQLStatementFromTextToContain(databasePathConfig.TitleColumnName));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            builder.Title = reader.GetSqlString(1).ToString();
                            builder.Content = StringUtilities.ConvertSqlStringToByteArray(reader.GetSqlString(2));
                            builder.MainImage = StringUtilities.ConvertSqlStringToByteArray(reader.GetSqlString(3));

                            var dateCreated = reader.GetSqlDateTime(4).Value;
                            var dateLastModified = reader.GetSqlDateTime(5).Value;

                            builder.DateTimeCreated = dateCreated;
                            builder.DateTimeLastModified = dateLastModified;

                            var id = reader.GetSqlInt32(0).Value;

                            list.Add(builder.Build(id));
                        }
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Returns an empty list when an exception occurs.</returns>
        public IReadOnlyList<Announcement> TryAdvancedGetAnnouncementsAsList(AdvancedGetParameters adGetParameter)
        {
            try
            {
                return AdvancedGetAnnouncementsAsList(adGetParameter);
            }
            catch (Exception)
            {
                return new List<Announcement>();
            }
        }

    }
}