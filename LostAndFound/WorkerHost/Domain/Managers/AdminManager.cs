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
        private Cache cache;
        private Logger logger = Logger.getInstance;

        private AdminManager()
        {
            cache = Cache.getInstance;
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

        public string addComapny(string userName, string password, string companyName, string phone, 
            HashSet<string> facebookGroups, String companyProfileID, String managerUserName, String managerPassword)
        {
            //check not exist
            //check phone type
            //check password
            string logg;
            if (userName == null || password == null || companyName == null || phone == null || facebookGroups == null 
                || companyProfileID==null || managerUserName==null|| managerPassword==null)
            {
                logg = "one of the arguments or more is null, add company failed";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            Company company = cache.getCompany(companyName);
            if (company != null)
            {
                logg = "company already exists in the system";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if (userName.Equals("") || password.Equals("") || phone.Equals("") || companyName.Equals("")
                || companyProfileID.Equals("") || managerUserName.Equals("") || managerPassword.Equals(""))
            {
                logg = "one or more of the fields is missing";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            // check if the password is strong enough
            bool isNumExist = false;
            bool isSmallKeyExist = false;
            bool isBigKeyExist = false;
            bool isKeyRepeting3Times = false;
            if (password.Length < 6)
            {
                logg = "password should contain at least 6 ccharacters, add company Fail";
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
            }
            // check if the the phone is in a correct format
            if (phone.Length != 10 && phone.Length != 9)
            {
                logg = "phone number's length is invalid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            isNumExist = false;
            isSmallKeyExist = false;
            isBigKeyExist = false;
            isKeyRepeting3Times = false;
            if (managerPassword.Length < 6)
            {
                logg = "password should contain at least 6 ccharacters, add company Fail";
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
                logg = "password isnt strong enough";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            company = new Company(userName, password, companyName, phone, facebookGroups, companyProfileID,managerUserName, managerPassword);
            logg = "company has been added";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }

        public string deleteCompany(string companyName)
        {
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

        public string editCompany(string companyName, string password, string phone)
        {
            string logg;
            if (companyName == null || password == null || phone == null || companyName.Equals("") || password.Equals("") ||
                phone.Equals(""))
            {
                logg = "one or more of the fields is missing";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
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
            }
            // check if the the phone is in a correct format
            if (phone.Length != 10 && phone.Length != 9)
            {
                logg = "phone number's length is invalid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 1);
                return logg;
            }
            Company company = cache.getCompany(companyName);
            return company.edit(password, phone);
        }

        public string login()
        {
            throw new NotImplementedException();
        }
    }
}
