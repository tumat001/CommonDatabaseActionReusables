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
    public class MatchAccountWithPasswordAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal MatchAccountWithPasswordAction(AccountRelatedDatabasePathConfig config) : base(config)
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
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>.</returns>
        public bool AccountPasswordMatchesWithGiven(string accUsername, string password)
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

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{2}] = @UsernameVal",
                        databasePathConfig.PasswordColumnName, databasePathConfig.AccountTableName, databasePathConfig.UsernameColumnName);
                    command.Parameters.Add(new SqlParameter("UsernameVal", accUsername));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashedPassword = reader.GetSqlString(0).ToString();
                        }
                    }
                }
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
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="accUsername"/>.</returns>
        public bool TryAccountPasswordMatchesWithGiven(string accUsername, string password)
        {
            try
            {
                return AccountPasswordMatchesWithGiven(accUsername, password);
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
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>.</returns>
        public bool AccountPasswordMatchesWithGiven(int id, string password)
        {
            
            bool idExists = new AccountExistsAction(databasePathConfig).TryIfAccountIdExists(id);
            if (!idExists)
            {
                throw new AccountDoesNotExistException(null, id);
            }

            //


            string hashedPassword = null;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{2}] = @IdVal",
                        databasePathConfig.PasswordColumnName, databasePathConfig.AccountTableName, databasePathConfig.IdColumnName);
                    command.Parameters.Add(new SqlParameter("IdVal", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashedPassword = reader.GetSqlString(0).ToString();
                        }
                    }
                }
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
        /// <returns>True if the given <paramref name="password"/> matches with the password of the account with the provided <paramref name="id"/>. False otherwise.< /returns>
        public bool TryAccountPasswordMatchesWithGiven(int id, string password)
        {
            try
            {
                return AccountPasswordMatchesWithGiven(id, password);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}