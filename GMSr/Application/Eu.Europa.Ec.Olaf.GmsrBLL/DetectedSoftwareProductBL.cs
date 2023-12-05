using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class DetectedSoftwareProductBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }
        public string Version { get; set; }
        public string Source { get; set; }
        public DateTime DetectionDate { get; set; }

        public string Concatenation
        {
            get
            {
                return Name + " | " + Version + " | (" + Id + ")";
            }
        }

        #endregion

        #region Public Methods

        #region Standard 

        public bool Select(int id)
        {
            DetectedSoftwareProduct item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<DetectedSoftwareProduct>(id);

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

        public static List<DetectedSoftwareProductBL> GetByInfix(string infix)
        {
            List<DetectedSoftwareProductBL> itemBL_List = new List<DetectedSoftwareProductBL>();
            List<DetectedSoftwareProduct> itemList = new List<DetectedSoftwareProduct>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<DetectedSoftwareProduct>(x => (x.SoftwareName + " " + x.Version).Contains(infix)).ToList();

                DetectedSoftwareProductBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new DetectedSoftwareProductBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(DetectedSoftwareProduct item)
        {
            Id = item.Id;

            Name = item.SoftwareName;
            Version = item.Version;
            Source = item.SourceName;
            DetectionDate = item.DetectionDate;
        }

        internal DetectedSoftwareProduct MapData()
        {
            DetectedSoftwareProduct item = new DetectedSoftwareProduct();
            item.Id = Id;

            item.SoftwareName = Name;
            item.Version = Version;
            item.SourceName = Source;
            item.DetectionDate = DetectionDate;

            item.EntityKey = new EntityKey("GmsrEntities.DetectedSoftwareProductSet", "Id", Id);

            return item;
        }

        #endregion
    }
}