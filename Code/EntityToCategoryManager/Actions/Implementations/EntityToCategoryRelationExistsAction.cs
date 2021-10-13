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
    public class EntityToCategoryRelationExistsAction : AbstractAction<EntityToCategoryDatabasePathConfig>
    {

        internal EntityToCategoryRelationExistsAction(EntityToCategoryDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if a relation between the <paramref name="entityId"/> and <paramref name="categoryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfEntityToCategoryRelationExsists(int entityId, int categoryId)
        {
            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetEntId AND [{2}] = @TargetCatId",
                        databasePathConfig.EntityIdColumnName, databasePathConfig.EntityToCategoryTableName, databasePathConfig.CategoryIdColumnName);
                    command.Parameters.Add(new SqlParameter("TargetEntId", entityId));
                    command.Parameters.Add(new SqlParameter("TargetCatId", categoryId));

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
        /// <param name="categoryId"></param>
        /// <returns>True if a relation between the <paramref name="entityId"/> and <paramref name="categoryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfEntityToCategoryRelationExsists(int entityId, int categoryId)
        {
            try
            {
                return IfEntityToCategoryRelationExsists(entityId, categoryId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
