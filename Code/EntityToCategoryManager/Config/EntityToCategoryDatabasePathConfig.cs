using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;

namespace CommonDatabaseActionReusables.EntityToCategoryManager.Config
{
    public class EntityToCategoryDatabasePathConfig : DatabasePathConfig
    {

        public string EntityIdColumnName { get; }

        public string CategoryIdColumnName { get; }


        public string EntityToCategoryTableName { get; }


        public EntityToCategoryDatabasePathConfig(string connString, string entityIdColName, string catIdColName, string entityToCatTableName) : base(connString)
        {
            EntityIdColumnName = entityIdColName;
            CategoryIdColumnName = catIdColName;
            EntityToCategoryTableName = entityToCatTableName;
        }

    }
}
