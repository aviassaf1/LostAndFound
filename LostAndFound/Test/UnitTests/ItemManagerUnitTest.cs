using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using Domain;
using Domain.BLBackEnd;
using Domain.Managers;
using System.Collections.Generic;

namespace Test.UnitTests
{
    [TestClass]
    public class ItemManagerUnitTest
    {
        private Cache cache;
        private Database db;
        private IItemManager IIM;
        private ICompanyManager ICM;
        private string token="";
        [TestInitialize]
        public void setUp()
        {
            db = Database.getInstance();
            db.clear();
            cache = Cache.getInstance;
            cache.initCache();
            cache.setUp();
            IIM = ItemManager.getInstance;
            ICM = ComapanyManager.getInstance;
        }

        [TestCleanup]
        public void cleanUp()
        {
            cache.clear();
        }
        
        [TestMethod]
        public void TestGetAllCompanyItems()
        {
            List <CompanyItem> compItems=IIM.getAllCompanyItems("Guy");
            Assert.IsNotNull(compItems);
            Assert.AreEqual(6, compItems.ToArray().Length);
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestGetAllCompanyItemsNullCopamany()
        {
            List<CompanyItem> compItems = IIM.getAllCompanyItems(null);
            Assert.IsNull(compItems);
        }

        [TestMethod]
        public void TestGetAllCompanyItemsNotExit()
        {
            List<CompanyItem> compItems = IIM.getAllCompanyItems("");
            Assert.IsNull(compItems);
        }

        [TestMethod]
        public void TestAddLostItem()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string>{ "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today,"location", "description",
            4564, "Guy2","contactName","054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(1, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNoColors()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> ();
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(1, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNull()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string>();
            IIM.addLostItem(null, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNullType()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, null, DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNulllLocation()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, null, "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNullDescription()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", null,
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNullCompany()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, null, "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNullContactName()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", null, "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNullPhone()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", null, "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNullPhotoLocation()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", null, token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNullToken()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", null);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemNoType()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemBiggerDate()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today.AddDays(2), "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemWrongComapny()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddLostItemBadItem()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addLostItem(sColors, "fol", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItem()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(1, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNoColors()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string>();
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(1, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNull()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string>();
            IIM.addFoundItem(null, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNullType()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, null, DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNulllLocation()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, null, "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNullDescription()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", null,
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNullCompany()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, null, "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNullContactName()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", null, "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNullPhone()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", null, "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNullPhotoLocation()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", null, token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNullToken()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", null);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemNoType()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemBiggerDate()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today.AddDays(2), "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemWrongComapny()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "FOLDER", DateTime.Today, "location", "description",
            4564, "", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }

        [TestMethod]
        public void TestAddFoundItemBadItem()
        {
            List<CompanyItem> compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.IsNotNull(compItems2);
            Assert.AreEqual(0, compItems2.ToArray().Length);
            List<string> sColors = new List<string> { "BLUE", "GRAY" };
            IIM.addFoundItem(sColors, "fol", DateTime.Today, "location", "description",
            4564, "Guy2", "contactName", "054444444", "photoLocation", token);
            compItems2 = IIM.getAllCompanyItems("Guy2");
            Assert.AreEqual(0, compItems2.ToArray().Length);
        }


        [TestMethod]
        public void TestTransactionComplete()
        {

            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];
            if ((ci.GetType()).Equals(typeof(FoundItem)))
                Assert.IsFalse(((FoundItem)ci).Delivered );
            if ((ci.GetType()).Equals(typeof(LostItem)))
                Assert.IsFalse(((LostItem)ci).WasFound);
            IIM.transactionComplete(ci.ItemID);
            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
            {
                if(c.ItemID==ci.ItemID)
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
            IIM.transactionComplete(-7);
        }

        [TestMethod]
        public void TestDeleteItem()
        {

            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];
            int len = IIM.getAllCompanyItems("Guy").ToArray().Length;
            IIM.deleteItem(ci.ItemID);
            Assert.AreEqual(len - 1, IIM.getAllCompanyItems("Guy").ToArray().Length);
            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
            {
                Assert.AreNotEqual(ci.ItemID, c.ItemID);
            }
        }

        [TestMethod]
        public void TestDeleteItemNoItem()
        {
            int len = IIM.getAllCompanyItems("Guy").ToArray().Length;
            IIM.deleteItem(-7);
            Assert.AreEqual(len, IIM.getAllCompanyItems("Guy").ToArray().Length);
        }


        [TestMethod]
        public void TestEditItem()
        {
            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];

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

            IIM.editItem(olditemID, date, location, description, serialNumber, contactName, contactPhone);

            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
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
            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];

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

            IIM.editItem(-8, date, location, description, serialNumber, contactName, contactPhone);
        }


        [TestMethod]
        public void TestEditItemWrongDate()
        {
            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];

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

            IIM.editItem(olditemID, date.AddDays(1), location, description, serialNumber, contactName, contactPhone);

            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
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
            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];

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

            IIM.editItem(olditemID, date, null, description, serialNumber, contactName, contactPhone);

            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
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
            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];

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

            IIM.editItem(olditemID, date, location, null, serialNumber, contactName, contactPhone);

            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
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
            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];

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

            IIM.editItem(olditemID, date, location, description, serialNumber, null, contactPhone);

            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
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
            CompanyItem ci = IIM.getAllCompanyItems("Guy")[0];

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

            IIM.editItem(olditemID, date, location, description, serialNumber, contactName, null);

            foreach (CompanyItem c in IIM.getAllCompanyItems("Guy"))
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
