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
        String updateCompany(string companyName, string userNameNew, string phoneNew);
        Companies findCompanyByCompanyName(string companyName);
        int AddCompanyItem(int serialNumber, string contactName, string contactPhone, string companyName);
        String removeCompanyItem(int itemId);
        String updateCompanyItem(int itemId, int serialNumberNew, string contactNameNew, string contactPhoneNew, string companyNameNew);
        CompanyItems findCompanyItemByItemId(int itemId);
        String addFacebookGroup(string companyName, string groupURL);
        String removeFacebookGroup(string companyName, string groupURL);
        String updateFacebookGroup(string companyName, string groupURL);//not clear waht should be change url, companyName or 2?...
        FacebookGroups findFacebookGroup(string companyName, string groupUrl);
        Items findItemByItemId(int itemId);
        int addFBItem(List<string> colors, string itemType, DateTime lostDate, string location, string decription, string postId, string publisherName, string type);
        String removeFBItem(int itemId);
        String updateFBItem(int itemId, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string decriptionNew, string postIdNew, string publisherNameNew, string typeNew);
        int addFoundItem(List<string> colors, string itemType, DateTime findingDate, string location,
                string description, int serialNumber, string companyName, string contactName,
                string contactPhone, string photoLocation, bool wasFound);
        String removeFoundItem(int itemId);
        String updateFoundItem(int itemId, string companyNameNew, List<string> colorsNew, string itemTypeNew, DateTime findingDateNew, string locationNew, string descriptionNew, string photoLocationNew, bool deliveredNew);
        String removeItem(int itemId);
        int addLostItem(List<string> colors, string itemType, DateTime lostDate, string location,
                string description, int serialNumber, string companyName, string contactName,
                string contactPhone, string photoLocation, bool wasFound);
        String removeLostItem(int itemId);
        String updateLostItem(int itemId, string companyNameNew, List<string> colorsNew, string itemTypeNew, DateTime lostDateNew, string locationNew, string descriptionNew, string photoLocationNew, bool deliveredNew);
        int addMatch(int companyItemId, int itemID, string matchStatus);
        String removeMatch(int matchId);
        String updateMatch(int matchId, string matchStatusNew);
        Matches findMathByMatchId(int matchId);
        String addUser(string userName, string password, bool isAdmin);
        String removeUser(string userName);
        String updateUser(string userName, string newPassword);
        User findUserByUserName(string userName);
        List<User> getAdminsList();
        List<Companies> getCompaniesList();
        List<FacebookGroups> getFBGroupsList();
        List<LostItems> getLostItemsList();
        List<FoundItems> getFoundItemsList();
        List<FBItem> getFBItemsList();
        List<Matches> getMatchesList();
        List<Items> getItemsList();







    }
}
