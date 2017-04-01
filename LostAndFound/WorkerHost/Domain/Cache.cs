using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.DataLayer;
using WorkerHost.Domain.BLBackEnd;
namespace WorkerHost.Domain
{
    public class Cache
    {
        private static IDB _db;

        private Dictionary<string, Admin> _admins;
        private Dictionary<string, Company> _companies;
        private Dictionary<int, LostItem> _lostItems;
        private Dictionary<int, FoundItem> _foundItems;
        private Dictionary<string, Domain.BLBackEnd.FBItem> _FBItems;
        private Dictionary<int, Match> _matches;

        private static Cache singleton;
        public static Cache getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Cache();
                }
                return singleton;
            }
        }

        private Cache()
        {
            try
            {
                _db = Database.getInstance();
                initCache();
            }
            catch
            {
                _admins = new Dictionary<string, Admin>();
                _companies = new Dictionary<string, Company>();
                _lostItems = new Dictionary<int, LostItem>();
                _foundItems = new Dictionary<int, FoundItem>();
                _FBItems = new Dictionary<string, Domain.BLBackEnd.FBItem>();
                _matches = new Dictionary<int, Match>();
                HashSet<string> fbg = new HashSet<string>() { "1538105046204967" };
                //_companies.Add("GuyCompany", new Company("GuyCompany", "guy", "GuyComapany", "050000000",fbg, new HashSet<int>(), new HashSet<int>(), new HashSet<int>()));
            }
        }

        public void setUp()
        {
            Company comp1 = new Company("Guy", "Hello6", "Guy", "05000000", new HashSet<string>());
            Company comp2 = new Company("Guy2", "Hello6", "Guy2", "05000000", new HashSet<string>());
            comp1.addFacebookGroup("1538105046204967");
            comp2.addFacebookGroup("1538105046204967");
            List<Color> colors1 = new List<Color>();
            List<Color> colors2 = new List<Color>();
            colors2.Add(Color.RED);
            List<Color> colors3 = new List<Color>();
            colors3.Add(Color.BLUE);
            colors1.Add(Color.BLACK);
            FoundItem fi1 = new FoundItem(colors1, ItemType.FOLDER, DateTime.Today, "BGU", "bla bla", 8876, "Guy", "Noam", "05000000", "c");
            FoundItem fi2 = new FoundItem(colors2, ItemType.HEADPHONES, DateTime.Today, "BGU", "bla bla", 85656, "Guy", "Noam", "05000000", "c");
            FoundItem fi3 = new FoundItem(colors3, ItemType.FOLDER, DateTime.Today, "BGU", "bla bla", 3376, "Guy", "Noam", "05000000", "c");
            fi1.addToDB();
            fi2.addToDB();
            fi3.addToDB();

            colors1 = new List<Color>();
            colors2 = new List<Color>();
            colors2.Add(Color.RED);
            colors3 = new List<Color>();
            colors3.Add(Color.BLUE);
            colors1.Add(Color.BLACK);
            LostItem li1 = new LostItem(colors1, ItemType.FOLDER, DateTime.Today, "BGU", "bla bla", 8876, "Guy", "Noam", "05000000", "c");
            LostItem li2 = new LostItem(colors2, ItemType.HEADPHONES, DateTime.Today, "BGU", "bla bla", 85656, "Guy", "Noam", "05000000", "c");
            LostItem li3 = new LostItem(colors3, ItemType.FOLDER, DateTime.Today, "BGU", "bla bla", 3376, "Guy", "Noam", "05000000", "c");
            li1.addToDB();
            li2.addToDB();
            li3.addToDB();

            addMatch(new Match(fi1.ItemID, li1.ItemID, MatchStatus.POSSIBLE));
            addMatch(new Match(fi3.ItemID, li3.ItemID, MatchStatus.POSSIBLE));
        }

        public void initCache()//public for test only
        {
            _admins = new Dictionary<string, Admin>();
            _companies = new Dictionary<string, Company>();
            _lostItems = new Dictionary<int, LostItem>();
            _foundItems = new Dictionary<int, FoundItem>();
            _FBItems = new Dictionary<string, Domain.BLBackEnd.FBItem>();
            _matches = new Dictionary<int, Match>();

            List<DataLayer.User> admins = _db.getAdminsList();//String userName,String password
            List<Companies> companies = _db.getCompaniesList();//String userName,String password, String companyName, String phone, 
            List<FacebookGroups> facebookGroups = _db.getFBGroupsList();//String companyName, String url, //maybe add group name
            List<LostItems> lostItems = _db.getLostItemsList();//int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description, int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation
            List<FoundItems> foundItems = _db.getFoundItemsList();//int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description, int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation
            List<DataLayer.FBItem> FBItems = _db.getFBItemsList();//int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description, String postUrl, String publisherName, FBType fbType
            List<Matches> matches = _db.getMatchesList();//int matchID, int companyItemID, int item2ID, MatchStatus matchStatus
            List<> companyUsers = new ;
            foreach (DataLayer.User user in admins)
            {
                _admins.Add(user.UserName, new Admin(user.UserName, user.password));//add encryption to pass
            }

            Dictionary<string, ItemType> HebTypes = DataType.English2EnglishTypes;
            Dictionary<string, FBType> FBTypes = DataType.FBTypes;
            Dictionary<string, MatchStatus> status = DataType.status;
            Dictionary<string, Color> Colors = DataType.EnglishColors;
            foreach (LostItems li in lostItems)
            {
                List<Color> colors = new List<Color>();
                if (li.colors != null)
                {
                    foreach (string col in stringToListOfColors(li.colors))
                    {
                        colors.Add(Colors[col]);
                    }
                }
                _lostItems.Add(li.itemID, new LostItem(li.itemID, colors, HebTypes[li.itemType], li.lostDate.Value, li.location, li.description, li.CompanyItems.serialNumber.Value, li.companyName, li.CompanyItems.contactName, li.CompanyItems.contactPhone, li.photoLocation, li.delivered.Value));
            }

            foreach (FoundItems fi in foundItems)
            {
                List<Color> colors = new List<Color>();
                if (fi.colors != null)
                {
                    foreach (string col in stringToListOfColors(fi.colors))
                    {
                        colors.Add(Colors[col]);
                    }
                }
                _foundItems.Add(fi.itemID, new FoundItem(fi.itemID, colors, HebTypes[fi.itemType], fi.findingDate.Value, fi.location, fi.description, fi.CompanyItems.serialNumber.Value, fi.companyName, fi.CompanyItems.contactName, fi.CompanyItems.contactPhone, fi.photoLocation, fi.delivered.Value));
            }
            foreach (DataLayer.FBItem fbi in FBItems)
            {
                List<Color> colors = new List<Color>();
                if (fbi.colors != null)
                {
                    foreach (string col in stringToListOfColors(fbi.colors))
                    {
                        colors.Add(Colors[col]);
                    }
                }
                _FBItems.Add(fbi.postId, new Domain.BLBackEnd.FBItem(fbi.itemID, colors, HebTypes[fbi.itemType], fbi.lostDate.Value/* change lost date name*/, fbi.location, fbi.description, fbi.postId, fbi.publisherName, FBTypes[fbi.type]));
            }
            foreach (Matches m in matches)
            {
                _matches.Add(m.matchID, new Match(m.matchID, m.companyItemId, m.itemID, status[m.matchStatus]));
            }
            foreach (Companies company in companies)
            {
                HashSet<String> FBGroups = new HashSet<string>();
                foreach (FacebookGroups fb in company.FacebookGroups)
                {
                    FBGroups.Add(fb.groupURL);
                }
                HashSet<int> LostItems = new HashSet<int>();
                foreach (LostItem li in _lostItems.Values)
                {
                    if (li.CompanyName.Equals(company.companyName))
                        LostItems.Add(li.ItemID);
                }
                HashSet<int> FoundItems = new HashSet<int>();
                foreach (FoundItem fi in _foundItems.Values)
                {
                    if (fi.CompanyName.Equals(company.companyName))
                        FoundItems.Add(fi.ItemID);
                }
                HashSet<int> Matches = new HashSet<int>();
                foreach (Match m in _matches.Values)
                {
                    if (LostItems.Contains(m.CompanyItemID) || FoundItems.Contains(m.CompanyItemID))
                        Matches.Add(m.MatchID);
                }
                _companies.Add(company.userName, new Company(company.userName, company.User.password, company.companyName, company.phone, FBGroups, LostItems, FoundItems, Matches));//add encryption to pass
            }
        }

        internal String editCompany(string companyName, string newPassword, string newPhone)
        {
            return _db.updateCompany(companyName, newPassword, newPhone);
        }

        internal String deleteCompany(string _userName, string _companyName)
        {
            if (_companies.ContainsKey(_userName))
            {
                _companies.Remove(_userName);
                return _db.removeCompany(_userName);
            }
            return "Company didn't remove";
        }

        internal CompanyItem getCompanyItem(int itemID)
        {
            if (_lostItems.ContainsKey(itemID))
                return (_lostItems[itemID]);
            if (_foundItems.ContainsKey(itemID))
                return (_foundItems[itemID]);
            return null;
        }

        internal String deleteMatch(int matchID)
        {
            _matches.Remove(matchID);
            return _db.removeMatch(matchID);
        }

        internal BLBackEnd.FBItem getFBItemByPostID(string postID)
        {
            if (_FBItems.ContainsKey(postID))
            {
                return _FBItems[postID];
            }
            return null;

        }

        private List<string> stringToListOfColors(string colors)
        {
            string color = "";
            List<string> colorList = new List<string>();
            for (int i = 0; i < colors.Length; i++)
            {
                if ((i == colors.Length - 1) || colors.ElementAt(i).Equals(","))
                {
                    if (i == colors.Length - 1)
                    {
                        color += colors.ElementAt(i);
                    }
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

        internal Company getCompany(string companyName)
        {
            if (_companies.Keys.Contains(companyName))
                return _companies[companyName];
            return null;
        }



        public void clear()
        {
            if (this._admins != null)
                this._admins.Clear();
            if (this._companies != null)
                this._companies.Clear();
            if (this._foundItems != null)
                this._foundItems.Clear();
            if (this._lostItems != null)
                this._lostItems.Clear();
            if (this._FBItems != null)
                this._FBItems.Clear();
            if (this._matches != null)
                this._matches.Clear();
            _db.clear();
        }

        internal List<Match> getItemMatches(int itemID,string companyName)
        {
            List<Match> matches = new List<Match>();
            Match match;
            foreach(int matchID in _companies[companyName].Matches)
            {
                match = _matches[matchID];
                if (match.CompanyItemID == itemID || match.Item2ID == itemID)
                {
                    matches.Add(match);
                }
            }
            return matches;
        }

        internal void updateFoundItem(FoundItem foundItem)
        {
            _db.updateFoundItem(foundItem.ItemID, foundItem.getColorsList(), foundItem.ItemType.ToString(), foundItem.Date, foundItem.Location,
                foundItem.Description, foundItem.PhotoLocation, foundItem.Delivered);
        }

        internal Match getMatch(int matchID)
        {
            return _matches[matchID];
        }

        internal void removeLostItem(int lostItemID)
        {
            if (_lostItems.ContainsKey(lostItemID))
            {
                _lostItems.Remove(lostItemID);
                _db.removeItem(lostItemID);
            }
        }

        internal void addMatch(Match match)
        {
            int id = _db.addMatch(match.CompanyItemID, match.Item2ID, match.MatchStatus.ToString());
            _matches.Add(id, match);
            match.MatchID = id;
        }

        internal void updateLostItem(LostItem lostItem)
        {
            _db.updateLostItem(lostItem.ItemID, lostItem.getColorsList(), lostItem.ItemType.ToString(), lostItem.Date, lostItem.Location,
                lostItem.Description, lostItem.PhotoLocation, lostItem.WasFound);
        }

        internal void updateUser(string _userName, string _password)
        {
            _db.updateUser(_userName, _password);
        }

        internal void addLostItem(LostItem lostItem)
        {
            int id = _db.addLostItem(lostItem.getColorsList(), lostItem.ItemType.ToString(), lostItem.Date, lostItem.Location,
                lostItem.Description, lostItem.SerialNumber, lostItem.CompanyName, lostItem.ContactName,
                lostItem.ContactPhone, lostItem.PhotoLocation, lostItem.WasFound);
            _lostItems.Add(id, lostItem);
            getCompany(lostItem.CompanyName).addLostItem(id);
            lostItem.ItemID = id;
        }

        internal void addFoundItem(FoundItem foundItem)
        {
            int id = _db.addFoundItem(foundItem.getColorsList(), foundItem.ItemType.ToString(), foundItem.Date, foundItem.Location,
                foundItem.Description, foundItem.SerialNumber, foundItem.CompanyName, foundItem.ContactName,
                foundItem.ContactPhone, foundItem.PhotoLocation, foundItem.Delivered);
            _foundItems.Add(id, foundItem);
            getCompany(foundItem.CompanyName).addFoundItem(id);

            foundItem.ItemID = id;
        }

        internal String updateCompanyItem(CompanyItem companyItem)
        {
            if ((companyItem.GetType()).Equals(typeof(FoundItem)))
                _db.updateFoundItemDescription(companyItem.ItemID, companyItem.Description);
            if ((companyItem.GetType()).Equals(typeof(LostItem)))
                _db.updateLostItemDescription(companyItem.ItemID, companyItem.Description);
            return _db.updateCompanyItem(companyItem.ItemID, companyItem.SerialNumber, companyItem.ContactName, companyItem.ContactPhone);
        }

        internal void removefoundItem(int foundItemID)
        {
            if (_foundItems.ContainsKey(foundItemID))
            {
                _foundItems.Remove(foundItemID);
                _db.removeItem(foundItemID);
            }
        }

        internal void updateMatch(int matchID, MatchStatus matchStatus)
        {
            _db.updateMatch(matchID, matchStatus.ToString());
        }

        internal void addFacebookGroup(string companyName, string url)
        {
            _db.addFacebookGroup(companyName, url);
        }

        internal void removeFacebookGroup(string companyName, string url)
        {
            _db.removeFacebookGroup(companyName, url);
        }

        internal void addNewCompany(Company company)
        {
            _companies.Add(company.UserName, company);
            _db.addCompany(company.UserName, company.Password, company.CompanyName, company.Phone, company.FacebookGroups,);
        }

        internal void updateFacebbokItem(Domain.BLBackEnd.FBItem fBItem)
        {
            _db.updateFBItem(fBItem.ItemID, fBItem.getColorsList(), fBItem.ItemType.ToString(), fBItem.Date, fBItem.Location, fBItem.Description,
            fBItem.PostID, fBItem.PublisherName, fBItem.Type.ToString());
        }

        internal void addNewFBItemToDB(Domain.BLBackEnd.FBItem fBItem)
        {

            int id = _db.addFBItem(fBItem.getColorsList(), fBItem.ItemType.ToString(), fBItem.Date, fBItem.Location, fBItem.Description,
                fBItem.PostID, fBItem.PublisherName, fBItem.Type.ToString());
            _FBItems.Add(fBItem.PostID, fBItem);
            fBItem.ItemID = id;
        }

        internal List<LostItem> getLostItems()
        {
            List<LostItem> itemList = new List<LostItem>();
            foreach (LostItem lItem in _lostItems.Values)
            {
                itemList.Add(lItem);
            }
            return itemList;
        }
        internal List<FoundItem> getFoundItems()
        {
            List<FoundItem> itemList = new List<FoundItem>();
            foreach (FoundItem fItem in _foundItems.Values)
            {
                itemList.Add(fItem);
            }
            return itemList;
        }
    }
}
