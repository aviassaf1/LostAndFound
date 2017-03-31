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

        public string addFBGroup(string companyName, string groupID)
        {
            return ICM.addFBGroup(companyName, groupID);
        }

        public Dictionary<string, string> getSystemCompanyFBGroup(string companyName, string token)
        {
            return ICM.getSystemCompanyFBGroup(companyName, token);
        }

        public string login(string companyName, string token)
        {
            return ICM.login(companyName, token);
        }

        public string publishInventory(string token, string GroupID, int days, string companyUserName)
        {
            return ICM.publishInventory(token, GroupID, days, companyUserName);
        }

        public string removeFBGroup(string companyName, string groupID)
        {
            return ICM.removeFBGroup(companyName, groupID);
        }
    }
}
