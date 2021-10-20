using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.RelationManager.Exceptions
{
    public class RelationDoesNotExistException : ApplicationException
    {

        public int PrimaryId { get; }

        public int TargetId { get; }


        internal RelationDoesNotExistException(int primaryId, int targetId)
        {
            PrimaryId = primaryId;
            TargetId = targetId;
        }

    }
}
