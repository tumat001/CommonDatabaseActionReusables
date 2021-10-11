using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AnnouncementManager.Configs;
using CommonDatabaseActionReusables.AnnouncementManager.Actions;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;

namespace CommonDatabaseActionReusables.AnnouncementManager
{
    public class AnnouncementDatabaseManagerHelper
    {

        public AnnouncementDatabasePathConfig PathConfig { set; get; }


        public AnnouncementDatabaseManagerHelper(AnnouncementDatabasePathConfig argPathConfig)
        {
            PathConfig = argPathConfig;
        }

        //

        #region "IfAnnouncementExists"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the announcement id exists in the given parameters of <see cref="DatabasePathConfig"/></returns>
        public bool IfAnnouncementIdExsists(int id)
        {
            return new AnnouncementExistsAction(PathConfig).IfAnnouncementIdExsists(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the announcement id exists in the given parameters of <see cref="DatabasePathConfig"/>, false otherwise.</returns>
        public bool TryIfAnnouncementIdExsists(int id)
        {
            return new AnnouncementExistsAction(PathConfig).TryIfAnnouncementIdExsists(id);
        }

        #endregion

        //

        #region "Create Announcement"

        /// <summary>
        /// Creates an announcement with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The announcement is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// The <paramref name="builder"/>'s <see cref="Announcement.Builder.DateTimeCreated"/> and <see cref="Announcement.Builder.DateTimeLastModified"/> is set to the current time.
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>The Id corresponding to the created announcement.</returns>
        public int CreateAnnouncement(Announcement.Builder builder)
        {
            return new CreateAnnouncementAction(PathConfig).CreateAnnouncement(builder);
        }

        /// <summary>
        /// Creates an announcement with the given parameters in the given <paramref name="builder"/>.<br></br>
        /// The announcement is created in the database and table specified in this object's <see cref="DatabasePathConfig"/><br/><br/>
        /// The <paramref name="builder"/>'s <see cref="Announcement.Builder.DateTimeCreated"/> and <see cref="Announcement.Builder.DateTimeLastModified"/> is set to the current time.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>The Id corresponding to the created announcement. -1 is returned when an exception occurred.</returns>
        public int TryCreateAnnouncement(Announcement.Builder builder)
        {
            return new CreateAnnouncementAction(PathConfig).TryCreateAnnouncement(builder);
        }

        #endregion

        //

        #region "Delete All Announcements"

        /// <summary>
        /// Deletes all announcements.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>True if the delete operation was successful, even if no announcment was deleted.</returns>
        public bool DeleteAllAnnouncements()
        {
            return new DeleteAllAnnouncementAction(PathConfig).DeleteAllAnnouncements();
        }

        /// <summary>
        /// Deletes all announcements.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <returns>True if the delete operation was successful, even if no announcment was deleted.</returns>
        public bool TryDeleteAllAnnouncements()
        {
            return new DeleteAllAnnouncementAction(PathConfig).TryDeleteAllAnnouncements();
        }

        #endregion

        //

        #region "Get Announcement"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>An <see cref="Announcement"/> object containing information about the announcement with the provided <paramref name="id"/>.</returns>
        public Announcement GetAnnouncementInfoFromId(int id)
        {
            return new GetAnnouncementAction(PathConfig).GetAnnouncementInfoFromId(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="Announcement"/> object containing information about the announcement with the provided <paramref name="id"/>.</returns>
        public Announcement TryGetAnnouncementInfoFromId(int id)
        {
            return new GetAnnouncementAction(PathConfig).TryGetAnnouncementInfoFromId(id);
        }

        #endregion

        //

        #region "Advanced Get Announcements

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>.</returns>
        public IReadOnlyList<Announcement> AdvancedGetAnnouncementsAsList(AdvancedGetParameters adGetParameter)
        {
            return new AdvancedGetAnnouncementsAction(PathConfig).AdvancedGetAnnouncementsAsList(adGetParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Returns an empty list when an exception occurs.</returns>
        public IReadOnlyList<Announcement> TryAdvancedGetAnnouncementsAsList(AdvancedGetParameters adGetParameter)
        {
            return new AdvancedGetAnnouncementsAction(PathConfig).TryAdvancedGetAnnouncementsAsList(adGetParameter);
        }

        #endregion

        //

        #region "Get All Announcements"

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/></returns>
        public IReadOnlyList<Announcement> GetAllAnnouncementsAsList()
        {
            return new GetAllAnnouncementAction(PathConfig).GetAllAnnouncementsAsList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of announcements found in the database given in this object's <see cref="DatabasePathConfig"/>. Returns an empty list if an exception occurs.</returns>
        public IReadOnlyList<Announcement> TryGetAllAnnouncementsAsList()
        {
            return new GetAllAnnouncementAction(PathConfig).TryGetAllAnnouncementsAsList();
        }

        #endregion

        //

        #region "Edit Announcement"

        /// <summary>
        /// Edits the announcement with the provided <paramref name="id"/> using the properties found in <paramref name="builder"/>.<br/>
        /// Setting <paramref name="builder"/> to null makes no edits to the announcement.<br/><br/>
        /// Set's the <paramref name="builder"/>'s <see cref="Announcement.Builder.DateTimeLastModified"/> to the current time. Ignores the builder's <see cref="Announcement.Builder.DateTimeCreated"/> field.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="builder"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AnnouncementDoesNotExistException"></exception>
        /// <exception cref="InputStringConstraintsViolatedException"></exception>
        /// <returns>True if the announcement was edited successfully, even if <paramref name="builder"/> is set to null.</returns>
        public bool EditAnnouncement(int id, Announcement.Builder builder)
        {
            return new EditAnnouncementAction(PathConfig).EditAnnouncement(id, builder);
        }


        /// <summary>
        /// Edits the announcement with the provided <paramref name="id"/> using the properties found in <paramref name="builder"/>.<br/>
        /// Setting <paramref name="builder"/> to null makes no edits to the announcement.<br/><br/>
        /// Set's the <paramref name="builder"/>'s <see cref="Announcement.Builder.DateTimeLastModified"/> to the current time. Ignores the builder's <see cref="Announcement.Builder.DateTimeCreated"/> field.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="builder"></param>
        /// <returns>True if the announcement was edited successfully, even if <paramref name="builder"/> is set to null. Otherwise false.</returns>
        public bool TryEditAnnouncement(int id, Announcement.Builder builder)
        {
            return new EditAnnouncementAction(PathConfig).TryEditAnnouncement(id, builder);
        }

        #endregion

        //

        #region "Delete Announcement

        /// <summary>
        /// Deletes the announcement with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="AnnouncementDoesNotExistException"></exception>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool DeleteAnnouncementWithId(int id)
        {
            return new DeleteAnnouncementAction(PathConfig).DeleteAnnouncementWithId(id);
        }


        /// <summary>
        /// Deletes the announcement with the given <paramref name="id"/>.<br/>
        /// This object's <see cref="DatabasePathConfig"/> determines which database and table is affected.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the delete operation was successful. False otherwise.</returns>
        public bool TryDeleteAnnouncementWithId(int id)
        {
            return new DeleteAnnouncementAction(PathConfig).TryDeleteAnnouncementWithId(id);
        }

        #endregion


    }
}