using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkerHost.DataLayer;

namespace Test.UnitTests
{
    [TestClass]
    public class DatabaseUnitTests
    {
        private Database db;
        string validExistUserName = "testUser";
        string validNotExistUserName = "notExistUserName";
        string notValidUserName = "";
        string validExistCompanyName = "testCompany";
        string validNotExistCompanyName = "notExistCompanyName";
        string notValidCompanyName = "";
        string validExistUrl = "urlTest";
        string validNotExistUrl = "notExistUrl";
        string notValidUrl = "";


        [TestInitialize]
        public void setUp()
        {
            db = WorkerHost.DataLayer.Database.getInstance();
            db.clear();
            db.setUp();


        }

        [TestCleanup]
        public void cleanUp()
        {
            db.clear();
        }


        /******************************company tests***********************************/
        //////////////////////find company
        [TestMethod]
        public void find_company_valid_andExist()
        {
            string realCompanyName = "testCompany";
            WorkerHost.DataLayer.Companies company = db.findCompanyByCompanyName(realCompanyName);
            Assert.IsNotNull(company);
            Assert.AreEqual(company.companyName, realCompanyName);
        }
        [TestMethod]
        public void find_company_valid_andNotExist()
        {
            WorkerHost.DataLayer.Companies company = db.findCompanyByCompanyName("notRealCompany");
            Assert.IsNull(company);
        }
        [TestMethod]
        public void find_company_not_valid()
        {
            WorkerHost.DataLayer.Companies company = db.findCompanyByCompanyName("");
            Assert.IsNull(company);
        }


