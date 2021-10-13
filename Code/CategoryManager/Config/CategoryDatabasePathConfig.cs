using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;

namespace CommonDatabaseActionReusables.CategoryManager.Config
{
    public class CategoryDatabasePathConfig : DatabasePathConfig
    {

        public string IdColumnName { get; }

        public string NameColumnName { get; }


        public string CategoryTableName { get; }


        public CategoryDatabasePathConfig(string connString, string idColName, string nameColName, string categoryTableName) : base(connString)
        {
            IdColumnName = idColName;
            NameColumnName = nameColName;
            CategoryTableName = categoryTableName;
        }
    }
}
