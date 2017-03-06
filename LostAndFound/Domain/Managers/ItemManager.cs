﻿using System;
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
        private Dictionary<string, Color> enColors = DataType.EnglishColors;
        private Dictionary<string, ItemType> enTypes = DataType.English2EnglishTypes;

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
            return cache.getCompany(companyName).getAllItems();
        }

        public string addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token)
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
            cache.getCompany(companyName).addFoundItem(newItem.ItemID);
            MatchManager.getInstance.findMatches(newItem, token);
            return "add found item: item was added successfully";
        }

        public string addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token)
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
            Cache.getInstance.getCompany(companyName).addLostItem(newItem.ItemID);
            MatchManager.getInstance.findMatches(newItem, token);
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

        public string deleteItem(int itemID)
        {
            CompanyItem item = cache.getCompanyItem(itemID);
            if (item == null)
                return "itemID wasn't found";
            Company company = cache.getCompany(item.CompanyName);
            if ((item.GetType()).Equals(typeof(FoundItem)))
                return company.removeFoundItem(itemID);
            if ((item.GetType()).Equals(typeof(LostItem)))
                return company.removeLostItem(itemID);
            return "";
        }

        public string editItem(int itemID, DateTime date, string location, string description, int serialNumber, string contactName, string contactPhone, string photoLocation)
        {
            CompanyItem item = cache.getCompanyItem(itemID);
            return item.updateItem( date,  location,  description,  serialNumber,  contactName,  contactPhone);
        }
    }
}
