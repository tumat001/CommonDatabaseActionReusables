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
    public class RelationExistsAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal RelationExistsAction(RelationDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if a relation between the <paramref name="primaryId"/> and <paramref name="targetId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfRelationExsists(int primaryId, int targetId)
        {
            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetEntId AND [{2}] = @TargetCatId",
                        databasePathConfig.EntityIdColumnName, databasePathConfig.EntityToCategoryTableName, databasePathConfig.CategoryIdColumnName);
                    command.Parameters.Add(new SqlParameter("TargetEntId", primaryId));
                    command.Parameters.Add(new SqlParameter("TargetCatId", targetId));

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
        /// <param name="targetId"></param>
        /// <returns>True if a relation between the <paramref name="primaryId"/> and <paramref name="targetId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfRelationExsists(int primaryId, int targetId)
        {
            try
            {
                return IfRelationExsists(primaryId, targetId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
