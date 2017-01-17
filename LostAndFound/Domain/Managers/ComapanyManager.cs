using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;
using Facebook;

namespace Domain.Managers
{
    public class ComapanyManager :ICompanyManager
    {


        private Dictionary<String ,String > _FBTokens=new Dictionary<string, string>();//company name, token
        private static ICompanyManager singleton;

        public static ICompanyManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ComapanyManager();
                }
                return singleton;
            }
        }




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

        public String login(String companyName, String token)
        {
            //check if company exist
            if (_FBTokens.ContainsKey(companyName))
                _FBTokens[companyName] = token;
            else {
                _FBTokens.Add(companyName, token);
            }
            return "login was succeeded";
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

        public List<Item> getLostItems3Days(string companyName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Item> getFoundItems3Days(string companyName, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
