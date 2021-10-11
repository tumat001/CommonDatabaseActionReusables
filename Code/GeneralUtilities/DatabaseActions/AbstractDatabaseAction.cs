using System;
using System.Collections.Generic;
using System.Text;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;

namespace CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions
{
    public class AbstractAction<T> where T : DatabasePathConfig
    {

        protected T databasePathConfig;

        protected AbstractAction(T pathConfig)
        {
            databasePathConfig = pathConfig;
        }


        protected object GetParamOrDbNullIfParamIsNull(object val)
        {
            if (val == null)
            {
                return DBNull.Value;
            }
            else
            {
                return val;
            }
        }

        protected T GetParamOrNullIfParamIsDbNull(object val, T type)
        {
            if (val == DBNull.Value)
            {
                return null;
            }
            else
            {
                return (T) val;
            }
        }

    }
}
