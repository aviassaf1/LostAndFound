using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    class Company : User
    {
        private String _companyName;
        private String _phone;
        private HashSet<int> _lostItems;
        private HashSet<int> _foundItems;
        private HashSet<int> _matches;
        private HashSet<string> _facebookGroups;

        public Company(String userName,String password, String companyName, String phone, HashSet<string> facebookGroups)
        {
            _userName = userName;
            Password = password;
            _companyName = companyName;
            _phone = phone;
            _facebookGroups = facebookGroups;
            _lostItems = new HashSet<int>();
            _foundItems = new HashSet<int>();
            _matches = new HashSet<int>();
            cache.addNewCompany(UserName, Password, _companyName, _phone,facebookGroups);
        }
        public Company(String userName, String password, String companyName, String phone, HashSet<string> facebookGroups,
            HashSet<int> lostItems,HashSet<int> foundItems, HashSet<int> matches)
        {
            _userName = userName;
            Password = password;
            _companyName = companyName;
            _phone = phone;
            _facebookGroups = facebookGroups;
            _lostItems = lostItems;
            _foundItems = foundItems;
            _matches = matches;
        }

        public HashSet<int> LostItems
        {
            get
            {
                return _lostItems;
            }
        }
        public Boolean addLostItem(int lostItemID)
        {
            if (_lostItems.Contains(lostItemID))
                return false;
            _lostItems.Add(lostItemID);
            return true;
        }
        public Boolean removeLostItem(int lostItemID)
        {
            if (!_lostItems.Contains(lostItemID))
                return false;
            _lostItems.Remove(lostItemID);
            cache.removeLostItem(lostItemID);
            return true;
        }
        public Boolean addLostItem(LostItem lostItem)
        {
            int id=cache.getAvialbleCompanyItemID();
            lostItem.ItemID = id;
            _lostItems.Add(id);
            cache.addLostItem(lostItem);
            return true;
        }

        public HashSet<int> FoundItems
        {
            get
            {
                return _foundItems;
            }
        }
        public Boolean addFoundItem(int foundItemID)
        {
            if (_foundItems.Contains(foundItemID))
                return false;
            _foundItems.Add(foundItemID);
            return true;
        }
        public Boolean removeFoundItem(int foundItemID)
        {
            if (!_foundItems.Contains(foundItemID))
                return false;
            _foundItems.Remove(foundItemID);
            cache.removefoundItem(foundItemID);
            return true;
        }
        public Boolean addFoundItem(FoundItem foundItem)
        {
            int id = cache.getAvialbleCompanyItemID();
            foundItem.ItemID = id;
            _foundItems.Add(id);
            cache.addFoundItem(foundItem);
            return true;
        }

        public HashSet<int> Matches
        {
            get
            {
                return _matches;
            }
        }
        public Boolean addMatch(int matchID)
        {
            if (Matches.Contains(matchID))
                return false;
            Match match = cache.getMatch(matchID);
            if (match != null &&(_lostItems.Contains(match.CompanyItemID) || _foundItems.Contains(match.CompanyItemID))) 
            {
                Matches.Add(matchID);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean removeMatch(int matchID)
        {
            if (!Matches.Contains(matchID))
                return false;
            Matches.Remove(matchID);
            return true;
        }


        public string Phone
        {
            get
            {
                return _phone;
            }
        }

        public string CompanyName
        {
            get
            {
                return _companyName;
            }
        }

        public HashSet<string> FacebookGroups
        {
            get
            {
                return _facebookGroups;
            }
        }
        public Boolean addFacebookGroup(string url)
        {
            if (!_facebookGroups.Contains(url))
            {
                cache.addFacebookGroup(_companyName, url);
                _facebookGroups.Add(url);
                return true;
            }
            return false;
        }
        public Boolean removeFacebookGroup(string url)
        {
            if (_facebookGroups.Contains(url))
            {
                cache.removeFacebookGroup(_companyName, url);
                _facebookGroups.Remove(url);
                return true;
            }
            return false;
        }
    }
}
