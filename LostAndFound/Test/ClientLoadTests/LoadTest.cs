using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.DataLayer;
using WorkerHost.Domain;
using WorkerHost.Domain.Managers;
using System.Threading;
using System.Collections.Generic;
using NewWebClient;
using System.Threading.Tasks;
using Facebook;
using Microsoft.CSharp;

namespace Test.ClientLoadTests
{
    [TestClass]
    public class LoadTest
    {
        private Cache cache;
        private Database db;
        private ICompanyManager ICM;
        private IItemManager _iim;
        private int key;
        private string FBToken = "EAACEdEose0cBABHYaUmk35BR2bAZA8ZAfpLu5CelCDTqExYTetRI8uoWYpe7OiBDN97GZC789mIzaLvV9ZAe48EG7YKyC18PGxkRv5G0b4sBSrKlaofxsvhUTHaWkzZCuyJu38zmTZAvu800XeLCDMYhRvvfsE64H82NZChuivmb7fWEzxg2gyRc6td9mYX0ucZD";
        private string addFItemTrueRes = "add found item: item was added successfully";
        private string _companyName;

        [TestInitialize]
        public void setUp()
        {
            ICM = CompanyManager.getInstance;
            _iim = ItemManager.getInstance;
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp2();
            string fbid = "";
            var fb = new FacebookClient();
            _companyName = "testComp";
            try
            {
                //make sure the token is good
                fb = new FacebookClient(FBToken);
            }
            catch(Exception e)
            {
                throw new Exception("problem relating to facebook");
            }
            var parameters = new Dictionary<string, object>();
            parameters["fields"] = "id";
            dynamic result;
            try
            {
                //make sure post succeeds with GID
                result = fb.Get("me", parameters);
                fbid = result.id;
            }
            catch (Exception e)
            {
                throw new Exception("problem relating to facebook");
            }
            foreach (var cu in db.getCompanyUsersList())
            {
                if (cu.companyName.Equals("testComp"))
                {
                    cu.fbProfileId = fbid;
                    db.saveChanges("test");
                    cache.initCache();
                }
            }
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
        public void add100FoundItems()
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
            
            string res = _iim.addFoundItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, _companyName, "09765592636", "", key);
            return res;
        }
    }
}
