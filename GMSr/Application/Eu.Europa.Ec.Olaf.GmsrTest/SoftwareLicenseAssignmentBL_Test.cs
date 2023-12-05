using Eu.Europa.Ec.Olaf.GmsrBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{
    
    
    /// <summary>
    ///This is a test class for SoftwareLicenseAssignmentBL_Test and is intended
    ///to contain all SoftwareLicenseAssignmentBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class SoftwareLicenseAssignmentBL_Test
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
        ///A test for SoftwareLicenseAssignmentBL Constructor
        ///</summary>
        [TestMethod()]
        public void SoftwareLicenseAssignmentBLConstructorTest()
        {
            SoftwareLicenseAssignmentBL target = new SoftwareLicenseAssignmentBL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetByOlafUser
        ///</summary>
        [TestMethod()]
        public void GetByOlafUserTest()
        {
            string userLogin = string.Empty; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetByOlafUser(userLogin);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByOlafUser
        ///</summary>
        [TestMethod()]
        public void GetByOlafUserTest1()
        {
            int olafUserId = 0; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetByOlafUser(olafUserId);
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
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetByKeyword(keyword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByHardwareItem
        ///</summary>
        [TestMethod()]
        public void GetByHardwareItemTest()
        {
            int hardwareItemId = 0; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetByHardwareItem(hardwareItemId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetAll();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            SoftwareLicenseAssignmentBL target = new SoftwareLicenseAssignmentBL(); // TODO: Initialize to an appropriate value
            ArrayList validationErrorList = null; // TODO: Initialize to an appropriate value
            ArrayList validationErrorListExpected = null; // TODO: Initialize to an appropriate value
            string userLogin = string.Empty; // TODO: Initialize to an appropriate value
            target.Delete(ref validationErrorList, userLogin);
            Assert.AreEqual(validationErrorListExpected, validationErrorList);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetByOlafUserAndHardwareItem
        ///</summary>
        [TestMethod()]
        public void GetByOlafUserAndHardwareItemTest()
        {
            int olafUserId = 0; // TODO: Initialize to an appropriate value
            int hardwareItemId = 0; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetByOlafUserAndHardwareItem(olafUserId, hardwareItemId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByParameters
        ///</summary>
        [TestMethod()]
        public void GetByParametersTest()
        {
            string softwareProductName = string.Empty; // TODO: Initialize to an appropriate value
            string softwareProductVersion = string.Empty; // TODO: Initialize to an appropriate value
            string status = string.Empty; // TODO: Initialize to an appropriate value
            string olafUser = string.Empty; // TODO: Initialize to an appropriate value
            string hardwareItem = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> assignmentDateFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> assignmentDateTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> installationDateFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> installationDateTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> unassignmentDateFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> unassignmentDateTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string comment = string.Empty; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetByParameters(softwareProductName, softwareProductVersion, status, olafUser, hardwareItem, assignmentDateFrom, assignmentDateTo, installationDateFrom, installationDateTo, unassignmentDateFrom, unassignmentDateTo, comment);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBySoftwareLicense
        ///</summary>
        [TestMethod()]
        public void GetBySoftwareLicenseTest()
        {
            int softwareLicenseId = 0; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseAssignmentBL> actual;
            actual = SoftwareLicenseAssignmentBL.GetBySoftwareLicense(softwareLicenseId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBySoftwareLicenseAndHardwareItem
        ///</summary>
        [TestMethod()]
        public void GetBySoftwareLicenseAndHardwareItemTest()
        {
            int softwareLicenseId = 0; // TODO: Initialize to an appropriate value
            int hardwareItemId = 0; // TODO: Initialize to an appropriate value
            SoftwareLicenseAssignmentBL expected = null; // TODO: Initialize to an appropriate value
            SoftwareLicenseAssignmentBL actual;
            actual = SoftwareLicenseAssignmentBL.GetBySoftwareLicenseAndHardwareItem(softwareLicenseId, hardwareItemId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBySoftwareLicenseAndOlafUser
        ///</summary>
        [TestMethod()]
        public void GetBySoftwareLicenseAndOlafUserTest()
        {
            int softwareLicenseId = 0; // TODO: Initialize to an appropriate value
            int olafUserId = 0; // TODO: Initialize to an appropriate value
            SoftwareLicenseAssignmentBL expected = null; // TODO: Initialize to an appropriate value
            SoftwareLicenseAssignmentBL actual;
            actual = SoftwareLicenseAssignmentBL.GetBySoftwareLicenseAndOlafUser(softwareLicenseId, olafUserId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            SoftwareLicenseAssignmentBL target = new SoftwareLicenseAssignmentBL(); // TODO: Initialize to an appropriate value
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
            SoftwareLicenseAssignmentBL target = new SoftwareLicenseAssignmentBL(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Select(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Status
        ///</summary>
        [TestMethod()]
        public void StatusTest()
        {
            SoftwareLicenseAssignmentBL target = new SoftwareLicenseAssignmentBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Status;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
