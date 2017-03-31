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
    interface IItemController
    {
        [OperationContract]
        List<CompanyItemData> getAllCompanyItems(String companyName); //returns null if name is not valid
        [OperationContract]
        String addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token);
        [OperationContract]
        String addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token);
        [OperationContract]
        string transactionComplete(int itemID);
        [OperationContract]
        String deleteItem(int itemID);
        [OperationContract]
        String editItem(int itemID, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone);
    }
}
