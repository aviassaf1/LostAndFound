﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;

namespace WorkerHost.Domain.Managers
{
    public interface IItemManager
    {
        List<CompanyItem> getAllCompanyItems(String companyName, int key); //returns null if name is not valid
        String addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token, int key);
        String addFoundItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token, int key);
        string transactionComplete(int itemID, int key);

        String deleteItem(int itemID, int key);

        String editItem(int itemID, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, int key);
    }
}
