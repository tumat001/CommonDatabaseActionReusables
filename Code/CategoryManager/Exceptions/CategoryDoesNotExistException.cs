using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.CategoryManager.Exceptions
{
    public class CategoryDoesNotExistException : ApplicationException
    {

        /// <summary>
        /// The name that does not exist. This is null if the query leading to this exception does not state the name.
        /// </summary>
        public string NonExistingCategoryName { get; }

        /// <summary>
        /// The id that does not exist. This is set to -1 if the query leading to this exception does not state the id.
        /// </summary>
        public int NonExistingCategoryId { get; }


        internal CategoryDoesNotExistException(string nonExistingCategoryName = null, int nonExistingCategoryId = -1)
        {
            NonExistingCategoryName = nonExistingCategoryName;
            NonExistingCategoryId = nonExistingCategoryId;
        }

    }
}
