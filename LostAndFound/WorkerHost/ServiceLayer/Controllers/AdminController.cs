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

        public string addComapny(string userName, string password, string companyName, string phone, HashSet<string> facebookGroups)
        {
            return IAM.addComapny(userName, password, companyName, phone, facebookGroups);
        }

        public string deleteCompany(string companyName)
        {
            return IAM.deleteCompany(companyName);
        }

        public string editCompany(string companyName, string password, string phone)
        {
            return IAM.editCompany(companyName, password, phone);
        }

        public string login()
        {
            return IAM.login();
        }
    }
}
