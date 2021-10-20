using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.RelationManager.Config;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.RelationManager.Exceptions;

namespace CommonDatabaseActionReusables.RelationManager.Actions
{
    public class PrimaryExistsAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal PrimaryExistsAction(RelationDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if an <paramref name="primaryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfPrimaryExsists(int primaryId)
        {
            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetEntId",
                        databasePathConfig.EntityIdColumnName, databasePathConfig.EntityToCategoryTableName);
                    command.Parameters.Add(new SqlParameter("TargetEntId", primaryId));

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
        /// <param name="primaryId"></param>
        /// <returns>True if an <paramref name="primaryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfPrimaryExists(int primaryId)
        {
            try
            {
                return IfPrimaryExsists(primaryId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
