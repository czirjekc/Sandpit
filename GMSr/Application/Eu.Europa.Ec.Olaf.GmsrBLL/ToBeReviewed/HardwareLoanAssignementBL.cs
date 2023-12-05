using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Collections;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class HardwareLoanAssignmentBL
    {
        #region Properties

        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string TicketCode { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Save(ref ArrayList validationErrorList, string userLogin)
        {
            ValidateSave(ref validationErrorList);

            if (validationErrorList.Count == 0)
            {
                using (IRepository repository = new EntityFrameworkRepository())
                {
                    MapData(repository.Save<GLoanAssignment>(MapData(), userLogin));
                }

                return true;
            }
            else
            {
                return false;
            }
        }


        public void Delete(ref ArrayList validationErrorList, string userLogin)
        {
            ValidateDelete(ref validationErrorList);

            if (validationErrorList.Count == 0)
            {
                using (IRepository repository = new EntityFrameworkRepository())
                {
                    repository.Delete<GLoanAssignment>(MapData(), userLogin);
                }
            }
        }

        #endregion

        public static HardwareLoanAssignmentBL GetById(int id) 
        {
            HardwareLoanAssignmentBL item_BL = new HardwareLoanAssignmentBL();         
            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities())) {
                item_BL.MapData(repository.Find<GLoanAssignment>(x => x.Id == id).FirstOrDefault());

            }

            return item_BL;
        }

        public static List<HardwareLoanAssignmentBL> GetAllByItemId(int id)
        {
            List<HardwareLoanAssignmentBL> itemList_BL = new List<HardwareLoanAssignmentBL>();
            HardwareLoanAssignmentBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanAssignment>(x => x.GLoanItem.Id == id).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanAssignmentBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);

                });
            }

            return itemList_BL;
        }

        #endregion

        

        #region Internal Methods

        internal GLoanAssignment  MapData() 
        {
            GLoanAssignment item = new GLoanAssignment();
            item.Id = Id;
            item.ItemId = ItemId;
            item.DateStart = DateStart;
            item.DateEnd = DateEnd;
            item.TicketCode = TicketCode;

            item.EntityKey = new EntityKey("GmsrEntities.GLoanAssignmentSet", "Id", Id);

            return item;
        }

        internal void MapData(GLoanAssignment item)
        {
            Id = item.Id;
            ItemId = item.ItemId;
            DateStart = item.DateStart;
            DateEnd = item.DateEnd;
            TicketCode = item.TicketCode;
        }

        #endregion

        #region Private Methods 

        private void ValidateSave(ref ArrayList validationErrorList)
        {
            //todo: Check for referential integrity.
        }


        private void ValidateDelete(ref ArrayList validationErrorList)
        {
            //todo: Check for referential integrity.
        }

        #endregion
    }




}


