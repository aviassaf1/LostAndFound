using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using Domain;
using Domain.BLBackEnd;
using Domain.Managers;
using System.Collections.Generic;

namespace Test.UnitTests
{
    [TestClass]
    public class ItemManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private IItemManager IIM;
        private ICompanyManager ICM;
        private string token;
        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp();
            IIM = ItemManager.getInstance;
            ICM = ComapanyManager.getInstance;
        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }
        
        [TestMethod]
        public void TestGetAllCompanyItems()
        {
            List <CompanyItem> compItems=IIM.getAllCompanyItems("Guy");
            Assert.IsNotNull(compItems);
            Assert.AreEqual(6, compItems.ToArray().Length);
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestGetAllCompanyItemsNullCopamany()
        {
            List<CompanyItem> compItems = IIM.getAllCompanyItems(null);
            Assert.IsNull(compItems);
        }

        [TestMethod]
        public void TestGetAllCompanyItemsNotExit()
        {
            List<CompanyItem> compItems = IIM.getAllCompanyItems("");
            Assert.IsNull(compItems);
        }

        [TestMethod]
        public void TestAddLostItem()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string>{ "blue", "Gray" };
            IIM.addLostItem(sColors, "folder", DateTime.Today,"location", "description",
            4564, "Guy2","contactName","054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(1, compItems2.ToArray().Length);
        }
        
        /*
        [TestMethod]
        public void TestAddCompanyWrong()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "Aa12345", "TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>());
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "Aa12345", "TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>());
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "Aa12345", "TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>());
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
        }
        */
    }
}
