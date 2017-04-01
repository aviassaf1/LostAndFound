﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.ServiceLayer.Controllers
{
    [ServiceContract]
    interface ICompanyController
    {
        [OperationContract]
        String login(String companyName, String token);
        //Company getCompanyByName(string companyName); //returns null if fails
        [OperationContract]
        String publishInventory(String token, String GroupID, int days, string companyUserName);//max days=8, string "true" is ok
        //List<Item> getLostItems3Days(string companyName, DateTime date); //returns null if fails
        //List<Item> getFoundItems3Days(string companyName, DateTime date); //returns null if fails
        [OperationContract]
        String addFBGroup(string companyName, string groupID); //return true if good
        [OperationContract]
        String removeFBGroup(string companyName, string groupID); //return true if good
        [OperationContract]
        Dictionary<string, string> getSystemCompanyFBGroup(string companyName, string token); //return null if fails
        //Dictionary<string, string> getAllCompanyFBGroup(string companyName, string token); //return null if fails

    }
}