﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Database : IDB
    {
        private static Database singleton;
        private Entities db;

        private Database()
        {

        }

        public static Database getInstance()
        {
                if (singleton == null)
                {
                    singleton = new Database();
                    singleton.initializeDB();
                    singleton.clear();
                }
                return singleton;
        }
        public void setUp() 
        {
            db.SaveChanges();
            User admin = new User();
            admin.UserName = "testAdmin";
            admin.password = "testpassAdmin";
            admin.isAdmin = true;
            //db.User = new System.Data.Entity.DbSet<User>();
            db.User.Add(admin);
            DataLayer.User user = new DataLayer.User();
            user.UserName = "testUser";
            user.password = "testpassUser";
            user.isAdmin = false;
            db.User.Add(user);
            Companies company = new Companies();
            company.User = user;
            user.Companies.Add(company);
            company.userName = user.UserName;
            //company.phone = "testphone";
            company.companyName = "testCompany";
           // db.SaveChanges();
            //db.SaveChanges();
            Items item = new Items();
            db.Items.Add(item);
            db.SaveChanges();
            item.CompanyItems = new CompanyItems();
            item.CompanyItems.itemId = item.itemID;
            item.CompanyItems.Companies = company;
            company.CompanyItems.Add(item.CompanyItems);
            item.CompanyItems.FoundItems = new FoundItems();
            item.CompanyItems.FoundItems.itemID = item.itemID;
            item.CompanyItems.FoundItems.CompanyItems = item.CompanyItems;
            db.SaveChanges();
            CompanyItems cItem = new CompanyItems();
            item.CompanyItems = cItem;
            cItem.Items = item;
            //cItem.itemId = item.itemID;
            cItem.Companies = company;
            company.CompanyItems.Add(cItem);
            cItem.companyName = company.companyName;
            //db.CompanyItems.Add(cItem);
            db.SaveChanges();
            FoundItems fItem = new FoundItems();
            //fItem.itemID = item.itemID;
            fItem.companyName = company.companyName;
            cItem.FoundItems = fItem;
            fItem.CompanyItems = cItem;
            //companyItems.Add(cItem);
            //db.CompanyItems.Add(cItem);
            //db.FoundItems.Add(fItem);
            db.SaveChanges();
            FacebookGroups fbg = new FacebookGroups();
            fbg.Companies = company;
            fbg.CompanyName = company.companyName;
            fbg.groupURL = "urlTest";
            company.FacebookGroups.Add(fbg);
            db.SaveChanges();

        }
        private Boolean initializeDB()
        {
            try
            {
                this.db = new Entities();
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                int index = baseDir.IndexOf("LostAndFound");
                string dataDir = baseDir.Substring(0, index) + "LostAndFound\\LostAndFound\\";
                AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
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
            /*
            User user = new User();
            user.UserName = "testUser";
            user.password = "testpass";
            user.isAdmin = true;
            Companies company = new Companies();
            company.User = user;
            company.userName = user.UserName;
            company.phone = "testphone";
            company.companyName = "testCompany";
            List <CompanyItems>  companyItems = new List<CompanyItems>();
            company.CompanyItems = companyItems;
            CompanyItems cItem = new CompanyItems();
            cItem.Companies = company;
            cItem.companyName = company.companyName;
            cItem.contactName = "";
            cItem.contactPhone = "";
            cItem.FoundItems = null;
            cItem.itemId = 15;
            Items item = new Items();
            item.itemID = 15;
            item.CompanyItems = cItem;
            cItem.Items = item;
            company.CompanyItems.Add(cItem);
            company.FacebookGroups = new LinkedList<FacebookGroups>();
            FacebookGroups fbg = new FacebookGroups();
            fbg.Companies = company;
            fbg.CompanyName = company.companyName;
            fbg.groupURL = "urlTest";
            company.FacebookGroups.Add(fbg);
            user.Companies = new LinkedList<Companies>();
            user.Companies.Add(company);

    */
            //  db.User.Add(new User());
            User user = new User();
            user.UserName = "user2";
            user.password = "123";
            db.User.Add(user);
            Companies company = new Companies();
            company.companyName = "comp1";
            company.User = user;
            // db.User = new LinkedList<User>();
            //db.User = new System.Data.Entity.DbSet<User>();
            
            user.Companies.Add(company);
            //Console.WriteLine(db.User);
           // while (true) ;
            //company.User = user;
            db.SaveChanges();
            //Companies comp = findCompanyByCompanyName(company.companyName);
            //Console.WriteLine(comp.User.UserName);
            //user.Companies.Remove(comp);
            //db.Companies.Remove(comp);
            Items item = new Items();
            db.Items.Add(item);
            user.password = "456";
            db.SaveChanges();
            int x = item.itemID;
            int y = x;
            //while (true) ;
            
            //db.User.Remove(user);
            //db.User.re;
            //db.SaveChanges();
            //db.di
        }
        public void clear()
        {
            try
            {
                clearUsers();
                //cache.clear();
            }
            catch
            {
                
            }
        }
        private void clearFbGroups()
        {
            try
            {
                List<FacebookGroups> fbgList = new List<FacebookGroups>();
                foreach (FacebookGroups fbg in db.FacebookGroups)
                {
                    fbgList.Add(fbg);
                }
                foreach (Companies company in db.Companies)
                {
                    company.FacebookGroups = new List<FacebookGroups>();
                }
                foreach (FacebookGroups fbg in fbgList)
                {
                    db.FacebookGroups.Remove(fbg);
                }
                db.SaveChanges();
            }
            catch
            {

            }            
        }
        private void clearFoundItems()
        {
            try
            {
                List<FoundItems> fItemList = new List<FoundItems>();
                foreach (FoundItems fItem in db.FoundItems)
                {
                    fItemList.Add(fItem);
                }
                foreach (CompanyItems cItem in db.CompanyItems)
                {
                    cItem.FoundItems = null;
                }
                foreach (FoundItems fItem in fItemList)
                {
                    db.FoundItems.Remove(fItem);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }

        private void clearLostItems()
        {
            try
            {
                List<LostItems> lItemList = new List<LostItems>();
                
                foreach (LostItems lItem in db.LostItems)
                {
                    lItemList.Add(lItem);
                }
                foreach (CompanyItems cItem in db.CompanyItems)
                {
                    cItem.LostItems = null;
                }
                foreach (LostItems lItem in lItemList)
                {
                    db.LostItems.Remove(lItem);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }

        private void clearFbItems()
        {
            try
            {
                List<FBItem> fbItemList = new List<FBItem>();
                foreach (FBItem fbItem in db.FBItem)
                {
                    fbItemList.Add(fbItem);
                }
                foreach (Items item in db.Items)
                {
                    item.FBItem = null;
                }
                foreach (FBItem fbItem in fbItemList)
                {
                    db.FBItem.Remove(fbItem);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }
        private void clearMatches()
        {
            try
            {
                List<Matches> matchList = new List<Matches>();
                foreach (Matches match in db.Matches)
                {
                    matchList.Add(match);
                }
                foreach (Items item in db.Items)
                {
                    item.Matches = new List<Matches>();
                }
                foreach (CompanyItems cItem in db.CompanyItems)
                {
                    cItem.Matches = new List<Matches>();
                }
                foreach (Matches match in matchList)
                {
                    db.Matches.Remove(match);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }
        private void clearCompanyItems()
        {
            try
            {
                clearFoundItems();
                clearLostItems();
                clearMatches();
                List<CompanyItems> cItemList = new List<CompanyItems>();
                foreach (CompanyItems cItem in db.CompanyItems)
                {
                    cItemList.Add(cItem);
                }
                foreach (Items item in db.Items)
                {
                    item.CompanyItems = null;
                }
                foreach (Companies company in db.Companies)
                {
                    company.CompanyItems = new List<CompanyItems>();
                }
                foreach (CompanyItems cItem in cItemList)
                {
                    db.CompanyItems.Remove(cItem);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }
        private void clearItems()
        {
            try
            {
                clearMatches();
                clearFbItems();
                clearCompanyItems();
                List<Items> itemList = new List<Items>();
                foreach (Items item in db.Items)
                {
                    itemList.Add(item);
                }
                foreach (Items item in itemList)
                {
                    db.Items.Remove(item);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }

        private void clearCompanies()
        {
            try
            {
                clearItems();
                clearFbGroups();
                List<Companies> companyList = new List<Companies>();
                foreach (Companies company in db.Companies)
                {
                    companyList.Add(company);
                }
                foreach (User user in db.User)
                {
                    user.Companies = new List<Companies>();
                }
                foreach(Companies company in companyList)
                {
                    db.Companies.Remove(company);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }

        private void clearUsers()
        {
            try
            {
                clearCompanies();
                List<User> userList = new List<User>();
                foreach (User user in db.User)
                {
                    userList.Add(user);
                }
                foreach (User user in userList)
                {
                    db.User.Remove(user);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public bool findIdTests()
        {
            Items item = new Items();
            db.Items.Add(item);
            db.SaveChanges();
            int id = item.itemID;
            Items reItem = findItemByItemId(id);
            if (reItem != item)
            {
                return false;
            }
            reItem = findItemByItemId(id+1);
            if (reItem != null)
            {
                return false;
            }
            return true;
        }


        public string addCompany(string userName, string password, string companyName, string phone, HashSet<string> facebookGroups)
        {
            try
            {
                if (findCompanyByCompanyName(companyName) != null)
                {
                    return "company already exists";
                }
                Companies company = new Companies();
                company.userName = userName;
                company.companyName = companyName;
                company.phone = phone;
                User user = findUserByUserName(userName);
                if (user != null)
                {
                    return "user already exists in the system";
                }
                addUser(userName, password, false);
                user = findUserByUserName(userName);
                user.Companies.Add(company);
                company.User = user;
                if (facebookGroups != null)
                {
                    foreach (string url in facebookGroups)
                    {
                        FacebookGroups fbg = new FacebookGroups();
                        fbg.CompanyName = companyName;
                        fbg.groupURL = url;
                        company.FacebookGroups.Add(fbg);
                        fbg.Companies = company;
                    }
                }
                //db.Companies.Add(company);
                db.SaveChanges();
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string removeCompany(string companyName)
        {
            try
            {
                Companies company = findCompanyByCompanyName(companyName);
                if (company == null)
                {
                    return "company does not exist";
                }
                if (company.FacebookGroups != null)
                {
                    List<FacebookGroups> fbgList = company.FacebookGroups.ToList();
                    foreach (FacebookGroups fbg in fbgList)
                    {
                        removeFacebookGroup(companyName, fbg.groupURL);
                    }
                }
                if (company.CompanyItems != null)
                {
                    List<CompanyItems> cItemList = company.CompanyItems.ToList();
                    foreach (CompanyItems cItem in cItemList)
                    {
                        removeItem(cItem.itemId);
                    }
                }
                User user = findUserByUserName(company.userName);
                company.User = null;
                user.Companies.Remove(company);
                db.Companies.Remove(company);
                db.User.Remove(user);
                db.SaveChanges();
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string updateCompany(string companyName, string passwordNew, string phoneNew)
        {
            try
            {
                Companies company = findCompanyByCompanyName(companyName);
                User userNew = findUserByUserName(company.userName);
                if(userNew==null)
                {
                    return "the new user does not exist in the system";
                }
                userNew.password=passwordNew;
                company.phone = phoneNew;
                User oldUser = company.User;
                oldUser.Companies.Remove(company);
                userNew.Companies.Add(company);
                company.User = userNew;
                db.SaveChanges();
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

        public int AddCompanyItem(int serialNumber, string contactName, string contactPhone, string companyName)
        {
            try
            {
                Items item = new Items();
                db.SaveChanges();
                CompanyItems cItem = new CompanyItems();
                Companies company = findCompanyByCompanyName(companyName);
                if (company == null)
                {
                    return -2;
                }
                
                db.SaveChanges();
                company.CompanyItems.Add(cItem);
                item.CompanyItems = cItem;
                cItem.Items = item;
                cItem.itemId = item.itemID;
                cItem.Companies = company;
                cItem.companyName = companyName;
                cItem.contactName = contactName;
                cItem.contactPhone = contactPhone;
                cItem.serialNumber = serialNumber;
                db.SaveChanges();
                return cItem.itemId;
            }
            catch
            {
                return -1;
            }
        }


        public string updateCompanyItem(int itemId, int serialNumberNew, string contactNameNew, string contactPhoneNew)
        {
            try
            {
                CompanyItems cItem = findItemByItemId(itemId).CompanyItems;
                if(cItem==null)
                {
                    return "item does not exist in the system";
                }
                cItem.serialNumber = serialNumberNew;
                cItem.contactName = contactNameNew;
                cItem.contactPhone = contactPhoneNew;
                db.SaveChanges();
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string addFacebookGroup(string companyName, string groupURL)
        {
            try
            {
                FacebookGroups fbg = new FacebookGroups();
                Companies company = findCompanyByCompanyName(companyName);
                if (groupURL.Equals(""))
                {
                    return "the given url is empty";
                }
                if (company == null)
                {
                    return "company does not exist";
                }
                company.FacebookGroups.Add(fbg);
                fbg.Companies = company;
                fbg.groupURL = groupURL;
                fbg.CompanyName = companyName;
                db.SaveChanges();
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string removeFacebookGroup(string companyName, string groupURL)
        {
            try
            {
                FacebookGroups fbg = findFacebookGroup(companyName, groupURL);
                if (fbg == null)
                {
                    return "couldnt find the facebook group matching the company name and group url";
                }
                Companies company = findCompanyByCompanyName(companyName);
                company.FacebookGroups.Remove(fbg);
                db.FacebookGroups.Remove(fbg);
                db.SaveChanges();
                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        private string updateFacebookGroup(string companyName, string groupURL)
        {
            throw new NotImplementedException();
        }
        public FacebookGroups findFacebookGroup(string companyName, string groupUrl)
        {
            try
            {
                foreach(FacebookGroups fbg in db.FacebookGroups)
                {
                    if(fbg.CompanyName.Equals(companyName) && fbg.groupURL.Equals(groupUrl))
                    {
                        return fbg;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private string listOfColorsToString(List<string> colors)
        {
            string str = "";
            foreach(string color in colors)
            {
                str = str + "," + color;
            }
            if (!str.Equals(""))
            {
                str = str.Substring(1);
            }
            return str;
        }
        private List<string> stringToListOfColors(string colors)
        {
            string color = "";
            List<string> colorList = new List<string>();
            for(int i=0; i<colors.Length; i++)
            {
                if ((i == colors.Length - 1) || colors.ElementAt(i).Equals(","))
                {
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
        public Items findItemByItemId(int itemId)
        {
            try
            {
                foreach (Items item in db.Items)
                {
                    if (item.itemID == itemId)
                    {
                        return item;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public int addFBItem(List<string> colors, string itemType, DateTime lostDate, string location, string decription, string postId, string publisherName, string type)
        {
            Items item = new Items();
            db.Items.Add(item);
            db.SaveChanges();
            FBItem fbItem = new FBItem();
            fbItem.itemID = item.itemID;
            item.FBItem = fbItem;
            fbItem.Items = item;
            db.SaveChanges();
            try
            {
                fbItem.colors = null;
                if (colors != null)
                {
                    fbItem.colors = listOfColorsToString(colors);
                }
                //fbItem.description = decription;
                db.SaveChanges();
                fbItem.location = location;
                db.SaveChanges();
                fbItem.lostDate = lostDate;
                fbItem.postId = postId;
                db.SaveChanges();
                fbItem.publisherName = publisherName;
                db.SaveChanges();
                fbItem.type = type;
                //item.FBItem = fbItem;
                //db.FBItem.Add(fbItem);
                db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return fbItem.itemID;
        }



        public string updateFBItem(int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string decriptionNew, string postURLNew, string publisherNameNew, string typeNew)
        {
            try
            {
                FBItem fbItem = findItemByItemId(itemId).FBItem;
                if (fbItem == null)
                {
                    return "the item was found but it is not a fbItem";
                }
                fbItem.colors = listOfColorsToString(colorsNew);
                fbItem.location = locationNew;
                fbItem.lostDate = lostDateNew;
                fbItem.itemType = typeNew;
                fbItem.postId = postURLNew;
                fbItem.publisherName = publisherNameNew;
                fbItem.type = typeNew;
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            return "true";
        }

        public int addFoundItem(List<string> colors, string itemType, DateTime findingDate, string location, string description, int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, bool wasFound)
        {
            FoundItems fItem = new FoundItems();
            try
            {
                int citemId = AddCompanyItem(serialNumber, contactName, contactPhone, companyName);
                CompanyItems cItem = findItemByItemId(citemId).CompanyItems;
                if (cItem == null)
                {
                    return -2;
                }
                fItem.findingDate = findingDate;
                if (colors != null)
                {
                    fItem.colors = listOfColorsToString(colors);
                }
                fItem.companyName = companyName;
                fItem.delivered = wasFound;
                fItem.description = description;
                fItem.itemID = cItem.itemId;
                fItem.itemType = itemType;
                fItem.location = location;
                fItem.photoLocation = photoLocation;
                cItem.FoundItems = fItem;
                fItem.CompanyItems = cItem;
                db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return fItem.itemID;
        }

        public string updateFoundItem(int itemId,  List<string> colorsNew, string itemTypeNew, DateTime findingDateNew, string locationNew, string descriptionNew, string photoLocationNew, bool deliveredNew)
        {
            try
            {
                Items item = findItemByItemId(itemId);
                FoundItems fItem = item.CompanyItems.FoundItems;
                if (fItem == null)
                {
                    return "item was found but it is ot a found item of a company";
                }
                fItem.findingDate = findingDateNew;
                fItem.colors = listOfColorsToString(colorsNew);
                fItem.delivered = deliveredNew;
                fItem.description = descriptionNew;
                fItem.itemType = itemTypeNew;
                fItem.location = locationNew;
                fItem.photoLocation = photoLocationNew;
                updateCompanyItem(itemId, item.CompanyItems.serialNumber.Value, item.CompanyItems.contactName, item.CompanyItems.contactPhone);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }

        public string removeItem(int itemId)
        {
            try
            {
                Items item = findItemByItemId(itemId);
                if (item == null)
                {
                    return "item was not found in the system";
                }
                
                List<Matches> itemMatchesList = item.Matches.ToList();
                foreach(Matches match in itemMatchesList)
                {
                    removeMatch(match.matchID);
                }
                //item.FBItem.Items = null;
                item.FBItem = null;
                //item.CompanyItems.FoundItems.CompanyItems = null;
                //item.CompanyItems.FoundItems = null;
                //item.CompanyItems.LostItems.CompanyItems = null;
                //item.CompanyItems.LostItems = null;
                //item.CompanyItems.Items = null;
                item.CompanyItems = null;
                db.Items.Remove(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            
            return "true";
        }

        public int addLostItem(List<string> colors, string itemType, DateTime lostDate, string location, string description, int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, bool wasFound)
        {
            LostItems lItem = new LostItems();
            try
            {
                int citemId = AddCompanyItem(serialNumber, contactName, contactPhone, companyName);
                CompanyItems cItem = findItemByItemId(citemId).CompanyItems;
                if (cItem == null)
                {
                    return -2;
                }
                lItem.lostDate = lostDate;
                if (colors != null)
                {
                    lItem.colors = listOfColorsToString(colors);
                }
                lItem.companyName = companyName;
                lItem.delivered = wasFound;
                lItem.description = description;
                lItem.itemID = cItem.itemId;
                lItem.itemType = itemType;
                lItem.location = location;
                lItem.photoLocation = photoLocation;
                cItem.LostItems = lItem;
                lItem.CompanyItems = cItem;
                db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return lItem.itemID;
        }

        public string updateLostItem(int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string descriptionNew, string photoLocationNew, bool deliveredNew)
        {
            try
            {
                Items item = findItemByItemId(itemId);
                LostItems lItem = item.CompanyItems.LostItems;
                if (lItem == null)
                {
                    return "item was found but it is ot a found item of a company";
                }
                lItem.lostDate = lostDateNew;
                if (colorsNew != null)
                {
                    lItem.colors = listOfColorsToString(colorsNew);
                }
                lItem.delivered = deliveredNew;
                lItem.description = descriptionNew;
                lItem.itemType = itemTypeNew;
                lItem.location = locationNew;
                lItem.photoLocation = photoLocationNew;
                updateCompanyItem(itemId, item.CompanyItems.serialNumber.Value, item.CompanyItems.contactName, item.CompanyItems.contactPhone);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }

        public int addMatch(int companyItemId, int itemID, string matchStatus)
        {
            Matches match = new Matches();
            try
            {
                CompanyItems cItem = findItemByItemId(companyItemId).CompanyItems;
                if (cItem == null)
                {
                    return -2;
                }
                Items item = findItemByItemId(itemID);
                if(cItem == null)
                {
                    return -3;
                }
                match.companyItemId = companyItemId;
                match.itemID = itemID;
                match.matchStatus = matchStatus;
                cItem.Matches.Add(match);
                item.Matches.Add(match);
                match.CompanyItems = cItem;
                match.Items = item;
                db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return match.matchID;
        }

        public string removeMatch(int matchId)
        {
            try
            {
                Matches match = findMathByMatchId(matchId);
                match.CompanyItems.Matches.Remove(match);
                match.CompanyItems = null;
                match.Items.Matches.Remove(match);
                match.Items = null;
                db.Matches.Remove(match);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }

        public string updateMatch(int matchId, string matchStatusNew)
        {
            try
            {
                Matches match = findMathByMatchId(matchId);
                match.matchStatus = matchStatusNew;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }
        public Matches findMathByMatchId(int matchId)
        {
            try
            {
                foreach(Matches match in db.Matches)
                {
                    if (match.matchID == matchId)
                    {
                        return match;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string addUser(string userName, string password, bool isAdmin)
        {
            try
            {
                if (userName.Equals(""))
                {
                    return "user name is empty";
                }
                User user = new User();
                user.UserName = userName;
                user.password = password;
                user.isAdmin = isAdmin;
                db.User.Add(user);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }

        public string removeUser(string userName)
        {
            try
            {
                User user = findUserByUserName(userName);
                if(user==null)
                {
                    return "user does not exist in the system";
                }
                List<Companies> companyList = db.Companies.ToList();
                foreach(Companies company in companyList)
                {
                    removeCompany(company.companyName);
                }
                //db.User.Remove(user);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }

        public string updateUser(string userName, string newPassword)
        {
            try
            {
                User user = findUserByUserName(userName);
                if (user == null)
                {
                    return "user does not exist in the system";
                }
                user.password = newPassword;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }
        public User findUserByUserName(string userName)
        {
            try
            {
                foreach (User user in db.User)
                {
                    if (user.UserName.Equals(userName))
                    {
                        return user;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<User> getAdminsList()
        {
            try
            {
                List<User> userList = new List<User>();
                foreach (User user in db.User)
                {
                    userList.Add(user);
                }
                List<User> retList = new List<User>();
                foreach(User user in userList)
                {
                    if (user.isAdmin.Value)
                    {
                        retList.Add(user);
                    }
                }
                return retList;
            }
            catch
            {
                return null;
            }
        }

        public List<Companies> getCompaniesList()
        {
            try
            {
                List<Companies> companyList = db.Companies.ToList();
                return companyList;
            }
            catch
            {
                return null;
            }
        }

        public List<FacebookGroups> getFBGroupsList()
        {
            try
            {
                List<FacebookGroups> facebookGroupsList = db.FacebookGroups.ToList();
                return facebookGroupsList;
            }
            catch
            {
                return null;
            }
        }

        public List<LostItems> getLostItemsList()
        {
            try
            {
                List<LostItems> itemList = db.LostItems.ToList();
                return itemList;
            }
            catch
            {
                return null;
            }
        }

        public List<FoundItems> getFoundItemsList()
        {
            try
            {
                List<FoundItems> itemList = db.FoundItems.ToList();
                return itemList;
            }
            catch
            {
                return null;
            }
        }

        public List<FBItem> getFBItemsList()
        {
            try
            {
                List<FBItem> itemList = db.FBItem.ToList();
                return itemList;
            }
            catch
            {
                return null;
            }
        }

        public List<Matches> getMatchesList()
        {
            try
            {
                List<Matches> matchList = db.Matches.ToList();
                return matchList;
            }
            catch
            {
                return null;
            }
        }
        public List<Items> getItemsList()
        {
            try
            {
                List<Items> itemList = db.Items.ToList();
                return itemList;
            }
            catch
            {
                return null;
            }
        }

        public string updateFoundItemDescription(int itemID, string description)
        {
            try
            {
                Items item = findItemByItemId(itemID);
                FoundItems fItem = item.CompanyItems.FoundItems;
                if (fItem == null)
                {
                    return "item was found but it is ot a found item of a company";
                }
                fItem.description = description;
                updateCompanyItem(itemID, item.CompanyItems.serialNumber.Value, item.CompanyItems.contactName, item.CompanyItems.contactPhone, item.CompanyItems.companyName);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }

        public String updateLostItemDescription(int itemID, string description)
        {
            try
            {
                Items item = findItemByItemId(itemID);
                LostItems lItem = item.CompanyItems.LostItems;
                if (lItem == null)
                {
                    return "item was found but it is ot a found item of a company";
                }
                lItem.description = description;
                updateCompanyItem(itemID, item.CompanyItems.serialNumber.Value, item.CompanyItems.contactName, item.CompanyItems.contactPhone, item.CompanyItems.companyName);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "true";
        }
    }
}
