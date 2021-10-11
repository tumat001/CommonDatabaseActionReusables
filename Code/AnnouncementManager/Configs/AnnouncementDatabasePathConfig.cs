using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;

namespace CommonDatabaseActionReusables.AnnouncementManager.Configs
{
    public class AnnouncementDatabasePathConfig : DatabasePathConfig
    {

        public string IdColumnName { get; }

        public string TitleColumnName { get; }

        public string ContentColumnName { get; }

        public string MainImageColumnName { get; }

        public string DateTimeCreatedColumnName { get; }

        public string DateTimeLastModifiedColumnName { get; }


        public string AnnouncementTableName { get; }


        public AnnouncementDatabasePathConfig(string connString, string idColName, string titleColName, string contentColName,
            string mainImageColName, string dateTimeCreatedColName, string dateTimeLastModifiedColName, string announcementTableName) : base(connString)
        {
            IdColumnName = idColName;
            TitleColumnName = titleColName;
            ContentColumnName = contentColName;
            MainImageColumnName = mainImageColName;
            DateTimeCreatedColumnName = dateTimeCreatedColName;
            DateTimeLastModifiedColumnName = dateTimeLastModifiedColName;
            AnnouncementTableName = announcementTableName;
        }


    }
}