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
        //    dbEntities db = new dbEntities();
         //   db.Configuration.ProxyCreationEnabled = false;
        //    db.Configuration.LazyLoadingEnabled = false;
        //    foreach(var company in db.@try)
       //     {
      ///          Console.WriteLine(company.a);
     ///       }
      ///      while (true) ;
    
            SqlConnection conn = new SqlConnection("user id=sa;" + "password=geraistheman;" + "server=LAPTOP-2CIHG2T7\\TOMERSERVER;"
                                                   + "Trusted_Connection=yes;" + "database=LostAndFound;"
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
