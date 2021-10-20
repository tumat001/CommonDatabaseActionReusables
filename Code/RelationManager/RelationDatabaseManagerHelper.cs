using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDatabaseActionReusables.RelationManager;
using CommonDatabaseActionReusables.RelationManager.Config;
using CommonDatabaseActionReusables.RelationManager.Actions;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.RelationManager
{
    public class RelationDatabaseManagerHelper
    {

        public RelationDatabasePathConfig PathConfig { set; get; }


        public RelationDatabaseManagerHelper(RelationDatabasePathConfig argPathConfig)
        {
            PathConfig = argPathConfig;
        }

        //

        #region "If Relation Exists"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if a relation between the <paramref name="primaryId"/> and <paramref name="targetId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfRelationExsists(int primaryId, int targetId)
        {
            return new RelationExistsAction(PathConfig).IfRelationExsists(primaryId, targetId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetId"></param>
        /// <returns>True if a relation between the <paramref name="primaryId"/> and <paramref name="targetId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfRelationExsists(int primaryId, int targetId)
        {
            return new RelationExistsAction(PathConfig).TryIfRelationExsists(primaryId, targetId);
        }

        #endregion

        //

        #region "Primary Id Exists"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if an <paramref name="primaryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfPrimaryExsists(int primaryId)
        {
            return new PrimaryExistsAction(PathConfig).IfPrimaryExsists(primaryId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>True if an <paramref name="primaryId"/> exists in the database in given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfPrimaryExists(int primaryId)
        {
            return new PrimaryExistsAction(PathConfig).TryIfPrimaryExists(primaryId);
        }

        #endregion

        //

        #region "Create relation action"

        /// <summary>
        /// Creates a relation with the <paramref name="primaryId"/> to a <paramref name="targetId"/> with the given parameters.<br></br>
        /// The relation is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="EntityToCategoryRelationAlreadyExistsException"></exception>
        /// <returns>True if the relation was created. False otherwise.</returns>
        public bool CreatePrimaryToTargetRelation(int primaryId, int targetId)
        {
            return new CreatePrimaryToTargetRelationAction(PathConfig).CreatePrimaryToTargetRelation(primaryId, targetId);
        }

        /// <summary>
        /// Creates a relation with the <paramref name="primaryId"/> to a <paramref name="targetId"/> with the given parameters.<br></br>
        /// The relation is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetId"></param>
        /// <returns>True if the relation was created. False otherwise.</returns>
        public bool TryCreatePrimaryToTargetRelation(int primaryId, int targetId)
        {
            return new CreatePrimaryToTargetRelationAction(PathConfig).TryCreatePrimaryToTargetRelation(primaryId, targetId);
        }

        #endregion

        //

        #region "Delete all relations"


        /// <summary>
        /// Deletes all relations.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no relation was deleted.</returns>
        public bool DeleteAllRelations()
        {
            return new DeleteAllRelationAction(PathConfig).DeleteAllRelations();
        }

        /// <summary>
        /// Deletes all relations.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <returns>True if the delete operation was successful, even if no relation was deleted.</returns>
        public bool TryDeleteAllRelations()
        {
            return new DeleteAllRelationAction(PathConfig).TryDeleteAllRelations();
        }


        #endregion

        //

        #region "Delete primary"

        /// <summary>
        /// Deletes the primary with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="EntityDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeletePrimaryWithId(int id)
        {
            return new DeletePrimaryAction(PathConfig).DeletePrimaryWithId(id);
        }

        /// <summary>
        /// Deletes the primary with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeletePrimaryWithId(int id)
        {
            return new DeletePrimaryAction(PathConfig).TryDeletePrimaryWithId(id);
        }

        #endregion

        //

        #region "Delete relation"

        /// <summary>
        /// Deletes the relation between then entity with the given <paramref name="entityId"/> and the category with the given param <paramref name="categoryId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="EntityToCategoryRelationDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteEntityCategoryRelation(int entityId, int categoryId)
        {
            return new DeleteEntityToCategoryRelationAction(PathConfig).DeleteEntityCategoryRelation(entityId, categoryId);
        }

        /// <summary>
        /// Deletes the relation between then entity with the given <paramref name="entityId"/> and the category with the given param <paramref name="categoryId"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="categoryId"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteEntityCategoryRelation(int entityId, int categoryId)
        {
            return new DeleteEntityToCategoryRelationAction(PathConfig).TryDeleteEntityCategoryRelation(entityId, categoryId);
        }

        #endregion

        //

        #region "Edit Relation"

        /// <summary>
        /// Edits the relation of the primary with the provided <paramref name="primaryId"/> and the target <paramref name="targetIdToReplace"/>.<br/>
        /// The <paramref name="newTargetId"/> replaces the <paramref name="targetIdToReplace"/>.<br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetIdToReplace"></param>
        /// <param name="newTargetId"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="EntityToCategoryRelationAlreadyExistsException"></exception>
        /// <exception cref="EntityToCategoryRelationDoesNotExistException"></exception>
        /// <returns>True if the relation was edited successfully. False otherwise.</returns>
        public bool EditRelation(int primaryId, int targetIdToReplace, int newTargetId)
        {
            return new EditRelationAction(PathConfig).EditRelation(primaryId, targetIdToReplace, newTargetId);
        }


        /// <summary>
        /// Edits the relation of the primary with the provided <paramref name="primaryId"/> and the target <paramref name="targetIdToReplace"/>.<br/>
        /// The <paramref name="newTargetId"/> replaces the <paramref name="targetIdToReplace"/>.<br/><br/>
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="targetIdToReplace"></param>
        /// <param name="newTargetId"></param>
        /// <returns>True if the relation was edited successfully. False otherwise.</returns>
        public bool TryEditRelation(int primaryId, int targetIdToReplace, int newTargetId)
        {
            return new EditRelationAction(PathConfig).TryEditRelation(primaryId, targetIdToReplace, newTargetId);
        }

        #endregion

        //

        #region "Advanced Get"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="EntityDoesNotExistException"></exception>
        /// <returns>A set of target ids with relation to the given <paramref name="primaryId"/> found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Ignores <see cref="AdvancedGetParameters.TextToContain"/>.</returns>
        public ISet<int> AdvancedGetRelationsOfPrimaryAsSet(int primaryId, AdvancedGetParameters adGetParameter)
        {
            return new AdvancedGetRelationsAction(PathConfig).AdvancedGetRelationsOfPrimaryAsSet(primaryId, adGetParameter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <returns>A set of category ids with relation to the given <paramref name="primaryId"/> found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Ignores <see cref="AdvancedGetParameters.TextToContain"/>.</returns>
        public ISet<int> TryAdvancedGetRelationsOfPrimaryAsSet(int primaryId, AdvancedGetParameters adGetParameter)
        {
            return new AdvancedGetRelationsAction(PathConfig).TryAdvancedGetRelationsOfPrimaryAsSet(primaryId, adGetParameter);
        }

        #endregion

    }
}
