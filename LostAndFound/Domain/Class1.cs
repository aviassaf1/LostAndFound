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
            //List<string> colors = new List<string>() { "PINK" };
            HashSet<string> fbg = new HashSet<string>() { "1538105046204967" };
            AdminManager.getInstance.addComapny("GuyCompany", "gG1", "GuyCompany", "050000000", fbg);
            Console.WriteLine("Hello, welcom to FIND IT!");
            Console.WriteLine("Please enter the list of the item's colors.");
            string colorList = Console.ReadLine();
            colorList = colorList.ToUpper();
            List<string> colors = stringToListOfColors(colorList);
            Console.WriteLine("Please enter the list of the item's type.");
            string itemType = Console.ReadLine();
            itemType = itemType.ToUpper();
            Console.WriteLine("Please enter the contact's name");
            string cname = Console.ReadLine();
            Console.WriteLine("Please enter the contact's phone");
            string cphone = Console.ReadLine();
            ItemManager.getInstance.addFoundItem(colors, itemType, DateTime.Today, "here", "desc",56658, "GuyCompany", cname,
                cphone, "location",token);

            for(int j = 0; j<5; j++)
            {
                colors.Clear();
                colors.Add(((Color)j).ToString());
                itemType = ((ItemType)j).ToString();
                ItemManager.getInstance.addFoundItem(colors, itemType, DateTime.Today, "here", "desc", 56658, "GuyCompany", cname,
                cphone, "location", token);
            }
            ComapanyManager.getInstance.publishInventory(token, "1538105046204967", 2, "GuyCompany");
            Cache cache = Cache.getInstance;
            int i = 0;
            i++;
        }

        private static List<string> stringToListOfColors(string colors)
        {
            string color = "";
            List<string> colorList = new List<string>();
            for (int i = 0; i < colors.Length; i++)
            {
                if ((i == colors.Length - 1) || colors.ElementAt(i).Equals(","))
                {
                    if (i == colors.Length - 1)
                    {
                        color += colors.ElementAt(i);
                    }
                    colorList.Add(color);
                    color = "";
                }
                else
                {
                    color += colors.ElementAt(i);
                }
            }
            return colorList;
        }
    }
}