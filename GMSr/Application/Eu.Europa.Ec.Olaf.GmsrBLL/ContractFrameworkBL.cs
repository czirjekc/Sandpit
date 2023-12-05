using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class ContractFrameworkBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal? Prices { get; set; }
        public string Title { get; set; }
        public string Object { get; set; }
        public decimal? Duration { get; set; }
        public bool? IsSLA { get; set; }
        public bool? IsIntraMuros { get; set; }
        public string CE_Contact { get; set; }
        public int? Company { get; set; }
        public string CompanyContact { get; set; }
        public string OlafContact { get; set; }
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
                    MapData(repository.Save<ContractFramework>(MapData(), userLogin));
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
                    repository.Delete<ContractFramework>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            ContractFramework item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<ContractFramework>(id);

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

        public static List<ContractFrameworkBL> GetAll()
        {
            List<ContractFrameworkBL> itemBL_List = new List<ContractFrameworkBL>();
            List<ContractFramework> itemList = new List<ContractFramework>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ContractFramework>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractFrameworkBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractFrameworkBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ContractFrameworkBL> GetByParameters(string name,
                                                                DateTime? dateBeginFrom,
                                                                DateTime? dateBeginTo,
                                                                DateTime? dateEndFrom,
                                                                DateTime? dateEndTo
                                                                )
        {
            List<ContractFrameworkBL> itemBL_List = new List<ContractFrameworkBL>();
            List<ContractFramework> itemList = new List<ContractFramework>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ContractFramework>(x =>
                                                              (name == "" || x.Name.Contains(name)) &&
                                                              (dateBeginFrom == null || x.DateBegin >= dateBeginFrom) &&
                                                              (dateBeginTo == null || x.DateBegin <= dateBeginTo) &&
                                                              (dateEndFrom == null || x.DateEnd == null || x.DateEnd >= dateEndFrom) &&
                                                              (dateEndTo == null || x.DateEnd <= dateEndTo)
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractFrameworkBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractFrameworkBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ContractFrameworkBL> GetByContractAmendment(int contractAmendmentId)
        {
            List<ContractFrameworkBL> itemBL_List = new List<ContractFrameworkBL>();
            List<ContractFramework> itemList = new List<ContractFramework>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                ContractAmendment contractAmendment = repository.Get<ContractAmendment>(x => x.Id == contractAmendmentId);
                
                itemList = repository.Find<ContractFramework>(x => x.ContractAmendment.Contains(contractAmendment)).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractFrameworkBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractFrameworkBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ContractFrameworkBL> GetBySpecificContract(int specificContractId)
        {
            List<ContractFrameworkBL> itemBL_List = new List<ContractFrameworkBL>();
            List<ContractFramework> itemList = new List<ContractFramework>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                SpecificContract specificContract = repository.Get<SpecificContract>(x => x.Id == specificContractId);

                itemList = repository.Find<ContractFramework>(x => x.SpecificContract.Contains(specificContract)).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                ContractFrameworkBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ContractFrameworkBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }


        #endregion

        #region Internal Methods

        internal void MapData(ContractFramework item)
        {
            Id = item.Id;

            Name = item.Name;
            DateBegin = item.DateBegin;
            DateEnd = item.DateEnd;
            Prices = item.Prices;
            Title = item.Title;
            Object = item.Object;
            Duration = item.Duration;
            IsSLA = item.IsSLA;
            IsIntraMuros = item.IsIntraMuros;
            CE_Contact = item.CE_Contact;
            Company = item.Company;
            CompanyContact = item.CompanyContact;
            OlafContact = item.OlafContact;
            Status = item.Status;
        }

        internal ContractFramework MapData()
        {
            ContractFramework item = new ContractFramework();
            item.Id = Id;

            item.Name = Name;
            item.DateBegin = DateBegin;
            item.DateEnd = DateEnd;
            item.Prices = Prices;
            item.Title = Title;
            item.Object = Object;
            item.Duration = Duration;
            item.IsSLA = IsSLA;
            item.IsIntraMuros = IsIntraMuros;
            item.CE_Contact = CE_Contact;
            item.Company = Company;
            item.CompanyContact = CompanyContact;
            item.OlafContact = OlafContact;
            item.Status = Status;

            item.EntityKey = new EntityKey("GmsrEntities.ContractFrameworkSet", "Id", Id);

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