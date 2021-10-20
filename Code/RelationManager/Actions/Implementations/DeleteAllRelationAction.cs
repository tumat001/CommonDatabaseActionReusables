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

namespace CommonDatabaseActionReusables.RelationManager.Actions
{
    public class DeleteAllRelationAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal DeleteAllRelationAction(RelationDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Deletes all relations.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no relation was deleted.</returns>
        public bool DeleteAllRelations()
        {

            bool isSuccessful = true;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}]",
                        databasePathConfig.EntityToCategoryTableName);

                    command.ExecuteNonQuery();

                }
            }

            return isSuccessful;
        }


        /// <summary>
        /// Deletes all relations.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <returns>True if the delete operation was successful, even if no relation was deleted.</returns>
        public bool TryDeleteAllRelations()
        {
            try
            {
                return DeleteAllRelations();
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
