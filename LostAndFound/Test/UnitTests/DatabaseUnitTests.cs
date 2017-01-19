using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;

namespace Test.UnitTests
{
    [TestClass]
    public class DatabaseUnitTests
    {
        private Database db;
        string validExistUserName = "testUser";
        string validNotExistUserName = "tomer";
        string notValidUserName = "";
        string validExistCompanyName = "testCompany";
        string validNotExistCompanyName = "myComp";
        string notValidCompanyName = "";
        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            db.setUp();


        }

        [TestCleanup]
        public void cleanUp()
        {
            db.clear();
        }


        /******************************company tests***********************************/
        //////////////////////find company
        [TestMethod]
        public void find_company_valid_andExist()
        {
            string realCompanyName = "testCompany";
            Companies company = db.findCompanyByCompanyName(realCompanyName);
            Assert.IsNotNull(company);
            Assert.AreEqual(company.companyName, realCompanyName);
        }
        [TestMethod]
        public void find_company_valid_andNotExist()
        {
            Companies company = db.findCompanyByCompanyName("notRealCompany");
            Assert.IsNull(company);
        }
        [TestMethod]
        public void find_company_not_valid()
        {
            Companies company = db.findCompanyByCompanyName("");
            Assert.IsNull(company);
        }


        //////////////////////add company
        [TestMethod]
        public void add_company_notExist_valid()
        {
            string userName = "tomer";
            string companyName = "myComp";
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
            User user = db.findUserByUserName(userName);
            Companies company = db.findCompanyByCompanyName(companyName);
            Assert.AreEqual(str, "true");
            Assert.IsNotNull(company);
            Assert.IsNotNull(user);
            Assert.AreEqual(company.companyName, companyName);
            Assert.AreEqual(user.UserName, userName);
        }
        [TestMethod]
        public void add_company_Exist_valid()
        {
            string userName = "testUser";
            string companyName = "testCompany";
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void add_company_userName_notValid()
        {
            string userName = "";
            string companyName = "testCompany";
            db.addCompany(userName, "tomer", companyName, "0522222222", null);
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
        }
        [TestMethod]
        public void add_company_companyName_notValid()
        {
            string userName = "testUser";
            string companyName = "";
            db.addCompany(userName, "tomer", companyName, "0522222222", null);
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
        }
        [TestMethod]
        public void remove_company_valid_exist()
        {
            string str = db.removeCompany(validExistCompanyName);
            Assert.AreEqual("true", str);
            Companies company = db.findCompanyByCompanyName(validExistCompanyName);
            Assert.IsNull(company);
        }
        [TestMethod]
        public void remove_company_valid_notExist()
        {
            string str = db.removeCompany(validNotExistCompanyName);
            Assert.AreNotEqual("true", str);
            Companies company = db.findCompanyByCompanyName(validNotExistCompanyName);
            Assert.IsNull(company);
        }
        [TestMethod]
        public void remove_company_NotValid()
        {
            string str = db.removeCompany(notValidCompanyName);
            Assert.AreNotEqual("true", str);
            Companies company = db.findCompanyByCompanyName(notValidCompanyName);
            Assert.IsNull(company);
        }
    }
}
