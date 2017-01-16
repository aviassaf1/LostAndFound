using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Database db = Database.getInstance();
            // ent.
            //ent.Database.Connection.Open();
            db.testop();
        }
    }
}
