using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;
using CommonDatabaseActionReusables.CategoryManager.Config;
using CommonDatabaseActionReusables.CategoryManager.Exceptions;


namespace CommonDatabaseActionReusables.CategoryManager.Actions
{
    public class DeleteCategoryAction : AbstractAction<CategoryDatabasePathConfig>
    {
        internal DeleteCategoryAction(CategoryDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// Deletes the category with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="CategoryDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteCategoryWithId(int id)
        {
            bool idExists = new CategoryExistsAction(databasePathConfig).IfCategoryIdExsists(id);
            if (!idExists)
            {
                throw new CategoryDoesNotExistException(null, id);
            }

            //


            bool isSuccessful = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM [{0}] WHERE [{1}] = @Id",
                        databasePathConfig.CategoryTableName, databasePathConfig.IdColumnName);
                    command.Parameters.Add(new SqlParameter("Id", id));

                    isSuccessful = command.ExecuteNonQuery() > 0;

                }
            }

            return isSuccessful;
        }


        /// <summary>
        /// Deletes the category with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteCategoryWithId(int id)
        {
            try
            {
                return DeleteCategoryWithId(id);
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
