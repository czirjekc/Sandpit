using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class ContractAmendmentBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }
        public int? ContractFrameworkId { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? Activity { get; set; }
        public int? BudgetLine { get; set; }
        public decimal? Prices { get; set; }
        public string Title { get; set; }
        public string Object { get; set; }
        public decimal? Duration { get; set; }
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
                    MapData(repository.Save<ContractAmendment>(MapData(), userLogin));
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
                    repository.Delete<ContractAmendment>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            ContractAmendment item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<ContractAmendment>(id);

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

        public static List<ContractAmendmentBL> GetAll()
        {
            List<ContractAmendmentBL> itemBL_List = new List<ContractAmendmentBL>();
            List<ContractAmendment> itemList = new List<ContractAmendment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ContractAmendment>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractAmendmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractAmendmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ContractAmendmentBL> GetByParameters(string name,
                                                                DateTime? dateBeginFrom,
                                                                DateTime? dateBeginTo,
                                                                DateTime? dateEndFrom,
                                                                DateTime? dateEndTo
                                                                )
        {
            List<ContractAmendmentBL> itemBL_List = new List<ContractAmendmentBL>();
            List<ContractAmendment> itemList = new List<ContractAmendment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ContractAmendment>(x =>
                                                              (name == "" || x.Name.Contains(name)) &&
                                                              (dateBeginFrom == null || x.DateBegin >= dateBeginFrom) &&
                                                              (dateBeginTo == null || x.DateBegin <= dateBeginTo) &&
                                                              (dateEndFrom == null || x.DateEnd == null || x.DateEnd >= dateEndFrom) &&
                                                              (dateEndTo == null || x.DateEnd <= dateEndTo)
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractAmendmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractAmendmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ContractAmendmentBL> GetByContractFramework(int contractFrameworkId)
        {
            List<ContractAmendmentBL> itemBL_List = new List<ContractAmendmentBL>();
            List<ContractAmendment> itemList = new List<ContractAmendment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ContractAmendment>(x => x.ContractFrameworkId == contractFrameworkId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractAmendmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractAmendmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ContractAmendmentBL> GetBySpecificContract(int specificContractId)
        {
            List<ContractAmendmentBL> itemBL_List = new List<ContractAmendmentBL>();
            List<ContractAmendment> itemList = new List<ContractAmendment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                SpecificContract specificContract = repository.Get<SpecificContract>(x => x.Id == specificContractId);

                itemList = repository.Find<ContractAmendment>(x => x.SpecificContract.Contains(specificContract)).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractAmendmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractAmendmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ContractAmendmentBL> GetByOrderForm(int orderFormId)
        {
            List<ContractAmendmentBL> itemBL_List = new List<ContractAmendmentBL>();
            List<ContractAmendment> itemList = new List<ContractAmendment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                OrderForm orderForm = repository.Get<OrderForm>(x => x.Id == orderFormId);

                itemList = repository.Find<ContractAmendment>(x => x.OrderForm.Contains(orderForm)).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractAmendmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractAmendmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(ContractAmendment item)
        {
            Id = item.Id;

            Name = item.Name;
            ContractFrameworkId = item.ContractFrameworkId;
            DateBegin = item.DateBegin;
            DateEnd = item.DateEnd;
            Activity = item.Activity;
            BudgetLine = item.BudgetLine;
            Prices = item.Prices;
            Title = item.Title;
            Object = item.Object;
            Duration = item.Duration;
            Status = item.Status;
        }

        internal ContractAmendment MapData()
        {
            ContractAmendment item = new ContractAmendment();
            item.Id = Id;

            item.Name = Name;
            item.ContractFrameworkId = ContractFrameworkId;
            item.DateBegin = DateBegin;
            item.DateEnd = DateEnd;
            item.Activity = Activity;
            item.BudgetLine = BudgetLine;
            item.Prices = Prices;
            item.Title = Title;
            item.Object = Object;
            item.Duration = Duration;
            item.Status = Status;

            item.EntityKey = new EntityKey("GmsrEntities.ContractAmendmentSet", "Id", Id);

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