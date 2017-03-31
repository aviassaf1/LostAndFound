using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.Domain.Managers;
using WorkerHost.ServiceLayer.DataContracts;

namespace WorkerHost.ServiceLayer.Controllers
{
    class ItemController : IItemController
    {
        private static IItemController singleton;
        private static IItemManager IIM;
        private ItemController()
        {
            IIM = ItemManager.getInstance;
        }

        public static IItemController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ItemController();
                }
                return singleton;
            }
        }

        public string addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description, int serialNumber, string companyName,
            string contactName, string contactPhone, string photoLocation, string token)
        {
            return IIM.addFoundItem(sColors, sType, date, location, description, serialNumber, companyName, contactName, contactPhone, photoLocation, token);
        }

        public string addLostItem(List<string> sColors, string sType, DateTime date, string location, string description, int serialNumber, string companyName, 
            string contactName, string contactPhone, string photoLocation, string token)
        {
            return IIM.addLostItem(sColors, sType, date, location, description, serialNumber, companyName, contactName, contactPhone, photoLocation, token);
        }

        public string deleteItem(int itemID)
        {
            return IIM.deleteItem(itemID);
        }

        public string editItem(int itemID, DateTime date, string location, string description, int serialNumber, string contactName, string contactPhone)
        {
            return IIM.editItem( itemID,  date,  location,  description,  serialNumber,  contactName,  contactPhone);
        }

        public List<CompanyItemData> getAllCompanyItems(string companyName)
        {
            List<CompanyItemData> res = new List<CompanyItemData>();
            List<CompanyItem> items = IIM.getAllCompanyItems(companyName);
            bool stat;
            string type;
            foreach ( CompanyItem i in items)
            {
                
                if (i.GetType() == typeof(FoundItem))
                {
                    stat = ((FoundItem)i).Delivered;
                    type = "found";
                }
                else 
                {
                    stat = ((LostItem)i).WasFound;
                    type = "lost";
                }
                res.Add(new CompanyItemData(i.ItemID, i.getColorsList(), i.ItemType.ToString(), i.Location, i.Date, i.Description,
                    i.SerialNumber, i.CompanyName, i.ContactName, i.ContactPhone, stat, type));
            }
            return res;
        }

        public string transactionComplete(int itemID)
        {
            return IIM.transactionComplete(itemID);
        }
    }
}
