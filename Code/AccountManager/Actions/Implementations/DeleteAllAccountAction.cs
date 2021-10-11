using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.AccountManager.Actions.Exceptions;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class DeleteAllAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal DeleteAllAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Deletes all accounts.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no account was deleted.</returns>
        public bool DeleteAllAccounts()
        {

            bool isSuccessful = true;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}]",
                        databasePathConfig.AccountTableName);

                    command.ExecuteNonQuery();

                }
            }

            return isSuccessful;

        }


        /// <summary>
        /// Deletes all accounts.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True if the delete operation was successful, even if no account was deleted.</returns>
        public bool TryDeleteAllAccounts()
        {
            try
            {
                return DeleteAllAccounts();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}