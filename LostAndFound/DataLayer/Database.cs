using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Database : IDB
    {
        private static Database singleton;
        LostAndFoundDBEntities db;

        private Database()
        {

        }


        public static Database getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Database();
                    singleton.initializeDB();
                }
                return singleton;
            }
        }
        private Boolean initializeDB()
        {
            try
            {
                this.db = new LostAndFoundDBEntities();
                string Path = Environment.CurrentDirectory;
                string[] appPath = Path.Split(new string[] { "bin" }, StringSplitOptions.None);
                AppDomain.CurrentDomain.SetData("DataDirectory", appPath[0]);
                //initialAddToCache();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void testop()
        {
            foreach (var item in db.User)
            {
                Console.WriteLine(item.UserName);
            }
            while (true) ;
        }
        public void clear()
        {
            try
            {
                
                //cache.clear();
            }
            catch
            {
                
            }
        }

        public bool AddCompany(string userName, string companyName, string phone)
        {
            throw new NotImplementedException();
        }

        public bool removeCompany(string companyName)
        {
            throw new NotImplementedException();
        }

        public bool updateCompany(string UserNameNew, string CompanyNameNew, string PhoneNew)
        {
            throw new NotImplementedException();
        }

        public bool AddCompanyItem(int itemId, int serialNumber, string contactName, string contactPhone, string companyName)
        {
            throw new NotImplementedException();
        }

        public bool removeCompanyItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public bool updateCompanyItem(int itemId, int serialNumberNew, string contactNameNew, string contactPhoneNew, string companyNameNew)
        {
            throw new NotImplementedException();
        }

        public bool addFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        public bool removeFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        public bool updateFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        public bool addFBItem(int itemId, List<string> colors, string itemType, DateTime lostDate, string location, string decription, string postURL, string publisherName, string type)
        {
            throw new NotImplementedException();
        }

        public bool removeFBItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public bool updateFBItem(int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string decriptionNew, string postURLNew, string publisherNameNew, string typeNew)
        {
            throw new NotImplementedException();
        }
    }
}
