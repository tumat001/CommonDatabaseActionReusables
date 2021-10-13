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
    public class EditCategoryAction : AbstractAction<CategoryDatabasePathConfig>
    {

        internal EditCategoryAction(CategoryDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// Edits the category with the provided <paramref name="id"/> using the properties found in <paramref name="builder"/>.<br/>
        /// Setting <paramref name="builder"/> to null makes no edits to the category.<br/><br/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="builder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="CategoryAlreadyExistsException"></exception>
        /// <exception cref="CategoryDoesNotExistException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>True if the category was edited successfully, even if <paramref name="builder"/> is set to null.</returns>
        public bool EditCategory(int id, Category.Builder builder)
        {

            if (builder == null)
            {
                return true;
            }

            bool nameExists = new CategoryExistsAction(databasePathConfig).IfCategoryNameExsists(builder.Name);
            if (nameExists)
            {
                throw new CategoryAlreadyExistsException(builder.Name);
            }


            bool idExists = new CategoryExistsAction(databasePathConfig).IfCategoryIdExsists(id);
            if (!idExists)
            {
                throw new CategoryDoesNotExistException(null, id);
            }

            


            var inputConstraintsChecker = new AntiSQLInjectionInputConstraint();
            inputConstraintsChecker.SatisfiesConstraint(builder.Name);

            //


            var success = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("UPDATE [{0}] SET [{1}] = @NewNameVal WHERE [{2}] = @IdVal",
                        databasePathConfig.CategoryTableName,
                        databasePathConfig.NameColumnName, databasePathConfig.IdColumnName);
                    command.Parameters.Add(new SqlParameter("NewNameVal", builder.Name));
                    command.Parameters.Add(new SqlParameter("IdVal", id));

                    success = command.ExecuteNonQuery() > 0;
                }
            }

            return success;

        }



        /// <summary>
        /// Edits the category with the provided <paramref name="id"/> using the properties found in <paramref name="builder"/>.<br/>
        /// Setting <paramref name="builder"/> to null makes no edits to the category.<br/><br/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="builder"></param>
        /// <returns>True if the category was edited successfully, even if <paramref name="builder"/> is set to null.</returns>
        public bool TryEditCategory(int id, Category.Builder builder)
        {
            try
            {
                return EditCategory(id, builder);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
