using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.EntityToCategoryManager.Config;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.EntityToCategoryManager.Exceptions;

namespace CommonDatabaseActionReusables.EntityToCategoryManager.Actions
{
    public class EntityExistsAction : AbstractAction<EntityToCategoryDatabasePathConfig>
    {

        internal EntityExistsAction(EntityToCategoryDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if an <paramref name="entityId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfEntityExsists(int entityId)
        {
            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetEntId",
                        databasePathConfig.EntityIdColumnName, databasePathConfig.EntityToCategoryTableName);
                    command.Parameters.Add(new SqlParameter("TargetEntId", entityId));

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
        /// <param name="entityId"></param>
        /// <returns>True if an <paramref name="entityId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfEntityExsists(int entityId)
        {
            try
            {
                return IfEntityExsists(entityId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
