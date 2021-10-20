using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.RelationManager.Exceptions
{
    public class RelationAlreadyExistsException : ApplicationException
    {

        public int PrimaryId { get; }

        public int TargetId { get; }


        internal RelationAlreadyExistsException(int primaryId, int targetId)
        {
            PrimaryId = primaryId;
            TargetId = targetId;
        }

    }
}
