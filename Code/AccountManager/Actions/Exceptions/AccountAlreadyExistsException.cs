using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.AccountManager.Actions.Exceptions
{
    public class AccountAlreadyExistsException : ApplicationException
    {

        /// <summary>
        /// The username that already exists. This is null if the query leading to this exception does not state the username.
        /// </summary>
        public string ExistingAccountUsername { get; }

        /// <summary>
        /// The id that already exists. This is set to -1 if the query leading to this exception does not state the id.
        /// </summary>
        public int ExistingAccountId { get; }

        internal AccountAlreadyExistsException(string existingAccUsername, int existingAccId = -1)
        {
            ExistingAccountUsername = existingAccUsername;
            ExistingAccountId = existingAccId;
        }

    }
}