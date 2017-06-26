using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.DataLayer;
using WorkerHost.Domain;
using WorkerHost.Domain.Managers;
using WorkerHost.Domain.BLBackEnd;
using System.Linq;
using System.Collections.Generic;

namespace Test.UnitTests
{
    [TestClass]
    public class AdminManagerUnitTest
    {

        private Cache cache;
        private Database db;
        private IAdminManager IDM;
        private ICompanyManager ICM;
        private int adminKey;
        private int comapnyKey;
        private string FBToken = FacebookConnector.testFBToken;

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
            string res = ICM.login(FBToken, "Guy", "Mc123456");
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                comapnyKey = int.Parse(res);
            }

            string adminRes = IDM.login("admin1", "Mc123456");
            if (adminRes.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                adminRes = adminRes.Split(ar)[1];
                adminKey = int.Parse(adminRes);
            }
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
        public void TestAddCompanyWrongPassword() ///weird test
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "gg123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "GG123456", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gggggggg", adminKey);
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "00123456", adminKey);
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
            IDM.editCompany("Guy", "", "051111111", adminKey);
            comp = ICM.getCompanyByName("Guy");
            Assert.IsNotNull(ICM.getCompanyByName("Guy"));
            Assert.AreNotEqual(phone1, comp.Phone);
            Assert.AreEqual("051111111", comp.Phone);
        }

        [TestMethod]
        public void TestEditCompanyNoCompany()
        {
            Company comp = ICM.getCompanyByName("");
            Assert.IsNull(comp);
            Assert.AreEqual("עריכת חברה לא התבצעה, אחד או יותר מהערכים חסרים", IDM.editCompany("", "TestEdit12", "051111111", adminKey));
        }

        [TestMethod]
        public void TestEditCompanyNullCompany()
        {
            Assert.AreEqual("עריכת חברה לא התבצעה, אחד או יותר מהערכים חסרים", IDM.editCompany(null, "TestEdit12", "051111111", adminKey));
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
        public void TestLogin()
        {
            String adminRes2;
            int adminKey2;
            string adminRes = IDM.login("admin1", "Mc123456");
            Assert.IsTrue(adminRes.Contains("login succeeded,"));
            if (adminRes.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                adminRes2 = adminRes.Split(ar)[1];
                adminKey2 = int.Parse(adminRes);
            }
        }

        [TestMethod]
        public void TestLoginNotExistAdmin()
        {
            string adminName = RandomString();
            while (adminName.Equals("admin1"))
            {
                adminName = RandomString();
            }
            string adminRes = IDM.login(adminName, "Mc123456");
            Assert.IsFalse(adminRes.Contains("login succeeded,"));
        }

        [TestMethod]
        public void TestAddCompanyInvalidPassword()
        {
            string adminPassword = RandomString();
            while (adminPassword.Equals("Mc123456"))
            {
                adminPassword = RandomString();
            }
            string adminRes = IDM.login("admin1", adminPassword);
            Assert.IsFalse(adminRes.Contains("login succeeded,"));
        }

        [TestMethod]
        public void TestGetAllCompanies()
        {
            List<Company> comps= IDM.getAllCompanies(adminKey);
            Assert.IsNotNull(comps);
            Assert.AreEqual(2, comps.Count, "number of companies is not the same as it should be");
        }

        [TestMethod]
        public void TestGetAllCompaniesAfterAddAndDelete()
        {
            List<Company> comps = IDM.getAllCompanies(adminKey);
            Assert.IsNotNull(comps);
            Assert.AreEqual(2, comps.Count, "number of companies is not the same as it should be");
            IDM.addComapny("TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>(), "testfbID", "mangerName", "Gg123456", adminKey);
            comps = IDM.getAllCompanies(adminKey);
            Assert.IsNotNull(comps);
            Assert.AreEqual(3, comps.Count, "number of companies is not the same as it should be");
            IDM.deleteCompany("TestAddCompany", adminKey);
            comps = IDM.getAllCompanies(adminKey);
            Assert.IsNotNull(comps);
            Assert.AreEqual(2, comps.Count, "number of companies is not the same as it should be");
        }

        [TestMethod]
        public void TestGetAllCompaniesInvalidAdminKey()
        {
            Random random = new Random();
            int adminK = random.Next();
            while (adminK.Equals(adminKey))
            {
                adminK = random.Next();
            }
            List<Company> comps = IDM.getAllCompanies(adminK);
            Assert.IsNull(comps);
        }

        [TestMethod]
        public void TestUpdateToken()
        {
            string compToken = RandomString();
            string adminRes = IDM.updateToken(compToken,"Guy", adminKey);
            Assert.AreEqual("המפתח שונה בהצלחה",adminRes);
            Assert.AreEqual(compToken,ICM.getToken("Guy"));
        }

        [TestMethod]
        public void TestUpdateTokenInvalidKey()
        {
            Random random = new Random();
            int adminK = random.Next();
            while (adminK.Equals(adminKey))
            {
                adminK = random.Next();
            }
            string compToken = RandomString();
            string adminRes = IDM.updateToken(compToken, "Guy", adminK);
            Assert.AreNotEqual("המפתח שונה בהצלחה", adminRes);
            Assert.AreNotEqual(compToken, ICM.getToken("Guy"));
        }

        public static string RandomString()
        {
            Random random = new Random();
            int length = random.Next();
            while (length < 1)
            {
                length = random.Next();
            }
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}