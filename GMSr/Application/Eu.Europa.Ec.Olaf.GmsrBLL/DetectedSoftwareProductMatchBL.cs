using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class DetectedSoftwareProductMatchBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public int DetectedSoftwareProductId { get; set; }
        public int SoftwareProductId { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Save(ref ArrayList validationErrorList, string userLogin)
        {
            ValidateSave(ref validationErrorList);

            if (validationErrorList.Count == 0)
            {
                using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
                {
                    MapData(repository.Save<DetectedSoftwareProductMatch>(MapData(), userLogin));
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
                    repository.Delete<DetectedSoftwareProductMatch>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            DetectedSoftwareProductMatch item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<DetectedSoftwareProductMatch>(id);
            }

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

        #endregion         

        public static List<DetectedSoftwareProductMatchBL> GetBySoftwareProduct(int softwareProductId)
        {
            List<DetectedSoftwareProductMatchBL> itemBL_List = new List<DetectedSoftwareProductMatchBL>();
            List<DetectedSoftwareProductMatch> itemList = new List<DetectedSoftwareProductMatch>();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                itemList = repository.Find<DetectedSoftwareProductMatch>(x => x.SoftwareProductId == softwareProductId).ToList();

                DetectedSoftwareProductMatchBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new DetectedSoftwareProductMatchBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(DetectedSoftwareProductMatch item)
        {
            Id = item.Id;
            DetectedSoftwareProductId = item.DetectedSoftwareProductId;
            SoftwareProductId = item.SoftwareProductId;
        }

        internal DetectedSoftwareProductMatch MapData()
        {
            DetectedSoftwareProductMatch item = new DetectedSoftwareProductMatch();
            item.Id = Id;
            item.DetectedSoftwareProductId = DetectedSoftwareProductId;
            item.SoftwareProductId = SoftwareProductId;

            item.EntityKey = new EntityKey("GmsrEntities.DetectedSoftwareProductMatchSet", "Id", Id);

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