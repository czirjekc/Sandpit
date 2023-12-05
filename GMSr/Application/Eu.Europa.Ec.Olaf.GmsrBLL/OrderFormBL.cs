using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class OrderFormBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public int? ContractAmendmentId { get; set; }
        public int? SpecificContractId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Scope { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal? Duration { get; set; }
        public string ServiceType { get; set; }
        public int? ProfileId { get; set; }
        public string Status { get; set; }
        public string SoftwareProductSource { get; set; }

        public string Concatenation
        {
            get
            {
                return Name + " | (" + Id + ")";
            }
        }

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
                    MapData(repository.Save<OrderForm>(MapData(), userLogin));
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
                    repository.Delete<OrderForm>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            OrderForm item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<OrderForm>(id);

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

        public static List<OrderFormBL> GetAll()
        {
            List<OrderFormBL> itemBL_List = new List<OrderFormBL>();
            List<OrderForm> itemList = new List<OrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderForm>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Name).ToList();

                OrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormBL> GetByInfix(string infix)
        {
            List<OrderFormBL> itemBL_List = new List<OrderFormBL>();
            List<OrderForm> itemList = new List<OrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderForm>(x => x.Name.Contains(infix)).ToList();

                OrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormBL> GetByPrefix(string prefix)
        {
            List<OrderFormBL> itemBL_List = new List<OrderFormBL>();
            List<OrderForm> itemList = new List<OrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderForm>(x => x.Name.Contains(prefix)).ToList();

                itemList = itemList.OrderBy(x => x.Name).Take(50).ToList();

                OrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormBL> GetByKeyword(string keyword)
        {
            List<OrderFormBL> itemBL_List = new List<OrderFormBL>();
            List<OrderForm> itemList = new List<OrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {

                if (keyword == "")
                {
                    itemList = repository.Find<OrderForm>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<OrderForm>(x =>
                                                          x.Name.Contains(keyword) ||
                                                          x.SoftwareOrderItem.FirstOrDefault().SoftwareProduct.Source.Contains(keyword)
                                                          ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Name).ToList();

                OrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormBL> GetByParameters(string name,
                                                                DateTime? dateBeginFrom,
                                                                DateTime? dateBeginTo,
                                                                DateTime? dateEndFrom,
                                                                DateTime? dateEndTo
                                                                )
        {
            List<OrderFormBL> itemBL_List = new List<OrderFormBL>();
            List<OrderForm> itemList = new List<OrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderForm>(x =>
                                                              (name == "" || x.Name.Contains(name)) &&
                                                              (dateBeginFrom == null || x.DateBegin >= dateBeginFrom) &&
                                                              (dateBeginTo == null || x.DateBegin <= dateBeginTo) &&
                                                              (dateEndFrom == null || x.DateEnd == null || x.DateEnd >= dateEndFrom) &&
                                                              (dateEndTo == null || x.DateEnd <= dateEndTo)
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                OrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormBL> GetByContractAmendment(int contractAmendmentId)
        {
            List<OrderFormBL> itemBL_List = new List<OrderFormBL>();
            List<OrderForm> itemList = new List<OrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderForm>(x => x.ContractAmendmentId == contractAmendmentId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                OrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormBL> GetBySpecificContract(int specificContractId)
        {
            List<OrderFormBL> itemBL_List = new List<OrderFormBL>();
            List<OrderForm> itemList = new List<OrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderForm>(x => x.SpecificContractId == specificContractId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                OrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(OrderForm item)
        {
            Id = item.Id;
            ContractAmendmentId = item.ContractAmendmentId;
            SpecificContractId = item.SpecificContractId;
            Name = item.Name;
            Title = item.Title;
            Scope = item.Scope;
            DateBegin = item.DateBegin;
            DateEnd = item.DateEnd;
            Duration = item.Duration;
            ServiceType = item.ServiceType;
            ProfileId = item.ProfileId;
            Status = item.Status;
            SoftwareProductSource = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.Count == 0 || item.SoftwareOrderItem.First().SoftwareProduct == null) ? null : item.SoftwareOrderItem.First().SoftwareProduct.Source;
        }

        internal OrderForm MapData()
        {
            OrderForm item = new OrderForm();
            item.Id = Id;
            item.ContractAmendmentId = ContractAmendmentId;
            item.SpecificContractId = SpecificContractId;
            item.Name = Name;
            item.Title = Title;
            item.Scope = Scope;
            item.DateBegin = DateBegin;
            item.DateEnd = DateEnd;
            item.Duration = Duration;
            item.ServiceType = ServiceType;
            item.ProfileId = ProfileId;
            item.Status = Status;

            item.EntityKey = new EntityKey("GmsrEntities.OrderFormSet", "Id", Id);

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
            if (SoftwareOrderItemBL.GetByOrderForm(Id).Count != 0)
            {
                validationErrorList.Add("You can not delete this order because there are still entries for this order. You have to delete these entries first.");
            }
            else if (OrderFormDocumentBL.GetByOrderForm(Id).Count != 0)
            {
                validationErrorList.Add("You can not delete this order because there are still documents for this order. You have to delete these documents first.");
            }
            else if (HardwareItemOrderFormBL.GetByOrderForm(Id).Count != 0)
            {
                validationErrorList.Add("You can not delete this order because there are still hardware items linked to this order. You have to delete these links first.");
            }
        }

        #endregion
    }
}