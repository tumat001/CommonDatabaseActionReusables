using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.AccountManager.Actions.Exceptions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class GetAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal GetAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="username"/>.</returns>
        public Account GetAccountInfoFromUsername(string username)
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

            var accBuilder = new Account.Builder();
            Account account = null;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}], [{3}], [{4}], [{5}] FROM [{1}] WHERE [{2}] = @UsernameVal",
                        databasePathConfig.IdColumnName, databasePathConfig.AccountTableName, 
                        databasePathConfig.UsernameColumnName, databasePathConfig.DisabledFromLogInColumnName, 
                        databasePathConfig.EmailColumnName, databasePathConfig.AccountTypeColumnName);
                    command.Parameters.Add(new SqlParameter("UsernameVal", username));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            accBuilder.Username = username;
                            accBuilder.DisabledFromLogIn = reader.GetSqlBoolean(1).Value;
                            accBuilder.Email = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(2));
                            accBuilder.AccountType = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(3));

                            account = accBuilder.Build(reader.GetSqlInt32(0).Value);
                        }
                    }
                }
            }


            return account;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="username"/>, or null if not found..</returns>
        public Account TryGetAccountInfoFromUsername(string username)
        {
            try
            {
                return GetAccountInfoFromUsername(username);
            }
            catch (Exception)
            {
                return null;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AccountDoesNotExistException"></exception>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="id"/>.</returns>
        public Account GetAccountInfoFromId(int id)
        {

            //

            bool idExists = new AccountExistsAction(databasePathConfig).TryIfAccountIdExists(id);
            if (!idExists)
            {
                throw new AccountDoesNotExistException(null, id);
            }

            //

            var accBuilder = new Account.Builder();
            Account account = null;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}], [{3}], [{4}], [{5}] FROM [{1}] WHERE [{2}] = @IdVal",
                        databasePathConfig.UsernameColumnName, databasePathConfig.AccountTableName, 
                        databasePathConfig.IdColumnName, databasePathConfig.DisabledFromLogInColumnName, 
                        databasePathConfig.EmailColumnName, databasePathConfig.AccountTypeColumnName);
                    command.Parameters.Add(new SqlParameter("IdVal", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            accBuilder.Username = reader.GetSqlString(0).ToString();
                            accBuilder.DisabledFromLogIn = reader.GetSqlBoolean(1).Value;
                            accBuilder.Email = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(2));
                            accBuilder.AccountType = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(3));

                            account = accBuilder.Build(id);
                        }
                    }
                }
            }


            return account;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="Account"/> object containing information about the account with the provided <paramref name="id"/>, or null if not found.</returns>
        public Account TryGetAccountInfoFromId(int id)
        {
            try
            {
                return GetAccountInfoFromId(id);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}