using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.Managers;

namespace WorkerHost.ServiceLayer.Controllers
{
    class AdminController : IAdminController
    {
        private static IAdminController singleton;
        private static IAdminManager IAM;
        private AdminController()
        {
            IAM = AdminManager.getInstance;
        }

        public static IAdminController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new AdminController();
                }
                return singleton;
            }
        }

        public string addComapny(string userName, string password, string companyName, string phone, HashSet<string> facebookGroups,
            String companyProfileID, String managerUserName, String managerPassword, int key)
        {
            return IAM.addComapny(userName, password, companyName, phone, facebookGroups, companyProfileID, managerUserName, managerPassword, key);
        }

        public string deleteCompany(string companyName, int key)
        {
            return IAM.deleteCompany(companyName, key);
        }

        public string editCompany(string companyName, string password, string phone, int key)
        {
            return IAM.editCompany(companyName, password, phone, key);
        }

        public string login(String username, String password)
        {
            return IAM.login(username, password);
        }
    }
}
