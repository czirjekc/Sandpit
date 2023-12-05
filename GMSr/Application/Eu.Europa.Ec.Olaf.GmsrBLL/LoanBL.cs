using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class LoanBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string TicketCode { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsPermanent { get; set; }

        public string Model { get; set; }
        public string Description { get; set; }
        public string OlafName { get; set; }
        public string InventoryNo { get; set; }

        #endregion

        #region Public Methods

        public static List<LoanBL> GetByUserLogin(string userLogin)
        {
            // get all tickets for the user
            List<TicketBL> ticketBL_List = TicketBL.GetOpenByUserLogin(userLogin);
            ticketBL_List.AddRange(TicketBL.GetResolvedByUserLogin(userLogin));
            
            List<LoanBL> itemBL_List = new List<LoanBL>();
            List<GLoanAssignment> itemList = new List<GLoanAssignment>();

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                ticketBL_List.ForEach(ticket =>
                {
                    itemList.AddRange(repository.Find<GLoanAssignment>(x => x.TicketCode == ticket.Code).ToList());
                });

                LoanBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new LoanBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(GLoanAssignment item)
        {
            Id = item.Id;
            DateStart = item.DateStart;
            DateEnd = item.DateEnd;
            TicketCode = item.TicketCode;
            Category = (item.GLoanItem == null || item.GLoanItem.GLoanCategory == null) ? null : item.GLoanItem.GLoanCategory.Name;
            Status = (item.GLoanItem == null || item.GLoanItem.GLoanStatus == null) ? null : item.GLoanItem.GLoanStatus.Name;
            IsActive = item.GLoanItem.IsItemActive;
            IsPermanent = item.GLoanItem.IsPermaLoan;
            
            Model = (item.GLoanItem == null || item.GLoanItem.HardwareItem == null) ? null : item.GLoanItem.HardwareItem.Model;
            Description = (item.GLoanItem == null || item.GLoanItem.HardwareItem == null) ? null : item.GLoanItem.HardwareItem.Description;
            OlafName = (item.GLoanItem == null || item.GLoanItem.HardwareItem == null) ? null : item.GLoanItem.HardwareItem.OlafName;
            InventoryNo = (item.GLoanItem == null || item.GLoanItem.HardwareItem == null) ? null : item.GLoanItem.HardwareItem.InventoryNo;
        }

        #endregion
    }
}