using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelper
{
    public static class DbConfiguration
    {
        public static void SetSqlConnectionString(string server, string port, string userid, string password, string database)
        {
            Properties.Settings.Default.Server = server;
            Properties.Settings.Default.Port = port;
            Properties.Settings.Default.UserID = userid;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.Database = database;

            Properties.Settings.Default.Save();

            Properties.Settings.Default.Reload();
        }

        public static string GetServer() => Properties.Settings.Default.Server;

        public static string GetPort() => Properties.Settings.Default.Port;

        public static string GetUserID() => Properties.Settings.Default.UserID;

        public static string GetPassword() => Properties.Settings.Default.Password;

        public static string GetDatabase() => Properties.Settings.Default.Database;
    }
}
