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
            catch (Exception e)
            {
                return null;
            }
            Company company = cache.getCompanyByfb(fbid);
            if (company != null && company.FbProfileID.Equals(fbid)&&(
                (company.Workers.Keys.Contains(userName)&& company.Workers[userName].Equals(userPassword)) || 
                (company.Managers.Keys.Contains(userName) && company.Managers[userName].Equals(userPassword))))
            {
                /*if (_FBTokens.ContainsKey(company.CompanyName))
                    _FBTokens[company.CompanyName] = token;
                else
                {
                    _FBTokens.Add(company.CompanyName, token);
                }*/
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
            if (token == null || /*groupID == null ||*/ companyName == null)
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
            List<CompanyItem> items = ItemManager.getInstance.getAllCompanyItems(key);
            Company comp = getCompanyByName(companyName);
            return FacebookConnector.publishInvetory(token, comp.FacebookGroups, days, items);
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
            try
            {
                string token = _FBTokens[companyName];
                FacebookClient fb = new FacebookClient(token);
                dynamic fbResult = fb.Get(groupID);
            }
            catch
            {
                logg = "הוספת קבוצת פייסבוק נכשלה, אנא הכנס מספר תקין של קבוצה בפייסבוק";
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
            if (!_FBTokens.Keys.Contains(companyName))
            {
                logg = "CompanyManager-getSystemCompanyFBGroup: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            if (!_FBTokens.ContainsKey(companyName))
            {
                return null;
            }
            if (!_FBTokens.ContainsKey(companyName))
            {
                return null;
            }
            string token = _FBTokens[companyName];
            if (token == null)
            {
                return null;
            }
            Company c = getCompanyByName(companyName);
            if (c == null)
            {
                return null;
            }
            return FacebookConnector.getFBGroups(token,c.FacebookGroups);
        }


        public string getToken(string companyName)
        {
            if (_FBTokens.ContainsKey(companyName))
                return _FBTokens[companyName];
            else
                return null;
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

        public void setToken(string companyName, string token)
        {
            _FBTokens[companyName] = token;
        }
    }
}
