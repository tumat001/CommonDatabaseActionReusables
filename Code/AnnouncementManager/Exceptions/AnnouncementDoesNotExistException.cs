using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.AnnouncementManager.Exceptions
{
    public class AnnouncementDoesNotExistException : ApplicationException
    {

        /// <summary>
        /// The id that does not exist.
        /// </summary>
        public int NonExistingAnnouncementId { get; }

        public AnnouncementDoesNotExistException(int nonExistingId)
        {
            NonExistingAnnouncementId = nonExistingId;
        }

    }
}