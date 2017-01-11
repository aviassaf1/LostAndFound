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
            /* LostAndFoundDataBaseEntities db = new LostAndFoundDataBaseEntities();
             db.Configuration.ProxyCreationEnabled = false;
             db.Configuration.LazyLoadingEnabled = false;
            foreach(var company in db.Companies)
            {
                Console.WriteLine(company.userName);
            }
            while (true) ;
    */
            SqlConnection conn = new SqlConnection("user id=LAPTOP-2CIHG2T7\\Tomer;" + "password=password;" + "server=LAPTOP-2CIHG2T7\\SQLEXPRESSWORK;"
                                                   + "Trusted_Connection=yes;" + "database=LostAndFoundDataBase;"
                                                   + "connection timeout=30");
            try
            {
                conn.Open();
                Console.WriteLine("worked");
                while (true) ;
            }
            catch
            {
                Console.WriteLine("didnt work");
                while (true) ;
            }
        }
    }
}
