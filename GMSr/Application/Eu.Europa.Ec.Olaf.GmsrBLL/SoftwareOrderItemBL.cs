using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SoftwareOrderItemBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public int? OrderFormId { get; set; }
        public string OrderFormName { get; set; }
        public int? SoftwareProductId { get; set; }
        public string SoftwareProductName { get; set; }
        public string SoftwareProductVersion { get; set; }
        public string SoftwareProductCompany { get; set; }
        public string SoftwareProductSource { get; set; }
        public int? SoftwareOrderItemTypeId { get; set; }
        public string SoftwareOrderItemTypeName { get; set; }
        public int? PreviousSoftwareOrderItemId { get; set; }
        public string OlafRef { get; set; }
        public string Vendor { get; set; }
        public string Comment { get; set; }
        public DateTime? MaintenanceStartDate { get; set; }
        public DateTime? MaintenanceEndDate { get; set; }
        public string CreatorLogin { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? HasUpgrade { get; set; }

        public string Concatenation
        {
            get
            {
                return SoftwareProductName + " | " + SoftwareProductVersion + " | " + SoftwareProductCompany + " | " + SoftwareProductId + " | " + SoftwareOrderItemTypeName + " | " + OrderFormName + " | " + OlafRef + " | " + SoftwareProductSource + " | (" + Id + ")";
            }
        }

        public string PreviousSoftwareOrderItemConcatenation
        {
            get
            {
                string concatenation = "";

                if ((SoftwareOrderItemTypeId == 2 || SoftwareOrderItemTypeId == 3) && PreviousSoftwareOrderItemId.HasValue)
                {
                    SoftwareOrderItemBL previousOrderItem = new SoftwareOrderItemBL();
                    previousOrderItem.Select(PreviousSoftwareOrderItemId.Value);

                    concatenation = previousOrderItem.Concatenation;
                }

                return concatenation;
            }
        }

        #endregion

        #region Public Methods

        #region Standard

        public bool Save(ref ArrayList validationErrorList, string userLogin)
        {
            ValidateSave(ref validationErrorList);

            if (SoftwareOrderItemTypeId == 3) //Maintenance Entry
            {
                //copy if necessary the enddate and the startdate to the License Entry where it is the maintenance for
                SoftwareOrderItemBL previousOrderItem = new SoftwareOrderItemBL();
                previousOrderItem.Select(PreviousSoftwareOrderItemId.Value);

                if (previousOrderItem.MaintenanceStartDate == null || MaintenanceStartDate < previousOrderItem.MaintenanceStartDate)
                {
                    previousOrderItem.MaintenanceStartDate = MaintenanceStartDate;
                }

                if (previousOrderItem.MaintenanceEndDate == null || MaintenanceEndDate > previousOrderItem.MaintenanceEndDate)
                {
                    previousOrderItem.MaintenanceEndDate = MaintenanceEndDate;
                }

                previousOrderItem.Save(ref validationErrorList, userLogin);
            }
            else if (SoftwareOrderItemTypeId == 2 && Id == 0) //Upgrade Entry
            {                
                //copy the enddate and the startdate from the 'previous' License Entry
                SoftwareOrderItemBL previousOrderItem = new SoftwareOrderItemBL();
                previousOrderItem.Select(PreviousSoftwareOrderItemId.Value);

                MaintenanceStartDate = previousOrderItem.MaintenanceStartDate;
                MaintenanceEndDate = previousOrderItem.MaintenanceEndDate;

                //update the hasUpgrade of the previous
                previousOrderItem.HasUpgrade = true;
                previousOrderItem.Save(ref validationErrorList, userLogin);
            }

            if (validationErrorList.Count == 0)
            {
                using (IRepository repository = new EntityFrameworkRepository())
                {
                    MapData(repository.Save<SoftwareOrderItem>(MapData(), userLogin));
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
                if (SoftwareOrderItemTypeId == 3) //Maintenance Entry
                {
                    // todo: the maintenanceStartDate and maintenanceEndDate of the corresponding entry has to be updated
                }
                else if (SoftwareOrderItemTypeId == 2)
                {
                    SoftwareOrderItemBL previousOrderItem = new SoftwareOrderItemBL();
                    previousOrderItem.Select(PreviousSoftwareOrderItemId.Value);
                    //update the hasUpgrade of the previous
                    previousOrderItem.HasUpgrade = null;
                }

                using (IRepository repository = new EntityFrameworkRepository())
                {
                    repository.Delete<SoftwareOrderItem>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            SoftwareOrderItem item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<SoftwareOrderItem>(id);

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

        public static List<SoftwareOrderItemBL> GetAll()
        {
            List<SoftwareOrderItemBL> itemBL_List = new List<SoftwareOrderItemBL>();
            List<SoftwareOrderItem> itemList = new List<SoftwareOrderItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareOrderItem>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareOrderItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareOrderItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareOrderItemBL> GetByKeyword(string keyword)
        {
            List<SoftwareOrderItemBL> itemBL_List = new List<SoftwareOrderItemBL>();
            List<SoftwareOrderItem> itemList = new List<SoftwareOrderItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                int number = 0;
                if (int.TryParse(keyword, out number))
                {
                    itemList = repository.Find<SoftwareOrderItem>(x =>
                                                              x.Id == number ||
                                                              x.PreviousSoftwareOrderItemId == number ||

                                                              x.OrderForm.Name.Contains(keyword) ||
                                                              x.OlafRef.Contains(keyword) ||
                                                              x.SoftwareOrderItemType.Name.Contains(keyword) ||
                                                              x.Comment.Contains(keyword) ||
                                                              (x.SoftwareProduct.Name + " " + x.SoftwareProduct.Version).Contains(keyword)
                                                              ).ToList();
                }
                else if (keyword == "")
                {
                    itemList = repository.Find<SoftwareOrderItem>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<SoftwareOrderItem>(x =>
                                                              x.OrderForm.Name.Contains(keyword) ||
                                                              x.OlafRef.Contains(keyword) ||
                                                              x.SoftwareOrderItemType.Name.Contains(keyword) ||
                                                              x.Comment.Contains(keyword) ||
                                                              x.SoftwareProduct.Source.Contains(keyword) ||
                                                              (x.SoftwareProduct.Name + " " + x.SoftwareProduct.Version).Contains(keyword)
                                                              ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareOrderItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareOrderItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareOrderItemBL> GetByParameters(string order,
                                                                string olafRef,
                                                                int? softwareOrderItemTypeId,
                                                                string softwareProductSource,
                                                                string softwareProductName,
                                                                string softwareProductVersion,
                                                                DateTime? dateStartFrom,
                                                                DateTime? dateStartTo,
                                                                DateTime? dateEndFrom,
                                                                DateTime? dateEndTo,
                                                                string previousConcatenation,
                                                                string comment,
                                                                string creator,
                                                                DateTime? creationDateFrom,
                                                                DateTime? creationDateTo,
                                                                bool onlyWithoutUpgrade
                                                                )
        {
            List<SoftwareOrderItemBL> itemBL_List = new List<SoftwareOrderItemBL>();
            List<SoftwareOrderItem> itemList = new List<SoftwareOrderItem>();


            int previousConcatenationId = 0;
            int.TryParse(previousConcatenation, out previousConcatenationId);

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareOrderItem>(x =>
                                                              (order == "" || x.OrderForm.Name.Contains(order)) &&
                                                              (olafRef == "" || x.OlafRef.Contains(olafRef)) &&
                                                              (softwareOrderItemTypeId == null || x.SoftwareOrderItemTypeId == softwareOrderItemTypeId) &&
                                                              (softwareProductSource == "" || x.SoftwareProduct.Source.Contains(softwareProductSource) || x.PreviousSoftwareOrderItem.SoftwareProduct.Source.Contains(softwareProductSource)) &&
                                                              (softwareProductName == "" || x.SoftwareProduct.Name.Contains(softwareProductName)) &&
                                                              (softwareProductVersion == "" || x.SoftwareProduct.Version.Contains(softwareProductVersion)) &&
                                                              (dateStartFrom == null || x.MaintenanceStartDate >= dateStartFrom) &&
                                                              (dateStartTo == null || x.MaintenanceStartDate <= dateStartTo) &&
                                                              (dateEndFrom == null || x.MaintenanceEndDate == null || x.MaintenanceEndDate >= dateEndFrom) &&
                                                              (dateEndTo == null || x.MaintenanceEndDate <= dateEndTo) &&
                                                              (previousConcatenation == "" || x.PreviousSoftwareOrderItem.Id == previousConcatenationId || (x.PreviousSoftwareOrderItem.SoftwareProduct.Name + " | " + x.PreviousSoftwareOrderItem.SoftwareProduct.Version + " | " + x.PreviousSoftwareOrderItem.SoftwareProduct.CompanyName).Contains(previousConcatenation)) &&
                                                              (comment == "" || x.Comment.Contains(comment)) &&
                                                              (creator == "" || x.CreatorLogin.Contains(creator)) &&
                                                              (creationDateFrom == null || x.CreationDate >= creationDateFrom) &&
                                                              (creationDateTo == null || x.CreationDate <= creationDateTo) &&
                                                              (onlyWithoutUpgrade == false || !(x.HasUpgrade.HasValue && x.HasUpgrade.Value))
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareOrderItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareOrderItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareOrderItemBL> GetBySoftwareOrderType(int softwareOrderTypeId)
        {
            List<SoftwareOrderItemBL> itemBL_List = new List<SoftwareOrderItemBL>();
            List<SoftwareOrderItem> itemList = new List<SoftwareOrderItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareOrderItem>(x => x.SoftwareOrderItemTypeId == softwareOrderTypeId).ToList();

                itemList = itemList.OrderBy(x => x.OlafRef).ToList();

                SoftwareOrderItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareOrderItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareOrderItemBL> GetBySoftwareProduct(int softwareProductId)
        {
            List<SoftwareOrderItemBL> itemBL_List = new List<SoftwareOrderItemBL>();
            List<SoftwareOrderItem> itemList = new List<SoftwareOrderItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareOrderItem>(x => x.SoftwareProductId == softwareProductId).ToList();

                itemList = itemList.OrderBy(x => x.OlafRef).ToList();

                SoftwareOrderItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareOrderItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareOrderItemBL> GetByOrderForm(int orderFormId)
        {
            List<SoftwareOrderItemBL> itemBL_List = new List<SoftwareOrderItemBL>();
            List<SoftwareOrderItem> itemList = new List<SoftwareOrderItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareOrderItem>(x => x.OrderFormId == orderFormId).ToList();

                SoftwareOrderItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareOrderItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareOrderItemBL> GetPackByInfix(string infix)
        {
            List<SoftwareOrderItemBL> itemBL_List = new List<SoftwareOrderItemBL>();
            List<SoftwareOrderItem> itemList = new List<SoftwareOrderItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareOrderItem>(x => (x.SoftwareProduct.Name + " " + x.SoftwareProduct.Version + " " + x.SoftwareProduct.CompanyName).Contains(infix)
                                                                    && (x.SoftwareOrderItemTypeId == 1 || x.SoftwareOrderItemTypeId == 2) && x.SoftwareProduct.Source == "GMSr").ToList();

                SoftwareOrderItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareOrderItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SoftwareOrderItem item)
        {
            Id = item.Id;

            OrderFormId = item.OrderFormId;
            OrderFormName = item.OrderForm == null ? null : item.OrderForm.Name;
            SoftwareProductId = item.SoftwareProductId;
            SoftwareProductName = item.SoftwareProduct == null ? null : item.SoftwareProduct.Name;
            SoftwareProductVersion = item.SoftwareProduct == null ? null : item.SoftwareProduct.Version;
            SoftwareProductCompany = item.SoftwareProduct == null ? null : item.SoftwareProduct.CompanyName;
            SoftwareProductSource = item.SoftwareProduct == null ? null : item.SoftwareProduct.Source;
            SoftwareOrderItemTypeId = item.SoftwareOrderItemTypeId;
            SoftwareOrderItemTypeName = item.SoftwareOrderItemType == null ? null : item.SoftwareOrderItemType.Name;
            PreviousSoftwareOrderItemId = item.PreviousSoftwareOrderItemId;
            OlafRef = item.OlafRef;
            Vendor = item.Vendor;
            Comment = item.Comment;
            MaintenanceStartDate = item.MaintenanceStartDate;
            MaintenanceEndDate = item.MaintenanceEndDate;
            CreatorLogin = item.CreatorLogin;
            CreationDate = item.CreationDate;
            HasUpgrade = item.HasUpgrade;
        }

        internal SoftwareOrderItem MapData()
        {
            SoftwareOrderItem item = new SoftwareOrderItem();
            item.Id = Id;

            item.OrderFormId = OrderFormId;
            item.SoftwareProductId = SoftwareProductId;
            item.SoftwareOrderItemTypeId = SoftwareOrderItemTypeId;
            item.PreviousSoftwareOrderItemId = PreviousSoftwareOrderItemId;
            item.OlafRef = OlafRef;
            item.Vendor = Vendor;
            item.Comment = Comment;
            item.MaintenanceStartDate = MaintenanceStartDate;
            item.MaintenanceEndDate = MaintenanceEndDate;
            item.CreatorLogin = CreatorLogin;
            item.CreationDate = CreationDate;
            item.HasUpgrade = HasUpgrade;

            item.EntityKey = new EntityKey("GmsrEntities.SoftwareOrderItemSet", "Id", Id);

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
            if (HasUpgrade.HasValue && HasUpgrade.Value)
            {
                validationErrorList.Add("You can not delete this entry because it has a successor.");
            }
        }

        #endregion
    }
}