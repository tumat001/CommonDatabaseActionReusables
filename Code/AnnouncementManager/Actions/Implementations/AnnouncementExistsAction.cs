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
    public class AnnouncementExistsAction : AbstractAction<AnnouncementDatabasePathConfig>
    {

        internal AnnouncementExistsAction(AnnouncementDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the announcement id exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfAnnouncementIdExsists(int id)
        {

            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetId",
                        databasePathConfig.IdColumnName, databasePathConfig.AnnouncementTableName);
                    command.Parameters.Add(new SqlParameter("TargetId", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result = reader.HasRows;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the announcement id exists in the given parameters of <see cref="DatabasePathConfig"/>, false otherwise</returns>
        public bool TryIfAnnouncementIdExsists(int id)
        {
            try
            {
                return IfAnnouncementIdExsists(id);
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}