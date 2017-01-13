using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDB
    {
        void clear();
        String addCompany(string userName, string password, string companyName, string phone, HashSet<String> facebookGroups);
        String removeCompany(string companyName);
        String updateCompany(string UserNameNew, string CompanyNameNew, string PhoneNew);
        String AddCompanyItem(int serialNumber, string contactName, string contactPhone, string companyName);
        String removeCompanyItem(int itemId);
        String updateCompanyItem(int itemId, int serialNumberNew, string contactNameNew, string contactPhoneNew, string companyNameNew);
        String addFacebookGroup(string companyName, string groupURL);
        String removeFacebookGroup(string companyName, string groupURL);
        String updateFacebookGroup(string companyName, string groupURL);
        String addFBItem(List<string> colors, string itemType, DateTime lostDate, string location, string decription, string postURL, string publisherName, string type);
        String removeFBItem(int itemId);
        String updateFBItem(int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string decriptionNew, string postURLNew, string publisherNameNew, string typeNew);
        String addFoundItem(string companyName, string photoLocation, List<string> colors, string itemType, DateTime findingDate, string location, string description, bool delivered);
        String removeFoundItem(string itemId);
        String updateFoundItem(string itemId, string companyNameNew, List<string> colorsNew, string itemTypeNew, DateTime findingDateNew, string locationNew, string descriptionNew, string photoLocationNew, bool deliveredNew);
        String addItem(int itemId);
        String removeItem(int itemId);
        String addLostItem(string companyName,  List<string> colors, string itemType, DateTime lostDate, string location, string description, string photoLocation, bool delivered);
        String removeLostItem(int itemId);
        String updateLostItem(int itemId, string companyNameNew, string photoLocationNew, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string descriptionNew, bool deliveredNew);
        String addMatch(int companyItemId, int itemID, string matchStatus);
        String removeMatch(string matchId);
        String updateMatch(string matchId, string companyItemIdNew, string itemIDNew, string matchStatusNew);
        String addUser(string userName, string password, bool isAdmin);
        String removeUser(string userName);
        String updateUser(string userName, string newPassword, bool isAdminNew);








    }
}
