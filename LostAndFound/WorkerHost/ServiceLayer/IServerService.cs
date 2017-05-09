using System;
using System.Collections.Generic;
using System.ServiceModel;
using WorkerHost.ServiceLayer.DataContracts;

namespace WorkerHost
{
    [ServiceContract]
    public interface IServerService
    {
        [OperationContract]
        String Adminlogin(String username, String password);
        [OperationContract]
        String addComapny(String companyName, String phone, HashSet<String> facebookGroups,
            String companyProfileID, String managerUserName, String managerPassword, int key);
        [OperationContract]
        String deleteCompany(String companyName, int key);
        [OperationContract]
        String editCompany(String companyName, String password, String phone, int key);
        [OperationContract]
        List<CompanyData> getAllCompanies(int key);





        [OperationContract]
        String login(String token, String userName, String userPassword);
        //Company getCompanyByName(string companyName); //returns null if fails
        [OperationContract]
        String publishInventory(String GroupID, int days, int key);//max days=8, string "true" is ok
        List<WorkerData> getCompanyWorkers(int token);

        //List<Item> getLostItems3Days(string companyName, DateTime date); //returns null if fails
        //List<Item> getFoundItems3Days(string companyName, DateTime date); //returns null if fails
        [OperationContract]
        String addFBGroup(string groupID, int key); //return true if good
        [OperationContract]
        String removeFBGroup(string groupID, int key); //return true if good
        [OperationContract]
        List<GroupData> getSystemCompanyFBGroup(int key); //return null if fails
        //Dictionary<string, string> getAllCompanyFBGroup(string companyName, string token); //return null if fails
        [OperationContract]
        String removeWorker(String delUsername, int key);
        [OperationContract]
        String addWorker(String newUsername, String newPassword, bool isManager, int key);






        [OperationContract]
        List<CompanyItemData> getAllCompanyItems(int key); //returns null if name is not valid
        [OperationContract]
        String addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, string photoLocation, int key);
        [OperationContract]
        String addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, string photoLocation, int key);
        [OperationContract]
        string transactionComplete(int itemID, int key);
        [OperationContract]
        String deleteItem(int itemID, int key);
        [OperationContract]
        String editItem(int itemID, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, int key);





        [OperationContract]
        String changeMatchStatus(int matchID, string statusNum, int key);
        [OperationContract]
        List<MatchData> getMatchesByItemID(int itemID, int key);

        [OperationContract]
        CompanyItemData getCompanyItem(int itemID, int key);

        /*/[OperationContract]
        //string testClass1(string color, string type, string name, string phone);

        [OperationContract]
        String addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, string photoLocation, int key);
        //string login(string text1, string text2, bool @checked, string fbToken);
        */
    }
}