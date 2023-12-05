using Eu.Europa.Ec.Olaf.GmsrBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{


    /// <summary>
    ///This is a test class for SoftwareLicenseBL_Test and is intended
    ///to contain all SoftwareLicenseBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class SoftwareLicenseBL_Test
    {


        /// <summary>
        ///A test for SoftwareLicenseBL Constructor
        ///</summary>
        [TestMethod()]
        public void SoftwareLicenseBLConstructorTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
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
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetAll();
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
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetByHardwareItem(hardwareItemId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByInfix
        ///</summary>
        [TestMethod()]
        public void GetByInfixTest()
        {
            string infix = string.Empty; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetByInfix(infix);
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
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetByKeyword(keyword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByOlafUser
        ///</summary>
        [TestMethod()]
        public void GetByOlafUserTest()
        {
            string userLogin = string.Empty; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetByOlafUser(userLogin);
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
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetByOlafUser(olafUserId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByParameters
        ///</summary>
        [TestMethod()]
        public void GetByParametersTest()
        {
            Nullable<int> idFrom = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> idTo = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> multiUserQuantityFrom = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> multiUserQuantityTo = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> softwareOrderItemIdFrom = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> softwareOrderItemIdTo = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> softwareOrderPreviousIdFrom = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> softwareOrderPreviousIdTo = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> previousIdFrom = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> previousIdTo = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> softwareLicenseTypeId = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> softwareOrderTypeId = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> softwareProductStatusId = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStartFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStartTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEndFrom = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEndTo = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string serialKey = string.Empty; // TODO: Initialize to an appropriate value
            string softwareOrderOlafRef = string.Empty; // TODO: Initialize to an appropriate value
            string softwareProductName = string.Empty; // TODO: Initialize to an appropriate value
            string softwareProductVersion = string.Empty; // TODO: Initialize to an appropriate value
            string softwareProductCompanyName = string.Empty; // TODO: Initialize to an appropriate value
            string softwareProductSource = string.Empty; // TODO: Initialize to an appropriate value
            string availability = string.Empty; // TODO: Initialize to an appropriate value
            string orderFormName = string.Empty; // TODO: Initialize to an appropriate value
            string specificContractName = string.Empty; // TODO: Initialize to an appropriate value
            string contractFrameworkName = string.Empty; // TODO: Initialize to an appropriate value
            bool onlyWithoutUpgrade = false; // TODO: Initialize to an appropriate value
            bool onlyOneLicensePerProduct = false; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetByParameters(idFrom, idTo, multiUserQuantityFrom, multiUserQuantityTo, softwareOrderItemIdFrom, softwareOrderItemIdTo, softwareOrderPreviousIdFrom, softwareOrderPreviousIdTo, previousIdFrom, previousIdTo, softwareLicenseTypeId, softwareOrderTypeId, softwareProductStatusId, dateStartFrom, dateStartTo, dateEndFrom, dateEndTo, serialKey, softwareOrderOlafRef, softwareProductName, softwareProductVersion, softwareProductCompanyName, softwareProductSource, availability, orderFormName, specificContractName, contractFrameworkName, onlyWithoutUpgrade, onlyOneLicensePerProduct);
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
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetBySoftwareOrder(softwareOrderItemId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetFreeByInfix
        ///</summary>
        [TestMethod()]
        public void GetFreeByInfixTest()
        {
            string infix = string.Empty; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> expected = null; // TODO: Initialize to an appropriate value
            List<SoftwareLicenseBL> actual;
            actual = SoftwareLicenseBL.GetFreeByInfix(infix);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
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
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Select(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AssignmentsCount
        ///</summary>
        [TestMethod()]
        public void AssignmentsCountTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.AssignmentsCount = expected;
            actual = target.AssignmentsCount;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Availability
        ///</summary>
        [TestMethod()]
        public void AvailabilityTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Availability;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Concatenation
        ///</summary>
        [TestMethod()]
        public void ConcatenationTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Concatenation;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FileInfo
        ///</summary>
        [TestMethod()]
        public void FileInfoTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FileInfo;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FileSize
        ///</summary>
        [TestMethod()]
        public void FileSizeTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FileSize;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Filename
        ///</summary>
        [TestMethod()]
        public void FilenameTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Filename = expected;
            actual = target.Filename;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SoftwareOrderItemHasUpgrade
        ///</summary>
        [TestMethod()]
        public void SoftwareOrderItemHasUpgradeTest()
        {
            SoftwareLicenseBL target = new SoftwareLicenseBL(); // TODO: Initialize to an appropriate value
            Nullable<bool> expected = new Nullable<bool>(); // TODO: Initialize to an appropriate value
            Nullable<bool> actual;
            target.SoftwareOrderItemHasUpgrade = expected;
            actual = target.SoftwareOrderItemHasUpgrade;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
