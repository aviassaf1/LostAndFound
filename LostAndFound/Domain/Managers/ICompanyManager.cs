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
        List<Item> getLostItems3Days(string companyName, DateTime date);
        List<Item> getFoundItems3Days(string companyName, DateTime date);
        String addFBGroup(string companyName, string groupID);
        String removeFBGroup(string companyName, string groupID);
        Dictionary<string, string> getSystemCompanyFBGroup(string companyName, string token);
        Dictionary<string, string> getAllCompanyFBGroup(string companyName, string token);


    }
}
