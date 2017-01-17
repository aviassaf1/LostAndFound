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
            ////////////////////////////////////////////////add db to cache
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

            foreach (DataLayer.User user in admins)/////////////////////notice that sometimes u need to user datayer. somthig so it will use the one from datalayer and not domain
            {
                _admins.Add(user.UserName, new Admin(user.UserName, user.password));//add encryption to pass
            }
            foreach (Companies company in companies)
            {
                HashSet<String> FBGroups = new HashSet<string>();
                foreach (FacebookGroups fb in company.FacebookGroups)
                {
                    FBGroups.Add(fb.groupURL);
                }
                _companies.Add(company.userName, new Company(company.userName, company.User.password, company.companyName, company.phone, FBGroups));//add encryption to pass
            }
            foreach (FacebookGroups fbg in facebookGroups)
            {
                _companies[fbg.CompanyName].addFacebookGroup(fbg.groupURL);////not sure if i changed it correctly
            }
            //lost items
            //found items
            //fbitems
            //matches
            setMaxAvialbleItemID();
        }

        internal Company getCompany(string companyName)
        {
            if (_companies.Keys.Contains(companyName))
                return _companies[companyName];
            return null;
        }

        private void setMaxAvialbleItemID()
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
                lostItem.ContactPhone, lostItem.PhotoLocation, lostItem.WasFound);////all of this is defferent from database
        }

        internal void addFoundItem(FoundItem foundItem)
        {
            _foundItems.Add(foundItem.ItemID, foundItem);
            _db.addFoundItem(foundItem.getColorsList(), foundItem.ItemType.ToString(), foundItem.Date, foundItem.Location,
                foundItem.Description, foundItem.SerialNumber, foundItem.CompanyName, foundItem.ContactName,
                foundItem.ContactPhone, foundItem.PhotoLocation, foundItem.Delivered);//all of this is defferent from database
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
            _db.updateMatch(matchID, matchStatus.ToString());//either add all nesseray fields or crate a new method where the company name and the item's id shouldnt be changed
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
            _db.addCompany(_userName, _password, _companyName, _phone, facebookGroups);
        }

        internal void updateFacebbokItem(FBItem fBItem)
        {
            _db.updateFBItem(fBItem.ItemID, fBItem.getColorsList()/*colors isnt a list of string, that should be changed*/, fBItem.ItemType.ToString(), fBItem.Date, fBItem.Location, fBItem.Description,
            fBItem.PostUrl, fBItem.PublisherName, fBItem.Type.ToString());
        }

        internal void addNewFBItemToDB(FBItem fBItem)
        {
            _FBItems.Add(fBItem.ItemID, fBItem);
            _db.addFBItem(fBItem.getColorsList()/*colors isnt a list of string, that should be changed*/, fBItem.ItemType.ToString(), fBItem.Date, fBItem.Location, fBItem.Description,
                fBItem.PostUrl, fBItem.PublisherName, fBItem.Type.ToString());            
        }
    }
}
