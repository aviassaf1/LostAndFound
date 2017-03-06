using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;
using Facebook;

namespace Domain.Managers
{
    public class ComapanyManager :ICompanyManager
    {
        private Dictionary<String ,String > _FBTokens=new Dictionary<string, string>();//company name, token
        private static ICompanyManager singleton;
        private Cache cache;
        private const int MAXDAYS = 8;

        private ComapanyManager()
        {
            cache = Cache.getInstance;
        }
        public static ICompanyManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ComapanyManager();
                }
                return singleton;
            }
        }


        public Company getCompanyByName(string companyName)
        {
            return cache.getCompany(companyName);           
        }

        public String login(String companyName, String token)
        {
            //check if company exist
            if (_FBTokens.ContainsKey(companyName))
                _FBTokens[companyName] = token;
            else {
                _FBTokens.Add(companyName, token);
            }
            return "login was succeeded";
        }

        public string publishInventory(string token, string groupID, int days, string companyName)
        {
            if (token == null || groupID == null || companyName == null)
            {
                return "PublishInventory: values cannot be null";
            }
            if(days > MAXDAYS)
                return "PublishInventory: days is more than MAXDAYS";
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(token);
            }
            catch(Exception )
            {
                return "PublishInventory: token is invalid";
            }
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            List<CompanyItem> items = ItemManager.getInstance.getAllCompanyItems(companyName);
            if (items == null)
            {
                return "PublishInventory: companyName is invalid";
            }
            string inventory = "אלו הפריטים הנמצאים במחלקת אבדות ומציאות: \n";
            string format = " {0} בצבע {1}\n";
            DateTime nDaysAgo = DateTime.Now;
            nDaysAgo = nDaysAgo.AddDays(-days);
            foreach (CompanyItem item in items)
            {
                if ((item.GetType()).Equals(typeof(FoundItem)))
                {
                    if(!((FoundItem)item).Delivered && item.Date.CompareTo(nDaysAgo) > 0)
                    {
                        string type = DataType.Hebrew2EnglishTypes.FirstOrDefault(x => x.Value == item.ItemType).Key;
                        inventory += String.Format(format, type ,getColorsString(item.Colors));
                    }
                }
            }
            dynamic result = null;
            try
            {
                //make sure post succeeds with GID
                result = fb.Post(groupID + "/feed", new { message = inventory });
            }
            catch(Exception)
            {

                return "PublishInventory: post to facebook failed";
            }
            return "true";
        }

        private string getColorsString(List<Color> colors)
        {
            string resColors = "";
            foreach(Color color in colors)
            {
                resColors += DataType.HebColors.FirstOrDefault(x => x.Value == color).Key;
            }
            return resColors;
        }

        public List<Item> getLostItems3Days(string companyName, DateTime date)
        {
            if(companyName == null || date == null)
            {
                return null;
            }
            Company company = cache.getCompany(companyName);
            if (company == null)
            {
                return null;
            }
            List<LostItem> companyLostItemsList = company.getAllLostItems();
            List<Item> itemsFromLastThreeDays = new List<Item>();
            foreach (LostItem item in companyLostItemsList)
            {
                DateTime itemDate = item.Date;
                if (date.Subtract(itemDate).Days <= 2)
                {
                    itemsFromLastThreeDays.Add(item);
                }
            }
            return itemsFromLastThreeDays;
        }

        public List<Item> getFoundItems3Days(string companyName, DateTime date)
        {
            Company company = cache.getCompany(companyName);
            if (company == null)
            {
                return null;
            }
            List<FoundItem> companyFoundItemsList = company.getAllFoundItems();
            List<Item> itemsFromLastThreeDays = new List<Item>();
            foreach (FoundItem item in companyFoundItemsList)
            {
                DateTime itemDate = item.Date;
                if (date.Subtract(itemDate).Days <= 2)
                {
                    itemsFromLastThreeDays.Add(item);
                }
            }
            return itemsFromLastThreeDays;
        }

        public string addFBGroup(string companyName, string groupID)
        {
            if (companyName == null || groupID == null)
            {
                return null;
            }
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                return "CompanyManager-addFBGroup: company name is not valid";
            }
            bool ok = c.addFacebookGroup(groupID);
            if (ok)
                return "true";
            else
                return "CompanyManager-addFBGroup: facebook group was already added";
        }

        public string removeFBGroup(string companyName, string groupID)
        {
            if(companyName == null || groupID == null)
            {
                return null;
            }
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                return "CompanyManager-removeFBGroup: company name is not valid";
            }
            bool ok = c.removeFacebookGroup(groupID);
            if (ok)
                return "true";
            else
                return "CompanyManager-removeFBGroup: facebook group is not in the groups list";
        }

        public Dictionary<string, string> getSystemCompanyFBGroup(string companyName, string token)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, string> allFBGroups = getAllCompanyFBGroup(companyName, token);
            if(allFBGroups == null)
            {
                return null;
            }
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                return null;
            }
            HashSet<string> FBgroups = c.FacebookGroups;
            foreach(string groupID in FBgroups)
            {
                if (allFBGroups.ContainsKey(groupID))
                {
                    result.Add(groupID, allFBGroups[groupID]);
                }
            }
            return result;

        }

        public Dictionary<string, string> getAllCompanyFBGroup(string companyName, string token)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(token);
            }
            catch (Exception)
            {
                return null;
            }
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            parameters["fields"] = "groups{name}";
            dynamic result = fb.Get("me", parameters);
            var groups = result.groups["data"];
            bool isNext = true;
            var paging = result.groups["paging"];
            int i = 0;
            while (isNext)
            {
                foreach (var group in groups)
                {
                    string groupname = group["name"];
                    var gid = group["id"];
                    res.Add(gid, groupname);
                }
                if (i != 0)
                    paging = result["paging"];
                if (!paging.ContainsKey("next"))
                    isNext = false;
                else
                {
                    var nextURL = paging["next"];
                    result = fb.Get((string)nextURL);
                    groups = result["data"];
                    i++;
                }


            }
            return res;
        }
    }
}
