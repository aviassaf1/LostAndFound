using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;

namespace Test.UnitTests
{
    [TestClass]
    public class DatabaseUnitTests
    {
        private Database db;
        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.setUp();

        }

        [TestCleanup]
        public void cleanUp()
        {
            db.clear();
        }


        /******************************company tests***********************************/
        [TestMethod]
        public void find_company_valid_andExist()
        {
            string notRealCompanyName = "testCompany";
            Companies company = db.findCompanyByCompanyName(notRealCompanyName);
            Assert.IsNotNull(company);
            Assert.AreEqual(company.userName, notRealCompanyName);
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

        [TestMethod]
        public void add_company_valid()
        {
          //  db.addCompany("tomer", "tomer", "tomerComp", "0522222222", null);
        }
    }
}
