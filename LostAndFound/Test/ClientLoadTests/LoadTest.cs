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
using System.Linq;

namespace Test.ClientLoadTests
{
    [TestClass]
    public class LoadTest
    {
        private Cache cache;
        private Database db;
        private ICompanyManager ICM;
        private IItemManager _iim;
        private IAdminManager _iam;
        private int key;
        private string FBToken = "EAACEdEose0cBAKL76AV60ABmjr1itjdsEV5QBAxjZA9I7V1oL5gBCTRpTyxHhfBkxRLI5RdsaNiwmac0jTdVQPupzcUSG7a6USBP04mB7iXpmSyLWbpNeQzu90vVZBVnQXZC68d1yBoGVQe1CuyRw1dJsT9S9ZB2LlZCn3ZBIAtznac5vqxTfpI5wzQSNwJicZD";
        private string addFItemTrueRes = "add found item: item was added successfully";
        private string addLItemTrueRes = "add lost item: item was added successfully";
        private string deleteItemRes = "Item Removed";
        private string addCompanyRes = "company has been added";
        private Object _lock;
        private string _companyName;
        private int _adminKey;
        private static int _itemIndex;
        private static string _fbGroup;

        [TestInitialize]
        public void setUp()
        {

            _itemIndex = 0;
            _lock = new Object();
            _iam = AdminManager.getInstance;
            ICM = CompanyManager.getInstance;
            _iim = ItemManager.getInstance;
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp2();
            _fbGroup = "5";

            string fbid = "";
            var fb = new FacebookClient();
            _companyName = "testComp";
            try
            {
                //make sure the token is good
                fb = new FacebookClient(FBToken);
            }
            catch (Exception e)
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


            string keyAsString = _iam.login("admin1", "Aadmin123");
            keyAsString = keyAsString.Substring(keyAsString.IndexOf(",") + 1);
            _adminKey = int.Parse(keyAsString);
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
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(addFoundItemFunc));
            }
            foreach (Task<string> t in threads)
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

