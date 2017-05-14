using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.DataLayer;
using WorkerHost.Domain;
using WorkerHost.Domain.Managers;
using WorkerHost.Domain.BLBackEnd;

namespace Test.UnitTests
{
    [TestClass]
    public class AdminManagerUnitTest
    {

        private Cache cache;
        private Database db;
        private IAdminManager IDM;
        private ICompanyManager ICM;
        private int adminKey = SessionDirector.getInstance.generateAdminKey("admin");
        private int comapnyKey = SessionDirector.getInstance.generateAdminKey("TestAddCompany");

        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp();
            IDM = AdminManager.getInstance;
            ICM = CompanyManager.getInstance;
        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }

        [TestMethod]
        public void TestAddCompany()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNotNull(ICM.getCompanyByName("TestAddCompany"));
        }

        [TestMethod]
        public void TestAddCompanyWrongPassword()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
        }

        [TestMethod]
        public void TestAddCompanyWrongPhone()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "000101", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", null, new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
        }



        [TestMethod]
        public void TestAddCompanyWrongUserName()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("", "050000000", new System.Collections.Generic.HashSet<string>(),"testfbID","mangerName","Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny(null, "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
        }


        [TestMethod]
        public void TestAddCompanyWrongCompanyName()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny( "", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny( null, "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
        }


        [TestMethod]
        public void TestAddCompanyWrongFBList()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", null, "testfbID", "mangerName", "Gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
        }

        [TestMethod]
        public void TestDeleteCompany()
        {
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            int length = ItemManager.getInstance.getAllCompanyItems(comapnyKey).ToArray().Length;
            IDM.deleteCompany("Guy", adminKey);
            Assert.IsNull(ICM.getCompanyByName("Guy"));
            try
            {
                Assert.AreEqual(length, ItemManager.getInstance.getAllCompanyItems(comapnyKey).ToArray().Length);
            }
            catch (Exception)
            { }
        }

        [TestMethod]
        public void TestDeleteCompanyNotExist()
        {
            Assert.IsNull(ICM.getCompanyByName("Guy3"));
            Assert.AreEqual("company not exists in the system", IDM.deleteCompany("Guy3", adminKey));
            Assert.IsNull(ICM.getCompanyByName("Guy3"));
        }

        [TestMethod]
        public void TestEditCompany()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "TestEdit12", "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreNotEqual(pas1, comp.Password);
            Assert.AreNotEqual(phone1, comp.Phone);
            Assert.AreEqual("TestEdit12", comp.Password);
            Assert.AreEqual("051111111", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNoCompany()
        {
            Company comp = ICM.getCompanyByName("");
            Assert.IsNull(comp);
            Assert.AreEqual("one or more of the fields is missing", IDM.editCompany("", "TestEdit12", "051111111", adminKey));
        }

        [TestMethod]
        public void TestEditCompanyNullCompany()
        {
            Assert.AreEqual("one or more of the fields is missing", IDM.editCompany(null, "TestEdit12", "051111111", adminKey));
        }

        [TestMethod]
        public void TestEditCompanyNoPassword()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "", "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual("TestEdit12", comp.Password);
            Assert.AreNotEqual("", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNullPassword()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", null, "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual(null, comp.Password);
            Assert.AreNotEqual("051111111", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNoPhone()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "TestEdit12", "", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual("TestEdit12", comp.Password);
            Assert.AreNotEqual("", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNullPhone()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "TestEdit12", null, adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual("TestEdit12", comp.Password);
            Assert.AreNotEqual(null, comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyShortPassword()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "Tt1", "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual("Tt1", comp.Password);
            Assert.AreNotEqual("051111111", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNoNumberInPassword()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "Ttttttt", "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual("Ttttttt", comp.Password);
            Assert.AreNotEqual("051111111", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNoCapitalLetterInPassword()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "11111t1", "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual("11111t1", comp.Password);
            Assert.AreNotEqual("051111111", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNoSmallLetterInPassword()
        {
            Company comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(comp);
            string pas1 = comp.Password;
            string phone1 = comp.Phone;
            IDM.editCompany("Guy", "T111111", "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreEqual(pas1, comp.Password);
            Assert.AreEqual(phone1, comp.Phone);
            Assert.AreNotEqual("T111111", comp.Password);
            Assert.AreNotEqual("051111111", comp.Phone);
        }
    }
}