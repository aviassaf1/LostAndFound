using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    class Company : User
    {
        private static Cache cache =Cache.getInstance;
        private String _companyName;
        private String _phone;
        private HashSet<int> _lostItems;
        private HashSet<int> _foundItems;
        private HashSet<int> _matches;
        private HashSet<string> _facebookGroups;

        public Company(String userName,String password, String companyName, String phone, HashSet<string> facebookGroups)
        {
            _userName = userName;
            _password = password;
            _companyName = companyName;
            _phone = phone;
            _facebookGroups = facebookGroups;
            _lostItems = new HashSet<int>();
            _foundItems = new HashSet<int>();
            _matches = new HashSet<int>();
            cache.addNewCompany(UserName, Password, _companyName, _phone);
        }
        public Company(String userName, String password, String companyName, String phone, HashSet<string> facebookGroups,
            HashSet<int> lostItems,HashSet<int> foundItems, HashSet<int> matches)
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

        public HashSet<int> FoundItems
        {
            get
            {
                return _foundItems;
            }
        }

        public HashSet<int> Matches
        {
            get
            {
                return _matches;
            }
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
