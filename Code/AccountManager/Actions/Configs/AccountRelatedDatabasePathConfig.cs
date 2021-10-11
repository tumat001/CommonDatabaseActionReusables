using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonDatabaseActionReusables.AccountManager.Actions.Configs
{
    public class AccountRelatedDatabasePathConfig : DatabasePathConfig
    {

        public string IdColumnName { get; }
        public string UsernameColumnName { get; }
        public string PasswordColumnName { get; }
        public string EmailColumnName { get; }

        public string DisabledFromLogInColumnName { get; }
        public string AccountTypeColumnName { get; }

        public string AccountTableName { get; }


        public AccountRelatedDatabasePathConfig(string connString, string idColumnName, string usernameColumnName,
            string passwordColumnName, string disabledFromLogInColName, string emailColumName,
            string accountTypeColumnName, string accountTableName) : base(connString)
        {
            IdColumnName = idColumnName;
            UsernameColumnName = usernameColumnName;
            PasswordColumnName = passwordColumnName;
            DisabledFromLogInColumnName = disabledFromLogInColName;
            EmailColumnName = emailColumName;
            AccountTypeColumnName = accountTypeColumnName;

            AccountTableName = accountTableName;
        }




    }
}
