using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;

namespace Domain.Managers
{
    public class AdminManager : IAdminManager
    {
        private static IAdminManager singleton;
        private Cache cache;

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

        public string addComapny(string userName, string password, string companyName, string phone, HashSet<string> facebookGroups)
        {
            //check not exist
            //check phone type
            //check password
            Company company = cache.getCompany(companyName);
            if (company != null)
            {
                return "company already exists in the system";
            }
            if (userName.Equals("") || password.Equals("") || phone.Equals("")|| companyName.Equals(""))
            {
                return "one or more of the fields is missing";
            }
            // check if the password is strong enough
            bool isNumExist = false;
            bool isSmallKeyExist = false;
            bool isBigKeyExist = false;
            bool isKeyRepeting3Times = false;
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
                return "password isnt strong enough";
            }
            // check if the the phone is in a correct format
            if (phone.Length!=10 && phone.Length!=9)
            {
                return "phone number's length is invalid";
            }
            company = new Company(userName, password, companyName, phone, facebookGroups);
            return "company has been added";            
        }

        public string deleteCompany(string companyName)
        {
            Company company = cache.getCompany(companyName);
            if (company != null)
            {
                return "company not exists in the system";
            }            
            return company.delete();
        }

        public string editCompany(string companyName, string password, string phone)
        {
            if (companyName.Equals("") || password.Equals("") || phone.Equals(""))
            {
                return "one or more of the fields is missing";
            }
            // check if the password is strong enough
            bool isNumExist = false;
            bool isSmallKeyExist = false;
            bool isBigKeyExist = false;
            bool isKeyRepeting3Times = false;
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
                return "password isnt strong enough";
            }
            // check if the the phone is in a correct format
            if (phone.Length != 10 && phone.Length != 9)
            {
                return "phone number's length is invalid";
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
