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
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class CreateAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        private AbstractHasher passwordHasher;

        internal CreateAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {
            passwordHasher = new BCryptHasher();
        }


        /// <summary>
        /// Creates an account with the given parameters in the given <paramref name="builder"/> and the password.<br></br>
        /// The account is created in the database and table specified in this object's <see cref="DatabasePathConfig"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="AccountAlreadyExistsException"></exception>
        /// <returns>The Id corresponding to the created account.</returns>
        public int CreateAccount(Account.Builder builder, string password)
        {

            var usernameConstraintsChecker = new UsernameInputConstraint();
            usernameConstraintsChecker.SatisfiesConstraint(builder.Username);

            var passwordConstraintsChecker = new PasswordInputConstraint();
            passwordConstraintsChecker.SatisfiesConstraint(password);

            //

            bool usernameExists = new AccountExistsAction(databasePathConfig).TryIfAccountUsernameExsists(builder.Username);
            if (usernameExists)
            {
                throw new AccountAlreadyExistsException(builder.Username);
            }

            //

            var hashedPassword = passwordHasher.HashPlainTextWithSalt(password);


            int employeeId = -1;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("INSERT INTO [{0}] ({1}, {2}, {3}, {4}, {5}) VALUES (@Username, @Password, @DisabledFromLogIn, @Email, @AccountType); SELECT SCOPE_IDENTITY()",
                        databasePathConfig.AccountTableName, 
                        databasePathConfig.UsernameColumnName, databasePathConfig.PasswordColumnName, 
                        databasePathConfig.DisabledFromLogInColumnName, databasePathConfig.EmailColumnName,
                        databasePathConfig.AccountTypeColumnName);
                    
                    command.Parameters.Add(new SqlParameter("Username", builder.Username));
                    command.Parameters.Add(new SqlParameter("Password", hashedPassword));
                    command.Parameters.Add(new SqlParameter("DisabledFromLogIn", builder.DisabledFromLogIn));
                    command.Parameters.Add(new SqlParameter("Email", StringUtilities.ConvertStringToSqlString(builder.Email)));
                    command.Parameters.Add(new SqlParameter("AccountType", StringUtilities.ConvertStringToSqlString(builder.AccountType)));


                    employeeId = int.Parse(command.ExecuteScalar().ToString());
                }
            }

            return employeeId;

        }


        /// <summary>
        /// Creates an account with the given parameters in the given <paramref name="builder"/> and the password.<br></br>
        /// The account is created in the database and table specified in this object's <see cref="DatabasePathConfig"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="password"></param>
        /// <returns>The Id corresponding to the created account, or -1 if the creation has failed.</returns>
        public int TryCreateAccount(Account.Builder builder, string password)
        {
            try
            {
                return CreateAccount(builder, password);
            }
            catch (Exception)
            {
                return -1;
            }
        }


    }
}