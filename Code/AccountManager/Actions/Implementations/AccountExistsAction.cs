using System;
using System.Collections.Generic;
using System.Text;
using CommonDatabaseActionReusables.AccountManager.Actions.Configs;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.AccountManager.Actions
{
    public class AccountExistsAction : AbstractAction<AccountRelatedDatabasePathConfig>
    {


        internal AccountExistsAction(AccountRelatedDatabasePathConfig config) : base(config)
        {
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="argUsername"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>True if the username exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfAccountUsernameExsists(string argUsername)
        {

            var constraintsChecker = new UsernameInputConstraint();
            constraintsChecker.SatisfiesConstraint(argUsername);

            
            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetUsername",
                        databasePathConfig.UsernameColumnName, databasePathConfig.AccountTableName);
                    command.Parameters.Add(new SqlParameter("TargetUsername", argUsername));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result = reader.HasRows;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="argUsername"></param>
        /// <returns>True if the username exists in the given parameters of <see cref="DatabasePathConfig"/>. False otherwise, or if an exception was thrown.</returns>
        public bool TryIfAccountUsernameExsists(string argUsername)
        {
            bool result = false;

            try
            {
                result = IfAccountUsernameExsists(argUsername);
            }
            catch (Exception ex)
            {

            }

            return result;
        }


        //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the account id exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfAccountIdExsists(int accountId)
        {

            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetId",
                        databasePathConfig.IdColumnName, databasePathConfig.AccountTableName);
                    command.Parameters.Add(new SqlParameter("TargetId", accountId));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result = reader.HasRows;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>True if the account id exists in the given parameters of <see cref="DatabasePathConfig"/>. False otherwise</returns>
        public bool TryIfAccountIdExists(int accountId)
        {
            try
            {
                return IfAccountIdExsists(accountId);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