        private string addFoundItemFunc()
        {

            string res = _iim.addFoundItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, _companyName, "09765592636", "", key);
            return res;
        }

        [TestMethod]
        public void add100LostItems()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(addLostItemFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {
                res = t.Result;
                Assert.AreEqual(addLItemTrueRes, res);
            }
        }

        private string addLostItemFunc()
        {

            string res = _iim.addLostItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, _companyName, "09765592636", "", key);
            return res;
        }

        [TestMethod]
        public void deleteFoundiffetentItem100()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(deleteFoundDiffetentItemFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {
                res = t.Result;
                Assert.AreEqual(deleteItemRes, res);
            }
        }

        private string deleteFoundDiffetentItemFunc()
        {
            string res = _iim.addFoundItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, _companyName, "09765592636", "", key);
            int itemID = -1;
            int currItemIndex = -1;
            lock (_lock)
            {
                var items = db.getItemsList();
                var arrayItems = items.ToArray();
                currItemIndex = _itemIndex;
                itemID = (arrayItems[currItemIndex]).itemID;
                _itemIndex++;
                if (_itemIndex >= 100)
                {
                    Monitor.PulseAll(_lock);
                }
                else
                {
                    Monitor.Wait(_lock);
                }
            }
            res = _iim.deleteItem(itemID, key);
            return res;
        }



        [TestMethod]
        public void deleteLostDiffetentItem100()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(deleteLostDiffetentItemFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {
                res = t.Result;
                Assert.AreEqual(deleteItemRes, res);
            }
        }

        private string deleteLostDiffetentItemFunc()
        {
            string res = _iim.addLostItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, _companyName, "09765592636", "", key);
            int itemID = -1;
            int currItemIndex = -1;
            lock (_lock)
            {
                var items = db.getItemsList();
                var arrayItems = items.ToArray();
                currItemIndex = _itemIndex;
                itemID = (arrayItems[currItemIndex]).itemID;
                _itemIndex++;
                if (_itemIndex >= 100)
                {
                    Monitor.PulseAll(_lock);
                }
                else
                {
                    Monitor.Wait(_lock);
                }
            }
            res = _iim.deleteItem(itemID, key);
            return res;
        }

        [TestMethod]
        public void deleteLostDifferentAndSameItem100_checkNoFails()
        {
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(deleteLostDifferentAndSameItemFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());

        }

        private string deleteLostDifferentAndSameItemFunc()
        {
            string res = _iim.addLostItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, _companyName, "09765592636", "", key);
            int itemID = -1;
            int currItemIndex = -1;
            lock (_lock)
            {
                currItemIndex = _itemIndex;
                _itemIndex++;
                if (_itemIndex >= 100)
                {
                    Monitor.PulseAll(_lock);
                }
                else
                {
                    Monitor.Wait(_lock);
                }
            }
            var items = db.getItemsList();
            var arrayItems = items.ToArray();
            var singleItem = arrayItems[currItemIndex];
            if (singleItem != null)
            {
                itemID = singleItem.itemID;
                res = _iim.deleteItem(itemID, key);
            }
            return res;
        }

        [TestMethod]
        public void deleteFoundDifferentAndSameItem100_checkNoFails()
        {
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(deleteFoundDifferentAndSameItemFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());

        }

        private string deleteFoundDifferentAndSameItemFunc()
        {
            string res = _iim.addFoundItem(new List<string>() { "BLUE" }, "BAG", DateTime.Today, "here", "desc", 24354, _companyName, "09765592636", "", key);
            int itemID = -1;
            int currItemIndex = -1;
            lock (_lock)
            {
                currItemIndex = _itemIndex;
                _itemIndex++;
                if (_itemIndex >= 100)
                {
                    Monitor.PulseAll(_lock);
                }
                else
                {
                    Monitor.Wait(_lock);
                }
            }
            var items = db.getItemsList();
            var arrayItems = items.ToArray();
            var singleItem = arrayItems[currItemIndex];
            if (singleItem != null)
            {
                itemID = singleItem.itemID;
                res = _iim.deleteItem(itemID, key);
            }
            return res;
        }



        [TestMethod]
        public void add100Companies()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(addCompanyFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {
                res = t.Result;
                bool pred = res.Equals(addCompanyRes) || res.Equals("החברה לא נוספה, החברה כבר קיימת במערכת");
                Assert.IsTrue(pred);
            }
        }

        private string addCompanyFunc()
        {
            lock (_lock)
            {
                string companyName = generateRandomString(10);
                string res = _iam.addComapny(companyName, "0522222222", new HashSet<string>(), _fbGroup, _fbGroup, "Aadmin123", _adminKey);
                _fbGroup = (int.Parse(_fbGroup) + 1).ToString();
                return res;
            }
        }

        private static string generateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [TestMethod]
        public void remove100Companies()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(removeCompanyFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {               
                res = t.Result;
                bool pred = res.Equals("true") || res.Equals("company does not exist") || res.Equals("company not exists in the system");
                if (pred.Equals(false))
                {
                    string sabri = "very shamen";
                }
                Assert.IsTrue(pred);
            }
        }

        private string removeCompanyFunc()
        {
            lock (_lock)
            {
                string companyName = generateRandomString(10);
                string addRes = _iam.addComapny(companyName, "0522222222", new HashSet<string>(), _fbGroup, _fbGroup, "Aadmin123", _adminKey);
                _fbGroup = (int.Parse(_fbGroup)+1).ToString();
                string deleteRes = "";
                if (addRes.Equals(addCompanyRes))
                {
                    deleteRes = _iam.deleteCompany(companyName, _adminKey);
                }
                return deleteRes;
            }
        }

        [TestMethod]
        public void add100FBItems()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(addFBFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {
                res = t.Result;
                Assert.AreEqual("true", res);
            }
        }

        private string addFBFunc()
        {
            lock (_lock)
            {
                WorkerHost.Domain.BLBackEnd.FBItem fbItem = new WorkerHost.Domain.BLBackEnd.FBItem(new List<WorkerHost.Domain.BLBackEnd.Color> { WorkerHost.Domain.BLBackEnd.Color.BLACK }, WorkerHost.Domain.BLBackEnd.ItemType.BAG, DateTime.Today, "here", "desc", _fbGroup, "tomer", WorkerHost.Domain.BLBackEnd.FBType.LOST);
                fbItem.addToDB();
                _fbGroup = (int.Parse(_fbGroup) + 1).ToString();
                return "true";
            }
        }

        [TestMethod]
        public void remove100FBItems()
        {
            string res;
            List<Task<string>> threads = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Task<string>(removeFBFunc));
            }
            foreach (Task<string> t in threads)
            {
                t.Start();
            }
            Task.WaitAll(threads.ToArray());
            foreach (Task<string> t in threads)
            {
                res = t.Result;
                Assert.AreEqual("true", res);
            }
            bool pred = db.getFBItemsList().Count == 0;
            Assert.IsTrue(pred);
        }

        private string removeFBFunc()
        {
            lock (_lock)
            {
                WorkerHost.Domain.BLBackEnd.FBItem fbItem = new WorkerHost.Domain.BLBackEnd.FBItem(new List<WorkerHost.Domain.BLBackEnd.Color> { WorkerHost.Domain.BLBackEnd.Color.BLACK }, WorkerHost.Domain.BLBackEnd.ItemType.BAG, DateTime.Today, "here", "desc", _fbGroup, "tomer", WorkerHost.Domain.BLBackEnd.FBType.LOST);
                fbItem.addToDB();
                var tempFbItem = db.getFBItemsList().First();
                int itemID = tempFbItem.itemID;
                db.removeItem(itemID);
                _fbGroup = (int.Parse(_fbGroup) + 1).ToString();
                return "true";
            }
        }

    }
}
