using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.AccountManager.Actions.Exceptions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class DeleteAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {
        internal DeleteAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {
            
        }


        /// <summary>
        /// Deletes the account with the given <paramref name="username"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteAccountWithUsername(string username)
        {

            var usernameConstraintsChecker = new UsernameInputConstraint();
            usernameConstraintsChecker.SatisfiesConstraint(username);

            //

            bool usernameExists = new AccountExistsAction(databasePathConfig).TryIfAccountUsernameExsists(username);
            if (!usernameExists)
            {
                throw new AccountDoesNotExistException(username);
            }

            //


            bool isSuccessful = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}] WHERE [{1}] = @Username",
                        databasePathConfig.AccountTableName, databasePathConfig.UsernameColumnName);
                    command.Parameters.Add(new SqlParameter("Username", username));

                    isSuccessful = command.ExecuteNonQuery() > 0;

                }
            }

            return isSuccessful;

        }


        /// <summary>
        /// Deletes the account with the given <paramref name="username"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteAccountWithUsername(string username)
        {
            try
            {
                return DeleteAccountWithUsername(username);
            }
            catch (Exception)
            {
                return false;
            }
        }


        //


        /// <summary>
        /// Deletes the account with the given <paramref name="accountId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteAccountWithId(int accountId)
        {


            bool idExists = new AccountExistsAction(databasePathConfig).TryIfAccountIdExists(accountId);
            if (!idExists)
            {
                throw new AccountDoesNotExistException(null, accountId);
            }

            //


            bool isSuccessful = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}] WHERE [{1}] = @Id",
                        databasePathConfig.AccountTableName, databasePathConfig.IdColumnName);
                    command.Parameters.Add(new SqlParameter("Id", accountId));

                    isSuccessful = command.ExecuteNonQuery() > 0;

                }
            }

            return isSuccessful;
        }


        /// <summary>
        /// Deletes the account with the given <paramref name="accountId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteAccountWithId(int accountId)
        {
            try
            {
                return DeleteAccountWithId(accountId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}