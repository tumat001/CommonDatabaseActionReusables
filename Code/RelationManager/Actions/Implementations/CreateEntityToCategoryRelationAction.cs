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
    public class CreatePrimaryToTargetRelationAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal CreatePrimaryToTargetRelationAction(RelationDatabasePathConfig config) : base(config)
        {

        }




        /// <summary>
        /// Creates a relation with the <paramref name="primaryId"/> to a <paramref name="targetId"/> with the given parameters.<br></br>
        /// The relation is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="RelationAlreadyExistsException"></exception>
        /// <returns>True if the relation was created. False otherwise.</returns>
        public bool CreatePrimaryToTargetRelation(int primaryId, int targetId)
        {
            
            var relationExists = new RelationExistsAction(databasePathConfig).IfRelationExsists(primaryId, targetId);
            if (relationExists)
            {
                throw new RelationAlreadyExistsException(primaryId, targetId);
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

                    command.Parameters.Add(new SqlParameter("EntityId", primaryId));
                    command.Parameters.Add(new SqlParameter("CatId", targetId));

                    //successful = int.Parse(command.ExecuteScalar().ToString()).Equals(entityId.ToString());
                    command.ExecuteNonQuery();
                    successful = new RelationExistsAction(databasePathConfig).IfRelationExsists(primaryId, targetId);
                }
            }

            return successful;
        }


        /// <summary>
        /// Creates a relation with the <paramref name="primaryId"/> to a <paramref name="targetId"/> with the given parameters.<br></br>
        /// The relation is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetId"></param>
        /// <returns>True if the relation was created. False otherwise.</returns>
        public bool TryCreatePrimaryToTargetRelation(int primaryId, int targetId)
        {
            try
            {
                return CreatePrimaryToTargetRelation(primaryId, targetId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
