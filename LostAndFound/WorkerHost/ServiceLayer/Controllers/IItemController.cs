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
    public interface IItemController
    {
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
        CompanyItemData getCompanyItem(int itemID, int key);
    }
}
