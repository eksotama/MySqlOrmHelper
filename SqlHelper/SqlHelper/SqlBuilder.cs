using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelper
{
    public static class SqlBuilder
    {
        public static string BuildInsertCommand(string tableName, ICollection<string> entityNames, string[] excludeEntityNames)
        {
            string fieldBuilder = string.Empty;
            string valueBuilder = string.Empty;

            foreach (var entityName in entityNames)
            {
                bool included = true;

                if (excludeEntityNames != null)
                {
                    foreach (var excludeEntityName in excludeEntityNames)
                    {
                        if (entityName.Contains(excludeEntityName))
                        {
                            included = false;
                            break;
                        }
                    }
                }

                if (included)
                {
                    fieldBuilder += $"{ entityName },";
                    valueBuilder += $"@{ entityName },";
                }
            }

            fieldBuilder = fieldBuilder.TrimEnd(',');
            valueBuilder = valueBuilder.TrimEnd(',');

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"INSERT INTO { tableName } ( { fieldBuilder } )");
            queryBuilder.AppendLine($"VALUES ( { valueBuilder } );");

            return Convert.ToString(queryBuilder);
        }

        public static string BuildUpdateCommand(string tableName, ICollection<string> entityNames, ICollection<string> conditionalEntityNames, string[] excludeEntityNames)
        {
            string valueBuilder = string.Empty;
            string conditionValueBuilder = string.Empty;

            foreach (var entityName in entityNames)
            {
                bool included = true;

                if (excludeEntityNames != null)
                {
                    foreach (var excludeEntityName in excludeEntityNames)
                    {
                        if (entityName.Contains(excludeEntityName))
                        {
                            included = false;
                            break;
                        }
                    }
                }

                if (included)
                {
                    valueBuilder += $"{ entityName } = @{ entityName },";
                }
            }

            if (conditionalEntityNames != null)
            {
                foreach (var conditionalEntityName in conditionalEntityNames)
                {
                    conditionValueBuilder += $"{ conditionalEntityName } = @{ "c_" + conditionalEntityName } AND ";
                }
            }

            valueBuilder = valueBuilder.TrimEnd(',');
            conditionValueBuilder = conditionValueBuilder.Substring(0, conditionValueBuilder.Length - 4);

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"UPDATE { tableName } SET { valueBuilder }");
            queryBuilder.AppendLine($"WHERE { conditionValueBuilder };");

            return Convert.ToString(queryBuilder);
        }

        public static string BuildDeleteCommand(string tableName, ICollection<string> conditionalEntityNames)
        {
            string conditionBuilder = string.Empty;

            foreach(var conditionalEntityName in conditionalEntityNames)
            {
                conditionBuilder += $"{ conditionalEntityName } = @{ conditionalEntityName } AND ";
            }

            conditionBuilder = conditionBuilder.Substring(0, conditionBuilder.Length - 5);

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"DELETE FROM { tableName }");
            queryBuilder.AppendLine($"WHERE { conditionBuilder };");

            return Convert.ToString(queryBuilder);
        }
    }
}
