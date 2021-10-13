using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.AnnouncementManager.Configs;
using CommonDatabaseActionReusables.AnnouncementManager.Exceptions;
using CommonDatabaseActionReusables.AnnouncementManager;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.GeneralUtilities.InputConstraints;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;

namespace CommonDatabaseActionReusables.AnnouncementManager.Actions
{
    public class EditAnnouncementAction : AbstractAction<AnnouncementDatabasePathConfig>
    {

        internal EditAnnouncementAction(AnnouncementDatabasePathConfig config) : base(config)
        {

        }


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
            bool idExists = new AnnouncementExistsAction(databasePathConfig).IfAnnouncementIdExsists(id);
            if (!idExists)
            {
                throw new AnnouncementDoesNotExistException(id);
            }

            if (builder == null)
            {
                return true;
            }


            var inputConstraintsChecker = new AntiSQLInjectionInputConstraint();
            inputConstraintsChecker.SatisfiesConstraint(builder.Title);

            //


            var success = false;

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    builder.DateTimeLastModified = DateTime.Now;

                    command.CommandText = String.Format("UPDATE [{0}] SET [{1}] = @NewTitleVal, [{2}] = @NewContentVal, [{3}] = @NewMainImageVal, [{4}] = @NewDateLastModified WHERE [{5}] = @IdVal",
                        databasePathConfig.AnnouncementTableName, 
                        databasePathConfig.TitleColumnName, databasePathConfig.ContentColumnName, databasePathConfig.MainImageColumnName,
                        databasePathConfig.DateTimeLastModifiedColumnName, databasePathConfig.IdColumnName);
                    command.Parameters.Add(new SqlParameter("NewTitleVal", builder.Title));
                    command.Parameters.Add(new SqlParameter("NewContentVal", GetParamOrDbNullIfParamIsNull(builder.Content)));
                    command.Parameters.Add(new SqlParameter("NewMainImageVal", GetParamOrDbNullIfParamIsNull(builder.MainImage)));
                    command.Parameters.Add(new SqlParameter("NewDateLastModified", builder.DateTimeLastModified));
                    command.Parameters.Add(new SqlParameter("IdVal", id));

                    success = command.ExecuteNonQuery() > 0;
                }
            }

            return success;

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
            try
            {
                return EditAnnouncement(id, builder);
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}