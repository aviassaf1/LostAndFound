using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;

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
            db = Database.getInstance();
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
            Companies company = db.findCompanyByCompanyName(realCompanyName);
            Assert.IsNotNull(company);
            Assert.AreEqual(company.companyName, realCompanyName);
        }
        [TestMethod]
        public void find_company_valid_andNotExist()
        {
            Companies company = db.findCompanyByCompanyName("notRealCompany");
            Assert.IsNull(company);
        }
        [TestMethod]
        public void find_company_not_valid()
        {
            Companies company = db.findCompanyByCompanyName("");
            Assert.IsNull(company);
        }


        //////////////////////add company
        [TestMethod]
        public void add_company_notExist_valid()
        {
            string userName = "tomer";
            string companyName = "myComp";
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
            User user = db.findUserByUserName(userName);
            Companies company = db.findCompanyByCompanyName(companyName);
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
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
            Assert.AreNotEqual("true", str);
        }
        [TestMethod]
        public void add_company_userName_notValid()
        {
            string userName = "";
            string companyName = "testCompany";
            db.addCompany(userName, "tomer", companyName, "0522222222", null);
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
        }
        [TestMethod]
        public void add_company_companyName_notValid()
        {
            string userName = "testUser";
            string companyName = "";
            db.addCompany(userName, "tomer", companyName, "0522222222", null);
            string str = db.addCompany(userName, "tomer", companyName, "0522222222", null);
        }
        [TestMethod]
        public void remove_company_valid_exist()
        {
            string str = db.removeCompany(validExistCompanyName);
            Assert.AreEqual("true", str);
            Companies company = db.findCompanyByCompanyName(validExistCompanyName);
            Assert.IsNull(company);
        }
        [TestMethod]
        public void remove_company_valid_notExist()
        {
            string str = db.removeCompany(validNotExistCompanyName);
            Assert.AreNotEqual("true", str);
            Companies company = db.findCompanyByCompanyName(validNotExistCompanyName);
            Assert.IsNull(company);
        }
        [TestMethod]
        public void remove_company_NotValid()
        {
            string str = db.removeCompany(notValidCompanyName);
            Assert.AreNotEqual("true", str);
            Companies company = db.findCompanyByCompanyName(notValidCompanyName);
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
            FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validNotExistUrl);
            Assert.IsNotNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_notValid_toExistCompany()
        {
            string str = db.addFacebookGroup(validExistCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_exist_toValidNotExistCompany()
        {
            string str = db.addFacebookGroup(validNotExistCompanyName, validExistUrl);
            Assert.AreNotEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_Notexist_toValidNotExistCompany()
        {
            string str = db.addFacebookGroup(validNotExistCompanyName, validNotExistUrl);
            Assert.AreNotEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_notValid_toValidNotExistCompany()
        {
            string str = db.addFacebookGroup(validNotExistCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_exist_toNotValidCompany()
        {
            string str = db.addFacebookGroup(notValidCompanyName, validExistUrl);
            Assert.AreNotEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_valid_Notexist_toNotValidCompany()
        {
            string str = db.addFacebookGroup(notValidCompanyName, validNotExistUrl);
            Assert.AreNotEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void add_facebookGroup_notValid_toNotValidCompany()
        {
            string str = db.addFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.AreNotEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }

        //////////////////////////remove facebook groups
        [TestMethod]
        public void remove_facebookGroup_valid_exist_FromExistCompany()
        {
            string str = db.removeFacebookGroup(validExistCompanyName, validExistUrl);
            Assert.AreEqual("true", str);
            FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validExistUrl);
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
            FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validExistUrl);
            Assert.IsNotNull(fbg);
            Assert.AreEqual(fbg.groupURL, validExistUrl);
            Assert.AreEqual(fbg.CompanyName, validExistCompanyName);
        }
        [TestMethod]
        public void find_facebookGroup_valid_NotExist_fromExistCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_notValid_ExistCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(validExistCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_exist_ValidNotExistCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_Notexist_ValidNotExistCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(validNotExistCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_notValid_ValidNotExistCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_exist_mNotValidCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_valid_Notexist_NotValidCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, validNotExistUrl);
            Assert.IsNull(fbg);
        }
        [TestMethod]
        public void find_facebookGroup_notValid_toNotValidCompany()
        {
            FacebookGroups fbg = db.findFacebookGroup(notValidCompanyName, notValidUrl);
            Assert.IsNull(fbg);
        }

        /******************************items tests***********************************/
        [TestMethod]
        public void findIdTests()
        {
            bool isWork = db.findIdTests();
            Assert.IsTrue(isWork);
        }
        /******************************FB items tests***********************************/

        ///////////////add FB item
        [TestMethod]
        public void addFbItem()
        {
            int ret = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            bool cond = ret > 0;
            Assert.IsTrue(cond);
            Items item = db.findItemByItemId(ret);
            Assert.IsNotNull(item);
        }

        ///////////////update FB item
        [TestMethod]
        public void updateFbItem()
        {
            int ret = db.addFBItem(null, null, DateTime.Now, null, null, null, null, null);
            string update = db.updateFBItem(ret, new System.Collections.Generic.List<string>(), "", DateTime.Now, "1", "1", "1", "1", "1");
            Assert.AreEqual("true", update);
            FBItem item = db.findItemByItemId(ret).FBItem;
            Assert.IsNotNull(item.colors);
            Assert.AreEqual(item.itemType, "1");
            Assert.AreEqual(item.location, "1");
            Assert.AreEqual(item.postId, "1");
            Assert.AreEqual(item.publisherName, "1");
            Assert.AreEqual(item.type, "1");
        }



    }
}
