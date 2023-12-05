using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SoftwareLicenseBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public int? SoftwareOrderItemId { get; set; } // todo: this should not be nullable
        public int? SoftwareLicenseTypeId { get; set; }
        public string SoftwareLicenseTypeName { get; set; }
        public int? MultiUserQuantity { get; set; }
        public int? PreviousSoftwareLicenseId { get; set; } // todo: this property is obsolete so remove it and the corresponding database column
        public DateTime? MaintenanceStartDate { get; set; } // todo: this property is obsolete so remove it and the corresponding database column
        public DateTime? MaintenanceEndDate { get; set; } // todo: this property is obsolete so remove it and the corresponding database column
        public string SerialKey { get; set; }
        public string Filename { get; set; }
        public byte[] File { get; set; }
        public string Comment { get; set; }
        public string SoftwareOrderItemOlafRef { get; set; }
        public string SoftwareOrderItemTypeName { get; set; }
        public int? SoftwareOrderItemTypeId { get; set; }
        public DateTime? SoftwareOrderItemMaintenanceStartDate { get; set; }
        public DateTime? SoftwareOrderItemMaintenanceEndDate { get; set; }
        public bool? SoftwareOrderItemHasUpgrade { get; set; }
        public int? SoftwareOrderItemPreviousId { get; set; }
        public int? SoftwareProductId { get; set; }
        public string SoftwareProductName { get; set; }
        public string SoftwareProductVersion { get; set; }
        public string SoftwareProductCompanyName { get; set; }
        public string SoftwareProductOther { get; set; }
        public string SoftwareProductStatusName { get; set; }
        public string SoftwareProductSource { get; set; }
        public string OrderFormName { get; set; }
        public string SpecificContractName { get; set; }
        public string ContractFrameworkName { get; set; }        

        public int AssignmentsCount { get; set; }
        public string Availability
        {
            get
            {
                string availability = "";

                if (SoftwareLicenseTypeId == 1 && AssignmentsCount == 0)
                {
                    availability = "0 / 1 [Free]";
                }
                else if (SoftwareLicenseTypeId == 1)
                {
                    availability = AssignmentsCount + " / 1 [Not Free]";
                }
                else if (SoftwareLicenseTypeId == 2 && MultiUserQuantity.HasValue && AssignmentsCount < MultiUserQuantity)
                {
                    availability = AssignmentsCount + " / " + MultiUserQuantity + " [Free]";
                }
                else if (SoftwareLicenseTypeId == 2 && MultiUserQuantity.HasValue && AssignmentsCount >= MultiUserQuantity)
                {
                    availability = AssignmentsCount + " / " + MultiUserQuantity + " [Not Free]";
                }
                else if (SoftwareLicenseTypeId == 3 || SoftwareLicenseTypeId == 4 || SoftwareLicenseTypeId == 5)
                {
                    availability = AssignmentsCount + " [Free]";
                }

                return availability;
            }
        }

        public string Concatenation
        {
            get
            {
                return SoftwareProductName + " | " + SoftwareProductVersion + " | " + SoftwareProductCompanyName + " | " +
                       SoftwareProductStatusName + " | " + SoftwareProductId + " | " + OrderFormName + " | " +
                       SoftwareOrderItemOlafRef + " | " + SoftwareLicenseTypeName + " | " + Availability + " | " + (MaintenanceStartDate == null ? "" : MaintenanceStartDate.Value.ToShortDateString()) + " | " +
                       (MaintenanceEndDate == null ? "" : MaintenanceEndDate.Value.ToShortDateString()) + " | " + SoftwareProductSource + " | (" + Id + ")";
            }
        }

        public string FileSize
        {
            get
            {
                if (File != null)
                {
                    return File.Count() + " Bytes";
                }
                else
                {
                    return "";
                }
            }
        }

        public string FileInfo
        {
            get
            {
                if (File != null)
                {
                    return Filename + " (" + File.Count() + " Bytes)";
                }
                else
                {
                    return "";
                }
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
                    MapData(repository.Save<SoftwareLicense>(MapData(), userLogin));
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
                    repository.Delete<SoftwareLicense>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            SoftwareLicense item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<SoftwareLicense>(id);

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

        public static List<SoftwareLicenseBL> GetAll()
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicense>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetFreeByInfix(string infix)
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicense>(x => (x.SoftwareOrderItem.SoftwareProduct.Name + " " + x.SoftwareOrderItem.SoftwareProduct.Version + " " + x.SoftwareOrderItem.SoftwareProduct.CompanyName).Contains(infix)).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            itemBL_List = itemBL_List.FindAll(y => !y.Availability.Contains("(Not Free)"));

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetBySoftwareOrder(int softwareOrderItemId)
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicense>(x => x.SoftwareOrderItemId == softwareOrderItemId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetByOlafUser(int olafUserId)
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            List<SoftwareLicenseAssignmentBL> inUseBL_List = SoftwareLicenseAssignmentBL.GetByOlafUser(olafUserId);

            using (IRepository repository = new EntityFrameworkRepository())
            {
                inUseBL_List.ForEach(item =>
                {
                    itemList.Add(repository.Get<SoftwareLicense>(item.SoftwareLicenseId.Value));
                });

                itemList = itemList.OrderBy(x => x.SoftwareOrderItem.SoftwareProduct.Name).ThenBy(x => x.SoftwareOrderItem.SoftwareProduct.Version).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetByOlafUser(string userLogin)
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            List<SoftwareLicenseAssignmentBL> inUseBL_List = SoftwareLicenseAssignmentBL.GetByOlafUser(userLogin);

            using (IRepository repository = new EntityFrameworkRepository())
            {
                inUseBL_List.ForEach(item =>
                {
                    itemList.Add(repository.Get<SoftwareLicense>(item.SoftwareLicenseId.Value));
                });

                itemList = itemList.OrderBy(x => x.SoftwareOrderItem.SoftwareProduct.Name).ThenBy(x => x.SoftwareOrderItem.SoftwareProduct.Version).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetByHardwareItem(int hardwareItemId)
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            List<SoftwareLicenseAssignmentBL> inUseBL_List = SoftwareLicenseAssignmentBL.GetByHardwareItem(hardwareItemId);

            using (IRepository repository = new EntityFrameworkRepository())
            {
                inUseBL_List.ForEach(item =>
                {
                    itemList.Add(repository.Get<SoftwareLicense>(item.SoftwareLicenseId.Value));
                });

                itemList = itemList.OrderBy(x => x.SoftwareOrderItem.SoftwareProduct.Name).ThenBy(x => x.SoftwareOrderItem.SoftwareProduct.Version).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetByKeyword(string keyword)
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                int number = 0;
                if (int.TryParse(keyword, out number))
                {
                    itemList = repository.Find<SoftwareLicense>(x =>
                                                              x.Id == number ||
                                                              x.MultiUserQuantity == number ||
                                                              x.PreviousSoftwareLicenseId == number ||

                                                              x.SerialKey.Contains(keyword) ||
                                                              x.SoftwareLicenseType.Name.Contains(keyword) ||
                                                              x.SerialKey.Contains(keyword) ||
                                                              x.Comment.Contains(keyword)
                                                              ).ToList();
                }
                else if (keyword == "")
                {
                    itemList = repository.Find<SoftwareLicense>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<SoftwareLicense>(x =>
                                                              x.SerialKey.Contains(keyword) ||
                                                              x.SoftwareLicenseType.Name.Contains(keyword) ||
                                                              x.SerialKey.Contains(keyword) ||
                                                              x.Comment.Contains(keyword)
                                                              ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetByParameters(int? idFrom,
                                                              int? idTo,
                                                              int? multiUserQuantityFrom,
                                                              int? multiUserQuantityTo,
                                                              int? softwareOrderItemIdFrom,
                                                              int? softwareOrderItemIdTo,
                                                              int? softwareOrderPreviousIdFrom,
                                                              int? softwareOrderPreviousIdTo,
                                                              int? previousIdFrom,
                                                              int? previousIdTo,
                                                              int? softwareLicenseTypeId,
                                                              int? softwareOrderTypeId,
                                                              int? softwareProductStatusId,
                                                              DateTime? dateStartFrom,
                                                              DateTime? dateStartTo,
                                                              DateTime? dateEndFrom,
                                                              DateTime? dateEndTo,
                                                              string serialKey,
                                                              string softwareOrderOlafRef,
                                                              string softwareProductName,
                                                              string softwareProductVersion,
                                                              string softwareProductCompanyName,
                                                              string softwareProductSource,
                                                              string availability,
                                                              string orderFormName,
                                                              string specificContractName,
                                                              string contractFrameworkName,
                                                              bool onlyWithoutUpgrade,
                                                              bool onlyOneLicensePerProduct
                                                              )
        {

            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicense>(x =>
                                                              (idFrom == null || x.Id >= idFrom) &&
                                                              (idTo == null || x.Id <= idTo) &&
                                                              (multiUserQuantityFrom == null || x.MultiUserQuantity >= multiUserQuantityFrom) &&
                                                              (multiUserQuantityTo == null || x.MultiUserQuantity <= multiUserQuantityTo) &&
                                                              (softwareOrderItemIdFrom == null || x.SoftwareOrderItemId >= softwareOrderItemIdFrom) &&
                                                              (softwareOrderItemIdTo == null || x.SoftwareOrderItemId <= softwareOrderItemIdTo) &&
                                                              (softwareOrderPreviousIdFrom == null || x.SoftwareOrderItem.PreviousSoftwareOrderItemId >= softwareOrderPreviousIdFrom) &&
                                                              (softwareOrderPreviousIdTo == null || x.SoftwareOrderItem.PreviousSoftwareOrderItemId <= softwareOrderPreviousIdTo) &&
                                                              (previousIdFrom == null || x.PreviousSoftwareLicenseId >= previousIdFrom) &&
                                                              (previousIdTo == null || x.PreviousSoftwareLicenseId <= previousIdTo) &&

                                                              (softwareLicenseTypeId == null || x.SoftwareLicenseTypeId == softwareLicenseTypeId) &&
                                                              (softwareOrderTypeId == null || x.SoftwareOrderItem.SoftwareOrderItemTypeId == softwareOrderTypeId) &&
                                                              (softwareProductStatusId == null || x.SoftwareOrderItem.SoftwareProduct.SoftwareProductStatusId == softwareProductStatusId) &&

                                                              (dateStartFrom == null || x.SoftwareOrderItem.MaintenanceStartDate >= dateStartFrom) &&
                                                              (dateStartTo == null || x.SoftwareOrderItem.MaintenanceStartDate <= dateStartTo) &&
                                                              (dateEndFrom == null || x.SoftwareOrderItem.MaintenanceEndDate == null || x.SoftwareOrderItem.MaintenanceEndDate >= dateEndFrom) &&
                                                              (dateEndTo == null || x.SoftwareOrderItem.MaintenanceEndDate <= dateEndTo) &&

                                                              (serialKey == "" || x.SerialKey.Contains(serialKey)) &&
                                                              (softwareOrderOlafRef == "" || x.SoftwareOrderItem.OlafRef.Contains(softwareOrderOlafRef)) &&
                                                              (softwareProductName == "" || x.SoftwareOrderItem.SoftwareProduct.Name.Contains(softwareProductName)) &&
                                                              (softwareProductVersion == "" || x.SoftwareOrderItem.SoftwareProduct.Version.Contains(softwareProductVersion)) &&
                                                              (softwareProductCompanyName == "" || x.SoftwareOrderItem.SoftwareProduct.CompanyName.Contains(softwareProductCompanyName)) &&
                                                              (softwareProductSource == "" || x.SoftwareOrderItem.SoftwareProduct.Source.Contains(softwareProductSource)) &&
                                                              (orderFormName == "" || x.SoftwareOrderItem.OrderForm.Name.Contains(orderFormName)) &&
                                                              (specificContractName == "" || x.SoftwareOrderItem.OrderForm.SpecificContract.Name.Contains(specificContractName)) &&
                                                              (contractFrameworkName == "" || x.SoftwareOrderItem.OrderForm.SpecificContract.ContractFramework.Name.Contains(contractFrameworkName)) &&
                                                              (onlyWithoutUpgrade == false || !(x.SoftwareOrderItem.HasUpgrade.HasValue && x.SoftwareOrderItem.HasUpgrade.Value))
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });                
            }

            //filter on availability
            if (availability != "")
            {
                itemBL_List = itemBL_List.FindAll(y => y.Availability.Contains(availability));
            }

            ////throw out the items with a successor                
            //if (onlyWithoutSuccessor)
            //{
            //    List<int> previousSoftwareOrderItemIdList = new List<int>();
            //    List<int> previousSoftwareLicenseIdList = new List<int>();

            //    List<SoftwareLicenseBL> allItemBL_List = SoftwareLicenseBL.GetAll();

            //    allItemBL_List.ForEach(iBl =>
            //    {
            //        if (iBl.PreviousSoftwareLicenseId != null)
            //            previousSoftwareLicenseIdList.Add(iBl.PreviousSoftwareLicenseId.Value);
            //        if (iBl.SoftwareOrderItemPreviousId != null)
            //            previousSoftwareOrderItemIdList.Add(iBl.SoftwareOrderItemPreviousId.Value);
            //    });

            //    itemBL_List.RemoveAll(x => previousSoftwareLicenseIdList.Contains(x.Id) || previousSoftwareOrderItemIdList.Contains(x.SoftwareOrderItemId.Value));
            //}

            //Distinct on Product
            if (onlyOneLicensePerProduct)
            {
                // make sure every product appears only once
                itemBL_List = itemBL_List.GroupBy(i => i.SoftwareProductId).Select(j => j.First()).ToList();
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseBL> GetByInfix(string infix)
        {
            List<SoftwareLicenseBL> itemBL_List = new List<SoftwareLicenseBL>();
            List<SoftwareLicense> itemList = new List<SoftwareLicense>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicense>(x => (x.SoftwareOrderItem.SoftwareProduct.Name + " " + x.SoftwareOrderItem.SoftwareProduct.Version + " " + x.SoftwareOrderItem.SoftwareProduct.CompanyName).Contains(infix)).ToList();

                SoftwareLicenseBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SoftwareLicense item)
        {
            Id = item.Id;
            SoftwareOrderItemId = item.SoftwareOrderItemId;
            SoftwareLicenseTypeId = item.SoftwareLicenseTypeId;
            SoftwareLicenseTypeName = item.SoftwareLicenseType == null ? null : item.SoftwareLicenseType.Name;
            MultiUserQuantity = item.MultiUserQuantity;
            PreviousSoftwareLicenseId = item.PreviousSoftwareLicenseId;            
            SerialKey = item.SerialKey;
            Filename = item.Filename;
            File = item.File;
            Comment = item.Comment;

            SoftwareOrderItemOlafRef = item.SoftwareOrderItem == null ? null : item.SoftwareOrderItem.OlafRef;
            SoftwareOrderItemTypeName = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareOrderItemType == null) ? null : item.SoftwareOrderItem.SoftwareOrderItemType.Name;
            SoftwareOrderItemTypeId = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareOrderItemType == null) ? null : (int?)item.SoftwareOrderItem.SoftwareOrderItemType.Id;
            SoftwareOrderItemMaintenanceStartDate = item.SoftwareOrderItem == null ? null : item.SoftwareOrderItem.MaintenanceStartDate;
            SoftwareOrderItemMaintenanceEndDate = item.SoftwareOrderItem == null ? null : item.SoftwareOrderItem.MaintenanceEndDate;
            SoftwareOrderItemHasUpgrade = item.SoftwareOrderItem == null ? null : item.SoftwareOrderItem.HasUpgrade;
            SoftwareOrderItemPreviousId = item.SoftwareOrderItem == null ? null : item.SoftwareOrderItem.PreviousSoftwareOrderItemId;            
            SoftwareProductId = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareOrderItem.SoftwareProductId;
            SoftwareProductName = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareOrderItem.SoftwareProduct.Name;
            SoftwareProductVersion = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareOrderItem.SoftwareProduct.Version;
            SoftwareProductCompanyName = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareOrderItem.SoftwareProduct.CompanyName;
            SoftwareProductOther = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareOrderItem.SoftwareProduct.Other;
            SoftwareProductStatusName = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null || item.SoftwareOrderItem.SoftwareProduct.SoftwareProductStatus == null) ? null : item.SoftwareOrderItem.SoftwareProduct.SoftwareProductStatus.Name;
            SoftwareProductSource = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareOrderItem.SoftwareProduct.Source;
            OrderFormName = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.OrderForm == null) ? null : item.SoftwareOrderItem.OrderForm.Name;
            SpecificContractName = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.OrderForm == null || item.SoftwareOrderItem.OrderForm.SpecificContract == null) ? null : item.SoftwareOrderItem.OrderForm.SpecificContract.Name;
            ContractFrameworkName = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.OrderForm == null || item.SoftwareOrderItem.OrderForm.SpecificContract == null || item.SoftwareOrderItem.OrderForm.SpecificContract.ContractFramework == null) ? null : item.SoftwareOrderItem.OrderForm.SpecificContract.ContractFramework.Name;

            AssignmentsCount = item.SoftwareLicenseAssignment.Count;
        }

        internal SoftwareLicense MapData()
        {
            SoftwareLicense item = new SoftwareLicense();
            item.Id = Id;
            item.SoftwareOrderItemId = SoftwareOrderItemId;
            item.SoftwareLicenseTypeId = SoftwareLicenseTypeId;
            item.MultiUserQuantity = MultiUserQuantity;
            item.PreviousSoftwareLicenseId = PreviousSoftwareLicenseId;            
            item.SerialKey = SerialKey;
            item.Filename = Filename;
            item.File = File;
            item.Comment = Comment;
            item.EntityKey = new EntityKey("GmsrEntities.SoftwareLicenseSet", "Id", Id);

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