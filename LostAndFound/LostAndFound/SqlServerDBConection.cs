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
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "data source = LAPTOP-2CIHG2T7\\SQLEXPRESSWORK; database = LostAndFoundDataBase; integrated security = SSPI";
            conn.Open();
        }
    }
}
