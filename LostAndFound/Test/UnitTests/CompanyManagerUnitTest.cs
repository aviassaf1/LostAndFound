using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.Managers;
using Domain.BLBackEnd;
using DataLayer;

namespace Test.UnitTests
{
    [TestClass]
    public class CompanyManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private ICompanyManager ICM;
        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            ICM = ComapanyManager.getInstance;
        }

        [TestCleanup]
        public void cleanUp()
        {
            db.clear();
            cache.clear();
        }

        [TestMethod]
        public void TestGetComapnyByNameValidName()
        {
            string companyName = "Guy";
            Company c = ICM.getCompanyByName(companyName);
            Assert.IsNotNull(c);
            Assert.Equals(companyName, c.CompanyName);
        }

        [TestMethod]
        public void TestGetComapnyByNameNotValidName()
        {
            string companyName = "NotGoodName";
            Company c = ICM.getCompanyByName(companyName);
            Assert.IsNull(c);
        }
    }
}
