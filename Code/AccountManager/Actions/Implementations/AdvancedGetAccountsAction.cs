using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;


namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class AdvancedGetAccountsAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal AdvancedGetAccountsAction(AccountRelatedDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <param name="accountTypeFilter"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/> and <paramref name="accountTypeFilter"/>.</returns>
        public IReadOnlyList<Account> AdvancedGetAccountsAsList(AdvancedGetParameters adGetParameter, string accountTypeFilter = null)
        {

            var list = new List<Account>();
            var accBuilder = new Account.Builder();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    var adGetParamFromTextToContain = adGetParameter.GetSQLStatementFromTextToContain(databasePathConfig.UsernameColumnName);

                    command.CommandText = String.Format("SELECT * FROM (SELECT [{0}], [{1}], [{2}], [{3}], [{9}] FROM [{4}] ORDER BY {0} {5} {6}) T {7} {8}",
                        databasePathConfig.IdColumnName, databasePathConfig.UsernameColumnName, databasePathConfig.DisabledFromLogInColumnName, databasePathConfig.EmailColumnName, databasePathConfig.AccountTableName,
                        adGetParameter.GetSQLStatementFromOffset(),
                        adGetParameter.GetSQLStatementFromFetch(),
                        adGetParamFromTextToContain,
                        GetWhereCommandFromAccountTypeFilter(databasePathConfig.AccountTypeColumnName, accountTypeFilter, adGetParamFromTextToContain),
                        databasePathConfig.AccountTypeColumnName);

                    if (!String.IsNullOrEmpty(accountTypeFilter))
                    {
                        command.Parameters.Add(new SqlParameter("AccType", accountTypeFilter));
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accBuilder.Username = reader.GetSqlString(1).ToString();
                            accBuilder.DisabledFromLogIn = reader.GetSqlBoolean(2).Value;

                            accBuilder.Email = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(3));
                            accBuilder.AccountType = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(4));

                            list.Add(accBuilder.Build(reader.GetSqlInt32(0).Value));
                        }
                    }
                }
            }

            return list;
        }

        private string GetWhereCommandFromAccountTypeFilter(string accTypeColName, string accTypeFilter, string adGetParamFromTextToContain)
        {
            if (String.IsNullOrEmpty(accTypeFilter))
            {
                return "";
            }
            else
            {
                if (String.IsNullOrEmpty(adGetParamFromTextToContain))
                {
                    return String.Format("WHERE [{0}] = @AccType", accTypeColName, accTypeFilter);
                }
                else
                {
                    return String.Format("AND [{0}] = @AccType", accTypeColName, accTypeFilter);
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <param name="accountTypeFilter"></param>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/> and <paramref name="accountTypeFilter"/>.</returns>
        public IReadOnlyList<Account> TryAdvancedGetAccountsAsList(AdvancedGetParameters adGetParameter, string accountTypeFilter = null)
        {
            try
            {
                return AdvancedGetAccountsAsList(adGetParameter, accountTypeFilter);
            }
            catch (Exception)
            {
                return new List<Account>();
            }
        }


    }
}