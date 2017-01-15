using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public interface IAdminManager
    {
        String login();
        String addComapny(String userName, String password, String companyName, String phone, HashSet<String> facebookGroups);
    }
}
