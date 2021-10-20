using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.AccountManager.Actions.Exceptions
{
    public class AccountDoesNotExistException : ApplicationException
    {

        /// <summary>
        /// The username that does not exist. This is null if the query leading to this exception does not state the username.
        /// </summary>
        public string NonExistingAccountUsername { get; }

        /// <summary>
        /// The id that does not exist. This is set to -1 if the query leading to this exception does not state the id.
        /// </summary>
        public int NonExistingAccountId { get; }


        internal AccountDoesNotExistException(string nonExistingUsername = null, int nonExistingId = -1)
        {
            NonExistingAccountUsername = nonExistingUsername;
            NonExistingAccountId = nonExistingId;
        }

    }
}