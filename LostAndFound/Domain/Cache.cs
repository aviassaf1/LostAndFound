using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace Domain.BLBackEnd
{
    public class Cache
    {
        private static IDB _db;

        private Dictionary<string, Admin> _admins;
        private Dictionary<string, Company> _companies;
        private Dictionary<int, LostItem> _lostItems;
        private Dictionary<int, FoundItem> _foundItems;
        private Dictionary<int, FBItem> _FBItems;
        private Dictionary<int, Match> _matches;
        private int maxAvilableComapanyItemID;

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
            _db = Database.getInstance();
            initCache();
        }

        private void initCache()
        {
            _admins = new Dictionary<string, Admin>();
            _companies = new Dictionary<string, Company>();
            _lostItems = new Dictionary<int, LostItem>();
            _foundItems = new Dictionary<int, FoundItem>();
            _FBItems = new Dictionary<int, FBItem>();
            _matches = new Dictionary<int, Match>();

            List<DataLayer.User> admins= _db.getAdminsList();//String userName,String password
            List<Companies> companies = _db.getCompaniesList();//String userName,String password, String companyName, String phone, 
            List<FacebookGroups> facebookGroups= _db.getFBGroupsList();//String companyName, String url, //maybe add group name
            List<LostItems> lostItems = _db.getLostItemsList();//int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description, int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation
            List<FoundItems> foundItems = _db.getFoundItemsList();//int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description, int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation
            List<DataLayer.FBItem> FBItems = _db.getFBItemsList();//int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description, String postUrl, String publisherName, FBType fbType
            List<Matches> matches = _db.getMatchesList();//int matchID, int companyItemID, int item2ID, MatchStatus matchStatus

            foreach (DataLayer.User user in admins)
            {
                _admins.Add(user.UserName, new Admin(user.UserName, user.password));//add encryption to pass
            }

            Dictionary<string, ItemType> HebTypes = new Dictionary<string, ItemType>(){{ "ID" , ItemType.ID }, { "WALLET", ItemType.WALLET },
                { "PCMOUSE", ItemType.PCMOUSE }, { "PC", ItemType.PC }, { "PHONE", ItemType.PHONE }, { "KEYS", ItemType.KEYS }, { "BAG", ItemType.BAG }, { "UMBRELLA", ItemType.UMBRELLA },
                { "SWEATSHIRT", ItemType.SWEATSHIRT }, { "GLASSES", ItemType.GLASSES }, { "SHOES", ItemType.SHOES },{ "FLIPFLOPS", ItemType.FLIPFLOPS },
                { "FOLDER", ItemType.FOLDER }, { "CHARGER", ItemType.CHARGER }, { "EARING", ItemType.EARING }, { "RING", ItemType.RING },
                { "NECKLACE", ItemType.NECKLACE }, { "BRACELET", ItemType.BRACELET }, { "HEADPHONES", ItemType.HEADPHONES }};
            Dictionary<string, FBType> FBTypes = new Dictionary<string, FBType>() { { "FOUND", FBType.FOUND }, { "LOST", FBType.LOST } };
            Dictionary<string, MatchStatus> status = new Dictionary<string, MatchStatus>() { { "POSSIBLE", MatchStatus.POSSIBLE }, { "CORRECT", MatchStatus.CORRECT }, { "COMPLETE", MatchStatus.COMPLETE }, { "INCORRECT", MatchStatus.INCORRECT } };
            Dictionary<string, Color> HebColors = new Dictionary<string, Color>(){{ "PINK" , Color.PINK }, { "BLACK", Color.BLACK }, { "BLUE", Color.BLUE }, { "RED", Color.RED }, 
                { "GREEN", Color.GREEN }, { "YELLOW", Color.YELLOW }, { "WHITE", Color.WHITE },{ "PURPEL", Color.PURPEL }, { "ORANGE", Color.ORANGE }, 
                { "GRAY", Color.GRAY }, { "BROWN", Color.BROWN }, { "GOLD", Color.GOLD }, { "SILVER", Color.SILVER }};
            foreach (LostItems li in lostItems)
            {
                List<Color> colors = new List<Color>();
                foreach(string col in stringToListOfColors(li.colors))
                {
                    colors.Add(HebColors[col]);
                }
                _lostItems.Add(li.itemID, new LostItem(li.itemID, colors, HebTypes[li.itemType], li.lostDate.Value, li.location, li.description,  li.CompanyItems.serialNumber.Value, li.companyName, li.CompanyItems.contactName, li.CompanyItems.contactPhone, li.photoLocation, li.delivered.Value));
            }

            foreach (FoundItems fi in foundItems)
            {
                List<Color> colors = new List<Color>();
                foreach (string col in stringToListOfColors(fi.colors))
                {
                    colors.Add(HebColors[col]);
                }
                _foundItems.Add(fi.itemID, new FoundItem(fi.itemID, colors, HebTypes[fi.itemType], fi.findingDate.Value, fi.location, fi.description, fi.CompanyItems.serialNumber.Value, fi.companyName, fi.CompanyItems.contactName, fi.CompanyItems.contactPhone, fi.photoLocation, fi.delivered.Value));
            }
            foreach (DataLayer.FBItem fbi in FBItems)
            {
                List<Color> colors = new List<Color>();
                foreach (string col in stringToListOfColors(fbi.colors))
                {
                    colors.Add(HebColors[col]);
                }
                _FBItems.Add(fbi.itemID, new FBItem(fbi.itemID, colors, HebTypes[fbi.itemType], fbi.lostDate.Value/* change lost date name*/, fbi.location, fbi.description, fbi.postId, fbi.publisherName, FBTypes[fbi.type]));
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
                    if(li.CompanyName.Equals(company.companyName))
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
            setMaxAvialbleItemID();
        }

        internal CompanyItem getCompanyItem(int itemID)
        {
            if (_lostItems.ContainsKey(itemID))
                return (_lostItems[itemID]);
            if (_foundItems.ContainsKey(itemID))
                return (_foundItems[itemID]);
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

        private void setMaxAvialbleItemID()//sync
        {
            maxAvilableComapanyItemID = 0;
            foreach (int id in _lostItems.Keys)
            {
                maxAvilableComapanyItemID = Math.Max(id, maxAvilableComapanyItemID);
            }
            foreach (int id in _foundItems.Keys)
            {
                maxAvilableComapanyItemID = Math.Max(id, maxAvilableComapanyItemID);
            }
            maxAvilableComapanyItemID = maxAvilableComapanyItemID + 1;
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

        internal void updateFoundItem(FoundItem foundItem)
        {
            _db.updateFoundItem(foundItem.ItemID, foundItem.CompanyName, foundItem.getColorsList(), foundItem.ItemType.ToString(), foundItem.Date, foundItem.Location,
                foundItem.Description, foundItem.PhotoLocation, foundItem.Delivered);
        }

        internal Match getMatch(int matchID)
        {
            return _matches[matchID];
        }

        internal void removeLostItem(int lostItemID)
        {
            if (_lostItems[lostItemID] != null)
            {
                _lostItems.Remove(lostItemID);
                _db.removeLostItem(lostItemID);
            }
        }

        internal int getAvialbleCompanyItemID()//add syncronize
        {
            maxAvilableComapanyItemID++;
            return maxAvilableComapanyItemID - 1;
        }

        internal void addMatch(Match match)
        {
            _matches.Add(match.MatchID, match);
            _db.addMatch(match.CompanyItemID, match.Item2ID, match.MatchStatus.ToString());
        }

        internal void updateLostItem(LostItem lostItem)
        {
            _db.updateLostItem(lostItem.ItemID,lostItem.CompanyName , lostItem.getColorsList(), lostItem.ItemType.ToString(), lostItem.Date, lostItem.Location,
                lostItem.Description, lostItem.PhotoLocation,lostItem.WasFound);
        }

        internal void updateUser(string _userName, string _password)
        {
            _db.updateUser(_userName, _password);
        }

        internal void addLostItem(LostItem lostItem)
        {
            _lostItems.Add(lostItem.ItemID, lostItem);
            _db.addLostItem(lostItem.getColorsList(), lostItem.ItemType.ToString(), lostItem.Date, lostItem.Location,
                lostItem.Description, lostItem.SerialNumber, lostItem.CompanyName, lostItem.ContactName,
                lostItem.ContactPhone, lostItem.PhotoLocation, lostItem.WasFound);
        }

        internal void addFoundItem(FoundItem foundItem)
        {
            _foundItems.Add(foundItem.ItemID, foundItem);
            _db.addFoundItem(foundItem.getColorsList(), foundItem.ItemType.ToString(), foundItem.Date, foundItem.Location,
                foundItem.Description, foundItem.SerialNumber, foundItem.CompanyName, foundItem.ContactName,
                foundItem.ContactPhone, foundItem.PhotoLocation, foundItem.Delivered);
        }

        internal void updateCompanyItem(CompanyItem companyItem)
        {
            _db.updateCompanyItem(companyItem.ItemID, companyItem.SerialNumber, companyItem.ContactName, companyItem.ContactPhone, companyItem.CompanyName);
        }

        internal void removefoundItem(int foundItemID)
        {
            if (_lostItems[foundItemID] != null)
            {
                _lostItems.Remove(foundItemID);
                _db.removeLostItem(foundItemID);
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

        internal void addNewCompany(string _userName, string _password, string _companyName, string _phone, HashSet<String> facebookGroups)
        {
            _companies.Add(_userName, new Company(_userName, _password, _companyName, _phone, facebookGroups));
            _db.addCompany(_userName, _password, _companyName, _phone, facebookGroups);
        }

        internal void updateFacebbokItem(FBItem fBItem)
        {
            _db.updateFBItem(fBItem.ItemID, fBItem.getColorsList(), fBItem.ItemType.ToString(), fBItem.Date, fBItem.Location, fBItem.Description,
            fBItem.PostUrl, fBItem.PublisherName, fBItem.Type.ToString());
        }

        internal void addNewFBItemToDB(FBItem fBItem)
        {
            _FBItems.Add(fBItem.ItemID, fBItem);
            _db.addFBItem(fBItem.getColorsList(), fBItem.ItemType.ToString(), fBItem.Date, fBItem.Location, fBItem.Description,
                fBItem.PostUrl, fBItem.PublisherName, fBItem.Type.ToString());            
        }
        internal List<Item> getAllCompanyItems()
        {
            List<Item> itemList = new List<Item>();
            foreach(LostItem lItem in _lostItems.Values)
            {
                itemList.Add(lItem);
            }
            foreach (FoundItem fItem in _foundItems.Values)
            {
                itemList.Add(fItem);
            }
            return itemList;
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
