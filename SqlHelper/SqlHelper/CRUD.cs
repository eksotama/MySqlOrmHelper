using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelper
{
    public class CRUD: DbConnection
    {
        public static void Insert<T>(T entity, string[] excludeEntityNames = null)
        {
            using (var con = GetSqlConnection())
            {
                GetProperties(entity, out string tableName, out IDictionary<string, object> properties);

                DbCommands.Execute(
                    con,
                    SqlBuilder.BuildInsertCommand(tableName, properties.Keys, excludeEntityNames),
                    properties);
            }
        }

        public static void BulkInsert<T>(IEnumerable<T> entities, string[] excludeEntityNames = null)
        {
            using (var con = GetSqlConnection())
            {
                foreach(var entity in entities)
                {
                    Insert(entity, excludeEntityNames);
                }
            }
        }

        public static void Update<T>(T entity, IDictionary<string, object> conditionalEntities, string[] excludeEntityNames = null)
        {
            using (var con = GetSqlConnection())
            {
                GetProperties(entity, out string tableName, out IDictionary<string, object> properties);

                var mergeProperties = properties;

                foreach(var conditionalEntity in conditionalEntities)
                {
                    mergeProperties.Add("c_" + conditionalEntity.Key, conditionalEntity.Value);
                }

                DbCommands.Execute(
                    con,
                    SqlBuilder.BuildUpdateCommand(tableName, properties.Keys, conditionalEntities.Keys, excludeEntityNames),
                    mergeProperties);
            }
        }

        public static void Delete<T>(IDictionary<string, object> conditionalEntities)
        {
            using (var con = GetSqlConnection())
            {
                string tableName = typeof(T).Name;

                DbCommands.Execute(
                    con,
                    SqlBuilder.BuildDeleteCommand(tableName, conditionalEntities.Keys),
                    conditionalEntities);
            }
        }

        private static void GetProperties<T>(T entity, out string tableName, out IDictionary<string, object> properties)
        {
            tableName = entity.GetType().Name;

            var fields = entity.GetType().GetProperties();

            var dictFields = new Dictionary<string, object>();

            foreach(var field in fields)
            {
                dictFields.Add(field.Name, field.GetValue(entity, null));
            }

            properties = dictFields;
        }
    }
}
