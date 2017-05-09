using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.ServiceLayer.DataContracts;

namespace WorkerHost.ServiceLayer.Controllers
{
    [ServiceContract]
    public interface ICompanyController
    {
        [OperationContract]
        String login(String token, String userName, String userPassword);
        //Company getCompanyByName(string companyName); //returns null if fails
        [OperationContract]
        String publishInventory( String GroupID, int days, int key);//max days=8, string "true" is ok
        //List<Item> getLostItems3Days(string companyName, DateTime date); //returns null if fails
        //List<Item> getFoundItems3Days(string companyName, DateTime date); //returns null if fails
        [OperationContract]
        String addFBGroup(string groupID, int key); //return true if good
        [OperationContract]
        String removeFBGroup( string groupID, int key); //return true if good
        [OperationContract]
        List<GroupData> getSystemCompanyFBGroup( int key); //return null if fails
        //Dictionary<string, string> getAllCompanyFBGroup(string companyName, string token); //return null if fails
        [OperationContract]
        String removeWorker(String delUsername, int key);
        [OperationContract]
        String addWorker(String newUsername, String newPassword, bool isManager, int key);
        List<WorkerData> getCompanyWorkers(int token);
    }
}
