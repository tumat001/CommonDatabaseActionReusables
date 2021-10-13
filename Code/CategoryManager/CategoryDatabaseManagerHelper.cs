using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDatabaseActionReusables.CategoryManager.Config;
using CommonDatabaseActionReusables.CategoryManager.Actions;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.CategoryManager
{
    public class CategoryDatabaseManagerHelper
    {

        public CategoryDatabasePathConfig PathConfig { set; get; }


        public CategoryDatabaseManagerHelper(CategoryDatabasePathConfig argPathConfig)
        {
            PathConfig = argPathConfig;
        }


        #region "If Category Exists"

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
            return new CategoryExistsAction(PathConfig).IfCategoryIdExsists(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the category id exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool TryIfCategoryIdExsists(int id)
        {
            return new CategoryExistsAction(PathConfig).TryIfCategoryIdExsists(id);
        }



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
            return new CategoryExistsAction(PathConfig).IfCategoryNameExsists(name);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the category name exists in the given parameters of <see cref="DatabasePathConfig"/>. False otherwise.</returns>
        public bool TryIfCategoryNameExsists(string name)
        {
            return new CategoryExistsAction(PathConfig).TryIfCategoryNameExsists(name);
        }


        #endregion

        //

        #region "Create Category"

        /// <summary>
        /// Creates an category with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The category is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <exception cref="CategoryAlreadyExistsException"></exception>
        /// <returns>The Id corresponding to the created category.</returns>
        public int CreateCategory(Category.Builder builder)
        {
            return new CreateCategoryAction(PathConfig).CreateCategory(builder);
        }


        /// <summary>
        /// Creates an category with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The category is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>The Id corresponding to the created category, or -1 if the creation has failed.</returns>
        public int TryCreateCategory(Category.Builder builder)
        {
            return new CreateCategoryAction(PathConfig).TryCreateCategory(builder);
        }


        #endregion

        //

        #region "Delete All Category"

        /// <summary>
        /// Deletes all categories.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no category was deleted.</returns>
        public bool DeleteAllCategories()
        {
            return new DeleteAllCategoryAction(PathConfig).DeleteAllCategories();
        }


        /// <summary>
        /// Deletes all categories.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <returns>True if the delete operation was successful, even if no category was deleted.</returns>
        public bool TryDeleteAllCategories()
        {
            return new DeleteAllCategoryAction(PathConfig).TryDeleteAllCategories();
        }

        #endregion

        //

        #region "Edit Category"

        /// <summary>
        /// Edits the category with the provided <paramref name="id"/> using the properties found in <paramref name="builder"/>.<br/>
        /// Setting <paramref name="builder"/> to null makes no edits to the category.<br/><br/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="builder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="CategoryAlreadyExistsException"></exception>
        /// <exception cref="CategoryDoesNotExistException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>True if the category was edited successfully, even if <paramref name="builder"/> is set to null.</returns>
        public bool EditCategory(int id, Category.Builder builder)
        {
            return new EditCategoryAction(PathConfig).EditCategory(id, builder);
        }


        /// <summary>
        /// Edits the category with the provided <paramref name="id"/> using the properties found in <paramref name="builder"/>.<br/>
        /// Setting <paramref name="builder"/> to null makes no edits to the category.<br/><br/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="builder"></param>
        /// <returns>True if the category was edited successfully, even if <paramref name="builder"/> is set to null.</returns>
        public bool TryEditCategory(int id, Category.Builder builder)
        {
            return new EditCategoryAction(PathConfig).TryEditCategory(id, builder);
        }

        #endregion

        //

        #region "Get Category"

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
            return new GetCategoryAction(PathConfig).GetCategoryInfoFromId(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="Category"/> object containing information about the category with the provided <paramref name="id"/>, or null if an error occurs.</returns>
        public Category TryGetCategoryInfoFromId(int id)
        {
            return new GetCategoryAction(PathConfig).TryGetCategoryInfoFromId(id);
        }

        #endregion

        //

        #region "Advanced Get Category"

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
            return new AdvancedGetCategoryAction(PathConfig).AdvancedGetCategoriesAsList(adGetParameter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <returns>A list of categories found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Returns an empty list if an error has occurred.</returns>
        public IReadOnlyList<Category> TryAdvancedGetCategoriesAsList(AdvancedGetParameters adGetParameter)
        {
            return new AdvancedGetCategoryAction(PathConfig).TryAdvancedGetCategoriesAsList(adGetParameter);
        }

        #endregion

        //

        #region "Delete Category"

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
            return new DeleteCategoryAction(PathConfig).DeleteCategoryWithId(id);
        }


        /// <summary>
        /// Deletes the category with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteCategoryWithId(int id)
        {
            return new DeleteCategoryAction(PathConfig).TryDeleteCategoryWithId(id);
        }

        #endregion

    }
}
