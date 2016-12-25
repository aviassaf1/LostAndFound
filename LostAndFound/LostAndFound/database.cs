using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Database
    {
        private static Database singleton;
        OleDbConnection connection;

        private Database()
        {

        }

        public void closeConnectionDB()
        {
            try
            {
                connection.Close();
            }
            catch
            {
            }
        }
        public void OpenConnectionDB()
        {
            try
            {
                connection.Open();
            }
            catch
            {
            }
        }
        public static Database getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Database();
                    singleton.initializeDB();
                }
                return singleton;
            }
        }
        private Boolean initializeDB()
        {
            try
            {
                connection = new OleDbConnection();
                string s = System.IO.Directory.GetCurrentDirectory();
                s = s.Substring(0, s.IndexOf("LostAndFound")) + "database.mdb; Persist Security Info = False;";
                s = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + s;
                connection.ConnectionString = @s;
                connection.Open();
                connection.Close();
                //initialAddToCache();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void clear()
        {
            try
            {
                OpenConnectionDB();
                List<String> commands = new List<string>();
                //commands.Add("DELETE  from offlineNotify");
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                foreach (string commandTXT in commands)
                {
                    command.CommandText = commandTXT;
                    command.ExecuteNonQuery();
                }
                closeConnectionDB();
                //cache.clear();
            }
            catch
            {
                closeConnectionDB();
            }
        }
    }
}
