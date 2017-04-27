using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.Domain.Managers;

namespace WorkerHost.Domain.Managers
{
    public class ItemManager : IItemManager
    {
        private static IItemManager singleton;
        private Cache cache = Cache.getInstance;
        private Dictionary<string, Color> enColors = DataType.EnglishColors;
        private Dictionary<string, ItemType> enTypes = DataType.English2EnglishTypes;
        private Logger logger = Logger.getInstance;
        public static IItemManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ItemManager();
                }
                return singleton;
            }
        }

        public List<CompanyItem> getAllCompanyItems(int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "getAllCompanyItems: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "getAllCompanyItems: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            Company company = cache.getCompany(companyName);
            if (company == null)
                return null;
            return company.getAllItems();
        }

        public string addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, string photoLocation, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "addFoundItem: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "PublishInventory: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string token = CompanyManager.getInstance.getToken(companyName);
            if (token == null)
            {
                return null;
            }
            if (sColors == null || sType == null || date == null || location == null || description == null ||
                companyName == null || contactName == null || contactPhone == null ||
                photoLocation == null || token == null)
            {
                logg = "one of or more argument are null, add found itemm failed";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if (cache.getCompany(companyName) == null)
            {
                logg = "add found item fail, company does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            List<Color> colors = new List<Color>();
            foreach (string color in sColors)
            {
                if (!enColors.ContainsKey(color))
                {
                    logg = "add found item fail, there is no color like that";
                    logger.logPrint(logg, 0);
                    logger.logPrint(logg, 2);
                    return logg;
                }
                colors.Add(enColors[color]);
            }
            if (!enTypes.ContainsKey(sType))
            {
                logg = "add found item fail, there is no item type like that";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            ItemType type = enTypes[sType];
            //check Date is not bigger than today
            if (date.CompareTo(DateTime.Now) > 0)
            {
                logg = "add found item: date is invalid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            FoundItem newItem = new FoundItem(colors, type, date, location, description, serialNumber, companyName, contactName,
                contactPhone, photoLocation);
            newItem.addToDB();
            //cache.getCompany(companyName).addFoundItem(newItem.ItemID);
            MatchManager.getInstance.findMatches(newItem, token);
            logg = "add found item: item was added successfully";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }

        public string addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, string photoLocation, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "addLostItem: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "PublishInventory: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string token = CompanyManager.getInstance.getToken(companyName);
            if (token == null)
            {
                return null;
            }
            if (sColors == null || sType == null || date == null || location == null || description == null ||
                companyName == null || contactName == null || contactPhone == null ||
                photoLocation == null || token == null)
            {
                logg = "one of or more argument are null, add lost itemm failed";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if (cache.getCompany(companyName) == null)
            {
                logg = "add lost item fail, company does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            List<Color> colors = new List<Color>();
            foreach (string color in sColors)
            {
                if (!enColors.ContainsKey(color))
                {
                    logg = "add lost item fail, there is no color like that";
                    logger.logPrint(logg, 0);
                    logger.logPrint(logg, 2);
                    return logg;
                }
                colors.Add(enColors[color]);
            }
            if (!enTypes.ContainsKey(sType))
            {
                return "add lost item fail, there is no item type like that";
            }
            ItemType type = enTypes[sType];
            //check Date is not bigger than today
            if (date.CompareTo(DateTime.Now) > 0)
            {
                logg = "add lost item: date is invalid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            LostItem newItem = new LostItem(colors, type, date, location, description, serialNumber, companyName, contactName,
                contactPhone, photoLocation);
            newItem.addToDB();
            //Cache.getInstance.getCompany(companyName).addLostItem(newItem.ItemID);
            MatchManager.getInstance.findMatches(newItem, token);
            logg = "add lost item: item was added successfully";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }

        public string transactionComplete(int itemID, int key)
        {
            CompanyItem item = cache.getCompanyItem(itemID);
            string logg;
            if (item == null)
            {
                logg = "transaction complete: item id was not found";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if ((item.GetType()).Equals(typeof(FoundItem)))
                ((FoundItem)item).Delivered = true;
            if ((item.GetType()).Equals(typeof(LostItem)))
                ((LostItem)item).WasFound = true;
            logg = "transactionComplete: completed successfully";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }

        public string deleteItem(int itemID, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "getCompanyItem: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "getCompanyItem: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }

            CompanyItem item = cache.getCompanyItem(itemID);
            if (item == null)
            {
                logg = "itemID wasn't found";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }

            if (!item.CompanyName.Equals(companyName))
            {
                logg = "someone tried to hack our system";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return "bye bye";
            }
            Company company = cache.getCompany(item.CompanyName);
            if ((item.GetType()).Equals(typeof(FoundItem)))
                return company.removeFoundItem(itemID);
            if ((item.GetType()).Equals(typeof(LostItem)))
                return company.removeLostItem(itemID);
            logg = "delete Item worked";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }

        public string editItem(int itemID, DateTime date, string location, string description, int serialNumber, string contactName, string contactPhone, int key)
        {
            string logg;
            CompanyItem item = cache.getCompanyItem(itemID);
            if (item == null || date == null || location == null || description == null || contactName == null || contactName == null || contactPhone == null || DateTime.Today < date)
            {
                logg = "one or more of the arguments is incorrect, edit item fail";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            logg = item.updateItem(date, location, description, serialNumber, contactName, contactPhone);
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }

        public CompanyItem getCompanyItem(int itemID, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "getCompanyItem: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string companyName = cache.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "getCompanyItem: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            CompanyItem item = cache.getCompanyItem(itemID);
            if (item == null)
            {
                logg = "itemID wasn't found";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
            }
            if (!item.CompanyName.Equals(companyName))
            {
                logg = "someone tried to hack our system";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
            }
            return item;
        }
    }
}
