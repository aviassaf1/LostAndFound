using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.Managers;

namespace WorkerHost.ServiceLayer.Controllers
{
    class CompanyController : ICompanyController
    {
        private static ICompanyController singleton;
        private static ICompanyManager ICM;
        private CompanyController()
        {
            ICM = ComapanyManager.getInstance;
        }

        public static ICompanyController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new CompanyController();
                }
                return singleton;
            }
        }

        public string addFBGroup( string groupID, int key)
        {
            return ICM.addFBGroup( groupID, key);
        }

        public Dictionary<string, string> getSystemCompanyFBGroup(int key)
        {
            return ICM.getSystemCompanyFBGroup( key);
        }

        public string login(string companyName, string token, String userName, String userPassword)
        {
            return ICM.login(companyName, token , userName,  userPassword);
        }

        public string publishInventory(string GroupID, int days, int key)
        {
            return ICM.publishInventory(GroupID, days,  key);
        }

        public string removeFBGroup(string groupID, int key)
        {
            return ICM.removeFBGroup(groupID, key);
        }

        public string removeWorker(string delUsername, int key)
        {
            return ICM.removeWorker(delUsername, key);
        }

        public string addWorker(string newUsername, string newPassword, bool isManager, int key)
        {
            return ICM.addWorker( newUsername,  newPassword,  isManager,  key);
        }
    }
}
