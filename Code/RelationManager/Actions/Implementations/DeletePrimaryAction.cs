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
    public class DeletePrimaryAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal DeletePrimaryAction(RelationDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Deletes the primary with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="PrimaryDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeletePrimaryWithId(int id)
        {
            bool idExists = new PrimaryExistsAction(databasePathConfig).IfPrimaryExsists(id);
            if (!idExists)
            {
                throw new PrimaryDoesNotExistException(id);
            }

            //


            bool isSuccessful = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}] WHERE [{1}] = @Id",
                        databasePathConfig.EntityToCategoryTableName, databasePathConfig.EntityIdColumnName);
                    command.Parameters.Add(new SqlParameter("Id", id));

                    isSuccessful = command.ExecuteNonQuery() > 0;

                }
            }

            return isSuccessful;
        }


        /// <summary>
        /// Deletes the primary with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeletePrimaryWithId(int id)
        {
            try
            {
                return DeletePrimaryWithId(id);
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
