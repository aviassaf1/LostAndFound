using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.Managers;
using WorkerHost.ServiceLayer.DataContracts;

namespace WorkerHost.ServiceLayer.Controllers
{
    class CompanyController : ICompanyController
    {
        private static ICompanyController singleton;
        private static ICompanyManager ICM;
        private CompanyController()
        {
            ICM = CompanyManager.getInstance;
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

        public List<GroupData> getSystemCompanyFBGroup(int key)
        {
            Dictionary<string, string> fbs=ICM.getSystemCompanyFBGroup( key);
            List<GroupData> fbgs = new List<GroupData>();
            foreach(string id in fbs.Keys)
            {
                fbgs.Add(new GroupData(fbs[id], id));
            }
            return fbgs;
        }

        public string login( string token, String userName, String userPassword)
        {
            return ICM.login(token , userName,  userPassword);
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
