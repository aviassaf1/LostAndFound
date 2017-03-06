using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.Managers;
using Domain.BLBackEnd;
using DataLayer;
using System.Collections.Generic;

namespace Test.UnitTests
{
    [TestClass]
    public class MatchManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private IMatchManager IMM;
        private const string TOKEN = "EAACEdEose0cBAPHaC5zRwZCmSZCquCbxrCEjZAZBRUKB9JNA7ZBTHJQjEIIMOa9S3lXavZA1l9Hgw96XUax89b1pefzS4P2FsN0givVkG87eENOWwTZAOchXBbGck3tXrJOc88oPI0gEmahQPxYZAtCTu0BAVKVIzFPFIRPOW3tHHkjjylS42pedVFqxZCjZAUHVMZD";
        private const string GID = "1538105046204967";
        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp();
            IMM = MatchManager.getInstance;
        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }
        [TestMethod]
        public void changeMatchStatusValid()
        {
            Company c = ComapanyManager.getInstance.getCompanyByName("Guy");
            HashSet<int> m = c.Matches;
            string ans;
            bool done = false;
            foreach(int i in m)
            {
                if (!done)
                {
                    ans = IMM.changeMatchStatus(i, 1);
                    Assert.AreEqual(1.ToString(), IMM.getMatchByID(i).MatchStatus.ToString());
                    Assert.AreEqual("status Changed", ans);
                    done = true;
                }
            }
        }

        [TestMethod]
        public void changeMatchStatusInvalid()
        {
            Company c = ComapanyManager.getInstance.getCompanyByName("Guy");
            HashSet<int> m = c.Matches;
            string ans;
            bool done = false;
            foreach (int i in m)
            {
                if (!done)
                {
                    ans = IMM.changeMatchStatus(i, 5);
                    Assert.AreEqual("not good statusNumber", ans);

                    ans = IMM.changeMatchStatus(-2, 1);
                    Assert.AreEqual("no match with that id", ans);
                    done = true;
                }
            }
        }

        [TestMethod]
        public void findMatchesValid()
        {
            List<Color> colors1 = new List<Color>();
            colors1.Add(Color.BLACK);
            List<Match> ms = IMM.findMatches(new FoundItem(colors1, ItemType.FOLDER, DateTime.Today, "BGU", "bla bla", 8876, "Guy", "Noam", "05000000", "c"), TOKEN);
            Assert.IsNotNull(ms);
        }

        [TestMethod]
        public void findMatchesInvalid()
        {
            List<Match> ms = IMM.findMatches(null, TOKEN);
            Assert.IsNull(ms);

            List<Color> colors1 = new List<Color>();
            colors1.Add(Color.BLACK);
            ms = IMM.findMatches(new FoundItem(colors1, ItemType.FOLDER, DateTime.Today, "BGU", "bla bla", 8876, "Guy", "Noam", "05000000", "c"), null);
            Assert.IsNull(ms);
        }

        [TestMethod]
        public void getPostsFromGroupValid()
        {
            List<Domain.BLBackEnd.FBItem> fbis = IMM.getPostsFromGroup(TOKEN, GID);
            Assert.IsNotNull(fbis);
        }

        [TestMethod]
        public void getPostsFromGroupInvalid()
        {
            List<Domain.BLBackEnd.FBItem> fbis = IMM.getPostsFromGroup(null, GID);
            Assert.IsNull(fbis);

            fbis = IMM.getPostsFromGroup(TOKEN, null);
            Assert.IsNull(fbis);
        }
    }
}
