using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.AccountManager;
using CommonDatabaseActionReusables.AccountManager.Hasher;
using CommonDatabaseActionReusables.AccountManager.Actions.Exceptions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class EditAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal EditAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Edits the account with the provided <paramref name="username"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="accBuilder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountAlreadyExistsException"></exception>
        /// <returns>True if the account was edited successfully. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool EditAccountWithUsername(string username, Account.Builder accBuilder, string password)
        {
            var usernameConstraintsChecker = new UsernameInputConstraint();
            usernameConstraintsChecker.SatisfiesConstraint(username);

            if (password != null)
            {
                var passwordConstraintsChecker = new PasswordInputConstraint();
                passwordConstraintsChecker.SatisfiesConstraint(password);
            }

            if (accBuilder != null)
            {
                usernameConstraintsChecker.SatisfiesConstraint(accBuilder.Username);
            }

            //

            bool usernameExists = new AccountExistsAction(databasePathConfig).TryIfAccountUsernameExsists(username);
            if (!usernameExists)
            {
                throw new AccountDoesNotExistException(username);
            }

            if (accBuilder != null) {
                bool newUsernameExists = new AccountExistsAction(databasePathConfig).TryIfAccountUsernameExsists(accBuilder.Username);

                if (newUsernameExists & !accBuilder.Username.Equals(username))
                {
                    throw new AccountAlreadyExistsException(accBuilder.Username);
                }
            }

            //

            var success = false;
            string hashedPassword = null;

            if (password != null)
            {
                hashedPassword = new BCryptHasher().HashPlainTextWithSalt(password);
            }


            if (accBuilder != null & hashedPassword != null)
            {
                success = SQL_EditAccountWithUsername(username, accBuilder, hashedPassword);
            }
            else if (accBuilder == null & hashedPassword != null)
            {
                success = SQL_EditAccountWithUsername(username, hashedPassword);
            }
            else if (accBuilder != null & hashedPassword == null)
            {
                success = SQL_EditAccountWithUsername(username, accBuilder);
            }
            else if (accBuilder == null & hashedPassword == null)
            {
                success = true;
            }

            return success;
        }


        private bool SQL_EditAccountWithUsername(string currUsername, Account.Builder accBuilder, string hashedPassword)
        {
            var success = false;
            
            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE [{0}] SET [{1}] = @NewUsernameVal, [{2}] = @PasswordVal, [{3}] = @DisabledFromLogInVal, [{4}] = @Email, [{5}] = @AccountType WHERE [{1}] = @CurrUsernameVal",
                        databasePathConfig.AccountTableName, databasePathConfig.UsernameColumnName, databasePathConfig.PasswordColumnName, 
                        databasePathConfig.DisabledFromLogInColumnName, databasePathConfig.EmailColumnName, databasePathConfig.AccountTypeColumnName);
                    command.Parameters.Add(new SqlParameter("NewUsernameVal", accBuilder.Username));
                    command.Parameters.Add(new SqlParameter("PasswordVal", hashedPassword));
                    command.Parameters.Add(new SqlParameter("CurrUsernameVal", currUsername));
                    command.Parameters.Add(new SqlParameter("DisabledFromLogInVal", accBuilder.DisabledFromLogIn));
                    command.Parameters.Add(new SqlParameter("Email", StringUtilities.ConvertStringToSqlString(accBuilder.Email)));
                    command.Parameters.Add(new SqlParameter("AccountType", StringUtilities.ConvertStringToSqlString(accBuilder.AccountType)));

                    success = command.ExecuteNonQuery() > 0;
                }
            }

            return success;
        }

        private bool SQL_EditAccountWithUsername(string currUsername, Account.Builder accBuilder)
        {
            var success = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE [{0}] SET [{1}] = @NewUsernameVal, [{2}] = @DisabledFromLogInVal, [{3}] = @Email, [{4}] = @AccountType WHERE [{1}] = @CurrUsernameVal",
                        databasePathConfig.AccountTableName, databasePathConfig.UsernameColumnName, 
                        databasePathConfig.DisabledFromLogInColumnName, databasePathConfig.EmailColumnName, databasePathConfig.AccountTypeColumnName);
                    command.Parameters.Add(new SqlParameter("NewUsernameVal", accBuilder.Username));
                    command.Parameters.Add(new SqlParameter("CurrUsernameVal", currUsername));
                    command.Parameters.Add(new SqlParameter("DisabledFromLogInVal", accBuilder.DisabledFromLogIn));
                    command.Parameters.Add(new SqlParameter("Email", StringUtilities.ConvertStringToSqlString(accBuilder.Email)));
                    command.Parameters.Add(new SqlParameter("AccountType", StringUtilities.ConvertStringToSqlString(accBuilder.AccountType)));

                    success = command.ExecuteNonQuery() > 0;
                }
            }

            return success;
        }

        private bool SQL_EditAccountWithUsername(string currUsername, string hashedPassword)
        {
            var success = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE [{0}] SET [{1}] = @PasswordVal WHERE [{2}] = @CurrUsernameVal",
                        databasePathConfig.AccountTableName, databasePathConfig.PasswordColumnName, databasePathConfig.UsernameColumnName);
                    command.Parameters.Add(new SqlParameter("PasswordVal", hashedPassword));
                    command.Parameters.Add(new SqlParameter("CurrUsernameVal", currUsername));

                    success = command.ExecuteNonQuery() > 0;
                }
            }

            return success;
        }


        /// <summary>
        /// Edits the account with the provided <paramref name="username"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="accBuilder"></param>
        /// <returns>True if the account was edited successfully, false otherwise. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool TryEditAccountWithUsername(string username, Account.Builder accBuilder, string password)
        {
            try
            {
                return EditAccountWithUsername(username, accBuilder, password);
            }
            catch (Exception)
            {
                return false;
            }
        }



        /// <summary>
        /// Edits the account with the provided <paramref name="id"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accBuilder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountAlreadyExistsException"></exception>
        /// <returns>True if the account was edited successfully. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool EditAccountWithId(int id, Account.Builder accBuilder, string password)
        {
            var accUsername = new GetAccountAction(databasePathConfig).GetAccountInfoFromId(id).Username;

            return EditAccountWithUsername(accUsername, accBuilder, password);
        }

        /// <summary>
        /// Edits the account with the provided <paramref name="id"/> using the properties found in <paramref name="accBuilder"/> and using the provided <paramref name="password"/>.<br/>
        /// <br/>
        /// Set <paramref name="accBuilder"/> or <paramref name="password"/> to null to indicate that no change is desired. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accBuilder"></param>
        /// <returns>True if the account was edited successfully, false otherwise. Setting both <paramref name="accBuilder"/> and <paramref name="password"/> to null also results in true being returned.</returns>
        public bool TryEditAccountWithId(int id, Account.Builder accBuilder, string password)
        {
            try
            {
                return EditAccountWithId(id, accBuilder, password);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}