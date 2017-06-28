using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.DataLayer;
using WorkerHost.Domain;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.Domain.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Test.UnitTests
{
    [TestClass]
    public class ItemManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private IItemManager IIM;
        private ICompanyManager ICM;
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
            IIM = ItemManager.getInstance;
            ICM = CompanyManager.getInstance;
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
        public void TestGetAllCompanyItems()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems);
            Assert.AreEqual(6, compItems.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestGetAllCompanyItemsNotExit()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems = IIM.getAllCompanyItems(-1);
            Assert.IsNull(compItems);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItem()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(7, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNoColors()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string>();
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(7, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNull()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string>();
            IIM.addLostItem(null, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNullType()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, null, DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNulllLocation()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, null, "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNullDescription()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", null,
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }


        [TestMethod]
        public void TestAddLostItemNullContactName()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, null, "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNullPhone()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", null, "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNullPhotoLocation()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", null, comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNullcomapnyKey()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", -3);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemNoType()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemBiggerDate()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today.AddDays(2), "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddLostItemBadItem()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "fol", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItem()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(7, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNoColors()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string>();
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(7, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNull()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string>();
            IIM.addFoundItem(null, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNullType()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, null, DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNulllLocation()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, null, "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNullDescription()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", null,
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }



        [TestMethod]
        public void TestAddFoundItemNullContactName()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, null, "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNullPhone()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", null, "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNullPhotoLocation()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", null, comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNullcomapnyKey()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", -3);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemNoType()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }

        [TestMethod]
        public void TestAddFoundItemBiggerDate()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today.AddDays(2), "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }



        [TestMethod]
        public void TestAddFoundItemBadItem()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(6, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "fol", DateTime.Today, "location", "description",
            4564, "contactName", "054444444", "photoLocation", comapnyKey);
            compItems2 = IIM.getAllCompanyItems(comapnyKey);
            Assert.AreEqual(6, compItems2.ToArray().Length);
        }


        [TestMethod]
        public void TestTransactionComplete()
        {

            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];
            if ((ci.GetType()).Equals(typeof(FoundItem)))
                Assert.IsFalse(((FoundItem)ci).Delivered);
            if ((ci.GetType()).Equals(typeof(LostItem)))
                Assert.IsFalse(((LostItem)ci).WasFound);
            IIM.transactionComplete(ci.ItemID, comapnyKey);
            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                if (c.ItemID == ci.ItemID)
                {
                    if ((ci.GetType()).Equals(typeof(FoundItem)))
                        Assert.IsTrue(((FoundItem)ci).Delivered);
                    if ((ci.GetType()).Equals(typeof(LostItem)))
                        Assert.IsTrue(((LostItem)ci).WasFound);
                }
            }
        }


        [TestMethod]
        public void TestTransactionCompleteNoExistID()
        {
            IIM.transactionComplete(-7, comapnyKey);
        }

        [TestMethod]
        public void TestDeleteItem()
        {

            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];
            int len = IIM.getAllCompanyItems(comapnyKey).ToArray().Length;
            IIM.deleteItem(ci.ItemID, comapnyKey);
            Assert.AreEqual(len - 1, IIM.getAllCompanyItems(comapnyKey).ToArray().Length);
            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                Assert.AreNotEqual(ci.ItemID, c.ItemID);
            }
        }

        [TestMethod]
        public void TestDeleteItemNoItem()
        {
            int len = IIM.getAllCompanyItems(comapnyKey).ToArray().Length;
            IIM.deleteItem(-7, comapnyKey);
            Assert.AreEqual(len, IIM.getAllCompanyItems(comapnyKey).ToArray().Length);
        }


        [TestMethod]
        public void TestEditItem()
        {
            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];

            int olditemID = ci.ItemID;
            DateTime olddate = ci.Date;
            string oldlocation = ci.Location;
            string olddescription = ci.Description;
            int oldserialNumber = ci.SerialNumber;
            string oldcontactName = ci.ContactName;
            string oldcontactPhone = ci.ContactPhone;

            DateTime date = DateTime.Today;
            string location = "here";
            string description = "desc";
            int serialNumber = 444444;
            string contactName = "noamba";
            string contactPhone = "0459475859";

            IIM.editItem(olditemID, date, location, description, serialNumber, contactName, contactPhone, comapnyKey);

            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                if (c.ItemID == ci.ItemID)
                {
                    Assert.AreEqual(date, c.Date);
                    Assert.AreEqual(location, c.Location);
                    Assert.AreEqual(description, c.Description);
                    Assert.AreEqual(contactName, c.ContactName);
                    Assert.AreEqual(contactPhone, c.ContactPhone);
                }
            }
        }


        [TestMethod]
        public void TestEditItemWronItemID()
        {
            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];

            int olditemID = ci.ItemID;
            DateTime olddate = ci.Date;
            string oldlocation = ci.Location;
            string olddescription = ci.Description;
            int oldserialNumber = ci.SerialNumber;
            string oldcontactName = ci.ContactName;
            string oldcontactPhone = ci.ContactPhone;

            DateTime date = DateTime.Today;
            string location = "here";
            string description = "desc";
            int serialNumber = 444444;
            string contactName = "noamba";
            string contactPhone = "0459475859";

            IIM.editItem(-8, date, location, description, serialNumber, contactName, contactPhone, comapnyKey);
        }


        [TestMethod]
        public void TestEditItemWrongDate()
        {
            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];

            int olditemID = ci.ItemID;
            DateTime olddate = ci.Date;
            string oldlocation = ci.Location;
            string olddescription = ci.Description;
            int oldserialNumber = ci.SerialNumber;
            string oldcontactName = ci.ContactName;
            string oldcontactPhone = ci.ContactPhone;

            DateTime date = DateTime.Today;
            string location = "here";
            string description = "desc";
            int serialNumber = 444444;
            string contactName = "noamba";
            string contactPhone = "0459475859";

            IIM.editItem(olditemID, date.AddDays(1), location, description, serialNumber, contactName, contactPhone, comapnyKey);

            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                if (c.ItemID == olditemID)
                {
                    Assert.AreEqual(olddate, c.Date);
                    Assert.AreEqual(oldlocation, c.Location);
                    Assert.AreEqual(olddescription, c.Description);
                    Assert.AreEqual(oldcontactName, c.ContactName);
                    Assert.AreEqual(oldcontactPhone, c.ContactPhone);
                }
            }
        }


        [TestMethod]
        public void TestEditItemNullLocation()
        {
            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];

            int olditemID = ci.ItemID;
            DateTime olddate = ci.Date;
            string oldlocation = ci.Location;
            string olddescription = ci.Description;
            int oldserialNumber = ci.SerialNumber;
            string oldcontactName = ci.ContactName;
            string oldcontactPhone = ci.ContactPhone;

            DateTime date = DateTime.Today;
            string description = "desc";
            int serialNumber = 444444;
            string contactName = "noamba";
            string contactPhone = "0459475859";

            IIM.editItem(olditemID, date, null, description, serialNumber, contactName, contactPhone, comapnyKey);

            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                if (c.ItemID == olditemID)
                {
                    Assert.AreEqual(olddate, c.Date);
                    Assert.AreEqual(oldlocation, c.Location);
                    Assert.AreEqual(olddescription, c.Description);
                    Assert.AreEqual(oldcontactName, c.ContactName);
                    Assert.AreEqual(oldcontactPhone, c.ContactPhone);
                }
            }
        }


        [TestMethod]
        public void TestEditItemNulldescription()
        {
            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];

            int olditemID = ci.ItemID;
            DateTime olddate = ci.Date;
            string oldlocation = ci.Location;
            string olddescription = ci.Description;
            int oldserialNumber = ci.SerialNumber;
            string oldcontactName = ci.ContactName;
            string oldcontactPhone = ci.ContactPhone;

            DateTime date = DateTime.Today;
            string location = "here";
            int serialNumber = 444444;
            string contactName = "noamba";
            string contactPhone = "0459475859";

            IIM.editItem(olditemID, date, location, null, serialNumber, contactName, contactPhone, comapnyKey);

            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                if (c.ItemID == olditemID)
                {
                    Assert.AreEqual(olddate, c.Date);
                    Assert.AreEqual(oldlocation, c.Location);
                    Assert.AreEqual(olddescription, c.Description);
                    Assert.AreEqual(oldcontactName, c.ContactName);
                    Assert.AreEqual(oldcontactPhone, c.ContactPhone);
                }
            }
        }


        [TestMethod]
        public void TestEditItemnullContactName()
        {
            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];

            int olditemID = ci.ItemID;
            DateTime olddate = ci.Date;
            string oldlocation = ci.Location;
            string olddescription = ci.Description;
            int oldserialNumber = ci.SerialNumber;
            string oldcontactName = ci.ContactName;
            string oldcontactPhone = ci.ContactPhone;

            DateTime date = DateTime.Today;
            string location = "here";
            string description = "desc";
            int serialNumber = 444444;
            string contactPhone = "0459475859";

            IIM.editItem(olditemID, date, location, description, serialNumber, null, contactPhone, comapnyKey);

            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                if (c.ItemID == olditemID)
                {
                    Assert.AreEqual(olddate, c.Date);
                    Assert.AreEqual(oldlocation, c.Location);
                    Assert.AreEqual(olddescription, c.Description);
                    Assert.AreEqual(oldcontactName, c.ContactName);
                    Assert.AreEqual(oldcontactPhone, c.ContactPhone);
                }
            }
        }


        [TestMethod]
        public void TestEditItemNullPhone()
        {
            CompanyItem ci = IIM.getAllCompanyItems(comapnyKey)[0];

            int olditemID = ci.ItemID;
            DateTime olddate = ci.Date;
            string oldlocation = ci.Location;
            string olddescription = ci.Description;
            int oldserialNumber = ci.SerialNumber;
            string oldcontactName = ci.ContactName;
            string oldcontactPhone = ci.ContactPhone;

            DateTime date = DateTime.Today;
            string location = "here";
            string description = "desc";
            int serialNumber = 444444;
            string contactName = "noamba";

            IIM.editItem(olditemID, date, location, description, serialNumber, contactName, null, comapnyKey);

            foreach (CompanyItem c in IIM.getAllCompanyItems(comapnyKey))
            {
                if (c.ItemID == olditemID)
                {
                    Assert.AreEqual(olddate, c.Date);
                    Assert.AreEqual(oldlocation, c.Location);
                    Assert.AreEqual(olddescription, c.Description);
                    Assert.AreEqual(oldcontactName, c.ContactName);
                    Assert.AreEqual(oldcontactPhone, c.ContactPhone);
                }
            }
        }

        [TestMethod]
        public void TestGetCompanyItem()
        {
            foreach (CompanyItem ci in IIM.getAllCompanyItems(comapnyKey))
            {
                int itemID = ci.ItemID;
                CompanyItem item = IIM.getCompanyItem(itemID, comapnyKey);
                Assert.IsNotNull(item);
            }
        }

        [TestMethod]
        public void TestGetCompanyItemInvalidKey()
        {
            Random random = new Random();
            int compK = random.Next();
            while (compK.Equals(comapnyKey))
            {
                compK = random.Next();
            }
            foreach (CompanyItem ci in IIM.getAllCompanyItems(comapnyKey))
            {
                int itemID = ci.ItemID;
                CompanyItem item = IIM.getCompanyItem(itemID, compK);
                Assert.IsNull(item);
            }
        }

        [TestMethod]
        public void TestGetCompanyItemInvalidID()
        {
            Random random = new Random();
            int itemID = random.Next(); ;
            bool notFinish = true;
            while (notFinish)
            {
                itemID = random.Next();
                notFinish = false;
                foreach (CompanyItem ci in IIM.getAllCompanyItems(comapnyKey))
                {
                    if (ci.ItemID.Equals(itemID))
                    {
                        notFinish = true;
                    }
                }
            }
            CompanyItem item = IIM.getCompanyItem(itemID, comapnyKey);
            Assert.IsNull(item);
        }


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


        /*
        [TestMethod]
        public void TestAddCompanyWrong()
        {
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "Aa12345", "TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>());
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "Aa12345", "TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>());
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
            IDM.addComapny("TestAddCompany", "Aa12345", "TestAddCompany", "050000000", new System.Collections.Generic.HashSet<string>());
            Assert.IsNull(ICM.getCompanyByName("TestAddCompany"));
        }
        */
    }
}
