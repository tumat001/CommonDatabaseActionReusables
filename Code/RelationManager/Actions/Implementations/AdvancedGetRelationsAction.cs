using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CommonDatabaseActionReusables.RelationManager.Config;
using CommonDatabaseActionReusables.GeneralUtilities.PathConfig;
using CommonDatabaseActionReusables.GeneralUtilities.DatabaseActions;
using CommonDatabaseActionReusables.RelationManager.Exceptions;

namespace CommonDatabaseActionReusables.RelationManager.Actions
{
    public class AdvancedGetRelationsAction : AbstractAction<RelationDatabasePathConfig>
    {

        internal AdvancedGetRelationsAction(RelationDatabasePathConfig config) : base(config)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="PrimaryDoesNotExistException"></exception>
        /// <returns>A set of target ids with relation to the given <paramref name="primaryId"/> found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Ignores <see cref="AdvancedGetParameters.TextToContain"/>.</returns>
        public ISet<int> AdvancedGetRelationsOfPrimaryAsSet(int primaryId, AdvancedGetParameters adGetParameter)
        {

            var primaryExists = new PrimaryExistsAction(databasePathConfig).IfPrimaryExsists(primaryId);
            if (!primaryExists)
            {
                throw new PrimaryDoesNotExistException(primaryId);
            }


            var catSet = new HashSet<int>();

            using (SqlConnection sqlConn = databasePathConfig.GetSQLConnection())
            {
                sqlConn.Open();

                using (SqlCommand command = sqlConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT * FROM (SELECT [{0}], [{4}] FROM [{1}] ORDER BY {0} {2} {3}) T WHERE [{4}] = @EntId",
                        databasePathConfig.CategoryIdColumnName, databasePathConfig.EntityToCategoryTableName,
                        adGetParameter.GetSQLStatementFromOffset(),
                        adGetParameter.GetSQLStatementFromFetch(),
                        databasePathConfig.EntityIdColumnName);

                    command.Parameters.Add(new SqlParameter("EntId", primaryId));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var catId = reader.GetSqlInt32(0).Value;

                            catSet.Add(catId);
                        }
                    }
                }
            }

            return catSet;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adGetParameter"></param>
        /// <returns>A set of category ids with relation to the given <paramref name="primaryId"/> found in the database given in this object's <see cref="DatabasePathConfig"/>, taking into
        /// account the given <paramref name="adGetParameter"/>. Ignores <see cref="AdvancedGetParameters.TextToContain"/>.</returns>
        public ISet<int> TryAdvancedGetRelationsOfPrimaryAsSet(int primaryId, AdvancedGetParameters adGetParameter)
        {
            try
            {
                return AdvancedGetRelationsOfPrimaryAsSet(primaryId, adGetParameter);
            }
            catch (Exception)
            {
                return new HashSet<int>();
            }
        }

    }
}
