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
    public class GetCategoryAction : AbstractAction<CategoryDatabasePathConfig>
    {

        internal GetCategoryAction(CategoryDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>An <see cref="Category"/> object containing information about the category with the provided <paramref name="id"/>.</returns>
        public Category GetCategoryInfoFromId(int id)
        {

            //

            bool idExists = new CategoryExistsAction(databasePathConfig).IfCategoryIdExsists(id);
            if (!idExists)
            {
                throw new CategoryDoesNotExistException(null, id);
            }

            //

            var builder = new Category.Builder();
            Category category = null;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{2}] = @IdVal",
                        databasePathConfig.NameColumnName, databasePathConfig.CategoryTableName, databasePathConfig.IdColumnName);
                    command.Parameters.Add(new SqlParameter("IdVal", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            builder.Name = reader.GetSqlString(0).ToString();

                            category = builder.Build(id);
                        }
                    }
                }
            }


            return category;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="Category"/> object containing information about the category with the provided <paramref name="id"/>, or null if an error occurs.</returns>
        public Category TryGetCategoryInfoFromId(int id)
        {
            try
            {
                return GetCategoryInfoFromId(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
