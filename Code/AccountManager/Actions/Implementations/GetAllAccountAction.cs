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
    public class GetAllAccountAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {

        internal GetAllAccountAction(AccountRelatedDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyList<Account> GetAllAccountsAsList()
        {

            var accGetParams = new AdvancedGetParameters();
            return new AdvancedGetAccountsAction(databasePathConfig).AdvancedGetAccountsAsList(accGetParams);


            /*
            var list = new List<Account>();
            var accBuilder = new Account.Builder();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}], [{1}], [{2}] FROM [{3}]",
                        databasePathConfig.IdColumnName, databasePathConfig.UsernameColumnName, databasePathConfig.DisabledFromLogInColumnName, databasePathConfig.AccountTableName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
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
        /// <returns>A list of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>If an exception occurs, an empty list is returned.</returns>
        public IReadOnlyList<Account> TryGetAllAccountsAsList()
        {
            try
            {
                return GetAllAccountsAsList();
            }
            catch (Exception)
            {
                return new List<Account>();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of account ids found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyList<int> GetAllAccountIdsAsList()
        {
            var list = new List<int>();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}]",
                        databasePathConfig.IdColumnName, databasePathConfig.AccountTableName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetSqlInt32(0).Value);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of account ids found in the database given in this object's <see cref="DatabasePathConfig"/>If an exception occurs, an empty list is returned.</returns>
        public IReadOnlyList<int> TryGetAllAccountIdsAsList()
        {
            try
            {
                return GetAllAccountIdsAsList();
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A dictionary of account ids to accounts found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyDictionary<int, Account> GetAllAccountsAsDictionary()
        {

            var map = new Dictionary<int, Account>();
            var accBuilder = new Account.Builder();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}], [{1}], [{2}], [{4}], [{5}] FROM [{3}]",
                        databasePathConfig.IdColumnName, databasePathConfig.UsernameColumnName, 
                        databasePathConfig.DisabledFromLogInColumnName, databasePathConfig.AccountTableName, 
                        databasePathConfig.EmailColumnName, databasePathConfig.AccountTypeColumnName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accBuilder.Username = reader.GetSqlString(1).ToString();
                            accBuilder.DisabledFromLogIn = reader.GetSqlBoolean(2).Value;
                            accBuilder.Email = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(3));
                            accBuilder.AccountType = StringUtilities.ConvertSqlStringToString(reader.GetSqlString(4));
                            var id = reader.GetSqlInt32(0).Value;

                            map.Add(id, accBuilder.Build(id));
                        }
                    }
                }
            }

            return map;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A dictionary of account ids to accounts found in the database given in this object's <see cref="DatabasePathConfig"/>. If an exception occurs, an empty dictionary is returned.</returns>
        public IReadOnlyDictionary<int, Account> TryGetAllAccountsAsDictionary()
        {
            try
            {
                return GetAllAccountsAsDictionary();
            }
            catch (Exception)
            {
                return new Dictionary<int, Account>();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>The number of accounts found in the database given in this object's <see cref="DatabasePathConfig"/> </returns>
        public int GetTotalNumberOfAccounts()
        {

            var count = 0;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT COUNT(*) FROM [{0}]",
                        databasePathConfig.AccountTableName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            count = reader.GetSqlInt32(0).Value;
                        }
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of accounts found in the database given in this object's <see cref="DatabasePathConfig"/>. If an exception occurs, -1 is returned. </returns>
        public int TryGetTotalNumberOfAccounts()
        {
            try
            {
                return GetTotalNumberOfAccounts();
            }
            catch (Exception)
            {
                return -1;
            }
        }



    }
}