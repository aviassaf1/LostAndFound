using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public interface ICompanyManager
    {
        String login();
        String addLostItem();
        String addFoundItem();
        
    }
}
