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
    public class MatchManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private IMatchManager IMM;
        private ICompanyManager ICM;
        private IItemManager IIM;
        private const string GID = "1538105046204967";
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
            IMM = MatchManager.getInstance;
            ICM = CompanyManager.getInstance;
            IIM = ItemManager.getInstance;
            string res = ICM.login(FBToken, "Guy", "Mc123456");
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                comapnyKey = int.Parse(res);
            }
            ICM.setToken("Guy", FBToken);
        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }
        [TestMethod]
        public void changeMatchStatusValid()
        {
            Company c = ICM.getCompanyByName("Guy");
            HashSet<int> m = c.Matches;
            string ans;
            bool done = false;
            foreach (int i in m)
            {
                if (!done)
                {
                    ans = IMM.changeMatchStatus(i, 1, comapnyKey);
                    Assert.AreEqual("CORRECT", IMM.getMatchByID(i).MatchStatus.ToString());
                    Assert.AreEqual("status Changed", ans);
                    done = true;
                }
            }
        }

        [TestMethod]
        public void changeMatchStatusInvalid()
        {
            Company c = ICM.getCompanyByName("Guy");
            HashSet<int> m = c.Matches;
            string ans;
            bool done = false;
            foreach (int i in m)
            {
                if (!done)
                {
                    ans = IMM.changeMatchStatus(i, 5, comapnyKey);
                    Assert.AreEqual("not good statusNumber", ans);

                    ans = IMM.changeMatchStatus(-2, 1, comapnyKey);
                    Assert.AreEqual("changeMatchStatus: match id is not valid", ans);
                    done = true;
                }
            }
        }

        [TestMethod]
        public void findMatchesValid()
        {
            List<string> colors1 = new List<string>() { "BLACK" };
            IIM.addFoundItem(colors1, "FOLDER", DateTime.Today, "BGU", "bla bla",
                8876, "Guy", "Noam", "05000000", comapnyKey);
            IIM.getAllCompanyItems(comapnyKey);
            List<Match> ms = IMM.findMatches(IIM.getAllCompanyItems(comapnyKey)[0], ICM.getToken("Guy"));
            Assert.IsNotNull(ms);
        }

        [TestMethod]
        public void findMatchesInvalid()
        {
            List<Match> ms = IMM.findMatches(null, ICM.getToken("Guy"));
            Assert.IsNull(ms);

            List<Color> colors1 = new List<Color>();
            colors1.Add(Color.BLACK);
            ms = IMM.findMatches(new FoundItem(colors1, ItemType.FOLDER, DateTime.Today, "BGU", "bla bla", 8876, "Guy", "Noam", "05000000", "c"), null);
            Assert.IsNull(ms);
        }

        [TestMethod]
        public void getPostsFromGroupValid()
        {
            List<WorkerHost.Domain.BLBackEnd.FBItem> fbis = FacebookConnector.getPostsFromGroup(ICM.getToken("Guy"), GID);
            Assert.IsNotNull(fbis);
        }

        [TestMethod]
        public void getPostsFromGroupInvalid()
        {
            List<WorkerHost.Domain.BLBackEnd.FBItem> fbis = FacebookConnector.getPostsFromGroup(null, GID);
            Assert.IsNull(fbis);

            fbis = FacebookConnector.getPostsFromGroup(ICM.getToken("Guy"), null);
            Assert.IsNull(fbis);
        }
    }
}
