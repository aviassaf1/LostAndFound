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

        public string addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description, int serialNumber,
            string contactName, string contactPhone, string photoLocation, int key)
        {
            return IIM.addFoundItem(sColors, sType, date, location, description, serialNumber, contactName, contactPhone, photoLocation,  key);
        }

        public string addLostItem(List<string> sColors, string sType, DateTime date, string location, string description, int serialNumber, 
            string contactName, string contactPhone, string photoLocation, int key)
        {
            return IIM.addLostItem(sColors, sType, date, location, description, serialNumber, contactName, contactPhone, photoLocation, key);
        }

        public string deleteItem(int itemID, int key)
        {
            return IIM.deleteItem(itemID, key);
        }

        public string editItem(int itemID, DateTime date, string location, string description, int serialNumber, string contactName, string contactPhone, int key)
        {
            return IIM.editItem( itemID,  date,  location,  description,  serialNumber,  contactName,  contactPhone, key);
        }

        public List<CompanyItemData> getAllCompanyItems(int key)
        {
            List<CompanyItem> items = IIM.getAllCompanyItems(key);
            if (items == null)
                return null;
            List<CompanyItemData> res = new List<CompanyItemData>();
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
                res.Add(new CompanyItemData(i.ItemID, i.getHebColorsList(),  DataType.EnglishTypes2Hebrew[i.ItemType], i.Location, i.Date, i.Description,
                    i.SerialNumber, i.CompanyName, i.ContactName, i.ContactPhone, stat, type));
            }
            return res;
        }

        public string transactionComplete(int itemID, int key)
        {
            return IIM.transactionComplete(itemID, key);
        }

        public CompanyItemData getCompanyItem(int itemID, int key)
        {
            bool stat;
            string type;
            CompanyItem i = IIM.getCompanyItem(itemID, key);
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
            return new CompanyItemData(i.ItemID, i.getHebColorsList(), DataType.EnglishTypes2Hebrew[i.ItemType], i.Location, i.Date, i.Description,
                i.SerialNumber, i.CompanyName, i.ContactName, i.ContactPhone, stat, type);
        }
    }
}
