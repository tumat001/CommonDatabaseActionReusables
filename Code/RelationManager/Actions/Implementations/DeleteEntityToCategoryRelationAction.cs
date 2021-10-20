using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.RelationManager.Config;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.RelationManager.Exceptions;

namespace CommonDatabaseActionReusables.RelationManager.Actions
{
    public class DeleteEntityToCategoryRelationAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal DeleteEntityToCategoryRelationAction(RelationDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Deletes the relation between then entity with the given <paramref name="entityId"/> and the category with the given param <paramref name="categoryId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="RelationDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteEntityCategoryRelation(int entityId, int categoryId)
        {
            bool idExists = new RelationExistsAction(databasePathConfig).IfRelationExsists(entityId, categoryId);
            if (!idExists)
            {
                throw new RelationDoesNotExistException(entityId, categoryId);
            }

            //


            bool isSuccessful = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}] WHERE [{1}] = @EntId AND [{2}] = @CatId",
                        databasePathConfig.EntityToCategoryTableName, databasePathConfig.EntityIdColumnName, databasePathConfig.CategoryIdColumnName);
                    command.Parameters.Add(new SqlParameter("EntId", entityId));
                    command.Parameters.Add(new SqlParameter("CatId", categoryId));

                    isSuccessful = command.ExecuteNonQuery() > 0;

                }
            }

            return isSuccessful;
        }


        /// <summary>
        /// Deletes the relation between then entity with the given <paramref name="entityId"/> and the category with the given param <paramref name="categoryId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteEntityCategoryRelation(int entityId, int categoryId)
        {
            try
            {
                return DeleteEntityCategoryRelation(entityId, categoryId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
