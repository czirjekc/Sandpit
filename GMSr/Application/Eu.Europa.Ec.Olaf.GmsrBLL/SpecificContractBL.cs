using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SpecificContractBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }
        public int? ContractFrameworkId { get; set; }
        public int? ContractAmendmentId { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Object { get; set; }
        public string Duration { get; set; }
        public string AcceptanceType { get; set; }
        public string AcceptanceDelay { get; set; }
        public decimal? PriceTotal { get; set; }
        public string Price1Def { get; set; }
        public decimal? Price1 { get; set; }
        public string Price2Def { get; set; }
        public decimal? Price2 { get; set; }
        public string Price3Def { get; set; }
        public decimal? Price3 { get; set; }
        public string InvoicingPeriod { get; set; }
        public bool? IsPenalties { get; set; }
        public decimal? Garantees { get; set; }
        public string SpecificContractPatch { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }

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
                    MapData(repository.Save<SpecificContract>(MapData(), userLogin));
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
                    repository.Delete<SpecificContract>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            SpecificContract item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<SpecificContract>(id);

                if (item != null)
                {
                    MapData(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        public static List<SpecificContractBL> GetAll()
        {
            List<SpecificContractBL> itemBL_List = new List<SpecificContractBL>();
            List<SpecificContract> itemList = new List<SpecificContract>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SpecificContract>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SpecificContractBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SpecificContractBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SpecificContractBL> GetByParameters(string name,
                                                                DateTime? dateBeginFrom,
                                                                DateTime? dateBeginTo,
                                                                DateTime? dateEndFrom,
                                                                DateTime? dateEndTo
                                                                )
        {
            List<SpecificContractBL> itemBL_List = new List<SpecificContractBL>();
            List<SpecificContract> itemList = new List<SpecificContract>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SpecificContract>(x =>
                                                              (name == "" || x.Name.Contains(name)) &&
                                                              (dateBeginFrom == null || x.DateBegin >= dateBeginFrom) &&
                                                              (dateBeginTo == null || x.DateBegin <= dateBeginTo) &&
                                                              (dateEndFrom == null || x.DateEnd == null || x.DateEnd >= dateEndFrom) &&
                                                              (dateEndTo == null || x.DateEnd <= dateEndTo)
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SpecificContractBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SpecificContractBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SpecificContractBL> GetByContractFramework(int contractFrameworkId)
        {
            List<SpecificContractBL> itemBL_List = new List<SpecificContractBL>();
            List<SpecificContract> itemList = new List<SpecificContract>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SpecificContract>(x => x.ContractFrameworkId == contractFrameworkId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SpecificContractBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SpecificContractBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SpecificContractBL> GetByContractAmendment(int contractAmendmentId)
        {
            List<SpecificContractBL> itemBL_List = new List<SpecificContractBL>();
            List<SpecificContract> itemList = new List<SpecificContract>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SpecificContract>(x => x.ContractAmendmentId == contractAmendmentId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SpecificContractBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SpecificContractBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }
        
        public static List<SpecificContractBL> GetByOrderForm(int orderFormId)
        {
            List<SpecificContractBL> itemBL_List = new List<SpecificContractBL>();
            List<SpecificContract> itemList = new List<SpecificContract>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                OrderForm orderForm = repository.Get<OrderForm>(x => x.Id == orderFormId);

                itemList = repository.Find<SpecificContract>(x => x.OrderForm.Contains(orderForm)).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SpecificContractBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SpecificContractBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SpecificContract item)
        {
            Id = item.Id;

            Name = item.Name;
            ContractFrameworkId = item.ContractFrameworkId;
            ContractAmendmentId = item.ContractAmendmentId;
            DateBegin = item.DateBegin;
            DateEnd = item.DateEnd;
            Object = item.Object;
            Duration = item.Duration;
            AcceptanceType = item.AcceptanceType;
            AcceptanceDelay = item.AcceptanceDelay;
            PriceTotal = item.PriceTotal;
            Price1Def = item.Price1Def;
            Price1 = item.Price1;
            Price2Def = item.Price2Def;
            Price2 = item.Price2;
            Price3Def = item.Price3Def;
            Price3 = item.Price3;
            InvoicingPeriod = item.InvoicingPeriod;
            IsPenalties = item.IsPenalties;
            Garantees = item.Garantees;
            SpecificContractPatch = item.SpecificContractPatch;
            Comment = item.Comment;
            Status = item.Status;
        }

        internal SpecificContract MapData()
        {
            SpecificContract item = new SpecificContract();
            item.Id = Id;

            item.Name = Name;
            item.ContractFrameworkId = ContractFrameworkId;
            item.ContractAmendmentId = ContractAmendmentId;
            item.DateBegin = DateBegin;
            item.DateEnd = DateEnd;
            item.Object = Object;
            item.Duration = Duration;
            item.AcceptanceType = AcceptanceType;
            item.AcceptanceDelay = AcceptanceDelay;
            item.PriceTotal = PriceTotal;
            item.Price1Def = Price1Def;
            item.Price1 = Price1;
            item.Price2Def = Price2Def;
            item.Price2 = Price2;
            item.Price3Def = Price3Def;
            item.Price3 = Price3;
            item.InvoicingPeriod = InvoicingPeriod;
            item.IsPenalties = IsPenalties;
            item.Garantees = Garantees;
            item.SpecificContractPatch = SpecificContractPatch;
            item.Comment = Comment;
            item.Status = Status;

            item.EntityKey = new EntityKey("GmsrEntities.SpecificContractSet", "Id", Id);

            return item;
        }

        #endregion

        #region Private Methods

        private void ValidateSave(ref ArrayList validationErrorList)
        {
            //todo: validation
            //if (FirstName.Trim() == "")
            //{
            //    validationErrorList.Add("The First Name is required.");
            //}

            //if (LastName.Trim() == "")
            //{
            //    validationErrorList.Add("The Last Name is required.");
            //}
        }

        private void ValidateDelete(ref ArrayList validationErrorList)
        {
            //todo: Check for referential integrity.
        }

        #endregion
    }
}