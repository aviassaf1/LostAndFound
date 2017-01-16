using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace Domain.Managers
{
    public class ComapanyManager :ICompanyManager
    {
        private Dictionary<String ,int > _FBTokens;//company name, token

        public string addFoundItem()
        {
            throw new NotImplementedException();
        }

        public string addLostItem()
        {
            throw new NotImplementedException();
        }

        public string getCompanyByName()
        {
            throw new NotImplementedException();
        }

        public string login()
        {
            throw new NotImplementedException();
        }

        public string publishInventory(string token, string GroupID, int days, string companyUserName)
        {
            var fb = new FacebookClient(token);
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            //get inventory from db
            string inventory = "inv";
            dynamic result = fb.Post(GroupID + "/feed", new { message = inventory });
            return "true";
        }
    }
}
