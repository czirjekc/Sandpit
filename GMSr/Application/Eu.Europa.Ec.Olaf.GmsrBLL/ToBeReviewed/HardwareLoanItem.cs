using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class HardwareLoanItemBL
    {
        #region Properties

        public int Id { get; set; }
        public int HardwareItemId { get; set; }
        public string Description { get; set; }
        public string InventoryNumber { get; set; }
        public bool IsPermaLoan { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public bool IsItemActive { get; set; }

        public List<HardwareLoanAssignmentBL> Assignements { get; set; }


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
                    MapData(repository.Save<GLoanItem>(MapData(), userLogin));
                }

                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        public static List<HardwareLoanItemBL> GetByCategoryId(int categoryId)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanItem>(x => x.GLoanCategory.Id == categoryId).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);
                });
            }
            return itemList_BL;

        }

        public static List<HardwareLoanItemBL> GetByCategoryId(int categoryId, DateTime startDate, DateTime endDate)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanItem>(x => x.GLoanCategory.Id == categoryId).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x);
                    if (!itemBL.Assignements.Any(y =>
                    {
                        return DatesOverlap(startDate, endDate, y.DateStart, y.DateEnd);
                    })) { itemList_BL.Add(itemBL); }

                });
            }
            return itemList_BL;

        }


        public static List<HardwareLoanItemBL> GetByInventoryNumber(string inventoryNumber)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanItem>(x => x.HardwareItem.InventoryNo.ToLower().Contains(inventoryNumber.ToLower())).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);
                });
            }
            return itemList_BL;

        }
        public static List<HardwareLoanItemBL> GetByInventoryNumber(string inventoryNumber, DateTime startDate, DateTime endDate)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanItem>(x => x.HardwareItem.InventoryNo.ToLower().Contains(inventoryNumber.ToLower())).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x);

                    if (!itemBL.Assignements.Any(y =>
                        {
                            return DatesOverlap(startDate, endDate, y.DateStart, y.DateEnd);
                        })) { itemList_BL.Add(itemBL); }



                });
            }
            return itemList_BL;
        }




        public static List<HardwareLoanItemBL> GetByTicketCode(string ticketCode)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanAssignment>(x => x.TicketCode.ToLower() == ticketCode).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x.GLoanItem);
                    itemList_BL.Add(itemBL);
                });
            }
            return itemList_BL;

        }

        public static List<HardwareLoanItemBL> GetByTicketCode(string ticketCode, DateTime startDate, DateTime endDate)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanAssignment>(x => x.TicketCode.ToLower() == ticketCode).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x.GLoanItem);
                    if (!itemBL.Assignements.Any(y =>
                    {
                        return DatesOverlap(startDate, endDate, y.DateStart, y.DateEnd);
                    })) { itemList_BL.Add(itemBL); }
                });
            }
            return itemList_BL;

        }

        public static List<HardwareLoanItemBL> GetByItemId(int Id)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanItem>(x => x.Id == Id).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);
                });
            }
            return itemList_BL;

        }

        public static List<HardwareLoanItemBL> GetByItemId(int Id, DateTime startDate, DateTime endDate)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanAssignment>(x => x.Id == Id).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x.GLoanItem);
                    if (!itemBL.Assignements.Any(y =>
                    {
                        return DatesOverlap(startDate, endDate, y.DateStart, y.DateEnd);
                    })) { itemList_BL.Add(itemBL); }
                });
            }
            return itemList_BL;

        }

        public static List<HardwareLoanItemBL> GetByStatus(string status)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanItem>(x => x.GLoanStatus.Name == status).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);
                });
            }
            return itemList_BL;

        }

        public static List<HardwareLoanItemBL> GetByisPermaloan(bool isPermaLoan)
        {
            List<HardwareLoanItemBL> itemList_BL = new List<HardwareLoanItemBL>();
            HardwareLoanItemBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanItem>(x => x.IsPermaLoan == isPermaLoan).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanItemBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);
                });
            }
            return itemList_BL;

        }


        #endregion

        #region Internal Methods

        internal void MapData(GLoanItem item)
        {
            Id = item.Id;
            HardwareItemId = item.HardwareItemId;
            if (!(item.HardwareItem == null)) //If it's null, it is probably being saved, so we don't care about no-base properties.
            {

                Assignements = HardwareLoanAssignmentBL.GetAllByItemId(item.Id);
                if (item.HardwareItem.LongName != "")
                {
                    Description = item.HardwareItem.LongName;
                }
                else if (item.HardwareItem.Description != "")
                {
                    Description = item.HardwareItem.Description;
                }
                else if (item.HardwareItem.Model != "")
                {
                    Description = item.HardwareItem.Model;
                }
                else Description = "No description available";
                InventoryNumber = item.HardwareItem.InventoryNo;
            }
            if (!(item.GLoanStatus == null)) //If it's null, it is probably being saved, so we don't care about no-base properties.
            {
                Status = item.GLoanStatus.Name;
            }

            IsPermaLoan = item.IsPermaLoan;
            StatusId = item.StatusId.Value;

            CategoryId = item.CategoryId.Value;
            IsItemActive = item.IsItemActive;

        }

        internal GLoanItem MapData()
        {
            GLoanItem item = new GLoanItem();
            item.Id = Id;
            item.CategoryId = CategoryId;
            item.StatusId = StatusId;
            item.IsPermaLoan = IsPermaLoan;
            item.IsItemActive = IsItemActive;
            item.HardwareItemId = HardwareItemId;

            item.EntityKey = new EntityKey("GmsrEntities.GLoanItemSet", "Id", Id);

            return item;
        }


        #endregion

        #region Private Methods

        private static bool DatesOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (end1.Ticks >= start2.Ticks) && (end2.Ticks >= start1.Ticks);
        }

        private void ValidateSave(ref ArrayList validationErrorList)
        {

        }

        #endregion
    }
}
