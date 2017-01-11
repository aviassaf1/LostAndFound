using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    interface IDB
    {
        void clear();
        Boolean AddCompany(string adderUserName,string userName, string companyName, string phone);
        Boolean deleteCompany(string deleterUserName, string companyName);
        Boolean editCompany(string editerUserName, string oldCompanyName, string UserNameNew, string CompanyNameNew, string PhoneNew);
        Boolean AddCompanyItem(string adderUserName, int itemId, int serialNumber, string contactName, string contactPhone, string companyName);
        Boolean deleteCompanyItem(string deleterUserName, int itemId);
        Boolean editCompanyItem(string editerUserName, int itemId, int serialNumberNew, string contactNameNew, string contactPhoneNew, string companyNameNew);
        Boolean addFBGroups(string adderUserName, string companyName, string groupURL);
        Boolean deleteFBGroups(string deleterUserName, string companyName, string groupURL);
        Boolean editFBGroups(string editerUserName, string companyName, string groupURL);
        Boolean addFBItem(string adderUserName, int itemId, List<string> colors, string itemType, DateTime lostDate, string location, string decription, string postURL, string publisherName, string type);
        Boolean deleteFBItem(string deleterUserName, int itemId);
        Boolean editFBItem(string adderUserName, int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string decriptionNew, string postURLNew, string publisherNameNew, string typeNew);





    }
}
