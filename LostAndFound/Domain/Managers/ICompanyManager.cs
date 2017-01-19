using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;

namespace Domain.Managers
{
    public interface ICompanyManager
    {
        String login(String companyName, String token);
        Company getCompanyByName(string companyName);
        String publishInventory(String token, String GroupID, int days, string compaynUserName);//max days=8
        List<LostItem> getLostItems3Days(string companyName, DateTime date);
        List<FoundItem> getFoundItems3Days(string companyName, DateTime date);
    }
}
