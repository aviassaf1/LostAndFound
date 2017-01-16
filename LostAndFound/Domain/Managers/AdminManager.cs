using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public class AdminManager : IAdminManager
    {
        private static IAdminManager singleton;

        public static IAdminManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new AdminManager();
                }
                return singleton;
            }
        }
        public string addComapny(string userName, string password, string companyName, string phone, HashSet<string> facebookGroups)
        {
            throw new NotImplementedException();
        }

        public string login()
        {
            throw new NotImplementedException();
        }
    }
}
