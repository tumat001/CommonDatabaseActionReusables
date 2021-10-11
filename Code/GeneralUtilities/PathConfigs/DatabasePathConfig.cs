using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CommonDatabaseActionReusables.GeneralUtilities.PathConfig
{


    public class DatabasePathConfig
    {

        public string connString { set; get; }

        public DatabasePathConfig(string connString)
        {
            this.connString = connString;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>An SQL Connection with the connection string specified in this object's <see cref="connString"/></returns>
        public SqlConnection GetSQLConnection()
        {
            var sqlConn = new SqlConnection(connString);
            return sqlConn;
        }

    }
}
