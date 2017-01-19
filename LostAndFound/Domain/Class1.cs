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
            string token = "EAACEdEose0cBAM5rDkM1hRKGCImrxrA7QJ4gZAh2s2ZCv9PuQeuT8qfgNeJQKkvBYWruFfNZADesnp8PB35pU7ykzzG2HF9Wo0xCqbJFQkHLwFfsKQYfDZAZAZBvtDBmJXa45wQS3ZAavaWmmuc3TI9msR8lyAF5oGrSe8bTqhabgZDZD";
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