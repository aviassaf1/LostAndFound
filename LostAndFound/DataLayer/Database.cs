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
        Entities db;

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
                this.db = new Entities();
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

        public string addCompany(string userName, string password, string companyName, string phone, HashSet<string> facebookGroups)
        {
            try
            {
                Companies company = new Companies(userName,companyName, phone);
                addUser(userName, password, true);//not sure what the last one should be
                db.Companies.Add(company);
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            //needs to add also in facebook groups table
        }

        public string removeCompany(string companyName)
        {
            try
            {
                Companies company = findCompanyByCompanyName(companyName);
                db.Companies.Remove(company);
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            //needs also to remove in facebookgroups table
        }

        public string updateCompany(string companyName, string userNameNew, string phoneNew)
        {
            try
            {
                Companies company = findCompanyByCompanyName(companyName);
                company.userName = userNameNew;
                company.phone = phoneNew;
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }

        }
        public Companies findCompanyByCompanyName(string companyName)
        {
            try
            {
                foreach (Companies company in db.Companies)
                {
                    if (company.companyName.Equals(companyName))
                    {
                        return company;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string AddCompanyItem(int serialNumber, string contactName, string contactPhone, string companyName)
        {
            try
            {
                CompanyItems cItem = new CompanyItems(serialNumber, contactName, contactPhone, companyName);
                db.CompanyItems.Add(cItem);
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string removeCompanyItem(int itemId)
        {
            try
            {
                CompanyItems cItem = findCompanyItemByItemId(itemId);
                db.CompanyItems.Remove(cItem);
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string updateCompanyItem(int itemId, int serialNumberNew, string contactNameNew, string contactPhoneNew, string companyNameNew)
        {
            try
            {
                CompanyItems cItem = findCompanyItemByItemId(itemId);
                cItem.serialNumber = serialNumberNew;
                cItem.contactName = contactNameNew;
                cItem.contactPhone = contactPhoneNew;
                cItem.companyName = companyNameNew;
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
        public CompanyItems findCompanyItemByItemId(int itemId)
        {
            try
            {
                foreach(CompanyItems cItem in db.CompanyItems)
                {
                    if(cItem.itemId== itemId)
                    {
                        return cItem;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string addFacebookGroup(string companyName, string groupURL)
        {
            try
            {
                FacebookGroups fbg = new FacebookGroups(companyName, groupURL);
                db.FacebookGroups.Add(fbg);
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string removeFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        public string updateFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }

        public string addFBItem(List<string> colors, string itemType, DateTime lostDate, string location, string decription, string postURL, string publisherName, string type)
        {
            throw new NotImplementedException();
        }

        public string removeFBItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public string updateFBItem(int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string decriptionNew, string postURLNew, string publisherNameNew, string typeNew)
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
