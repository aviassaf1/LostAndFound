using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Database
    {
        private static Database singleton;
        LostAndFoundDBEntities db;

        private Database()
        {

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
                this.db = new LostAndFoundDBEntities();
                string Path = Environment.CurrentDirectory;
                string[] appPath = Path.Split(new string[] { "bin" }, StringSplitOptions.None);
                AppDomain.CurrentDomain.SetData("DataDirectory", appPath[0]);
                //initialAddToCache();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void testop()
        {
            foreach (var item in db.User)
            {
                Console.WriteLine(item.UserName);
            }
            while (true) ;
        }
        public void clear()
        {
            try
            {
                
                //cache.clear();
            }
            catch
            {
                
            }
        }
    }
}
