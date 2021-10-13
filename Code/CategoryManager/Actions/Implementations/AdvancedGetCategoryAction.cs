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

namespace CommonDatabaseActionReusables.CategoryManager.Actions
{
    class AdvancedGetCategoryAction : AbstractAction<CategoryDatabasePathConfig>
    {

        internal AdvancedGetCategoryAction(CategoryDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns>A list of categories found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>.</returns>
        public IReadOnlyList<Category> AdvancedGetCategoriesAsList(AdvancedGetParameters adGetParameter)
        {

            var list = new List<Category>();
            var builder = new Category.Builder();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT * FROM (SELECT [{0}], [{1}] FROM [{2}] ORDER BY {0} {3} {4}) T {5}",
                        databasePathConfig.IdColumnName, databasePathConfig.NameColumnName, databasePathConfig.CategoryTableName,
                        adGetParameter.GetSQLStatementFromOffset(),
                        adGetParameter.GetSQLStatementFromFetch(),
                        adGetParameter.GetSQLStatementFromTextToContain(databasePathConfig.NameColumnName));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            builder.Name = reader.GetSqlString(1).ToString();
                            
                            var id = reader.GetSqlInt32(0).Value;

                            list.Add(builder.Build(id));
                        }
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <returns>A list of categories found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Returns an empty list if an error has occurred.</returns>
        public IReadOnlyList<Category> TryAdvancedGetCategoriesAsList(AdvancedGetParameters adGetParameter)
        {
            try
            {
                return AdvancedGetCategoriesAsList(adGetParameter);
            }
            catch (Exception)
            {
                return new List<Category>();
            }
        }

    }
}
