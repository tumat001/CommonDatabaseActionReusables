using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDatabaseActionReusables.EntityToCategoryManager;
using CommonDatabaseActionReusables.EntityToCategoryManager.Config;
using CommonDatabaseActionReusables.EntityToCategoryManager.Actions;

namespace CommonDatabaseActionReusables.EntityToCategoryManager
{
    public class EntityToCategoryDatabaseManagerHelper
    {

        public EntityToCategoryDatabasePathConfig PathConfig { set; get; }


        public EntityToCategoryDatabaseManagerHelper(EntityToCategoryDatabasePathConfig argPathConfig)
        {
            PathConfig = argPathConfig;
        }

        //

        #region "If Entity to Category Relation Exists"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if a relation between the <paramref name="entityId"/> and <paramref name="categoryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfEntityToCategoryRelationExsists(int entityId, int categoryId)
        {
            return new EntityToCategoryRelationExistsAction(PathConfig).IfEntityToCategoryRelationExsists(entityId, categoryId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <returns>True if a relation between the <paramref name="entityId"/> and <paramref name="categoryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfEntityToCategoryRelationExsists(int entityId, int categoryId)
        {
            return new EntityToCategoryRelationExistsAction(PathConfig).TryIfEntityToCategoryRelationExsists(entityId, categoryId);
        }

        #endregion

        //

        #region "Entity Id Exists"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if an <paramref name="entityId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfEntityExsists(int entityId)
        {
            return new EntityExistsAction(PathConfig).IfEntityExsists(entityId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>True if an <paramref name="entityId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfEntityExsists(int entityId)
        {
            return new EntityExistsAction(PathConfig).TryIfEntityExsists(entityId);
        }

        #endregion

        //



    }
}
