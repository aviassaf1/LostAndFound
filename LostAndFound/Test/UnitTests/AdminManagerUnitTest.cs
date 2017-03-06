using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using Domain;

namespace Test.UnitTests
{
    [TestClass]
    public class AdminManagerUnitTest
    {

        private Cache cache;
        private Database db;
        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp();
        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }



        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
