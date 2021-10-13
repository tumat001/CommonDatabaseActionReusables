using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.EntityToCategoryManager.Exceptions
{
    internal class EntityToCategoryRelationAlreadyExistsException : ApplicationException
    {

        public int EntityId { get; }

        public int CategoryId { get; }


        internal EntityToCategoryRelationAlreadyExistsException(int entityId, int categoryId)
        {
            EntityId = entityId;
            CategoryId = categoryId;
        }

    }
}
