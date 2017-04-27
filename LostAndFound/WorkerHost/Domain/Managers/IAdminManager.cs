using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;

namespace WorkerHost.Domain.Managers
{
    public interface IAdminManager
    {
        String login(String username, String password);
        String addComapny(String companyName, String phone, HashSet<String> facebookGroups,
            String companyProfileID, String managerUserName, String managerPassword, int key);

        String deleteCompany(String companyName, int key);

        String editCompany(String companyName, String password, String phone, int key);

        List<Company> getAllCompanies(int key);
    }
}
