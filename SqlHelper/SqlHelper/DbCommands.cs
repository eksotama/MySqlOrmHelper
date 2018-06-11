using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SqlHelper
{
    public static class DbCommands
    {
        public static void Execute(IDbConnection con, string query, IDictionary<string, object> parameters)
        {
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = query;

                foreach (var p in parameters)
                {
                    var parameter = cmd.CreateParameter();

                    parameter.ParameterName = $"@{ p.Key }";
                    parameter.Value = p.Value;

                    cmd.Parameters.Add(parameter);
                }

                cmd.ExecuteNonQuery();
            }
        }
    }
}
