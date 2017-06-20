using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.Domain;
using WorkerHost.Domain.Managers;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.DataLayer;
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
        private const string GID = "1538105046204967";
        private const string CName = "Guy";
        private int comapnyKey = SessionDirector.getInstance.generateAdminKey("Guy");


        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();

            ICM = CompanyManager.getInstance;

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
            string ans = ICM.publishInventory(GID, 1, comapnyKey);
            Assert.AreEqual(ans, TRUESTRING);
        }

        [TestMethod]
        public void PublishInvetoryInvalidArgs()
        {
            string ans = ICM.publishInventory( "invalidGID", 1, comapnyKey);
            Assert.AreEqual(ans, "PublishInventory: post to facebook failed");

            ans = ICM.publishInventory( GID, 98, comapnyKey);
            Assert.AreEqual(ans, "PublishInventory: days is more than MAXDAYS");

            ans = ICM.publishInventory( GID, 1, comapnyKey);
            Assert.AreEqual(ans, "PublishInventory: companyName is invalid");
        }

        [TestMethod]
        public void PublishInvetoryNullArgs()
        {
            string ans = ICM.publishInventory( null, 1, comapnyKey);
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
            string ans = ICM.removeFBGroup(GID , comapnyKey);
            Assert.AreEqual(ans, TRUESTRING);
            Company c = ICM.getCompanyByName(CName);
            Assert.AreEqual(c.FacebookGroups.Count, 0);
        }

        [TestMethod]
        public void addFBGroupValid()
        {
            string ans = ICM.addFBGroup("12345678", comapnyKey);
            Assert.AreEqual(ans, TRUESTRING);
            Company c = ICM.getCompanyByName(CName);
            Assert.AreEqual(c.FacebookGroups.Count, 2);
        }

        [TestMethod]
        public void removeFBGroupInvalid()
        {
            string ans = ICM.removeFBGroup(GID, comapnyKey);
            Assert.AreEqual(ans, "CompanyManager-removeFBGroup: company name is not valid");
        }

        [TestMethod]
        public void addFBGroupInvalid()
        {
            string ans = ICM.addFBGroup( GID, comapnyKey);
            Assert.AreEqual(ans, "CompanyManager-addFBGroup: company name is not valid");
        }

        [TestMethod]
        public void addFBGroupNullArgs()
        {
            string ans = ICM.addFBGroup( GID, comapnyKey);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void removeFBGroupNullArgs()
        {
            string ans = ICM.removeFBGroup(null, comapnyKey);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getSystemCompanyFBGroupValid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup(comapnyKey);
            Company c = ICM.getCompanyByName(CName);
            Assert.IsNotNull(ans);
            Assert.AreEqual(c.FacebookGroups.Count, ans.Keys.Count);
        }

        [TestMethod]
        public void getSystemCompanyFBGroupInvalid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup(comapnyKey);
            Assert.IsNull(ans);

        }

        [TestMethod]
        public void getSystemCompanyFBGroupNullArgs()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup(comapnyKey);
            Assert.IsNull(ans);

        }

        /*[TestMethod]
        public void getAllCompanyFBGroupValid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getAllCompanyFBGroup(CName);
            Company c = ICM.getCompanyByName(CName);
            Assert.IsNotNull(ans);
            Assert.IsTrue(c.FacebookGroups.Count <= ans.Keys.Count);
        }

        [TestMethod]
        public void getAllCompanyFBGroupInvalid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getAllCompanyFBGroup("notCompany");
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getAllCompanyFBGroupNullArgs()
        {
            Dictionary<string, string> ans;
            ans = ICM.getAllCompanyFBGroup(null);
            Assert.IsNull(ans);
        }*/
    }
}
