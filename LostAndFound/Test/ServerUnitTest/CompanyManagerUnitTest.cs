using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.Domain;
using WorkerHost.Domain.Managers;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.DataLayer;
using System.Collections.Generic;
using System.Linq;

namespace Test.UnitTests
{
    [TestClass]
    public class CompanyManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private ICompanyManager ICM;
        private IAdminManager IAM;
        private const string TRUESTRING = "true";
        private const string GID = "1538105046204967";
        private const string CName = "Guy";
        private const string CPass = "Mc123456";
        private const string AName = "admin1";
        private const string APass = "Mc123456";
        private const int MAXDAYS = 8;
        private int companyKey;
        private int adminKey;
        private string FBToken = FacebookConnector.testFBToken;



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


        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp();
            IAM = AdminManager.getInstance;
            string res = IAM.login(AName, APass);
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                adminKey = int.Parse(res);
            }
            ICM = CompanyManager.getInstance;
            res = ICM.login(FBToken, CName, CPass);
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                companyKey = int.Parse(res);
            }
            ICM.setToken("Guy", FBToken);

        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }

        [TestMethod]
        public void TestGetComapnyByNameValidName()
        {
            string companyName = CName;
            Company c = ICM.getCompanyByName(companyName);
            Assert.IsNotNull(c);
            Assert.AreEqual(companyName, c.CompanyName);
        }

        [TestMethod]
        public void TestGetComapnyByNameNotValidName()
        {
            string companyName = "NotGoodName";
            Company c = ICM.getCompanyByName(companyName);
            Assert.IsNull(c);
        }

        [TestMethod]
        public void PublishInvetoryAllValid()
        {
            string ans = ICM.publishInventory(GID, 1, companyKey);
            Assert.AreEqual(ans, TRUESTRING);
        }

        [TestMethod]
        public void PublishInvetoryInvalidArgs()
        {
            string ans = ICM.publishInventory( GID, 98, companyKey);
            Assert.AreEqual(ans, "פרסום נכשל, כמות הימים שניתן לבחור היא " + MAXDAYS);
        }

        [TestMethod]
        public void getLostItems3DaysValid()
        {
            List<Item> ans = ICM.getLostItems3Days(CName, DateTime.Now);
            Assert.IsNotNull(ans);
            Assert.AreEqual(ans.ToArray().Length, 3);


        }

        [TestMethod]
        public void getFoundItems3DaysValid()
        {
            List<Item> ans = ICM.getFoundItems3Days(CName, DateTime.Now);
            Assert.IsNotNull(ans);
            Assert.AreEqual(ans.ToArray().Length, 3);


        }

        [TestMethod]
        public void getLostItems3DaysInvalid()
        {
            List<Item> ans = ICM.getLostItems3Days("notACompany", DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getFoundItems3DaysInvalid()
        {
            List<Item> ans = ICM.getFoundItems3Days("notACompany", DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getLostItems3DaysNullArgs()
        {
            List<Item> ans = ICM.getLostItems3Days(null, DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getFoundItems3DaysNullArgs()
        {
            List<Item> ans = ICM.getFoundItems3Days(null, DateTime.Now);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void removeFBGroupValid()
        {
            string ans = ICM.removeFBGroup(GID , companyKey);
            Assert.AreEqual(ans, TRUESTRING);
            Company c = ICM.getCompanyByName(CName);
            Assert.AreEqual(c.FacebookGroups.Count, 0);
        }

        [TestMethod]
        public void addFBGroupValid()
        {
            string ans = ICM.addFBGroup("809201352594934", companyKey);
            Assert.AreEqual(ans, "Add facebook group worked");
            Company c = ICM.getCompanyByName(CName);
            Assert.AreEqual(c.FacebookGroups.Count, 2);
        }

        [TestMethod]
        public void addFBGroupInvalid()
        {
            string ans = ICM.addFBGroup( GID, companyKey);
            Assert.AreEqual(ans, "הוספת קבוצת פייסבוק נכשלה, הקבוצה כבר קיימת במערכת");
        }

        [TestMethod]
        public void addFBGroupNullArgs()
        {
            string ans = ICM.addFBGroup(null, companyKey);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void removeFBGroupNullArgs()
        {
            string ans = ICM.removeFBGroup(null, companyKey);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void getSystemCompanyFBGroupValid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup(companyKey);
            Company c = ICM.getCompanyByName(CName);
            Assert.IsNotNull(ans);
            Assert.AreEqual(c.FacebookGroups.Count, ans.Keys.Count);
        }

        [TestMethod]
        public void getSystemCompanyFBGroupInvalid()
        {
            Dictionary<string, string> ans;
            ans = ICM.getSystemCompanyFBGroup(-1);
            Assert.IsNull(ans);
        }

        [TestMethod]
        public void companyLoginValid()
        {
            int cKey;
            string res =  ICM.login(FBToken, CName, CPass);
            Assert.IsTrue(res.Contains("login succeeded,"));
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                cKey = int.Parse(res);
            }
        }

        [TestMethod]
        public void companyLoginRandomInvalid() 
        {
            string randCompName = RandomString();
            string randPassword = RandomString();
            string randFBToken = RandomString();
            string res = ICM.login(FBToken, randCompName, CPass);
            Assert.Equals(res,"התחברות נכשלה, שם משתמש או סיסמה לא תקינים");
            res = ICM.login(FBToken, CName, randPassword);
            Assert.Equals(res, "התחברות נכשלה, שם משתמש או סיסמה לא תקינים");
            res = ICM.login(randFBToken, CName, CPass);
            Assert.IsNull(res);
        }

        [TestMethod]
        public void addWorkerValid()
        {
            Dictionary<string, bool> workers = ICM.getCompanyWorkers(companyKey);
            string workerUsername = "worker1";
            string workerPassword = "Pp12345";
            Assert.IsFalse(workers.ContainsKey("worker1"));
            string success = "worker added";
            string res = ICM.addWorker(workerUsername, workerPassword, false, companyKey);
            Assert.Equals(res, success);
            workers = ICM.getCompanyWorkers(companyKey);
            Assert.IsTrue(workers.ContainsKey("worker1"));
            int cKey;
            string ans = ICM.login(FBToken, workerUsername, workerPassword);
            Assert.IsTrue(res.Contains("login succeeded,"));
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                cKey = int.Parse(res);
            }

        }

        public void addWorkerFailValid()
        {
            Dictionary<string, bool> workers = ICM.getCompanyWorkers(companyKey);
            string workerUsername = "worker1";
            string workerPassword = "Pp12345";
            Assert.IsFalse(workers.ContainsKey("worker1"));
            string success = "worker added";
            string res = ICM.addWorker(null, workerPassword, false, companyKey);
            Assert.Equals(res, "worker not added, username or password is invalid");
            res = ICM.addWorker(workerUsername, null, false, companyKey);
            Assert.Equals(res, "worker not added, username or password is invalid");
            res = ICM.addWorker(workerUsername, workerPassword, false, -1);
            Assert.Equals(res, "הוספת עובד נכשלה");
            res = ICM.addWorker(workerUsername, workerPassword, false, companyKey);
            Assert.Equals(res, success);
            workers = ICM.getCompanyWorkers(companyKey);
            Assert.IsTrue(workers.ContainsKey("worker1"));
            res = ICM.addWorker(workerUsername, workerPassword, false, companyKey);
            Assert.Equals(res, "הוספת עובד נכשלה, משתמש כבר קיים במערכת");

        }

        [TestMethod]
        public void addWorkerRandomInvalid()
        {
            string randWorkerName = RandomString();
            string randWorkerPassword = RandomString();
            Dictionary<string, bool> workers = ICM.getCompanyWorkers(companyKey);
            Assert.IsFalse(workers.ContainsKey(randWorkerName));
            string res = ICM.addWorker(randWorkerName, randWorkerPassword, false, companyKey);
            Assert.Equals(res, "הוספת עובד נכשלה");
            Assert.IsFalse(workers.ContainsKey("randWorkerName"));
        }

        [TestMethod]
        public void RemoveWorkerValid()
        {
            Dictionary<string, bool> workers = ICM.getCompanyWorkers(companyKey);
            string workerUsername = "worker1";
            string workerPassword = "Pp12345";
            //Assert.IsFalse(workers.ContainsKey("worker1"));
            string success = "worker added";
            string res = ICM.addWorker(workerUsername, workerPassword, false, companyKey);
            //Assert.Equals(res, success);
            workers = ICM.getCompanyWorkers(companyKey);
            //Assert.IsTrue(workers.ContainsKey("worker1"));
            res = ICM.removeWorker(workerUsername, companyKey);
            Assert.IsFalse(workers.ContainsKey("worker1"));
            string ans = ICM.login(FBToken, workerUsername, workerPassword);
            Assert.Equals(res,("התחברות נכשלה, שם משתמש או סיסמה לא תקינים"));
        }

        [TestMethod]
        public void RemoveWorkerFailValid()
        {
            Dictionary<string, bool> workers = ICM.getCompanyWorkers(companyKey);
            string workerUsername = "worker1";
            string workerPassword = "Pp12345";
            //Assert.IsFalse(workers.ContainsKey(workerUsername));
            string success = "worker added";
            string res = ICM.addWorker(workerUsername, workerPassword, false, companyKey);
            //Assert.Equals(res, success);
            workers = ICM.getCompanyWorkers(companyKey);
            //Assert.IsTrue(workers.ContainsKey(workerUsername));
            //worker added
            res = ICM.removeWorker(workerUsername, -1);
            Assert.Equals(res, "remove worker failed");
            workers = ICM.getCompanyWorkers(companyKey);
            Assert.IsTrue(workers.ContainsKey(workerUsername));
            res = ICM.removeWorker("bad", companyKey);
            Assert.Equals(res, "remove worker failed, username not exists");
            workers = ICM.getCompanyWorkers(companyKey);
            Assert.IsTrue(workers.ContainsKey(workerUsername));
        }

        [TestMethod]
        public void getCompanyWorkersValid()
        {
            Dictionary<string, bool> workers = ICM.getCompanyWorkers(companyKey);
            List<string> myWorkers = new List<string>();
            Assert.IsNull(workers);
            string workerPassword = "Pp12345";
            for (int i = 1; i <= 3; i++)
            {
                ICM.addWorker("worker" + i, workerPassword, false, companyKey);
                myWorkers.Add("worker" + i);
            }
            workers = ICM.getCompanyWorkers(companyKey);
            Assert.IsTrue(workers.Count == 3);
            foreach(string w in myWorkers)
            {
                Assert.IsTrue(workers.ContainsKey(w));
            }
        }

        [TestMethod]
        public void getCompanyWorkersFailValid()
        {
            Dictionary<string, bool> workers = ICM.getCompanyWorkers(companyKey);
            List<string> myWorkers = new List<string>();
            Assert.IsNull(workers);
            string workerPassword = "Pp12345";
            for (int i = 1; i <= 3; i++)
            {
                ICM.addWorker("worker" + i, workerPassword, false, companyKey);
                myWorkers.Add("worker" + i);
            }
            workers = ICM.getCompanyWorkers(-1);
            Assert.IsNull(workers);
        }

        [TestMethod]
        public void isManagerValid()
        {
            string workerUsername = "worker1";
            string workerPassword = "Pp12345";
            int WKey = -1;
            ICM.addWorker(workerUsername, workerPassword, false, companyKey);
            string res = ICM.login(FBToken, workerUsername, workerPassword);
            Assert.IsTrue(res.Contains("login succeeded,"));
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                WKey = int.Parse(res);
            }
            Assert.IsFalse(ICM.isManager(WKey));
            ICM.removeWorker(workerUsername, companyKey);
            ICM.addWorker(workerUsername, workerPassword, true, companyKey);
            res = ICM.login(FBToken, workerUsername, workerPassword);
            Assert.IsTrue(res.Contains("login succeeded,"));
            if (res.Contains("login succeeded,"))
            {
                char[] ar = { ',' };
                res = res.Split(ar)[1];
                WKey = int.Parse(res);
            }
            Assert.IsTrue(ICM.isManager(WKey));
        }

        [TestMethod]
        public void isManagerFailValid()
        {
            Assert.IsNull(ICM.isManager(-1));
        }

        [TestMethod]
        public void isManagerRandom()
        {
            Random rnd = new Random();
            int key = rnd.Next(int.MaxValue);
            Assert.IsNull(ICM.isManager(key));
        }

        [TestMethod]
        public void setTokenValid()
        {
            string newT = "newToken";
            ICM.setToken(CName, newT);
            string t = ICM.getToken(CName);
            Assert.AreEqual(t, newT);
            ICM.setToken(CName, FBToken);
        }
    }
}
