using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    class Cache
    {
        //private Dictionary<string, User> _users;
        private static Cache singleton;

        private Cache()
        {
            //_users = new Dictionary<string, User>();
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

        internal void updateFoundItem(FoundItem foundItem)
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

        public void clear()
        {
//            if (this._forums != null)
//                this._forums.Clear();            

        }

        internal void addFacebookGroup(string _companyName, string url)
        {
            throw new NotImplementedException();
        }

        internal void removeFacebookGroup(string _companyName, string url)
        {
            throw new NotImplementedException();
        }

        internal void addNewCompany(string _userName, string _password, string _companyName, string _phone)
        {
            throw new NotImplementedException();
        }

        internal void addNewFBItemToDB(FBItem fBItem)
        {
            throw new NotImplementedException();
        }
    }
}
