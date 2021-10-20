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
    public class EditRelationAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal EditRelationAction(RelationDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// Edits the relation of the primary with the provided <paramref name="primaryId"/> and the target <paramref name="targetIdToReplace"/>.<br/>
        /// The <paramref name="newTargetId"/> replaces the <paramref name="targetIdToReplace"/>.<br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetIdToReplace"></param>
        /// <param name="newTargetId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="RelationAlreadyExistsException"></exception>
        /// <exception cref="RelationDoesNotExistException"></exception>
        /// <returns>True if the relation was edited successfully. False otherwise.</returns>
        public bool EditRelation(int primaryId, int targetIdToReplace, int newTargetId)
        {
            var relationExistsAction = new RelationExistsAction(databasePathConfig);

            bool currRelationExists = relationExistsAction.IfRelationExsists(primaryId, targetIdToReplace);
            if (!currRelationExists)
            {
                throw new RelationDoesNotExistException(primaryId, targetIdToReplace);
            }


            bool newRelationExists = relationExistsAction.IfRelationExsists(primaryId, newTargetId);
            if (newRelationExists)
            {
                throw new RelationAlreadyExistsException(primaryId, newTargetId);
            }

            //


            var success = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE [{0}] SET [{1}] = @NewCatVal WHERE [{2}] = @EntIdVal and [{1}] = @OldCatVal",
                        databasePathConfig.EntityToCategoryTableName,
                        databasePathConfig.CategoryIdColumnName, databasePathConfig.EntityIdColumnName);
                    command.Parameters.Add(new SqlParameter("NewCatVal", newTargetId));
                    command.Parameters.Add(new SqlParameter("EntIdVal", primaryId));
                    command.Parameters.Add(new SqlParameter("OldCatVal", targetIdToReplace));

                    success = command.ExecuteNonQuery() > 0;
                }
            }

            return success;

        }


        /// <summary>
        /// Edits the relation of the primary with the provided <paramref name="primaryId"/> and the target <paramref name="targetIdToReplace"/>.<br/>
        /// The <paramref name="newTargetId"/> replaces the <paramref name="targetIdToReplace"/>.<br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetIdToReplace"></param>
        /// <param name="newTargetId"></param>
        /// <returns>True if the relation was edited successfully. False otherwise.</returns>
        public bool TryEditRelation(int primaryId, int targetIdToReplace, int newTargetId)
        {
            try 
            {
                return EditRelation(primaryId, targetIdToReplace, newTargetId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
