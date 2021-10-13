using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.CategoryManager.Config;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.CategoryManager.Exceptions;

namespace CommonDatabaseActionReusables.CategoryManager.Actions
{
    public class CreateCategoryAction : AbstractAction<CategoryDatabasePathConfig>
    {

        internal CreateCategoryAction(CategoryDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// Creates an category with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The category is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="CategoryAlreadyExistsException"></exception>
        /// <returns>The Id corresponding to the created category.</returns>
        public int CreateCategory(Category.Builder builder)
        {
            var inputConstraintsChecker = new AntiSQLInjectionInputConstraint();
            inputConstraintsChecker.SatisfiesConstraint(builder.Name);


            var categoryNameExists = new CategoryExistsAction(databasePathConfig).TryIfCategoryNameExsists(builder.Name);
            if (categoryNameExists)
            {
                throw new CategoryAlreadyExistsException(builder.Name);
            }

            //

            int categoryId = -1;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("INSERT INTO [{0}] ({1}) VALUES (@Name); SELECT SCOPE_IDENTITY()",
                        databasePathConfig.CategoryTableName,
                        databasePathConfig.NameColumnName);

                    command.Parameters.Add(new SqlParameter("Name", builder.Name));

                    categoryId = int.Parse(command.ExecuteScalar().ToString());
                }
            }

            return categoryId;
        }


        /// <summary>
        /// Creates an category with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The category is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>The Id corresponding to the created category, or -1 if the creation has failed.</returns>
        public int TryCreateCategory(Category.Builder builder)
        {
            try
            {
                return CreateCategory(builder);
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
