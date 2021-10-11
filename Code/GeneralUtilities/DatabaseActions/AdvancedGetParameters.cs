using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions
{
    public class AdvancedGetParameters
    {

        public int Offset { set; get; }

        public int Fetch { set; get; }

        public string TextToContain { set; get; }

        public AdvancedGetParameters()
        {
            Offset = 0;
            Fetch = 0;
            TextToContain = null;
        }


        public string GetSQLStatementFromOffset()
        {
            if (Offset >= 0)
            {
                return String.Format("OFFSET {0} ROWS", Offset);
            }
            else
            {
                return "";
            }
        }

        public string GetSQLStatementFromFetch()
        {
            if (Fetch > 0)
            {
                return String.Format("FETCH FIRST {0} ROWS ONLY", Fetch);
            }
            else
            {
                return "";
            }
        }

        public string GetSQLStatementFromTextToContain(string colName)
        {
            if (TextToContain != null)
            {
                return String.Format("WHERE [{0}] LIKE '%{1}%'", colName, TextToContain);
            }
            else
            {
                return "";
            }
        }

    }
}