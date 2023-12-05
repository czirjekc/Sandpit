using Eu.Europa.Ec.Olaf.GmsrBLL;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{
    /// <summary>
    ///This is a test class for SoftwareProductBL_Test and is intended
    ///to contain all SoftwareProductBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class SoftwareProductBL_Test
    {
        #region Public Methods

        /// <summary>
        ///A test for Concatenation
        ///</summary>
        [TestMethod()]
        public void ConcatenationTest()
        {
            LoadData();
            
            SoftwareProductBL product = new SoftwareProductBL();
            product.Select(2);
            product.SoftwareProductStatusId = 6;
            product.SoftwareProductStatusName = "Operational";
            string actual;
            actual = product.Concatenation;

            Assert.AreEqual("DemoShop | 2.0 | DemoCompany | Operational | (2)", actual);
        }

        /// <summary>
        ///A test for Concatenation
        ///</summary>
        [TestMethod()]
        public void ConcatenationTest1()
        {
            LoadData();
            
            SoftwareProductBL product = new SoftwareProductBL();
            product.Select(4);
            string actual;
            actual = product.Concatenation;

            Assert.AreEqual("Gravity |  |  |  | (4)", actual);
        }

        /// <summary>
        ///A test for HasMatchWithDetectedSoftwareProduct
        ///</summary>
        [TestMethod()]
        public void HasMatchWithDetectedSoftwareProductTest()
        {
            LoadData();

            ArrayList validationErrors = new ArrayList();
            DetectedSoftwareProductMatchBL match;

            match = new DetectedSoftwareProductMatchBL();
            match.SoftwareProductId = 2;
            match.DetectedSoftwareProductId = 9;
            match.Save(ref validationErrors, null);

            SoftwareProductBL product = new SoftwareProductBL();
            product.Select(2);
            Assert.AreEqual(true, product.HasMatchWithDetectedSoftwareProduct);                        
        }

        /// <summary>
        ///A test for HasMatchWithDetectedSoftwareProduct
        ///</summary>
        [TestMethod()]
        public void HasMatchWithDetectedSoftwareProductTest1()
        {
            LoadData();            

            SoftwareProductBL product = new SoftwareProductBL();
            product.Select(2);
            
            Assert.AreEqual(false, product.HasMatchWithDetectedSoftwareProduct);
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            FakeRepository.Clear();

            ArrayList validationErrors = new ArrayList();
            SoftwareProductBL product;

            product = new SoftwareProductBL();
            product.Name = "DemoShop";
            product.Version = "1.0";
            product.CompanyName = "DemoCompany";
            product.SoftwareProductStatusId = 2;
            product.Source = "GMSr";
            
            product.Save(ref validationErrors, null);
            
            Assert.AreEqual(0, validationErrors.Count);
            Assert.AreEqual(1, SoftwareProductBL.GetAll().Count);
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest1()
        {
            LoadData();
            
            ArrayList validationErrors = new ArrayList();
            SoftwareProductBL product = new SoftwareProductBL();
            product.Select(2);
            product.Version = "2.0.5";
            
            product.Save(ref validationErrors, null);
            
            Assert.AreEqual(0, validationErrors.Count);
            Assert.AreEqual(5, SoftwareProductBL.GetAll().Count);
            Assert.AreEqual(SoftwareProductBL.GetByKeyword("2.0.5")[0].Id, 2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            LoadData();

            SoftwareProductBL target = new SoftwareProductBL();
            target.Select(2);
            ArrayList validationErrorList = new ArrayList();
            ArrayList validationErrorListExpected = new ArrayList();
            string userLogin = string.Empty;            
            target.Delete(ref validationErrorList, userLogin);
            
            Assert.AreEqual(0,validationErrorList.Count);
            Assert.AreEqual(4, SoftwareProductBL.GetAll().Count);
        }      

        /// <summary>
        ///A test for Select
        ///</summary>
        [TestMethod()]
        public void SelectTest()
        {
            LoadData();
            
            SoftwareProductBL target = new SoftwareProductBL();
            int id = 2;
            bool actual;            
            actual = target.Select(id);
            
            Assert.AreEqual(true, actual);
            Assert.AreEqual("DemoCompany", target.CompanyName);
        }

        [TestMethod()]
        public void SelectTest1()
        {
            LoadData();
            SoftwareProductBL target = new SoftwareProductBL();
            int id = 0;
            bool actual;
            
            actual = target.Select(id);
            
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void SelectTest2()
        {
            LoadData();
            SoftwareProductBL target = new SoftwareProductBL();
            int id = 5;
            bool actual;
            
            actual = target.Select(id);
            
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void SelectTest3()
        {
            LoadData();
            SoftwareProductBL target = new SoftwareProductBL();
            int id = 6;
            bool actual;
            
            actual = target.Select(id);
            
            Assert.AreEqual(false, actual);
        }

        /// <summary>
        ///A test for GetAll
        ///</summary>
        [TestMethod()]
        public void GetAllTest()
        {
            LoadData();
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetAll();
            
            Assert.AreEqual(5, actual.Count);
        }

        /// <summary>
        ///A test for GetByInfix
        ///</summary>
        [TestMethod()]
        public void GetByInfixTest()
        {
            LoadData();
            string infix = "Shop"; // "shop" will fail because in FakeRepository it is case sensitive and in the EntityFrameworkRepository it is not.
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetByInfix(infix);
            
            Assert.AreEqual(2, actual.Count);
        }

        /// <summary>
        ///A test for GetByInfix
        ///</summary>
        [TestMethod()]
        public void GetByInfixTest1()
        {
            LoadData();
            string infix = "1.0";
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetByInfix(infix);
            
            Assert.AreEqual(2, actual.Count);
        }

        /// <summary>
        ///A test for GetByInfix
        ///</summary>
        [TestMethod()]
        public void GetByInfixTest2()
        {
            LoadData();
            string infix = "Foert";
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetByInfix(infix);
            
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetByKeyword
        ///</summary>
        [TestMethod()]
        public void GetByKeywordTest()
        {
            LoadData();
            string keyword = "Shop";
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetByKeyword(keyword);
            
            Assert.AreEqual(3, actual.Count);
        }

        /// <summary>
        ///A test for GetByKeyword
        ///</summary>
        [TestMethod()]
        public void GetByKeywordTest1()
        {
            LoadData();
            string keyword = "Foert";
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetByKeyword(keyword);
            
            Assert.AreEqual(1, actual.Count);
        }

        /// <summary>
        ///A test for GetByKeyword
        ///</summary>
        [TestMethod()]
        public void GetByKeywordTest2()
        {
            LoadData();
            string keyword = "";
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetByKeyword(keyword);
            
            Assert.AreEqual(5, actual.Count);
        }

        /// <summary>
        ///A test for GetByParameters
        ///</summary>
        [TestMethod()]
        public void GetByParametersTest()
        {
            LoadData();
            string name = string.Empty;
            string version = "1.0";
            string company = "";
            string other = "";
            Nullable<int> softwareProductStatusId = 2;
            string comment = "";
            string source = "";
            List<SoftwareProductBL> actual;
            
            actual = SoftwareProductBL.GetByParameters(name, version, company, other, softwareProductStatusId, comment, source);
            
            Assert.AreEqual(1, actual.Count);
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            SoftwareProductBL.RepositoryFactory = new FakeRepositoryFactory();

            if (SoftwareProductBL.GetAll().Count != 5)
            {
                FakeRepository.Clear();
                
                ArrayList validationErrors = new ArrayList();
                SoftwareProductBL product;

                product = new SoftwareProductBL();
                product.Name = "DemoShop";
                product.Version = "1.0";
                product.CompanyName = "DemoCompany";                
                product.Source = "GMSr";
                product.Save(ref validationErrors, null);

                product = new SoftwareProductBL();
                product.Name = "DemoShop";
                product.Version = "2.0";
                product.CompanyName = "DemoCompany";                
                product.Source = "GMSr";
                product.Save(ref validationErrors, null);

                product = new SoftwareProductBL();
                product.Name = "FoertShop";
                product.Version = "1.0";
                product.Other = "What else?";
                product.Comment = "Foert!";
                product.SoftwareProductStatusId = 2;
                product.Source = "ABAC";
                product.Save(ref validationErrors, null);

                product = new SoftwareProductBL();
                product.Name = "Gravity";
                product.Source = "OldGMS";
                product.Save(ref validationErrors, null);

                product = new SoftwareProductBL();
                product.Name = "Gravity";
                product.Version = "1.0.0.5";
                product.CompanyName = "Galaxy";
                product.Source = "GMSr";
                product.Save(ref validationErrors, null);
            }
        }

        #endregion
    }
}
