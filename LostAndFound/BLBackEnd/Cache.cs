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

        internal void updateFacebbokItem(FBItem fBItem)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        internal Match getMatch(int matchID)
        {
            throw new NotImplementedException();
        }

        internal void removeLostItem(int lostItemID)
        {
            throw new NotImplementedException();
        }

        internal int getAvialbleItemID()
        {
            throw new NotImplementedException();
        }

        internal void addMatch(Match match)
        {
            throw new NotImplementedException();
        }

        internal void updateLostItem(LostItem lostItem)
        {
            throw new NotImplementedException();
        }

        internal void updateUser(string _userName, string _password)
        {
            throw new NotImplementedException();
        }

        internal void addLostItem(LostItem lostItem)
        {
            throw new NotImplementedException();
        }

        internal void addFoundItem(FoundItem foundItem)
        {
            throw new NotImplementedException();
        }

        internal void updateCompanyItem(CompanyItem companyItem)
        {
            throw new NotImplementedException();
        }

        internal void removefoundItem(int foundItemID)
        {
            throw new NotImplementedException();
        }

        internal void updateMatch(int _matchID, int _companyItemID, int _item2ID, MatchStatus _matchStatus)
        {
            throw new NotImplementedException();
        }

        internal void addFacebookGroup(string _companyName, string url)
        {
            throw new NotImplementedException();
        }

        internal void removeFacebookGroup(string _companyName, string url)
        {
            throw new NotImplementedException();
        }

        internal void addNewCompany(string _userName, string _password, string _companyName, string _phone, HashSet<String> facebookGroups)
        {
            throw new NotImplementedException();
        }

        internal void addNewFBItemToDB(FBItem fBItem)
        {
            _FBItems.Add(fBItem.ItemID, fBItem);
//            List<String> colors = new List<string>();

            //_db.addFBItem(fBItem.ItemID,fBItem.Colors, fBItem.ItemType, fBItem.Date, fBItem.Location, fBItem.Description,
              //  fBItem.PostUrl, fBItem.PublisherName, fBItem.Type);
            
        }
    }
}
