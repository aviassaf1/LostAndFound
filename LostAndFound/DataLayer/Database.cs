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

        public string addCompany(string userName, string password, string companyName, string phone, HashSet<string> facebookGroups)
        {
            throw new NotImplementedException();
        }

        string IDB.removeCompany(string companyName)
        {
            throw new NotImplementedException();
        }

        string IDB.updateCompany(string UserNameNew, string CompanyNameNew, string PhoneNew)
        {
            throw new NotImplementedException();
        }

        public string AddCompanyItem(int serialNumber, string contactName, string contactPhone, string companyName)
        {
            throw new NotImplementedException();
        }

        string IDB.removeCompanyItem(int itemId)
        {
            throw new NotImplementedException();
        }

        string IDB.updateCompanyItem(int itemId, int serialNumberNew, string contactNameNew, string contactPhoneNew, string companyNameNew)
        {
            throw new NotImplementedException();
        }

        string IDB.addFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        string IDB.removeFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        string IDB.updateFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        public string addFBItem(List<string> colors, string itemType, DateTime lostDate, string location, string decription, string postURL, string publisherName, string type)
        {
            throw new NotImplementedException();
        }

        string IDB.removeFBItem(int itemId)
        {
            throw new NotImplementedException();
        }

        string IDB.updateFBItem(int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string decriptionNew, string postURLNew, string publisherNameNew, string typeNew)
        {
            throw new NotImplementedException();
        }

        public string addFoundItem(List<string> colors, string itemType, DateTime findingDate, string location, string description, int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, bool wasFound)
        {
            throw new NotImplementedException();
        }

        public string removeFoundItem(string itemId)
        {
            throw new NotImplementedException();
        }

        public string updateFoundItem(int itemId, string companyNameNew, List<string> colorsNew, string itemTypeNew, DateTime findingDateNew, string locationNew, string descriptionNew, string photoLocationNew, bool deliveredNew)
        {
            throw new NotImplementedException();
        }

        public string removeItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public string addLostItem(List<string> colors, string itemType, DateTime lostDate, string location, string description, int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, bool wasFound)
        {
            throw new NotImplementedException();
        }

        public string removeLostItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public string updateLostItem(int itemId, string companyNameNew, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string descriptionNew, string photoLocationNew, bool deliveredNew)
        {
            throw new NotImplementedException();
        }

        public string addMatch(int companyItemId, int itemID, string matchStatus)
        {
            throw new NotImplementedException();
        }

        public string removeMatch(int matchId)
        {
            throw new NotImplementedException();
        }

        public string updateMatch(int matchId, string matchStatusNew)
        {
            throw new NotImplementedException();
        }

        public string addUser(string userName, string password, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public string removeUser(string userName)
        {
            throw new NotImplementedException();
        }

        public string updateUser(string userName, string newPassword)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> getAdminsList()
        {
            throw new NotImplementedException();
        }

        public List<List<string>> getCompaniesList()
        {
            throw new NotImplementedException();
        }

        public List<List<string>> getFBGroupsList()
        {
            throw new NotImplementedException();
        }

        public List<List<object>> getLostItemsList()
        {
            throw new NotImplementedException();
        }

        public List<List<object>> getFoundItemsList()
        {
            throw new NotImplementedException();
        }

        public List<List<object>> geFBItemsList()
        {
            throw new NotImplementedException();
        }

        public List<List<string>> getMatchesList()
        {
            throw new NotImplementedException();
        }
    }
}
