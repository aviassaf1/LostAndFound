using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Managers;
using Domain.BLBackEnd;
using DataLayer;

namespace Domain
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            //            List<FBItem> list =  MatchManager.getInstance.getPostsFromGroup("EAACEdEose0cBAK6rRVVd0ygPiF1id6G2HZBILpjTsldk5fVZAOJDkLzcdJ6qgvjq41E4AGrFQbL97P4So14vZCHTfTZA6HS2f10TABWvS9I5b1Uq9XgYwpfztcDewRUkr6NyVnvx0YDqnPbMjOc1yAZAMbvSB1cIKXlxDDdOS5QZDZD",
            //                "1538105046204967");
            //            int i = 0;

            Database db = Database.getInstance();
            db.testop();
        }
    }
}