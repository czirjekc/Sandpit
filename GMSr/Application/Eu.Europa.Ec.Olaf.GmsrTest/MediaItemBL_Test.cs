using Eu.Europa.Ec.Olaf.GmsrBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{
    
    
    /// <summary>
    ///This is a test class for MediaItemBL_Test and is intended
    ///to contain all MediaItemBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class MediaItemBL_Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for MediaItemBL Constructor
        ///</summary>
        [TestMethod()]
        public void MediaItemBLConstructorTest()
        {
            MediaItemBL target = new MediaItemBL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            MediaItemBL target = new MediaItemBL(); // TODO: Initialize to an appropriate value
            ArrayList validationErrorList = null; // TODO: Initialize to an appropriate value
            ArrayList validationErrorListExpected = null; // TODO: Initialize to an appropriate value
            string userLogin = string.Empty; // TODO: Initialize to an appropriate value
            target.Delete(ref validationErrorList, userLogin);
            Assert.AreEqual(validationErrorListExpected, validationErrorList);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            List<MediaItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<MediaItemBL> actual;
            actual = MediaItemBL.GetAll();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByKeyword
        ///</summary>
        [TestMethod()]
        public void GetByKeywordTest()
        {
            string keyword = string.Empty; // TODO: Initialize to an appropriate value
            List<MediaItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<MediaItemBL> actual;
            actual = MediaItemBL.GetByKeyword(keyword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBySoftwareOrder
        ///</summary>
        [TestMethod()]
        public void GetBySoftwareOrderTest()
        {
            int softwareOrderItemId = 0; // TODO: Initialize to an appropriate value
            List<MediaItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<MediaItemBL> actual;
            actual = MediaItemBL.GetBySoftwareOrder(softwareOrderItemId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            MediaItemBL target = new MediaItemBL(); // TODO: Initialize to an appropriate value
            ArrayList validationErrorList = null; // TODO: Initialize to an appropriate value
            ArrayList validationErrorListExpected = null; // TODO: Initialize to an appropriate value
            string userLogin = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Save(ref validationErrorList, userLogin);
            Assert.AreEqual(validationErrorListExpected, validationErrorList);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Select
        ///</summary>
        [TestMethod()]
        public void SelectTest()
        {
            MediaItemBL target = new MediaItemBL(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Select(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
