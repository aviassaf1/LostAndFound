using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;

namespace Domain.Managers
{
    public interface IItemManager
    {
        List<CompanyItem> getAllCompanyItems(String companyName);
    }
}
