using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class MediaItemBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }
        public int? SoftwareOrderItemId { get; set; }
        public int? MediaItemTypeId { get; set; }
        public string MediaItemTypeName { get; set; }
        public int? MediaItemLocationId { get; set; }
        public string MediaItemLocationName { get; set; }
        public string Comment { get; set; }
        public string SoftwareProductSource { get; set; }

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
                    MapData(repository.Save<MediaItem>(MapData(), userLogin));
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
                    repository.Delete<MediaItem>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            MediaItem item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<MediaItem>(id);

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

        public static List<MediaItemBL> GetAll()
        {
            List<MediaItemBL> itemBL_List = new List<MediaItemBL>();
            List<MediaItem> itemList = new List<MediaItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<MediaItem>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Name).ToList();

                MediaItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new MediaItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<MediaItemBL> GetBySoftwareOrder(int softwareOrderItemId)
        {
            List<MediaItemBL> itemBL_List = new List<MediaItemBL>();
            List<MediaItem> itemList = new List<MediaItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<MediaItem>(x => x.SoftwareOrderItemId == softwareOrderItemId).ToList();

                itemList = itemList.OrderBy(x => x.Name).ToList();

                MediaItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new MediaItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<MediaItemBL> GetByKeyword(string keyword)
        {
            List<MediaItemBL> itemBL_List = new List<MediaItemBL>();
            List<MediaItem> itemList = new List<MediaItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {

                if (keyword == "")
                {
                    itemList = repository.Find<MediaItem>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<MediaItem>(x =>
                                                          x.Name.Contains(keyword) ||
                                                          x.MediaItemLocation.Name.Contains(keyword) ||
                                                          x.Comment.Contains(keyword) ||
                                                          x.MediaItemType.Name.Contains(keyword) ||
                                                          x.SoftwareOrderItem.SoftwareProduct.Source.Contains(keyword)
                                                          ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Id).ToList();

                MediaItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new MediaItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(MediaItem item)
        {
            Id = item.Id;

            Name = item.Name;
            SoftwareOrderItemId = item.SoftwareOrderItemId;
            MediaItemTypeId = item.MediaItemTypeId;
            MediaItemTypeName = item.MediaItemType == null ? null : item.MediaItemType.Name;
            MediaItemLocationId = item.MediaItemLocationId;
            MediaItemLocationName = item.MediaItemLocation == null ? null : item.MediaItemLocation.Name;
            Comment = item.Comment;
            SoftwareProductSource = (item.SoftwareOrderItem == null || item.SoftwareOrderItem.SoftwareProduct == null) ? null : item.SoftwareOrderItem.SoftwareProduct.Source;
        }

        internal MediaItem MapData()
        {
            MediaItem item = new MediaItem();
            item.Id = Id;

            item.Name = Name;
            item.SoftwareOrderItemId = SoftwareOrderItemId;
            item.MediaItemTypeId = MediaItemTypeId;
            item.MediaItemLocationId = MediaItemLocationId;
            item.Comment = Comment;

            item.EntityKey = new EntityKey("GmsrEntities.MediaItemSet", "Id", Id);

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