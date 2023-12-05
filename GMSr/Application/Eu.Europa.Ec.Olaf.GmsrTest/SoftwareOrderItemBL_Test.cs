using Eu.Europa.Ec.Olaf.GmsrBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{
    
    
    /// <summary>
    ///This is a test class for SoftwareOrderItemBL_Test and is intended
    ///to contain all SoftwareOrderItemBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class SoftwareOrderItemBL_Test
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
        ///A test for SoftwareOrderItemBL Constructor
        ///</summary>
        [TestMethod()]
        public void SoftwareOrderItemBLConstructorTest()
        {
            SoftwareOrderItemBL target = new SoftwareOrderItemBL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            SoftwareOrderItemBL target = new SoftwareOrderItemBL(); // TODO: Initialize to an appropriate value
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
            List<SoftwareOrderItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> actual;
            actual = SoftwareOrderItemBL.GetAll();
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
            List<SoftwareOrderItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> actual;
            actual = SoftwareOrderItemBL.GetByKeyword(keyword);
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
            List<SoftwareOrderItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> actual;
            actual = SoftwareOrderItemBL.GetByOrderForm(orderFormId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByParameters
        ///</summary>
        [TestMethod()]
        public void GetByParametersTest()
        {
            string order = string.Empty; // TODO: Initialize to an appropriate value
            string olafRef = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> softwareOrderItemTypeId = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string softwareProductSource = string.Empty; // TODO: Initialize to an appropriate value
            string softwareProductName = string.Empty; // TODO: Initialize to an appropriate value
            string softwareProductVersion = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStartFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStartTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEndFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEndTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string previousConcatenation = string.Empty; // TODO: Initialize to an appropriate value
            string comment = string.Empty; // TODO: Initialize to an appropriate value
            string creator = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> creationDateFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> creationDateTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            bool onlyWithoutUpgrade = false; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> actual;
            actual = SoftwareOrderItemBL.GetByParameters(order, olafRef, softwareOrderItemTypeId, softwareProductSource, softwareProductName, softwareProductVersion, dateStartFrom, dateStartTo, dateEndFrom, dateEndTo, previousConcatenation, comment, creator, creationDateFrom, creationDateTo, onlyWithoutUpgrade);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBySoftwareOrderType
        ///</summary>
        [TestMethod()]
        public void GetBySoftwareOrderTypeTest()
        {
            int softwareOrderTypeId = 0; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> actual;
            actual = SoftwareOrderItemBL.GetBySoftwareOrderType(softwareOrderTypeId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBySoftwareProduct
        ///</summary>
        [TestMethod()]
        public void GetBySoftwareProductTest()
        {
            int softwareProductId = 0; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> actual;
            actual = SoftwareOrderItemBL.GetBySoftwareProduct(softwareProductId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPackByInfix
        ///</summary>
        [TestMethod()]
        public void GetPackByInfixTest()
        {
            string infix = string.Empty; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareOrderItemBL> actual;
            actual = SoftwareOrderItemBL.GetPackByInfix(infix);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            SoftwareOrderItemBL target = new SoftwareOrderItemBL(); // TODO: Initialize to an appropriate value
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
            SoftwareOrderItemBL target = new SoftwareOrderItemBL(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Select(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Concatenation
        ///</summary>
        [TestMethod()]
        public void ConcatenationTest()
        {
            SoftwareOrderItemBL target = new SoftwareOrderItemBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Concatenation;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PreviousSoftwareOrderItemConcatenation
        ///</summary>
        [TestMethod()]
        public void PreviousSoftwareOrderItemConcatenationTest()
        {
            SoftwareOrderItemBL target = new SoftwareOrderItemBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.PreviousSoftwareOrderItemConcatenation;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
