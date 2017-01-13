using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;

namespace BLBackEnd
{
    class Cache
    {
        private static Database _db;

        private Dictionary<string, Admin> _admins;
        private Dictionary<string, Company> _companies;
        private Dictionary<int, LostItem> _lostItems;
        private Dictionary<int, FoundItem> _foundItems;
        private Dictionary<int, FBItem> _FBItems;
        private Dictionary<int, Match> _matches;
        private int maxAvilableComapanyItemID;

        private static Cache singleton;

        private Cache()
        {
            _admins = new Dictionary<string, Admin>();
            _companies = new Dictionary<string, Company>();
            _lostItems = new Dictionary<int, LostItem>();
            _foundItems = new Dictionary<int, FoundItem>();
            _FBItems = new Dictionary<int, FBItem>();
            _matches = new Dictionary<int, Match>();
            _db = Database.getInstance;
            ////////////////////////////////////////////////add db to cache
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
            _db.updateFoundItem(foundItem.ItemID, foundItem.Colors, foundItem.ItemType, foundItem.Date, foundItem.Location,
                foundItem.Description, foundItem.PhotoLocation,foundItem.Delivered);
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
            _db.addMatch(match.MatchID, match.CompanyItemID, match.Item2ID, match.MatchStatus.ToString);
        }

        internal void updateLostItem(LostItem lostItem)
        {
            _db.updateLostItem(lostItem.ItemID, lostItem.Colors, lostItem.ItemType, lostItem.Date, lostItem.Location,
                lostItem.Description, lostItem.PhotoLocation,lostItem.WasFound);
        }

        internal void updateUser(string _userName, string _password)
        {
            _db.updateUser(_userName, _password);
        }

        internal void addLostItem(LostItem lostItem)
        {
            _lostItems.Add(lostItem.ItemID, lostItem);
            _db.addLostItem(lostItem.ItemID, lostItem.Colors, lostItem.ItemType, lostItem.Date, lostItem.Location,
                lostItem.Description, lostItem.SerialNumber, lostItem.CompanyName, lostItem.ContactName,
                lostItem.ContactPhone, lostItem.PhotoLocation, lostItem.WasFound);
        }

        internal void addFoundItem(FoundItem foundItem)
        {
            _foundItems.Add(foundItem.ItemID, foundItem);
            _db.addFoundItem(foundItem.ItemID, foundItem.Colors, foundItem.ItemType, foundItem.Date, foundItem.Location,
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
            _db.updateMatch(matchID, matchStatus.ToString);
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
            _db.addNewCompany(_userName, _password, _companyName, _phone, facebookGroups);
        }

        internal void updateFacebbokItem(FBItem fBItem)
        {
            _db.updateFBItem(fBItem.ItemID, fBItem.Colors, fBItem.ItemType, fBItem.Date, fBItem.Location, fBItem.Description,
            fBItem.PostUrl, fBItem.PublisherName, fBItem.Type.ToString);
        }

        internal void addNewFBItemToDB(FBItem fBItem)
        {
            _FBItems.Add(fBItem.ItemID, fBItem);
            _db.addFBItem(fBItem.ItemID, fBItem.Colors, fBItem.ItemType.ToString, fBItem.Date, fBItem.Location, fBItem.Description,
                fBItem.PostUrl, fBItem.PublisherName, fBItem.Type.ToString);
//            List<String> colors = new List<string>();

            //_db.addFBItem(fBItem.ItemID,fBItem.Colors, fBItem.ItemType, fBItem.Date, fBItem.Location, fBItem.Description,
              //  fBItem.PostUrl, fBItem.PublisherName, fBItem.Type);
            
        }
    }
}
