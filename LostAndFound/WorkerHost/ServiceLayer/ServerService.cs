using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.Domain.Managers;


namespace WorkerHost
{
    class ServerService : IServerService
    {
        /*public string testClass1(string color, string type, string name, string phone)
        {
            string token = "EAACEdEose0cBAEZArIlzyRuf3du6KgtFrpAcSPJdPp0mUGZB1TiZA7FRSsBaII5oxFuI1z6BH6HGZBuOJv8m4WOt2FcZBvadGVOWOa7ShfyLoK7WfUwXSfE8xmndLCbgRmAXIvnA7LuiERa60ZCcJLmvicx9IaA5Luz29LPCyZBx89zQC0nn5yK";
            List<FBItem> list = MatchManager.getInstance.getPostsFromGroup(token,
                "1538105046204967");
            HashSet<string> fbg = new HashSet<string>() { "1538105046204967" };
            IAdminManager adm = AdminManager.getInstance;
            string ans1 = adm.addComapny("GuyCompany", "gG123456", "GuyCompany", "050000000", fbg, "10205175970541279", "Guy", "Mc123456");
            string colorList = color;
            colorList = colorList.ToUpper();
            List<string> colors = stringToListOfColors(colorList);
            string itemType = type;
            itemType = itemType.ToUpper();
            string cname = name;
            string cphone = phone;
            IItemManager itm = ItemManager.getInstance;
            string ans2 = itm.addFoundItem(colors, itemType, DateTime.Today, "here", "desc", 56658, "GuyCompany", cname,
                cphone, "location", token);
            /*
            for(int j = 0; j<5; j++)
            {
                colors.Clear();
                colors.Add(((Color)j).ToString());
                itemType = ((ItemType)j).ToString();
                ItemManager.getInstance.addFoundItem(colors, itemType, DateTime.Today, "here", "desc", 56658, "GuyCompany", cname,
                cphone, "location", token);
            }
            */
            /*
            ComapanyManager.getInstance.publishInventory(token, "1538105046204967", 2, "GuyCompany");
             Domain.Cache cache = Domain.Cache.getInstance;
            int i = 0;
            i++;
            return i.ToString();
        }
        */
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

        public string login(string text1, string text2, bool @checked, string fbToken)
        {
            throw new NotImplementedException();
        }

        string IServerService.addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, string photoLocation, int key)
        {

            HashSet<string> fbg = new HashSet<string>() { "1538105046204967" };
            string ans = AdminManager.getInstance.addComapny("GuyCompany", "050000000", fbg,
                "10205175970541279","Guy","Mc123456",key);

            IItemManager iim = ItemManager.getInstance;
            return iim.addLostItem(sColors, sType, date, location, description,
            serialNumber, contactName, contactPhone, photoLocation, key);
        }
    }
}
