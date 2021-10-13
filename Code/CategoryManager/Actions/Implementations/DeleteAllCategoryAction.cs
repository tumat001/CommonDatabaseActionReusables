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

namespace CommonDatabaseActionReusables.CategoryManager.Actions
{
    public class DeleteAllCategoryAction : AbstractAction<CategoryDatabasePathConfig>
    {

        internal DeleteAllCategoryAction(CategoryDatabasePathConfig config) : base(config)
        {

        }

        /// <summary>
        /// Deletes all categories.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no category was deleted.</returns>
        public bool DeleteAllCategories()
        {

            bool isSuccessful = true;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}]",
                        databasePathConfig.CategoryTableName);

                    command.ExecuteNonQuery();

                }
            }

            return isSuccessful;
        }


        /// <summary>
        /// Deletes all categories.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <returns>True if the delete operation was successful, even if no category was deleted.</returns>
        public bool TryDeleteAllCategories()
        {
            try
            {
                return DeleteAllCategories();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
