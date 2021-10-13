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
    public class CategoryExistsAction : AbstractAction<CategoryDatabasePathConfig>
    {

        internal CategoryExistsAction(CategoryDatabasePathConfig config) : base(config)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the category id exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfCategoryIdExsists(int id)
        {
            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{0}] = @TargetId",
                        databasePathConfig.IdColumnName, databasePathConfig.CategoryTableName);
                    command.Parameters.Add(new SqlParameter("TargetId", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result = reader.HasRows;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the category id exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfCategoryIdExsists(int id)
        {
            try
            {
                return IfCategoryIdExsists(id);
            }
            catch (Exception)
            {
                return false;
            }
        }


        //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>True if the category name exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfCategoryNameExsists(string name)
        {
            var inputConstraintsChecker = new AntiSQLInjectionInputConstraint();
            inputConstraintsChecker.SatisfiesConstraint(name);

            //


            bool result = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT [{0}] FROM [{1}] WHERE [{2}] = @TargetName",
                        databasePathConfig.IdColumnName, databasePathConfig.CategoryTableName, databasePathConfig.NameColumnName);
                    command.Parameters.Add(new SqlParameter("TargetName", name));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result = reader.HasRows;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the category name exists in the given parameters of <see cref="DatabasePathConfig"/>. False otherwise.</returns>
        public bool TryIfCategoryNameExsists(string name)
        {
            try
            {
                return IfCategoryNameExsists(name);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
