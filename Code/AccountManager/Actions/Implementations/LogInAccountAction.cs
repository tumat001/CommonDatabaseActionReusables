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

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class LogInAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal LogInAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accUsername"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountDisabledFromLoggingInException"></exception>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>.<br /><br /> 
        /// <see cref="AccountDisabledFromLoggingInException"/> is thrown when the account is disabled from logging in. </returns>
        public bool IfAccountCanLogInWithGivenPassword(string accUsername, string password)
        {
            var usernameConstraintsChecker = new UsernameInputConstraint();
            usernameConstraintsChecker.SatisfiesConstraint(accUsername);

            bool usernameExists = new AccountExistsAction(databasePathConfig).TryIfAccountUsernameExsists(accUsername);
            if (!usernameExists)
            {
                throw new AccountDoesNotExistException(accUsername);
            }

            //

            string hashedPassword = null;
            bool disabledFromLogIn = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}], [{3}] FROM [{1}] WHERE [{2}] = @UsernameVal",
                        databasePathConfig.PasswordColumnName, databasePathConfig.AccountTableName, databasePathConfig.UsernameColumnName, databasePathConfig.DisabledFromLogInColumnName);
                    command.Parameters.Add(new SqlParameter("UsernameVal", accUsername));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashedPassword = reader.GetSqlString(0).ToString();
                            disabledFromLogIn = reader.GetSqlBoolean(1).Value;
                        }
                    }
                }
            }

            if (disabledFromLogIn)
            {
                throw new AccountDisabledFromLoggingInException(accUsername);
            }

            var isPasswordMatches = false;

            if (hashedPassword != null)
            {
                isPasswordMatches = new BCryptHasher().VerifyPlainTextToHashedPassword(password, hashedPassword);
            }
            return isPasswordMatches;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="accUsername"></param>
        /// <param name="password"></param>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>, false otherwise.
        /// </returns>
        public bool TryIfAccountCanLogInWithGivenPassword(string accUsername, string password)
        {
            try
            {
                return IfAccountCanLogInWithGivenPassword(accUsername, password);
            }
            catch (Exception)
            {
                return false;
            }
        }


        //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <exception cref="AccountDisabledFromLoggingInException"></exception>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>.<br/><br/>
        /// <see cref="AccountDisabledFromLoggingInException"/> is thrown when the account is disabled from logging in. </returns>
        public bool IfAccountCanLogInWithGivenPassword(int id, string password)
        {

            bool idExists = new AccountExistsAction(databasePathConfig).TryIfAccountIdExists(id);
            if (!idExists)
            {
                throw new AccountDoesNotExistException(null, id);
            }

            //


            string hashedPassword = null;
            bool disabledFromLogIn = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}], [{3}] FROM [{1}] WHERE [{2}] = @IdVal",
                        databasePathConfig.PasswordColumnName, databasePathConfig.AccountTableName, databasePathConfig.IdColumnName, databasePathConfig.DisabledFromLogInColumnName);
                    command.Parameters.Add(new SqlParameter("IdVal", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashedPassword = reader.GetSqlString(0).ToString();
                            disabledFromLogIn = reader.GetSqlBoolean(1).Value;
                        }
                    }
                }
            }

            if (disabledFromLogIn)
            {
                throw new AccountDisabledFromLoggingInException(null, id);
            }

            var isPasswordMatches = false;

            if (hashedPassword != null)
            {
                isPasswordMatches = new BCryptHasher().VerifyPlainTextToHashedPassword(password, hashedPassword);
            }
            return isPasswordMatches;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>, false otherwise.<br/><br/>
        /// </returns>
        public bool TryIfAccountCanLogInWithGivenPassword(int id, string password)
        {
            try
            {
                return IfAccountCanLogInWithGivenPassword(id, password);
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}