using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;

namespace CommonDatabaseActionReusables.RelationManager.Config
{
    public class RelationDatabasePathConfig : DatabasePathConfig
    {

        public string EntityIdColumnName { get; }

        public string CategoryIdColumnName { get; }


        public string EntityToCategoryTableName { get; }


        public RelationDatabasePathConfig(string connString, string entityIdColName, string catIdColName, string entityToCatTableName) : base(connString)
        {
            EntityIdColumnName = entityIdColName;
            CategoryIdColumnName = catIdColName;
            EntityToCategoryTableName = entityToCatTableName;
        }

    }
}
