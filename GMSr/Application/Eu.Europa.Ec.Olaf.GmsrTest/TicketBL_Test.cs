using Eu.Europa.Ec.Olaf.GmsrBLL;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrTest
{
    /// <summary>
    ///This is a test class for TicketBL_Test and is intended
    ///to contain all TicketBL_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class TicketBL_Test
    {
        #region Public Methods

        /// <summary>
        ///A test for GetOpenByUserLogin
        ///</summary>
        [TestMethod()]
        public void GetOpenByUserLoginTest()
        {
            TicketBL.RepositoryFactory = new FakeRepositoryFactory();

            using (IRepository repository = TicketBL.RepositoryFactory.CreateWithGetSmcEntities())
            {
                HelpdeskItem item = new HelpdeskItem();
                item.Code = "vandesk-17911";
                item.Title = "Request for equipment";
                item.HelpdeskNature = null;
                item.CreationDate = new DateTime(2011, 4, 11, 12, 00, 00);
                item.ConcernDescription = "hugomic (HUGO Michele (OLAF))";
                item.IdState = 1;
                item.Description = "";
                item.EntityKey = new EntityKey("GetSmcEntities.HelpdeskItemSet", "Id", 0);                
                repository.Save<HelpdeskItem>(item, null);

                item = new HelpdeskItem();
                item.Code = "halioas-13851";
                item.Title = "Security Bulletin DIGIT-2011-004 (Publication)";
                item.HelpdeskNature = null;
                item.CreationDate = new DateTime(2010, 7, 01, 11, 15, 00);
                item.ConcernDescription = "strongr (STRONIKOWSKA Grazyna (OLAF))";
                item.IdState = 2;
                item.Description = "";
                item.EntityKey = new EntityKey("GetSmcEntities.HelpdeskItemSet", "Id", 0);                
                repository.Save<HelpdeskItem>(item, null);
            }

            Assert.AreEqual(1, TicketBL.GetOpenByUserLogin("hugomic").Count);
            Assert.AreEqual(0, TicketBL.GetOpenByUserLogin("hugomoc").Count);
            Assert.AreEqual(0, TicketBL.GetOpenByUserLogin("strongr").Count);
        }

        /// <summary>
        ///A test for GetResolvedByUserLogin
        ///</summary>
        [TestMethod()]
        public void GetResolvedByUserLoginTest()
        {
            TicketBL.RepositoryFactory = new FakeRepositoryFactory();

            using (IRepository repository = TicketBL.RepositoryFactory.CreateWithGetSmcEntities())
            {
                HelpdeskItem item = new HelpdeskItem();
                item.Code = "vandesk-17911";
                item.Title = "Request for equipment";
                item.HelpdeskNature = null;
                item.CreationDate = new DateTime(2011, 4, 11, 12, 00, 00);
                item.ConcernDescription = "hugomic (HUGO Michele (OLAF))";
                item.IdState = 2;
                item.Description = "";
                item.EntityKey = new EntityKey("GetSmcEntities.HelpdeskItemSet", "Id", 0);                

                HelpdeskActionGroup group = new HelpdeskActionGroup();
                group.HelpdeskItem = item;
                group.IdFunctionalServiceAffected = 59;
                group.Description = "";
                group.IsResponsabilityTransfer = "";
                group.EntityKey = new EntityKey("GetSmcEntities.HelpdeskActionGroupSet", "Id", 0);
                repository.Save<HelpdeskActionGroup>(group, null);
            }

            Assert.AreEqual(1, TicketBL.GetResolvedByUserLogin("hugomic").Count);
        }

        #endregion
    }
}
