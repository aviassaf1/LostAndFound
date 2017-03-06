﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.Managers;
using Domain.BLBackEnd;
using DataLayer;
using System.Collections.Generic;

namespace Test.UnitTests
{
    [TestClass]
    public class CompanyManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private ICompanyManager ICM;
        private const string TRUESTRING = "true";
        private const string TOKEN = "EAACEdEose0cBAPHaC5zRwZCmSZCquCbxrCEjZAZBRUKB9JNA7ZBTHJQjEIIMOa9S3lXavZA1l9Hgw96XUax89b1pefzS4P2FsN0givVkG87eENOWwTZAOchXBbGck3tXrJOc88oPI0gEmahQPxYZAtCTu0BAVKVIzFPFIRPOW3tHHkjjylS42pedVFqxZCjZAUHVMZD"; //TODO: use valid token 
        private const string GID = "1538105046204967";
        private const string CName = "Guy";


        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();

            ICM = ComapanyManager.getInstance;

            cache.setUp();

        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }

        [TestMethod]
        public void TestGetComapnyByNameValidName()
        {
            string companyName = CName;
            Company c = ICM.getCompanyByName(companyName);
            Assert.IsNotNull(c);
            Assert.AreEqual(companyName, c.CompanyName);
        }

        [TestMethod]
        public void TestGetComapnyByNameNotValidName()
        {
            string companyName = "NotGoodName";
            Company c = ICM.getCompanyByName(companyName);
            Assert.IsNull(c);
        }

        [TestMethod]
        public void PublishInvetoryAllValid()
        {
            string ans = ICM.publishInventory(TOKEN, GID, 1, CName);
            Assert.AreEqual(ans, TRUESTRING);
        }

        [TestMethod]
        public void PublishInvetoryInvalidArgs()
        {
            string ans = ICM.publishInventory(TOKEN, "invalidGID", 1, CName);
            Assert.AreEqual(ans, "PublishInventory: post to facebook failed");
            
            ans = ICM.publishInventory(TOKEN, GID, 98, CName);
            Assert.AreEqual(ans, "PublishInventory: days is more than MAXDAYS");

            ans = ICM.publishInventory(TOKEN, GID, 1, "notACompany");
            Assert.AreEqual(ans, "PublishInventory: companyName is invalid");
        }

        [TestMethod]
        public void PublishInvetoryNullArgs()
        {
            string ans = ICM.publishInventory(null, GID, 1, CName);
            Assert.AreEqual(ans, "PublishInventory: values cannot be null");

            ans = ICM.publishInventory(TOKEN, null, 1, CName);
            Assert.AreEqual(ans, "PublishInventory: values cannot be null");

            ans = ICM.publishInventory(TOKEN, GID, 1, null);
            Assert.AreEqual(ans, "PublishInventory: values cannot be null");
        }

        [TestMethod]
        public void getLostItems3DaysValid()
        {
            List<Item> ans = ICM.getLostItems3Days(CName, DateTime.Now);
            Assert.IsNotNull(ans);
            Assert.AreEqual(ans.ToArray().Length, 3);


        }

        [TestMethod]
        public void getFoundItems3DaysValid()
        {
            List<Item> ans = ICM.getFoundItems3Days(CName, DateTime.Now);
            Assert.IsNotNull(ans);
            Assert.AreEqual(ans.ToArray().Length, 3);


        }

        [TestMethod]
        public void getLostItems3DaysInvalid()
        {
            List<Item> ans = ICM.getLostItems3Days("notACompany", DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getFoundItems3DaysInvalid()
        {
            List<Item> ans = ICM.getFoundItems3Days("notACompany", DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getLostItems3DaysNullArgs()
        {
            List<Item> ans = ICM.getLostItems3Days(null, DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getFoundItems3DaysNullArgs()
        {
            List<Item> ans = ICM.getFoundItems3Days(null, DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void removeFBGroupValid()
        {
            string ans = ICM.removeFBGroup(CName, GID);
            Assert.AreEqual(ans, TRUESTRING);
            Company c = ICM.getCompanyByName(CName);
            Assert.AreEqual(c.FacebookGroups.Count, 0);
        }

        [TestMethod]
        public void addFBGroupValid()
        {
            string ans = ICM.addFBGroup(CName, "12345678");
            Assert.AreEqual(ans, TRUESTRING);
            Company c = ICM.getCompanyByName(CName);
            Assert.AreEqual(c.FacebookGroups.Count, 2);
        }

        [TestMethod]
        public void removeFBGroupInvalid()
        {
            string ans = ICM.removeFBGroup("notgoodname", GID);
            Assert.AreEqual(ans, "CompanyManager-removeFBGroup: company name is not valid");
        }

        [TestMethod]
        public void addFBGroupInvalid()
        {
            string ans = ICM.addFBGroup("notgoodname", GID);
            Assert.AreEqual(ans, "CompanyManager-addFBGroup: company name is not valid");
        }

        [TestMethod]
        public void addFBGroupNullArgs()
        {
            string ans = ICM.addFBGroup(null, GID);
            Assert.IsNull(ans);

            ans = ICM.addFBGroup(CName, null);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void removeFBGroupNullArgs()
        {
            string ans = ICM.removeFBGroup(null, GID);
            Assert.IsNull(ans);

            ans = ICM.removeFBGroup(CName, null);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getSystemCompanyFBGroupValid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup(CName, TOKEN);
            Company c = ICM.getCompanyByName(CName);
            Assert.IsNotNull(ans);
            Assert.AreEqual(c.FacebookGroups.Count, ans.Keys.Count);
        }

        [TestMethod]
        public void getSystemCompanyFBGroupInvalid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup("notCompany", TOKEN);
            Assert.IsNull(ans);

        }

        [TestMethod]
        public void getSystemCompanyFBGroupNullArgs()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup(null, TOKEN);
            Assert.IsNull(ans);

            ans = ICM.getSystemCompanyFBGroup(CName, null);
            Assert.IsNull(ans);

        }

        [TestMethod]
        public void getAllCompanyFBGroupValid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getAllCompanyFBGroup(CName, TOKEN);
            Company c = ICM.getCompanyByName(CName);
            Assert.IsNotNull(ans);
            Assert.IsTrue(c.FacebookGroups.Count <= ans.Keys.Count);
        }

        [TestMethod]
        public void getAllCompanyFBGroupInvalid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getAllCompanyFBGroup("notCompany", TOKEN);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getAllCompanyFBGroupNullArgs()
        {
            Dictionary<string, string> ans;
            ans = ICM.getAllCompanyFBGroup(null, TOKEN);
            Assert.IsNull(ans);

            ans = ICM.getAllCompanyFBGroup(CName, null);
            Assert.IsNull(ans);

        }
    }
}
