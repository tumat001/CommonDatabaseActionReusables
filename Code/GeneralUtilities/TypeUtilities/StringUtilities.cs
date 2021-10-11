using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlTypes;

namespace CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities
{
    public class StringUtilities
    {


        public static string ConvertByteArrayToString(byte[] byteArr)
        {
            if (byteArr != null)
            {
                return Encoding.Unicode.GetString(byteArr);
            }
            else
            {
                return null;
            }
        }


        public static byte[] ConvertStringToByteArray(string text)
        {
            if (text != null)
            {
                return Encoding.Unicode.GetBytes(text);
            }
            else
            {
                return null;
            }
        }

        //

        public static byte[] ConvertSqlStringToByteArray(SqlString sqlString)
        {
            if (!sqlString.IsNull)
            {
                return ConvertStringToByteArray(sqlString.Value);
            }
            else
            {
                return null;
            }
        }

        public static string ConvertSqlStringToString(SqlString sqlString)
        {
            if (!sqlString.IsNull)
            {
                return sqlString.Value;
            }
            else
            {
                return null;
            }
        }

        //

        public static object ConvertStringToSqlString(string text)
        {
            if (text != null)
            {
                return text;
            }
            else
            {
                return DBNull.Value;
            }
        }

    }
}