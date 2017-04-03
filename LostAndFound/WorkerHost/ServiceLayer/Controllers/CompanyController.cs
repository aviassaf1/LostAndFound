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

        public string addFBGroup(string companyName, string groupID, int key)
        {
            return ICM.addFBGroup(companyName, groupID, key);
        }

        public Dictionary<string, string> getSystemCompanyFBGroup(string companyName, string token, int key)
        {
            return ICM.getSystemCompanyFBGroup(companyName, token, key);
        }

        public string login(string companyName, string token, String userName, String userPassword)
        {
            return ICM.login(companyName, token , userName,  userPassword);
        }

        public string publishInventory(string token, string GroupID, int days, string companyUserName, int key)
        {
            return ICM.publishInventory(token, GroupID, days, companyUserName, key);
        }

        public string removeFBGroup(string companyName, string groupID, int key)
        {
            return ICM.removeFBGroup(companyName, groupID, key);
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
