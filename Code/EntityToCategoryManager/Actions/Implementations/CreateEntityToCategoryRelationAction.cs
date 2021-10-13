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
    public class CreateEntityToCategoryAction : AbstractAction<EntityToCategoryDatabasePathConfig>
    {

        internal CreateEntityToCategoryAction(EntityToCategoryDatabasePathConfig config) : base(config)
        {

        }




        /// <summary>
        /// Creates a relation with an entity id to a category id with the given parameters.<br></br>
        /// The relation is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="EntityToCategoryRelationAlreadyExistsException"></exception>
        /// <returns>True if the relation was created. False otherwise.</returns>
        public bool CreateEntityToCategoryRelation(int entityId, int categoryId)
        {
            
            var relationExists = new EntityToCategoryRelationExistsAction(databasePathConfig).IfEntityToCategoryRelationExsists(entityId, categoryId);
            if (relationExists)
            {
                throw new EntityToCategoryRelationAlreadyExistsException(entityId, categoryId);
            }

            //

            bool successful = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("INSERT INTO [{0}] ({1}, {2}) VALUES (@EntityId, @CatId); SELECT SCOPE_IDENTITY()",
                        databasePathConfig.EntityToCategoryTableName,
                        databasePathConfig.EntityIdColumnName, databasePathConfig.CategoryIdColumnName);

                    command.Parameters.Add(new SqlParameter("EntityId", entityId));
                    command.Parameters.Add(new SqlParameter("CategoryId", categoryId));

                    successful = (int.Parse(command.ExecuteScalar().ToString()).Equals(entityId.ToString()));
                }
            }

            return successful;
        }


        /// <summary>
        /// Creates a relation with an entity id to a category id with the given parameters.<br></br>
        /// The relation is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <returns>True if the relation was created. False otherwise.</returns>
        public bool TryCreateEntityToCategoryRelation(int entityId, int categoryId)
        {
            try
            {
                return CreateEntityToCategoryRelation(entityId, categoryId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
