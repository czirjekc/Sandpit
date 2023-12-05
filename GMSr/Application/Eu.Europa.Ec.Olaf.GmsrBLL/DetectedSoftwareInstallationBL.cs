using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class DetectedSoftwareInstallationBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Source { get; set; }
        public DateTime DetectionDate { get; set; }
        public string AdditionalInfo { get; set; }

        public string DetectedSoftwareProductName { get; set; }
        public string DetectedSoftwareProductVersion { get; set; }
        public string HardwareItemOlafName { get; set; }
        public string HardwareItemInventoryNo { get; set; }
        public string HardwareItemFullName { get; set; }

        #endregion

        #region Public Methods

        public static List<DetectedSoftwareInstallationBL> GetByKeyword(string keyword)
        {
            List<DetectedSoftwareInstallationBL> itemBL_List = new List<DetectedSoftwareInstallationBL>();
            List<DetectedSoftwareInstallation> itemList = new List<DetectedSoftwareInstallation>();

            using (IRepository repository = new EntityFrameworkRepository())
            {

                if (keyword == "")
                {
                    itemList = repository.Find<DetectedSoftwareInstallation>(x => 1 == 1).ToList();
                }
                else
                {

                    itemList = repository.Find<DetectedSoftwareInstallation>(x =>
                                                                             x.DetectedSoftwareProduct.SoftwareName.Contains(keyword) ||
                                                                             x.DetectedSoftwareProduct.Version.Contains(keyword) ||
                                                                             (x.HardwareItem.LastName + " " + x.HardwareItem.FirstName).Contains(keyword) ||
                                                                             x.HardwareItem.InventoryNo.Contains(keyword) ||
                                                                             x.AdditionalInfo.Contains(keyword)
                                                                            ).ToList();
                }

                itemList = itemList.OrderBy(x => x.DetectedSoftwareProduct.SoftwareName).ToList();

                DetectedSoftwareInstallationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new DetectedSoftwareInstallationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<DetectedSoftwareInstallationBL> GetByParameters(string olafUser,
                                                           string inventoryNo,
                                                           string softwareName,
                                                           string softwareVersion,
                                                           string source,
                                                           string additionalInfo,
                                                           DateTime? detectionDateFrom,
                                                           DateTime? detectionDateTo
                                                           )
        {
            List<DetectedSoftwareInstallationBL> itemBL_List = new List<DetectedSoftwareInstallationBL>();
            List<DetectedSoftwareInstallation> itemList = new List<DetectedSoftwareInstallation>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<DetectedSoftwareInstallation>(x =>
                                                              (olafUser == "" || (x.HardwareItem.LastName + " " + x.HardwareItem.FirstName).Contains(olafUser)) &&
                                                              (inventoryNo == "" || x.HardwareItem.InventoryNo.Contains(inventoryNo)) &&
                                                              (softwareName == "" || x.DetectedSoftwareProduct.SoftwareName.Contains(softwareName)) &&
                                                              (softwareVersion == "" || x.DetectedSoftwareProduct.Version.Contains(softwareVersion)) &&
                                                              (source == "" || x.SourceName == source) &&
                                                              (additionalInfo == "" || x.AdditionalInfo.Contains(additionalInfo)) &&
                                                              (detectionDateFrom == null || (x.DetectionDate != null && x.DetectionDate >= detectionDateFrom)) &&
                                                              (detectionDateTo == null || (x.DetectionDate != null && x.DetectionDate <= detectionDateTo))
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                DetectedSoftwareInstallationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new DetectedSoftwareInstallationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<DetectedSoftwareInstallationBL> GetByHardwareItem(int hardwareItemId)
        {
            List<DetectedSoftwareInstallationBL> itemBL_List = new List<DetectedSoftwareInstallationBL>();
            List<DetectedSoftwareInstallation> itemList = new List<DetectedSoftwareInstallation>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<DetectedSoftwareInstallation>(x => x.HardwareItemId == hardwareItemId).ToList();

                itemList = itemList.OrderBy(x => x.DetectedSoftwareProduct.SoftwareName).ToList();

                DetectedSoftwareInstallationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new DetectedSoftwareInstallationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<DetectedSoftwareInstallationBL> GetBySoftwareProduct(int softwareProductId)
        {
            List<DetectedSoftwareInstallationBL> itemBL_List = new List<DetectedSoftwareInstallationBL>();
            List<DetectedSoftwareInstallation> itemList = new List<DetectedSoftwareInstallation>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                List<DetectedSoftwareProductMatch> matchList = repository.Find<DetectedSoftwareProductMatch>(x => x.SoftwareProductId == softwareProductId).ToList();

                matchList.ForEach(match =>
                {
                    itemList = repository.Find<DetectedSoftwareInstallation>(x => x.DetectedSoftwareProductId == match.DetectedSoftwareProductId).ToList();
                });

                itemList = itemList.OrderBy(x => x.DetectedSoftwareProduct.SoftwareName).ToList();

                DetectedSoftwareInstallationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new DetectedSoftwareInstallationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(DetectedSoftwareInstallation item)
        {
            Id = item.Id;

            Source = item.SourceName;
            AdditionalInfo = item.AdditionalInfo;
            DetectionDate = item.DetectionDate;

            DetectedSoftwareProductName = item.DetectedSoftwareProduct.SoftwareName;
            DetectedSoftwareProductVersion = item.DetectedSoftwareProduct.Version;
            HardwareItemOlafName = item.HardwareItem.OlafName;
            HardwareItemInventoryNo = item.HardwareItem.InventoryNo;
            HardwareItemFullName = item.HardwareItem.LastName.ToUpper() + " " + item.HardwareItem.FirstName;
        }

        #endregion
    }
}