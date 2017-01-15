using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string publishInventory(string token, string GroupID, int days)
        {
            throw new NotImplementedException();
        }
    }
}
