using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;

namespace Domain.Managers
{
    public class ItemManager : IItemManager
    {
        private static IItemManager singleton;
        private Cache cache = Cache.getInstance;
        private Dictionary<string, Color> enColors = new Dictionary<string, Color>(){{ "PINK" , Color.PINK }, { "BLACK", Color.BLACK }, { "BLUE", Color.BLUE }, { "RED", Color.RED },
                { "GREEN", Color.GREEN }, { "YELLOW", Color.YELLOW }, { "WHITE", Color.WHITE },{ "PURPEL", Color.PURPEL }, { "ORANGE", Color.ORANGE },
                { "GRAY", Color.GRAY }, { "BROWN", Color.BROWN }, { "GOLD", Color.GOLD }, { "SILVER", Color.SILVER }};
        private Dictionary<string, ItemType> enTypes = new Dictionary<string, ItemType>(){{ "ID" , ItemType.ID }, { "WALLET", ItemType.WALLET },
                { "PCMOUSE", ItemType.PCMOUSE }, { "PC", ItemType.PC }, { "PHONE", ItemType.PHONE }, { "KEYS", ItemType.KEYS }, { "BAG", ItemType.BAG }, { "UMBRELLA", ItemType.UMBRELLA },
                { "SWEATSHIRT", ItemType.SWEATSHIRT }, { "GLASSES", ItemType.GLASSES }, { "SHOES", ItemType.SHOES },{ "FLIPFLOPS", ItemType.FLIPFLOPS },
                { "FOLDER", ItemType.FOLDER }, { "CHARGER", ItemType.CHARGER }, { "EARING", ItemType.EARING }, { "RING", ItemType.RING },
                { "NECKLACE", ItemType.NECKLACE }, { "BRACELET", ItemType.BRACELET }, { "HEADPHONES", ItemType.HEADPHONES }};

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

        public List<CompanyItem> getAllCompanyItems(string companyName)
        {
            throw new NotImplementedException();
        }

        public string addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation)
        {
            List<Color> colors = new List<Color>();
            foreach (string color in sColors)
            {
                colors.Add(enColors[color]);
            }
            ItemType type = enTypes[sType];
            //check Date is not bigger than today
            if (date.CompareTo(DateTime.Now) > 0)
            {
                return "add found item: date is invalid";
            }
            FoundItem newItem = new FoundItem(colors, type, date, location, description, serialNumber, companyName, contactName, 
                contactPhone, photoLocation);
            newItem.addToDB();
            return "add found item: item was added successfully";
        }

        public string addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation)
        {
            List<Color> colors = new List<Color>();
            foreach (string color in sColors)
            {
                colors.Add(enColors[color]);
            }
            ItemType type = enTypes[sType];
            //check Date is not bigger than today
            if (date.CompareTo(DateTime.Now) > 0)
            {
                return "add lost item: date is invalid";
            }
            LostItem newItem = new LostItem(colors, type, date, location, description, serialNumber, companyName, contactName,
                contactPhone, photoLocation);
            newItem.addToDB();
            return "add lost item: item was added successfully";
        }

        public string transactionComplete(int itemID)
        {
            CompanyItem item = cache.getCompanyItem(itemID);
            if (item == null)
                return "transaction complete: item id was not found";
            if ((item.GetType()).Equals(typeof(FoundItem)))
                ((FoundItem)item).Delivered = true;
            if ((item.GetType()).Equals(typeof(LostItem)))
                ((LostItem)item).WasFound = true;
            return "transactionComplete: completed successfully";
        }
    }
}
