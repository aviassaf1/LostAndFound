using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Managers;
using Domain.BLBackEnd;

namespace Domain
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            string token = "EAACEdEose0cBANqSssjLRNrYig5RK9NGZBOS7cbcdowIlDUYCNXklrsci9IgSZBZB26BcVZAHtxfehRCfnBZBwXiCrrfdSSZCfGYLJMEWETUWfsZBC0ZC5FLm2MyAotVsP0OjCrPEZANi5lakXIbqU7ZA6ehdo2kU6heRaxjD6XzXn0gZDZD";
            List<FBItem> list =  MatchManager.getInstance.getPostsFromGroup(token,
                "1538105046204967");
            List<string> colors = new List<string>() { "PINK" };
            HashSet<string> fbg = new HashSet<string>() { "1538105046204967" };

            AdminManager.getInstance.addComapny("GuyCompany", "gG1", "GuyComapany", "050000000", fbg);
            ItemManager.getInstance.addFoundItem(colors,"KEYS", DateTime.Today, "here", "desc",56658, "GuyCompany", "noam",
                "0555555555", "location",token);
            Cache cache = Cache.getInstance;
            int i = 0;
        }
    }
}