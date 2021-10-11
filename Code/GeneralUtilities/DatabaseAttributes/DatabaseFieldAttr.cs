using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CommonDatabaseActionReusables.GeneralUtilities.DatabaseAttributes
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    sealed public class DatabaseFieldAttr : Attribute
    {

        public string correspondingDatabaseFieldName;


        public DatabaseFieldAttr(string argFieldName)
        {
            correspondingDatabaseFieldName = argFieldName;

        }

        //

        public static IDictionary<FieldInfo, DatabaseFieldAttr> GetDatabaseFieldAttrFromFieldFromObject(object argObj)
        {
            IDictionary<FieldInfo, DatabaseFieldAttr> fieldMap = new Dictionary<FieldInfo, DatabaseFieldAttr>();

            foreach (FieldInfo field in argObj.GetType().GetFields())
            {
                var databaseFieldAttr = GetDatabaseFieldAttrFromField(field);
                if (databaseFieldAttr != null)
                {
                    fieldMap.Add(field, databaseFieldAttr);
                }
            }

            return fieldMap;
        }


        public static DatabaseFieldAttr GetDatabaseFieldAttrFromField(FieldInfo fieldInfo)
        {
            object[] attrs = fieldInfo.GetCustomAttributes(typeof(DatabaseFieldAttr), false);
            foreach (object attr in attrs)
            {
                if (attr.GetType() == typeof(DatabaseFieldAttr))
                {
                    return (DatabaseFieldAttr)attr;
                }
            }

            return null;
        }

        //


        public static IDictionary<PropertyInfo, DatabaseFieldAttr> GetDatabaseFieldAttrFromPropertyFromObject(object argObj)
        {
            IDictionary<PropertyInfo, DatabaseFieldAttr> propertyMap = new Dictionary<PropertyInfo, DatabaseFieldAttr>();

            foreach (PropertyInfo property in argObj.GetType().GetProperties())
            {
                var databaseFieldAttr = GetDatabaseFieldAttrFromProperty(property);
                if (databaseFieldAttr != null)
                {
                    propertyMap.Add(property, databaseFieldAttr);
                }
            }

            return propertyMap;
        }

        public static DatabaseFieldAttr GetDatabaseFieldAttrFromProperty(PropertyInfo propertyInfo)
        {
            object[] attrs = propertyInfo.GetCustomAttributes(typeof(DatabaseFieldAttr), false);
            foreach (object attr in attrs)
            {
                if (attr.GetType() == typeof(DatabaseFieldAttr))
                {
                    return (DatabaseFieldAttr)attr;
                }
            }

            return null;
        }

    }
}
