using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.RelationManager.Exceptions
{
    public class PrimaryDoesNotExistException : ApplicationException
    {

        public int PrimaryId { get; }

        internal PrimaryDoesNotExistException(int primaryId)
        {
            PrimaryId = primaryId;
        }

    }
}
