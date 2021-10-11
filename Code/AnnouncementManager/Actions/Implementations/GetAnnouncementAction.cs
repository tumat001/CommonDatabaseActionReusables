using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AnnouncementManager.Configs;
using CommonDatabaseActionReusables.AnnouncementManager.Exceptions;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;

namespace CommonDatabaseActionReusables.AnnouncementManager.Actions
{
    public class GetAnnouncementAction : AbstractAction<AnnouncementDatabasePathConfig>
    {

        internal GetAnnouncementAction(AnnouncementDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>An <see cref="Announcement"/> object containing information about the announcement with the provided <paramref name="id"/>.</returns>
        public Announcement GetAnnouncementInfoFromId(int id)
        {

            //

            bool idExists = new AnnouncementExistsAction(databasePathConfig).TryIfAnnouncementIdExsists(id);
            if (!idExists)
            {
                throw new AnnouncementDoesNotExistException(id);
            }

            //

            var builder = new Announcement.Builder();
            Announcement announcement = null;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{1}], [{2}], [{3}], [{4}], [{5}] FROM [{6}] WHERE [{0}] = @IdVal",
                        databasePathConfig.IdColumnName, databasePathConfig.TitleColumnName, databasePathConfig.ContentColumnName,
                        databasePathConfig.MainImageColumnName, databasePathConfig.DateTimeCreatedColumnName, databasePathConfig.DateTimeLastModifiedColumnName,
                        databasePathConfig.AnnouncementTableName);
                    command.Parameters.Add(new SqlParameter("IdVal", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            builder.Title = reader.GetSqlString(0).ToString();
                            builder.Content = StringUtilities.ConvertSqlStringToByteArray(reader.GetSqlString(1));
                            builder.MainImage = StringUtilities.ConvertSqlStringToByteArray(reader.GetSqlString(2));

                            var dateCreated = reader.GetSqlDateTime(3).Value;
                            var dateLastModified = reader.GetSqlDateTime(4).Value;

                            builder.DateTimeCreated = dateCreated;
                            builder.DateTimeLastModified = dateLastModified;

                            announcement = builder.Build(id);
                        }
                    }
                }
            }


            return announcement;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="Announcement"/> object containing information about the announcement with the provided <paramref name="id"/>.</returns>
        public Announcement TryGetAnnouncementInfoFromId(int id)
        {
            try
            {
                return GetAnnouncementInfoFromId(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}