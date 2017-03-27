﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.Domain.BLBackEnd
{
    public class Company : User
    {
        private String _companyName;
        private String _phone;
        private HashSet<int> _lostItems;
        private HashSet<int> _foundItems;
        private HashSet<int> _matches;
        private HashSet<string> _facebookGroups;

        public Company(String userName, String password, String companyName, String phone, HashSet<string> facebookGroups)
        {
            _userName = userName;
            _password = password;
            _companyName = companyName;
            _phone = phone;
            _facebookGroups = facebookGroups;
            _lostItems = new HashSet<int>();
            _foundItems = new HashSet<int>();
            _matches = new HashSet<int>();
            cache.addNewCompany(this);
        }
        public Company(String userName, String password, String companyName, String phone, HashSet<string> facebookGroups,
            HashSet<int> lostItems, HashSet<int> foundItems, HashSet<int> matches)
        {
            _userName = userName;
            _password = password;
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
        public String removeLostItem(int lostItemID)
        {
            if (!_lostItems.Contains(lostItemID))
                return "The company doesn't have that item"; ;
            _lostItems.Remove(lostItemID);
            cache.removeLostItem(lostItemID);
            return "Item Removed";
        }

        internal List<CompanyItem> getAllItems()
        {
            List<CompanyItem> items = new List<CompanyItem>();
            foreach (int li in _lostItems)
            {
                items.Add(cache.getCompanyItem(li));
            }
            foreach (int fi in _foundItems)
            {
                items.Add(cache.getCompanyItem(fi));
            }
            return items;
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

        internal List<Match> getComapanyMatches()
        {
            List<Match> matches = new List<Match>();
            foreach (int mID in _matches)
            {
                matches.Add(cache.getMatch(mID));
            }
            return matches;
        }

        public String removeFoundItem(int foundItemID)
        {
            if (!_foundItems.Contains(foundItemID))
                return "The company doesn't have that item";
            _foundItems.Remove(foundItemID);
            cache.removefoundItem(foundItemID);
            return "Item Removed";
        }

        internal String delete()
        {
            foreach (string group in FacebookGroups.ToArray())
            {
                cache.removeFacebookGroup(_companyName, group);
            }
            foreach (int matchID in Matches.ToArray())
            {
                removeMatch(matchID);
            }
            foreach (int foundItem in FoundItems.ToArray())
            {
                removeFoundItem(foundItem);
            }
            foreach (int lostItem in LostItems.ToArray())
            {
                removeLostItem(lostItem);
            }
            return cache.deleteCompany(_userName, _companyName);
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
            if (match != null && (_lostItems.Contains(match.CompanyItemID) || _foundItems.Contains(match.CompanyItemID)))
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
            cache.deleteMatch(matchID);
            return true;
        }

        internal String edit(string password, string phone)
        {
            _password = password;
            _phone = phone;
            return cache.editCompany(_companyName, password, phone);
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
        public List<LostItem> getAllLostItems()
        {
            List<LostItem> items = new List<LostItem>();
            foreach (int item in _lostItems)
            {
                items.Add((LostItem)(cache.getCompanyItem(item)));
            }
            return items;
        }
        public List<FoundItem> getAllFoundItems()
        {
            List<FoundItem> items = new List<FoundItem>();
            foreach (int item in _foundItems)
            {
                items.Add((FoundItem)(cache.getCompanyItem(item)));
            }
            return items;
        }
    }
}