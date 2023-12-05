using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SoftwareLicenseAssignmentBL : BaseBL
    {
        #region Fields

        private const string PENDING_UNINSTALLATION = "Pending for Uninstallation";
        private const string INSTALLED = "Installed";
        private const string PENDING_INSTALLATION = "Pending for Installation";

        #endregion

        #region Properties

        public int Id { get; set; }

        public int? SoftwareLicenseId { get; set; }
        public int? SoftwareProductId { get; set; }
        public string SoftwareProductName { get; set; }
        public string SoftwareProductVersion { get; set; }
        public string SoftwareProductCompanyName { get; set; }
        public int? OlafUserId { get; set; }
        public string OlafUserFullName { get; set; }
        public string OlafUserConcatenation { get; set; }
        public int? HardwareItemId { get; set; }
        public string HardwareItemName { get; set; }
        public string HardwareItemConcatenation { get; set; }
        public string Comment { get; set; }
        public DateTime? DateAssigned { get; set; }
        public DateTime? DateInstalled { get; set; }
        public DateTime? DateUnassigned { get; set; }

        public string Status
        {
            get
            {
                string status = "";

                if (DateUnassigned.HasValue)
                {
                    status = PENDING_UNINSTALLATION;
                }
                else if (DateInstalled.HasValue)
                {
                    status = INSTALLED;
                }
                else
                {
                    status = PENDING_INSTALLATION;
                }

                return status;
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
                    MapData(repository.Save<SoftwareLicenseAssignment>(MapData(), userLogin));
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
                    repository.Delete<SoftwareLicenseAssignment>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            SoftwareLicenseAssignment item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<SoftwareLicenseAssignment>(id);

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

        public static List<SoftwareLicenseAssignmentBL> GetAll()
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.SoftwareLicenseId).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseAssignmentBL> GetByKeyword(string keyword)
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                int number = 0;
                if (int.TryParse(keyword, out number))
                {
                    itemList = repository.Find<SoftwareLicenseAssignment>(x =>
                                                              x.SoftwareLicenseId == number ||
                                                              (x.OlafUser.LastName + " " + x.OlafUser.FirstName).Contains(keyword) ||
                                                              (x.HardwareItem.OlafName + " " + x.HardwareItem.InventoryNo + " " + x.HardwareItem.Model).Contains(keyword) ||
                                                              x.Comment.Contains(keyword)
                                                              ).ToList();
                }
                else if (keyword == "")
                {
                    itemList = repository.Find<SoftwareLicenseAssignment>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<SoftwareLicenseAssignment>(x =>
                                                              (x.OlafUser.LastName + " " + x.OlafUser.FirstName).Contains(keyword) ||
                                                              (x.HardwareItem.OlafName + " " + x.HardwareItem.InventoryNo + " " + x.HardwareItem.Model).Contains(keyword) ||
                                                              x.Comment.Contains(keyword)
                                                              ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseAssignmentBL> GetByParameters(
                                                           string softwareProductName,
                                                           string softwareProductVersion,
                                                           string status,
                                                           string olafUser,
                                                           string hardwareItem,
                                                           DateTime? assignmentDateFrom,
                                                           DateTime? assignmentDateTo,
                                                           DateTime? installationDateFrom,
                                                           DateTime? installationDateTo,
                                                           DateTime? unassignmentDateFrom,
                                                           DateTime? unassignmentDateTo,
                                                           string comment
                                                           )
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x =>
                                                              (softwareProductName == "" || x.SoftwareLicense.SoftwareOrderItem.SoftwareProduct.Name.Contains(softwareProductName)) &&
                                                              (softwareProductVersion == "" || x.SoftwareLicense.SoftwareOrderItem.SoftwareProduct.Version.Contains(softwareProductVersion)) &&
                                                              (status == "" || (status == PENDING_INSTALLATION && x.DateInstalled == null) || (status == PENDING_UNINSTALLATION && x.DateUnassigned.HasValue) || (status == INSTALLED && x.DateInstalled.HasValue)) &&
                                                              (olafUser == "" || (x.OlafUser.LastName + " " + x.OlafUser.FirstName).Contains(olafUser)) &&
                                                              (hardwareItem == "" || (x.HardwareItem.OlafName + " " + x.HardwareItem.InventoryNo + " " + x.HardwareItem.Model).Contains(hardwareItem)) &&
                                                              (comment == "" || x.Comment.Contains(comment)) &&
                                                              (assignmentDateFrom == null || (x.DateAssigned != null && x.DateAssigned >= assignmentDateFrom)) &&
                                                              (assignmentDateTo == null || (x.DateAssigned != null && x.DateAssigned <= assignmentDateTo)) &&
                                                              (installationDateFrom == null || (x.DateInstalled != null && x.DateInstalled >= installationDateFrom)) &&
                                                              (installationDateTo == null || (x.DateInstalled != null && x.DateInstalled <= installationDateTo)) &&
                                                              (unassignmentDateFrom == null || (x.DateUnassigned != null && x.DateUnassigned >= unassignmentDateFrom)) &&
                                                              (unassignmentDateTo == null || (x.DateUnassigned != null && x.DateUnassigned <= unassignmentDateTo))
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseAssignmentBL> GetBySoftwareLicense(int softwareLicenseId)
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => x.SoftwareLicenseId == softwareLicenseId).ToList();

                itemList = itemList.OrderBy(x => x.OlafUserId).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseAssignmentBL> GetByOlafUser(int olafUserId)
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => x.OlafUserId == olafUserId).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseAssignmentBL> GetByOlafUser(string userLogin)
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => x.OlafUser.Login == userLogin).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareLicenseAssignmentBL> GetByHardwareItem(int hardwareItemId)
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => x.HardwareItemId == hardwareItemId).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static SoftwareLicenseAssignmentBL GetBySoftwareLicenseAndOlafUser(int softwareLicenseId, int olafUserId)
        {
            SoftwareLicenseAssignmentBL itemBL = null;
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => x.SoftwareLicenseId == softwareLicenseId &&
                                                                      x.OlafUserId == olafUserId
                                                                 ).ToList();

                if (itemList.Count > 0)
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(itemList[0]);
                }
            }

            return itemBL;
        }

        public static SoftwareLicenseAssignmentBL GetBySoftwareLicenseAndHardwareItem(int softwareLicenseId, int hardwareItemId)
        {
            SoftwareLicenseAssignmentBL itemBL = null;
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => x.SoftwareLicenseId == softwareLicenseId &&
                                                                      x.HardwareItemId == hardwareItemId
                                                                 ).ToList();

                if (itemList.Count > 0)
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(itemList[0]);
                }
            }

            return itemBL;
        }

        public static List<SoftwareLicenseAssignmentBL> GetByOlafUserAndHardwareItem(int olafUserId, int hardwareItemId)
        {
            List<SoftwareLicenseAssignmentBL> itemBL_List = new List<SoftwareLicenseAssignmentBL>();
            List<SoftwareLicenseAssignment> itemList = new List<SoftwareLicenseAssignment>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseAssignment>(x => x.OlafUserId == olafUserId &&
                                                                      x.HardwareItemId == hardwareItemId
                                                                     ).ToList();

                SoftwareLicenseAssignmentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareLicenseAssignmentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SoftwareLicenseAssignment item)
        {
            Id = item.Id;

            SoftwareLicenseId = item.SoftwareLicenseId;
            SoftwareProductId = (item.SoftwareLicense == null || item.SoftwareLicense.SoftwareOrderItem == null || item.SoftwareLicense.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareLicense.SoftwareOrderItem.SoftwareProductId;
            SoftwareProductName = (item.SoftwareLicense == null || item.SoftwareLicense.SoftwareOrderItem == null || item.SoftwareLicense.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareLicense.SoftwareOrderItem.SoftwareProduct.Name;
            SoftwareProductVersion = (item.SoftwareLicense == null || item.SoftwareLicense.SoftwareOrderItem == null || item.SoftwareLicense.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareLicense.SoftwareOrderItem.SoftwareProduct.Version;
            SoftwareProductCompanyName = (item.SoftwareLicense == null || item.SoftwareLicense.SoftwareOrderItem == null || item.SoftwareLicense.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareLicense.SoftwareOrderItem.SoftwareProduct.CompanyName;
            OlafUserId = item.OlafUserId;
            OlafUserFullName = item.OlafUser == null ? "" : item.OlafUser.LastName.ToUpper() + " " + item.OlafUser.FirstName;
            OlafUserConcatenation = item.OlafUser == null ? "" : item.OlafUser.LastName + " " + item.OlafUser.FirstName + " | (" + item.OlafUser.Id + ")";
            HardwareItemId = item.HardwareItemId;
            HardwareItemName = item.HardwareItem == null ? "" : item.HardwareItem.Model;
            HardwareItemConcatenation = item.HardwareItem == null ? "" : item.HardwareItem.OlafName + " | " + item.HardwareItem.InventoryNo + " | " + item.HardwareItem.Model + " | Status: " + item.HardwareItem.Status + " | Local: " + item.HardwareItem.Local + " | (" + item.HardwareItem.Id + ")";
            Comment = item.Comment;
            DateAssigned = item.DateAssigned;
            DateInstalled = item.DateInstalled;
            DateUnassigned = item.DateUnassigned;
        }

        internal SoftwareLicenseAssignment MapData()
        {
            SoftwareLicenseAssignment item = new SoftwareLicenseAssignment();
            item.Id = Id;
            item.SoftwareLicenseId = SoftwareLicenseId.Value;
            item.OlafUserId = OlafUserId;
            item.HardwareItemId = HardwareItemId;
            item.Comment = Comment;
            item.DateAssigned = DateAssigned;
            item.DateInstalled = DateInstalled;
            item.DateUnassigned = DateUnassigned;
            item.EntityKey = new EntityKey("GmsrEntities.SoftwareLicenseAssignmentSet", "Id", Id);

            return item;
        }

        #endregion

        #region Private Methods

        private void ValidateSave(ref ArrayList validationErrorList)
        {
            if (SoftwareLicenseId.HasValue)
            {
                // check whether the license is an upgrade license
                SoftwareLicenseBL license = new SoftwareLicenseBL();
                license.Select(SoftwareLicenseId.Value);
                if (license.SoftwareOrderItemTypeId == 2 && license.SoftwareOrderItemPreviousId != null && OlafUserId.HasValue)
                {
                    bool isPreviousLicenseAssigned = false;
                    SoftwareLicenseBL.GetByOlafUser(OlafUserId.Value).ForEach(item =>
                    {
                        if (item.SoftwareOrderItemId == license.SoftwareOrderItemPreviousId)
                            isPreviousLicenseAssigned = true;
                    });

                    if (!isPreviousLicenseAssigned)
                    {
                        validationErrorList.Add("The upgrade license can not be assigned to this user. A corresponding previous license (where the upgrade license can be upgraded from) needs to be assigned to the user first.");
                    }
                }
            }
            else
            {
                validationErrorList.Add("There is no license.");
            }
        }

        private void ValidateDelete(ref ArrayList validationErrorList)
        {
            if (SoftwareLicenseId.HasValue)
            {
                // check whether the license has been upgraded and whether the upgrade license is assigned to the same user and machine
                SoftwareLicenseBL license = new SoftwareLicenseBL();
                license.Select(SoftwareLicenseId.Value);
                if (license.SoftwareOrderItemHasUpgrade.HasValue && license.SoftwareOrderItemHasUpgrade.Value && OlafUserId.HasValue && HardwareItemId.HasValue)
                {
                    bool isUpgradeAssigned = false;
                    // check whether one of the other licenses assigned to the same user and machine is an upgrade for the current license
                    SoftwareLicenseBL otherLicense = new SoftwareLicenseBL();
                    SoftwareLicenseAssignmentBL.GetByOlafUserAndHardwareItem(OlafUserId.Value, HardwareItemId.Value).ForEach(item =>
                    {
                        otherLicense.Select(item.SoftwareLicenseId.Value);
                        if (otherLicense.SoftwareOrderItemPreviousId == license.SoftwareOrderItemId)
                            isUpgradeAssigned = true;
                    });

                    if (isUpgradeAssigned)
                    {
                        validationErrorList.Add("The license can not be unassigned, because an upgrade license for that license is assigned to the same user and machine. So first remove the assignment of the corresponding upgrade license.");
                    }
                }
            }
        }

        #endregion
    }
}