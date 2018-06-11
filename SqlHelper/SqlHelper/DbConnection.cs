using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SqlHelper
{
    public abstract class DbConnection
    {
        protected static IDbConnection GetSqlConnection()
        {
            var con = new MySqlConnection
            {
                ConnectionString = string.Format("SERVER = {0}; PORT = {1}; USERID = {2}; PASSWORD = {3}; DATABASE = {4};"
                    , Properties.Settings.Default.Server
                    , Properties.Settings.Default.Port
                    , Properties.Settings.Default.UserID
                    , Properties.Settings.Default.Password
                    , Properties.Settings.Default.Database)
            };

            con.Open();

            return con;
        }
    }
}
