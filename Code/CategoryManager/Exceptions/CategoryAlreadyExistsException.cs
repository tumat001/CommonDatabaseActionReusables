using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.CategoryManager.Exceptions
{
    public class CategoryAlreadyExistsException : ApplicationException
    {

        /// <summary>
        /// The name that already exists. This is null if the query leading to this exception does not state the name.
        /// </summary>
        public string ExistingCategoryName { get; }

        /// <summary>
        /// The id that already exists. This is set to -1 if the query leading to this exception does not state the id.
        /// </summary>
        public int ExistingCategoryId { get; }

        internal CategoryAlreadyExistsException(string existingName, int existingCategoryId = -1)
        {
            ExistingCategoryId = existingCategoryId;
            ExistingCategoryName = existingName;
        }

    }
}
