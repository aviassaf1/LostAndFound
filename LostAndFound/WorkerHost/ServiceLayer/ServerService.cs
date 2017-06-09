using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.Domain.Managers;
using WorkerHost.ServiceLayer.DataContracts;
using WorkerHost.ServiceLayer.Controllers;

namespace WorkerHost
{
    class ServerService : IServerService
    {

        public string addComapny(string companyName, string phone, HashSet<string> facebookGroups, string companyProfileID, string managerUserName, string managerPassword, int key)
        {
            return AdminController.getInstance.addComapny(companyName, phone, facebookGroups, companyProfileID,
                managerUserName, managerPassword, key);
        }

        public string addFBGroup(string groupID, int key)
        {
            return CompanyController.getInstance.addFBGroup(groupID, key);
        }

        public string addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description, int serialNumber, string contactName, string contactPhone, string photoLocation, int key)
        {
            return ItemController.getInstance.addFoundItem(sColors, sType, date, location, description, serialNumber, contactName, contactPhone, photoLocation, key);
        }

        public string addLostItem(List<string> sColors, string sType, DateTime date, string location, string description, int serialNumber, string contactName, string contactPhone, string photoLocation, int key)
        {
            return ItemController.getInstance.addLostItem(sColors, sType, date, location, description, serialNumber, contactName, contactPhone, photoLocation, key);
        }

        public string addWorker(string newUsername, string newPassword, bool isManager, int key)
        {
            return CompanyController.getInstance.addWorker(newUsername, newPassword, isManager, key);
        }

        public string changeMatchStatus(int matchID, string statusNum, int key)
        {
            return MatchController.getInstance.changeMatchStatus(matchID,statusNum, key);
        }

        public string deleteCompany(string companyName, int key)
        {
            return AdminController.getInstance.deleteCompany(companyName, key);
        }

        public string deleteItem(int itemID, int key)
        {
            return ItemController.getInstance.deleteItem(itemID, key);
        }

        public string editCompany(string companyName, string password, string phone, int key)
        {
            return AdminController.getInstance.editCompany(companyName,password,phone, key);
        }

        public string editItem(int itemID, DateTime date, string location, string description, int serialNumber, string contactName, string contactPhone, int key)
        {
            return ItemController.getInstance.editItem(itemID, date, location, description, serialNumber, contactName,
                contactPhone, key);
        }

        public List<CompanyItemData> getAllCompanyItems(int key)
        {
            return ItemController.getInstance.getAllCompanyItems(key);
        }

        public List<MatchData> getMatchesByItemID(int itemID, int key)
        {
            return MatchController.getInstance.getMatchesByItemID(itemID, key);
        }

        public List<GroupData> getSystemCompanyFBGroup(int key)
        {
            return CompanyController.getInstance.getSystemCompanyFBGroup(key);
        }

        public string Adminlogin(string username, string password)
        {
            return AdminController.getInstance.login(username, password);
        }

        public string login(string token, string username, string userPassword)
        {
            return CompanyController.getInstance.login(token, username, userPassword);
        }

        public string publishInventory(string GroupID, int days, int key)
        {
            return CompanyController.getInstance.publishInventory(GroupID,days, key);
        }

        public string removeFBGroup(string groupID, int key)
        {
            return CompanyController.getInstance.removeFBGroup(groupID, key);
        }

        public string removeWorker(string delUsername, int key)
        {
            return CompanyController.getInstance.removeWorker(delUsername, key);
        }

        public string transactionComplete(int itemID, int key)
        {
            return ItemController.getInstance.transactionComplete(itemID, key);
        }

        public CompanyItemData getCompanyItem(int itemID, int key)
        {
            return ItemController.getInstance.getCompanyItem(itemID, key);
        }

        public List<CompanyData> getAllCompanies(int key)
        {
            return AdminController.getInstance.getAllCompanies(key);
        }

        public List<WorkerData> getCompanyWorkers(int token)
        {
            return CompanyController.getInstance.getCompanyWorkers(token);
        }

        public bool isManager(int key)
        {
            return CompanyController.getInstance.isManager(key);
        }

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
    }*/
    }
}
