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
        List<CompanyItem> getAllCompanyItems(String companyName); //returns null if name is not valid
        String addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token);
        String addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token);
        string transactionComplete(int itemID);

        String deleteItem(int itemID);

        String editItem(int itemID, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone);
    }
}
