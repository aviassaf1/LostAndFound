using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.ServiceLayer.Controllers
{
    [ServiceContract]
    interface IAdminController
    {
        [OperationContract]
        String login();
        [OperationContract]
        String addComapny(String userName, String password, String companyName, String phone, HashSet<String> facebookGroups,
            String companyProfileID, String managerUserName, String managerPassword, int key);
        [OperationContract]
        String deleteCompany(String companyName, int key);
        [OperationContract]
        String editCompany(String companyName, String password, String phone, int key);
    }
}
