using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.DataLayer;
using WorkerHost.Domain;
using WorkerHost.Domain.Managers;
using System.Threading;
using System.Collections.Generic;
using NewWebClient;
using System.Threading.Tasks;

namespace Test.ClientLoadTests
{
    [TestClass]
    public class LoadTest
    {
        private Cache cache;
        private Database db;
        private ICompanyManager ICM;
        private int key;
        private string FBToken = "EAACEdEose0cBACKLWX6g8H8XzPCUpq9sWvGZCDAuBiA4vhAX3kEVnZBPiacU5k6PlFdxbo2vhVkgSVwZA9tmzQlbWZBDsJHVOXZCZBhqWgkghWZBMfKT4N2S1W89ElTxKc3V9F6N0OR3FD2yPruaHn1B8hGXw8xpnsadT895UUn4mTuE1PM9HgjOzZBmF3fErXMZD";
        private string addFItemTrueRes = "add found item: item was added successfully";

        [TestInitialize]
        public void setUp()
        {
            ICM = CompanyManager.getInstance;
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp2();
            string res = ICM.login(FBToken, "testComp", "Mc123456");
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                key = int.Parse(res);
            }
            ICM.setToken("testComp", FBToken);

         }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }


        [TestMethod]
        public void TestMethod1()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for(int i=0; i<100; i++)
            {
                threads.Add(new Task<string>(addItemFunc));
            }
            foreach(Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {
                res = t.Result;
                Assert.AreEqual(addFItemTrueRes, res);
            }
        }

        private string addItemFunc()
        {
            Channel c = Channel.getNewTestChannel();
            string res = c.ServerService.addFoundItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, "guyGay", "09765592636", "", key);
            return res;
        }
    }
}
