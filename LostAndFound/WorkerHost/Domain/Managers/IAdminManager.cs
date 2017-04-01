using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.Domain.Managers
{
    public interface IAdminManager
    {
        String login();
        String addComapny(String userName, String password, String companyName, String phone, HashSet<String> facebookGroups,
            String companyProfileID, String managerUserName, String managerPassword);

        String deleteCompany(String companyName);

        String editCompany(String companyName, String password, String phone);
    }
}
