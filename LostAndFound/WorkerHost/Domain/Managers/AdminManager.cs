using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;

namespace WorkerHost.Domain.Managers
{
    public class AdminManager : IAdminManager
    {
        private static IAdminManager singleton;
        private static Cache cache= Cache.getInstance;
        private Logger logger = Logger.getInstance;
        private SessionDirector sd = SessionDirector.getInstance;

        private AdminManager()
        {
        }

        public static IAdminManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new AdminManager();
                }
                return singleton;
            }
        }

        public string addComapny(string companyName, string phone, 
            HashSet<string> facebookGroups, String companyProfileID, String managerUserName, String managerPassword, int key)
        {
            //check not exist
            //check phone type
            //check password
            string logg;
            if (sd.getAdminName(key) == null)
            {
                return "הוספת חברה נכשלה, למשתמש זה אין הרשאות מתאימות";
            }
            if (companyName == null ||  phone == null || facebookGroups == null 
                || companyProfileID==null || managerUserName==null|| managerPassword==null)
            {
                logg = "הוספת החברה נכשלה, בבקשה הזן את כל הפרטים הדרושים";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            Company company = cache.getCompany(companyName);
            if (company != null)
            {
                logg = "החברה לא נוספה, החברה כבר קיימת במערכת";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if (companyName.Equals("") || phone.Equals("")
                || companyProfileID.Equals("") || managerUserName.Equals("") || managerPassword.Equals(""))
            {
                logg = "אחד השדות או יותר חסרים";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            // check if the the phone is in a correct format
            if (phone.Length != 10 && phone.Length != 9)
            {
                logg = "מספר טלפון לא תקין";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            bool isNumExist = false;
            bool isSmallKeyExist = false;
            bool isBigKeyExist = false;
            bool isKeyRepeting3Times = false;
            if (managerPassword.Length < 6)
            {
                logg = logg = "החברה לא נוספה, נא הזן סיסמה חזקה יותר, באורך 6 תוים לפחות";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            for (int i = 0; i < managerPassword.Length; i++)
            {
                if (managerPassword.ElementAt(i) <= '9' && managerPassword.ElementAt(i) >= '0')
                {
                    isNumExist = true;
                }
                if (managerPassword.ElementAt(i) <= 'Z' && managerPassword.ElementAt(i) >= 'A')
                {
                    isBigKeyExist = true;
                }
                if (managerPassword.ElementAt(i) <= 'z' && managerPassword.ElementAt(i) >= 'a')
                {
                    isSmallKeyExist = true;
                }
                if (i < managerPassword.Length - 2 && (managerPassword.ElementAt(i).Equals(managerPassword.ElementAt(i + 1)) && managerPassword.ElementAt(i).Equals(managerPassword.ElementAt(i + 2))))
                {
                    isKeyRepeting3Times = true;
                }
            }
            if (!(isNumExist && isSmallKeyExist && isBigKeyExist && !isKeyRepeting3Times))
            {
                logg = "החברה לא נוספה, נא הזן סיסמה חזקה יותר";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            company = new Company(companyName, phone, facebookGroups, companyProfileID,managerUserName, managerPassword);
            logg = "company has been added";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }

        public string deleteCompany(string companyName, int key)
        {
            if (sd.getAdminName(key) == null)
            {
                return "user no admin";
            }

            Company company = cache.getCompany(companyName);
            if (company == null)
            {
                string logg = "company not exists in the system";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            return company.delete();
        }

        public string editCompany(string companyName, string password, string phone, int key)
        {
            string logg;
            if (sd.getAdminName(key) == null)
            {
                return "עריכת חברה לא התבצעה";
            }
            if (companyName == null || /*password == null ||*/ phone == null || companyName.Equals("") || /*password.Equals("") ||*/
                phone.Equals(""))
            {
                logg = "עריכת חברה לא התבצעה, אחד או יותר מהערכים חסרים";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            /*
            // check if the password is strong enough
            bool isNumExist = false;
            bool isSmallKeyExist = false;
            bool isBigKeyExist = false;
            bool isKeyRepeting3Times = false;
            if (password.Length < 6)
            {
                logg = "password should contain at least 6 ccharacters, edit company Fail";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            for (int i = 0; i < password.Length; i++)
            {
                if (password.ElementAt(i) <= '9' && password.ElementAt(i) >= '0')
                {
                    isNumExist = true;
                }
                if (password.ElementAt(i) <= 'Z' && password.ElementAt(i) >= 'A')
                {
                    isBigKeyExist = true;
                }
                if (password.ElementAt(i) <= 'z' && password.ElementAt(i) >= 'a')
                {
                    isSmallKeyExist = true;
                }
                if (i < password.Length - 2 && (password.ElementAt(i).Equals(password.ElementAt(i + 1)) && password.ElementAt(i).Equals(password.ElementAt(i + 2))))
                {
                    isKeyRepeting3Times = true;
                }
            }
            if (!(isNumExist && isSmallKeyExist && isBigKeyExist && !isKeyRepeting3Times))
            {
                logg = "password isnt strong enough";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }*/
            // check if the the phone is in a correct format
            if (phone.Length != 10 && phone.Length != 9)
            {
                logg = "עריכת חברה לא התבצעה, מספר טלפון לא תקין";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 1);
                return logg;
            }
            Company company = cache.getCompany(companyName);
            return company.edit(password, phone);
        }

        public string login(String username, String password)
        {
            if(cache.isAdmin(username, password)){
                int key = sd.generateAdminKey(username);
                return "login succeeded," + key;
            }
            else 
                return "התחברות נכשלה, שם משתמש או סיסמה לא תקינים";
        }

        public List<Company> getAllCompanies(int key)
        {
            if (sd.getAdminName(key) == null)
            {
                return null;
            }

            return cache.getAllCompanies();
        }
    }
}
