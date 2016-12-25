using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LostAndFound
{
    class SqlServerDBConection
    {
        public static void Main()
        {
            LostAndFoundDataBaseEntities db = new LostAndFoundDataBaseEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            foreach(var company in db.Companies)
            {
                Console.WriteLine(company.userName);
            }
            while (true) ;
        }
    }
}
