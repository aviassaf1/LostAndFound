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
            string token = "EAACEdEose0cBACFTRe2WTi4LfgT8g0fFzxthi9y2lWGFpB4tim64BYZBksIvZC3FKPHgxVGyWfRItRGU484KxkXkhUZBqdIKXLxmCI6SKd2b9QSUg5whuYgZCdhM725ZARwZCs9CxG9NkaVlTv1bA6PaQZCz4nU9D0OPI0ZCcAnmrQZDZD";
            List<FBItem> list =  MatchManager.getInstance.getPostsFromGroup(token,
                "1538105046204967");
            List<string> colors = new List<string>() { "PINK" };
            HashSet<string> fbg = new HashSet<string>() { "1538105046204967" };

            AdminManager.getInstance.addComapny("GuyCompany", "gG1", "GuyComapany", "050000000", fbg);
            ItemManager.getInstance.addLostItem(colors,"KEYS", DateTime.Today, "here", "desc",56658, "GuyCompany", "noam",
                "0555555555", "location",token);
            Cache cache = Cache.getInstance;
            int i = 0;
        }
    }
}