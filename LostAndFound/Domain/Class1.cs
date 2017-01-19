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
            string token = "EAACEdEose0cBAJ9eI3qfv8PByhWIPeBSBIZA39u7OiErw8yyZChHSGMqZBetIHJ3KiFXbWZB2PvZAs7wMUOluagKVGaMhOUkYatBraBzXWmz8wfmvE4OgdhQiIIVkhSRuxZCZBxPZAxERsRFVtvoPWG9L6wjIG8AhCHblezIzRah4wZDZD";
            List<FBItem> list =  MatchManager.getInstance.getPostsFromGroup(token,
                "1538105046204967");
            List<string> colors = new List<string>() { "PINK" };
            HashSet<string> fbg = new HashSet<string>() { "1538105046204967" };

            AdminManager.getInstance.addComapny("GuyCompany", "gG1", "GuyCompany", "050000000", fbg);
            ItemManager.getInstance.addFoundItem(colors,"KEYS", DateTime.Today, "here", "desc",56658, "GuyCompany", "noam",
                "0555555555", "location",token);
            Cache cache = Cache.getInstance;
            int i = 0;
            i++;
        }
    }
}