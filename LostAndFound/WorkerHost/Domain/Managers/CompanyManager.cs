using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;
using Facebook;

namespace WorkerHost.Domain.Managers
{
    public class CompanyManager : ICompanyManager
    {
        private Dictionary<String, String> _FBTokens = new Dictionary<string, string>();//company name, token
        
        private static ICompanyManager singleton;
        private Cache cache;
        private Logger logger = Logger.getInstance;
        private const int MAXDAYS = 8;

        private CompanyManager()
        {
            cache = Cache.getInstance;
        }
        public static ICompanyManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new CompanyManager();
                }
                return singleton;
            }
        }


        public Company getCompanyByName(string companyName)
        {
            return cache.getCompany(companyName);
        }

        public String login(String token, String userName, String userPassword)
        {
            string fbid = "";
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(token);
            }
            catch
            {
                return null;
            }
            var parameters = new Dictionary<string, object>();
            parameters["fields"]="id";
            dynamic result;
            try
            {
                //make sure post succeeds with GID
                result = fb.Get("me",parameters);
                fbid = result.id;
            }
            catch (Exception)
            {
                return null;
            }
            Company company = cache.getCompanyByfb(fbid);
            if (company != null && company.FbProfileID.Equals(fbid)&&(
                (company.Workers.Keys.Contains(userName)&& company.Workers[userName].Equals(userPassword)) || 
                (company.Managers.Keys.Contains(userName) && company.Managers[userName].Equals(userPassword))))
            {
                if (_FBTokens.ContainsKey(company.CompanyName))
                    _FBTokens[company.CompanyName] = token;
                else
                {
                    _FBTokens.Add(company.CompanyName, token);
                }
                int key = SessionDirector.getInstance.generateKey(userName);
                return "login succeeded,"+key;
            }
            else
            {
                return "התחברות נכשלה, שם משתמש או סיסמה לא תקינים";
            }

        }

        public String addWorker(String newUsername,String newPassword,bool isManager, int key)
        {
            String user= SessionDirector.getInstance.getUserName(key);
            if (user != null)
            {
                String companyName=cache.getCompanyNameByUsername(user);
                if(companyName != null)
                {
                    Company company = cache.getCompany(companyName);
                    if(company != null&&company.Managers.Keys.Contains(user))
                    {
                        if (cache.getCompanyNameByUsername(newUsername) == null)
                        {
                            if (!isManager)
                            {
                                company.addWorker(newUsername, newPassword);
                            }
                            else
                            {
                                company.addManager(newUsername, newPassword);
                            }
                            return "worker added";
                        }
                        return "הוספת עובד נכשלה, משתמש כבר קיים במערכת";
                    }

                }
            }
            return "הוספת עובד נכשלה";
        }

        public String removeWorker(String delUsername,  int key)
        {
            String user = SessionDirector.getInstance.getUserName(key);
            if (user != null&& !user.Equals(delUsername))
            {
                String companyName = cache.getCompanyNameByUsername(user);
                if (companyName != null)
                {
                    Company company = cache.getCompany(companyName);
                    if (company != null && company.Managers.Keys.Contains(user))
                    {
                        if (cache.getCompanyNameByUsername(delUsername) != null)
                        {
                            return company.removeWorker(delUsername);
                        }
                        return "remove worker failed, username not exists";
                    }

                }
            }
            return "remove worker failed";
        }


        public string publishInventory( string groupID, int days, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "פרסום נכשל, אנא נסה להתחבר מחדש";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "פרסום נכשל, אנא נסה להתחבר מחדש";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string token = _FBTokens[companyName];
            if (token == null)
            {
                return null;
            }
            if (token == null || groupID == null || companyName == null)
            {
                logg = "פרסום נכשל, אחד הערכים או יותר לא תקינים";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
                if (days > MAXDAYS)
            {
                logg = "פרסום נכשל, כמות הימים שניתן לבחור היא "+ MAXDAYS;
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(token);
            }
            catch
            {
                logg = "פרסום נכשל, אנא נסה להתחבר מחדש";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            List<CompanyItem> items = ItemManager.getInstance.getAllCompanyItems(key);
            if (items == null)
            {
                logg = "פרסום נכשל, שם חברה לא תקין";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string inventory = "אלו הפריטים הנמצאים במחלקת אבדות ומציאות: \n";
            string format = " {0} בצבע {1}\n";
            DateTime nDaysAgo = DateTime.Now;
            nDaysAgo = nDaysAgo.AddDays(-days);
            foreach (CompanyItem item in items)
            {
                if ((item.GetType()).Equals(typeof(FoundItem)))
                {
                    if (!((FoundItem)item).Delivered && item.Date.CompareTo(nDaysAgo) > 0)
                    {
                        string type = DataType.EnglishTypes2Hebrew[item.ItemType];//DataType.Hebrew2EnglishTypes.FirstOrDefault(x => x.Value == item.ItemType).Key;
                        string color = "";
                        foreach(string col in item.getHebColorsList())
                        {
                            color += col+" ";
                        }
                        inventory += String.Format(format, type, color);
                    }
                }
            }
            dynamic result = null;
            try
            {
                //make sure post succeeds with GID
                result = fb.Post(groupID + "/feed", new { message = inventory });
            }
            catch (Exception)
            {
                logg = "פרסום נכשל, החיבור עם פייסבוק לא צלח אנא נסה להתחבר שוב לפייסבוק ואז למערכת";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            logg = "PublishInventory Worked";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return "true";
        }

        private string getColorsString(List<Color> colors)
        {
            string resColors = "";
            foreach (Color color in colors)
            {
                resColors += DataType.HebColors.FirstOrDefault(x => x.Value == color).Key;
            }
            return resColors;
        }

        public List<Item> getLostItems3Days(string companyName, DateTime date)
        {
            if (companyName == null || date == null)
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
            if (companyName == null || date == null)
            {
                return null;
            }
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

        public string addFBGroup( string groupID, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "הוספת קבוצת פייסבוק נכשלה, מפתח כניסה לא מעודכן, אנא נסה להתחבר בשנית";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "הוספת קבוצת פייסבוק נכשלה, מפתח כניסה לא מעודכן, אנא נסה להתחבר בשנית";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if (companyName == null || groupID == null)
            {
                return null;
            }
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                logg = "הוספת קבוצת פייסבוק נכשלה, שם החברה אינו תקין";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            bool ok = c.addFacebookGroup(groupID);
            if (ok)
            {
                logg = "Add facebook group worked";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 1);
                return "Add facebook group worked";
            }
            else
            {
                logg = "הוספת קבוצת פייסבוק נכשלה, הקבוצה כבר קיימת במערכת";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
        }

        public string removeFBGroup( string groupID, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "CompanyManager-removeFBGroup: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "CompanyManager-removeFBGroup: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if (companyName == null || groupID == null)
            {
                return null;
            }
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                logg = "CompanyManager-removeFBGroup: company name is not valid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            bool ok = c.removeFacebookGroup(groupID);
            if (ok)
            {
                logg = "remove fb group worked";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 1);
                return "true";
            }
            else
            {
                logg = "CompanyManager-removeFBGroup: facebook group is not in the groups list";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return "CompanyManager-removeFBGroup: facebook group is not in the groups list";
            }
        }

        public Dictionary<string, string> getSystemCompanyFBGroup( int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "CompanyManager-getSystemCompanyFBGroup: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "CompanyManager-getSystemCompanyFBGroup: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string token = _FBTokens[companyName];
            if (token == null)
            {
                return null;
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            /*Dictionary<string, string> allFBGroups = getAllCompanyFBGroup( token);
            if (allFBGroups == null)
            {
                return null;
            }*/
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                return null;
            }
            Dictionary<string, string> res = new Dictionary<string, string>();
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(token);
            }
            catch
            {
                return null;
            }
            HashSet<string> FBgroups = c.FacebookGroups;
            foreach (string groupID in FBgroups)
            {
                dynamic fbResult = fb.Get(groupID);
                result.Add(groupID, fbResult["name"]);
            }
            return result;

        }

        public Dictionary<string, string> getAllCompanyFBGroup(string companyName)
        {
            if (companyName == null )
            {
                return null;
            }
            string token = _FBTokens[companyName];
            if ( token == null)
            {
                return null;
            }
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                return null;
            }
            Dictionary<string, string> res = new Dictionary<string, string>();
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(token);
            }
            catch
            {
                return null;
            }
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            parameters["fields"] = "name";
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

        public string getToken(string companyName)
        {
            return _FBTokens[companyName];
        }

        public Dictionary<string, bool> getCompanyWorkers(int key)
        {
            String user = SessionDirector.getInstance.getUserName(key);
            if (user != null)
            {
                String companyName = cache.getCompanyNameByUsername(user);
                if (companyName != null)
                {
                    Company company = cache.getCompany(companyName);
                    if (company != null && company.Managers.Keys.Contains(user))
                    {
                        Dictionary<string, bool> workers = new Dictionary<string, bool>();
                        foreach(string manager in company.Managers.Keys)
                        {
                            workers.Add(manager, true);
                        }
                        foreach (string worker in company.Workers.Keys)
                        {
                            workers.Add(worker, false);
                        }
                        return workers;
                    }
                }
            }
            return null;
        }

        public bool isManager(int key)
        {
            String user = SessionDirector.getInstance.getUserName(key);
            if (user != null)
            {
                String companyName = cache.getCompanyNameByUsername(user);
                if (companyName != null)
                {
                    Company company = cache.getCompany(companyName);
                    if (company != null && company.Managers.Keys.Contains(user))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
