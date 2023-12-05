﻿using Eu.Europa.Ec.Olaf.GmsrBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{
    
    
    /// <summary>
    ///This is a test class for MediaItemTypeBL_Test and is intended
    ///to contain all MediaItemTypeBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class MediaItemTypeBL_Test
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
        ///A test for MediaItemTypeBL Constructor
        ///</summary>
        [TestMethod()]
        public void MediaItemTypeBLConstructorTest()
        {
            MediaItemTypeBL target = new MediaItemTypeBL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            List<MediaItemTypeBL> expected = null; // TODO: Initialize to an appropriate value
            List<MediaItemTypeBL> actual;
            actual = MediaItemTypeBL.GetAll();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Select
        ///</summary>
        [TestMethod()]
        public void SelectTest()
        {
            MediaItemTypeBL target = new MediaItemTypeBL(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Select(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}