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
            string token = "EAACEdEose0cBAJZAw64b2cIJReLnTCZCNZCuSRAM1RE10z4fgN1bakJ43UQbEeeqqU60ZAIZAgbuxH046bIt4gNjXFZBrqKHwjGR5cb7MDRDQWVH66KA6WAiOEqN3fZCva9DlCZBv00pVc8CAZCbweYdYspAw4Fc7knZAW27tlUJV6RAZDZD";
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