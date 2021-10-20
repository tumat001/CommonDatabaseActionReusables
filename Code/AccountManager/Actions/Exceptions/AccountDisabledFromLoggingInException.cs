using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.AccountManager.Actions.Exceptions
{
    public class AccountDisabledFromLoggingInException : ApplicationException
    {

        /// <summary>
        /// The username that is disabed from logging in. This is null if the query leading to this exception does not state the username.
        /// </summary>
        public string AccountUsername { get; }

        /// <summary>
        /// The id that is disabed from logging in. This is set to -1 if the query leading to this exception does not state the id.
        /// </summary>
        public int AccountId { get; }

        internal AccountDisabledFromLoggingInException(string accUsername, int accId = -1)
        {
            AccountId = accId;
            AccountUsername = accUsername;
        }

    }
}