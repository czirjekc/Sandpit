using Eu.Europa.Ec.Olaf.GmsrBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{
    
    
    /// <summary>
    ///This is a test class for OrderFormDocumentBL_Test and is intended
    ///to contain all OrderFormDocumentBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class OrderFormDocumentBL_Test
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
        ///A test for FileSize
        ///</summary>
        [TestMethod()]
        public void FileSizeTest()
        {
            OrderFormDocumentBL target = new OrderFormDocumentBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FileSize;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FileInfo
        ///</summary>
        [TestMethod()]
        public void FileInfoTest()
        {
            OrderFormDocumentBL target = new OrderFormDocumentBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FileInfo;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for File
        ///</summary>
        [TestMethod()]
        public void FileTest()
        {
            OrderFormDocumentBL target = new OrderFormDocumentBL(); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            target.File = expected;
            actual = target.File;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Select
        ///</summary>
        [TestMethod()]
        public void SelectTest()
        {
            OrderFormDocumentBL target = new OrderFormDocumentBL(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Select(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            OrderFormDocumentBL target = new OrderFormDocumentBL(); // TODO: Initialize to an appropriate value
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
        ///A test for GetByOrderForm
        ///</summary>
        [TestMethod()]
        public void GetByOrderFormTest()
        {
            int orderFormId = 0; // TODO: Initialize to an appropriate value
            List<OrderFormDocumentBL> expected = null; // TODO: Initialize to an appropriate value
            List<OrderFormDocumentBL> actual;
            actual = OrderFormDocumentBL.GetByOrderForm(orderFormId);
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
            List<OrderFormDocumentBL> expected = null; // TODO: Initialize to an appropriate value
            List<OrderFormDocumentBL> actual;
            actual = OrderFormDocumentBL.GetByKeyword(keyword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            List<OrderFormDocumentBL> expected = null; // TODO: Initialize to an appropriate value
            List<OrderFormDocumentBL> actual;
            actual = OrderFormDocumentBL.GetAll();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            OrderFormDocumentBL target = new OrderFormDocumentBL(); // TODO: Initialize to an appropriate value
            ArrayList validationErrorList = null; // TODO: Initialize to an appropriate value
            ArrayList validationErrorListExpected = null; // TODO: Initialize to an appropriate value
            string userLogin = string.Empty; // TODO: Initialize to an appropriate value
            target.Delete(ref validationErrorList, userLogin);
            Assert.AreEqual(validationErrorListExpected, validationErrorList);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OrderFormDocumentBL Constructor
        ///</summary>
        [TestMethod()]
        public void OrderFormDocumentBLConstructorTest()
        {
            OrderFormDocumentBL target = new OrderFormDocumentBL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