        //////////////////////add company
        [TestMethod]
        public void add_company_notExist_valid()
        {
            string userName = "tomer";
            string companyName = "myComp";
            string str = db.addCompany(userName, "Gg12345", companyName, "0522222222", null, "1212323214", new System.Collections.Generic.Dictionary<string, string>(),
                new System.Collections.Generic.Dictionary<string, string>());
            WorkerHost.DataLayer.User user = db.findUserByUserName(userName);
            WorkerHost.DataLayer.Companies company = db.findCompanyByCompanyName(companyName);
            Assert.AreEqual(str, "true");
            Assert.IsNotNull(company);
            Assert.IsNotNull(user);
            Assert.AreEqual(company.companyName, companyName);
            Assert.AreEqual(user.UserName, userName);
        }
        [TestMethod]
        public void add_company_Exist_valid()
        {
            string userName = "testUser";
            string companyName = "testCompany";
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null, "1212323214", new System.Collections.Generic.Dictionary<string, string>(),
                new System.Collections.Generic.Dictionary<string, string>());
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void add_company_userName_notValid()
        {
            string userName = "";
            string companyName = "testCompany";
            db.addCompany(userName, "tomer", companyName, "0522222222", null, "1212323214", new System.Collections.Generic.Dictionary<string, string>(),
                new System.Collections.Generic.Dictionary<string, string>());
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null, "1212323214", new System.Collections.Generic.Dictionary<string, string>(),
                new System.Collections.Generic.Dictionary<string, string>());
        }
        [TestMethod]
        public void add_company_companyName_notValid()
        {
            string userName = "testUser";
            string companyName = "";
            db.addCompany(userName, "tomer", companyName, "0522222222", null, "1212323214", new System.Collections.Generic.Dictionary<string, string>(),
                new System.Collections.Generic.Dictionary<string, string>());
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null, "1212323214", new System.Collections.Generic.Dictionary<string, string>(),
                new System.Collections.Generic.Dictionary<string, string>());
        }
        [TestMethod]
        public void remove_company_valid_exist()
        {
            string str = db.removeCompany(validExistCompanyName);
            Assert.AreEqual("true", str);
            WorkerHost.DataLayer.Companies company = db.findCompanyByCompanyName(validExistCompanyName);
            Assert.IsNull(company);
        }
        [TestMethod]
        public void remove_company_valid_notExist()
        {
            string str = db.removeCompany(validNotExistCompanyName);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.Companies company = db.findCompanyByCompanyName(validNotExistCompanyName);
            Assert.IsNull(company);
        }
        [TestMethod]
        public void remove_company_NotValid()
        {
            string str = db.removeCompany(notValidCompanyName);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.Companies company = db.findCompanyByCompanyName(notValidCompanyName);
            Assert.IsNull(company);
        }
        //////////////////////update company




        /******************************company items tests***********************************/

        ///////////////update company item

        /******************************facebook groups tests***********************************/

        ///////////////////add facebook group

        [TestMethod]
        public void add_facebookGroup_valid_exist_toExistCompany()
        {
            string str = db.addFacebookGroup(validExistCompanyName, validExistUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void add_facebookGroup_valid_NotExist_toExistCompany()
        {
            string str = db.addFacebookGroup(validExistCompanyName, validNotExistUrl);
            Assert.AreEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validNotExistUrl);
            Assert.IsNotNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_notValid_toExistCompany()
        {
            string str = db.addFacebookGroup(validExistCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_exist_toValidNotExistCompany()
        {
            string str = db.addFacebookGroup(validNotExistCompanyName, validExistUrl);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_Notexist_toValidNotExistCompany()
        {
            string str = db.addFacebookGroup(validNotExistCompanyName, validNotExistUrl);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_notValid_toValidNotExistCompany()
        {
            string str = db.addFacebookGroup(validNotExistCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_exist_toNotValidCompany()
        {
            string str = db.addFacebookGroup(notValidCompanyName, validExistUrl);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_Notexist_toNotValidCompany()
        {
            string str = db.addFacebookGroup(notValidCompanyName, validNotExistUrl);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_notValid_toNotValidCompany()
        {
            string str = db.addFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }

        //////////////////////////remove facebook groups
        [TestMethod]
        public void remove_facebookGroup_valid_exist_FromExistCompany()
        {
            string str = db.removeFacebookGroup(validExistCompanyName, validExistUrl);
            Assert.AreEqual("true", str);
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void remove_facebookGroup_valid_NotExist_fromExistCompany()
        {
            string str = db.removeFacebookGroup(validExistCompanyName, validNotExistUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_facebookGroup_notValid_fromExistCompany()
        {
            string str = db.removeFacebookGroup(validExistCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_facebookGroup_valid_exist_fromValidNotExistCompany()
        {
            string str = db.removeFacebookGroup(validNotExistCompanyName, validExistUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_facebookGroup_valid_Notexist_fromValidNotExistCompany()
        {
            string str = db.removeFacebookGroup(validNotExistCompanyName, validNotExistUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_facebookGroup_notValid_fromValidNotExistCompany()
        {
            string str = db.removeFacebookGroup(validNotExistCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_facebookGroup_valid_exist_fromNotValidCompany()
        {
            string str = db.removeFacebookGroup(notValidCompanyName, validExistUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_facebookGroup_valid_Notexist_toNotValidCompany()
        {
            string str = db.removeFacebookGroup(notValidCompanyName, validNotExistUrl);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_facebookGroup_notValid_toNotValidCompany()
        {
            string str = db.removeFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
        }

        //////////////////////////find facebook groups
        [TestMethod]
        public void find_facebookGroup_valid_exist_ExistCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validExistUrl);
            Assert.IsNotNull(fbg);
            Assert.AreEqual(fbg.groupURL, validExistUrl);
            Assert.AreEqual(fbg.CompanyName, validExistCompanyName);
        }
        [TestMethod]
        public void find_facebookGroup_valid_NotExist_fromExistCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_notValid_ExistCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_exist_ValidNotExistCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_Notexist_ValidNotExistCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_notValid_ValidNotExistCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_exist_mNotValidCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_Notexist_NotValidCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_notValid_toNotValidCompany()
        {
            WorkerHost.DataLayer.FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }

        /******************************items tests***********************************/
        //////////////////////////find item
        [TestMethod]
        public void findIdTests()
        {
            bool isWork = db.findIdTests();
            Assert.IsTrue(isWork);
        }
        /// <summary>
        /// //////////////////////remove item
        /// </summary>
        [TestMethod]
        public void removeFoundItem()
        {
            int ret = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsTrue(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNotNull(item);
        }
        [TestMethod]
        public void removeLostItem()
        {
            int ret = db.addLostItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsTrue(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNotNull(item);
        }
        [TestMethod]
        public void removeFBItem()
        {
            int ret = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            bool cond = ret > 0;
            Assert.IsTrue(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNotNull(item);
        }

        /******************************FB items tests***********************************/

        ///////////////add FB item
        [TestMethod]
        public void addFbItem()
        {
            int ret = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            bool cond = ret > 0;
            Assert.IsTrue(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNotNull(item);
        }

        ///////////////update FB item
        [TestMethod]
        public void updateFbItem()
        {
            int ret = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            string update = db.updateFBItem(ret, new System.Collections.Generic.List<string>(), "", DateTime.Now, "1", "1", "1", "1", "1");
            Assert.AreEqual("true", update);
            WorkerHost.DataLayer.FBItem item = db.findItemByItemId(ret).FBItem;
            Assert.IsNotNull(item.colors);
            Assert.AreEqual(item.itemType, "1");
            Assert.AreEqual(item.location, "1");
            Assert.AreEqual(item.postId, "1");
            Assert.AreEqual(item.publisherName, "1");
            Assert.AreEqual(item.type, "1");
        }
        /******************************found items tests***********************************/
        ///////////////add found item
        [TestMethod]
        public void addFoundItemValidExistCompany()
        {
            int ret = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsTrue(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNotNull(item);
        }
        [TestMethod]
        public void addFoundItemValidNotExistCompany()
        {
            int ret = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validNotExistCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsFalse(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNull(item);
        }
        [TestMethod]
        public void addFoundItemNotValidCompany()
        {
            int ret = db.addFoundItem(null, null, DateTime.Now, null, null, 0, notValidCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsFalse(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNull(item);
        }

        ///////////////update found item
        [TestMethod]
        public void updateFoundItem()
        {
            int ret = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            string update = db.updateFoundItem(ret, new System.Collections.Generic.List<string>(), "", DateTime.Now, "", "", "", false);
            Assert.AreEqual("true", update);
            WorkerHost.DataLayer.FoundItems item = db.findItemByItemId(ret).CompanyItems.FoundItems;
            Assert.IsNotNull(item.colors);
            Assert.AreEqual(item.itemType, "");
            Assert.AreEqual(item.location, "");
            Assert.AreEqual(item.delivered, false);
            Assert.AreEqual(item.photoLocation, "");
            Assert.AreEqual(item.description, "");
        }

        /******************************lost items tests***********************************/
        ///////////////add lost item
        [TestMethod]
        public void addLostItemValidExistCompany()
        {
            int ret = db.addLostItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsTrue(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNotNull(item);
        }
        [TestMethod]
        public void addLostItemValidNotExistCompany()
        {
            int ret = db.addLostItem(null, null, DateTime.Now, null, null, 0, validNotExistCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsFalse(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNull(item);
        }
        [TestMethod]
        public void addLostItemNotValidCompany()
        {
            int ret = db.addLostItem(null, null, DateTime.Now, null, null, 0, notValidCompanyName, null, null, null, true);
            bool cond = ret > 0;
            Assert.IsFalse(cond);
            WorkerHost.DataLayer.Items item = db.findItemByItemId(ret);
            Assert.IsNull(item);
        }

        ///////////////update lost item
        [TestMethod]
        public void updateLostItem()
        {
            int ret = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            string update = db.updateFoundItem(ret, new System.Collections.Generic.List<string>(), "", DateTime.Now, "", "", "", false);
            Assert.AreEqual("true", update);
            WorkerHost.DataLayer.FoundItems item = db.findItemByItemId(ret).CompanyItems.FoundItems;
            Assert.IsNotNull(item.colors);
            Assert.AreEqual(item.itemType, "");
            Assert.AreEqual(item.location, "");
            Assert.AreEqual(item.delivered, false);
            Assert.AreEqual(item.photoLocation, "");
            Assert.AreEqual(item.description, "");
        }

        /******************************matches tests***********************************/

        ///////////////add match
        [TestMethod]
        public void add_match_existingCompanyItem_ExistingItem()
        {
            int cItemId = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            int itemId = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);

            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsTrue(cond);
            WorkerHost.DataLayer.Matches match = db.findMathByMatchId(matchId);
            Assert.IsNotNull(match);
            Assert.AreEqual(cItemId, match.CompanyItems.itemId);
            Assert.AreEqual(itemId, match.Items.itemID);
        }
        [TestMethod]
        public void add_match_notValidCompanyItem_ExistingItem()
        {
            int cItemId = -1;
            int itemId = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }
        [TestMethod]
        public void add_match_notExistingCompanyItem_ExistingItem()
        {
            int cItemId = 934892;
            int itemId = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }
        [TestMethod]
        public void add_match_existingCompanyItem_notExistingItem()
        {
            int cItemId = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            int itemId = 57483;
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }
        [TestMethod]
        public void add_match_notValidCompanyItem_notExistingItem()
        {
            int cItemId = -1;
            int itemId = 57483;
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }
        [TestMethod]
        public void add_match_notExistingCompanyItem_notExistingItem()
        {
            int cItemId = 934892;
            int itemId = 57483;
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }
        [TestMethod]
        public void add_match_existingCompanyItem_notValidItem()
        {
            int cItemId = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            int itemId = -1;
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }
        [TestMethod]
        public void add_match_notValidCompanyItem_notValidItem()
        {
            int cItemId = -1;
            int itemId = -1;
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }
        [TestMethod]
        public void add_match_notExistingCompanyItem_notvalidItem()
        {
            int cItemId = 934892;
            int itemId = -1;
            int matchId = db.addMatch(cItemId, itemId, "");
            bool cond = matchId > 0;
            Assert.IsFalse(cond);
        }


        ////////////////////////remove match
        [TestMethod]
        public void removeMatch()
        {
            int cItemId = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            int itemId = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            int matchId = db.addMatch(cItemId, itemId, "");
            db.removeMatch(matchId);
            WorkerHost.DataLayer.Matches match = db.findMathByMatchId(matchId);
            Assert.IsNull(match);
        }

        ////////////////////////update match
        [TestMethod]
        public void updateMatch()
        {
            int cItemId = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            int itemId = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            int matchId = db.addMatch(cItemId, itemId, "");
            db.updateMatch(matchId, "hello");
            WorkerHost.DataLayer.Matches match = db.findMathByMatchId(matchId);
            Assert.IsNotNull(match);
            Assert.AreEqual("hello", match.matchStatus);
        }

        ////////////////////////update match
        [TestMethod]
        public void findMatchValidExistId()
        {
            int cItemId = db.addFoundItem(null, null, DateTime.Now, null, null, 0, validExistCompanyName, null, null, null, true);
            int itemId = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            int matchId = db.addMatch(cItemId, itemId, "");
            WorkerHost.DataLayer.Matches match = db.findMathByMatchId(matchId);
            Assert.IsNotNull(match);
        }
        [TestMethod]
        public void findMatchValidNotExistId()
        {
            WorkerHost.DataLayer.Matches match = db.findMathByMatchId(337484);
            Assert.IsNull(match);
        }
        [TestMethod]
        public void findMatchNotValidId()
        {
            WorkerHost.DataLayer.Matches match = db.findMathByMatchId(-1);
            Assert.IsNull(match);
        }

        /******************************user tests***********************************/
        //////////////////////find user
        [TestMethod]
        public void find_user_valid_andExist()
        {
            WorkerHost.DataLayer.User user = db.findUserByUserName(validExistUserName);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.UserName, validExistUserName);
        }
        [TestMethod]
        public void find_user_valid_andNotExist()
        {
            WorkerHost.DataLayer.User user = db.findUserByUserName(validNotExistUserName);
            Assert.IsNull(user);
        }
        [TestMethod]
        public void find_user_not_valid()
        {
            WorkerHost.DataLayer.User user = db.findUserByUserName(notValidUserName);
            Assert.IsNull(user);
        }


        //////////////////////add user
        [TestMethod]
        public void add_user_notExist_valid()
        {
            string str = db.addUser(validNotExistUserName, "", true);
            WorkerHost.DataLayer.User user = db.findUserByUserName(validNotExistUserName);
            Assert.AreEqual(str, "true");
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void add_user_Exist_valid()
        {
            string str = db.addUser(validExistUserName, "", true);
            WorkerHost.DataLayer.User user = db.findUserByUserName(validExistUserName);
            Assert.AreNotEqual(str, "true");
        }
        [TestMethod]
        public void add_user_userName_notValid()
        {
            string str = db.addUser(notValidUserName, "", true);
            Assert.AreNotEqual(str, "true");
        }
        [TestMethod]
        public void remove_user_valid_exist()
        {
            string str = db.removeUser(validExistUserName);
            Assert.AreEqual("true", str);
            WorkerHost.DataLayer.User user = db.findUserByUserName(validExistUserName);
            Assert.IsNull(user);
        }
        [TestMethod]
        public void remove_user_valid_notExist()
        {
            string str = db.removeUser(validNotExistUserName);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void remove_user_NotValid()
        {
            string str = db.removeUser(notValidUserName);
            Assert.AreNotEqual("true", str);
        }

        /////////////////////update user
        [TestMethod]
        public void update_user()
        {
            string str = db.updateUser(validExistUserName, "new");
            Assert.AreEqual(str, "true");
            WorkerHost.DataLayer.User user = db.findUserByUserName(validExistUserName);
            Assert.AreEqual(user.password, "new");
        }

    }
}
