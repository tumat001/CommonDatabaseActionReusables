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

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class FilteredGetAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal FilteredGetAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AntiSQLInjectionInputConstraint"></exception>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/> that contains the parameter <paramref name="text"/>.</returns>
        public IReadOnlyList<Account> GetFilteredAccountsWithUsernamesContainingText(string text)
        {
            var advancedGetParam = new AdvancedGetParameters();
            advancedGetParam.TextToContain = text;

            return new AdvancedGetAccountsAction(databasePathConfig).AdvancedGetAccountsAsList(advancedGetParam);

            /*
            var inputConstraintsChecker = new AntiSQLInjectionInputConstraint();
            inputConstraintsChecker.SatisfiesConstraint(text);

            //

            var list = new List<Account>();
            var accBuilder = new Account.Builder();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}], [{1}], [{2}] FROM [{3}] WHERE [{1}] LIKE '%{4}%'",
                        databasePathConfig.IdColumnName, databasePathConfig.UsernameColumnName, databasePathConfig.DisabledFromLogInColumnName,
                        databasePathConfig.AccountTableName, text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accBuilder.Username = reader.GetSqlString(1).ToString();
                            accBuilder.DisabledFromLogIn = reader.GetSqlBoolean(2).Value;
                            list.Add(accBuilder.Build(reader.GetSqlInt32(0).Value));
                        }
                    }
                }
            }

            return list;
            */

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/> that contains the parameter <paramref name="text"/>. Returns an empty list when an exception occurs.</returns>
        public IReadOnlyList<Account> TryGetFilteredAccountsWithUsernamesContainingText(string text)
        {
            try
            {
                return GetFilteredAccountsWithUsernamesContainingText(text);
            }
            catch (Exception)
            {
                return new List<Account>();
            }
        }

    }
}