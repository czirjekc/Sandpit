using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class MediaItemTypeBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }        

        #endregion

        #region Public Methods

        #region Standard 

        public bool Select(int id)
        {

            MediaItemType item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<MediaItemType>(id);
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

        public static List<MediaItemTypeBL> GetAll()
        {
            List<MediaItemTypeBL> itemBL_List = new List<MediaItemTypeBL>();
            List<MediaItemType> itemList = new List<MediaItemType>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<MediaItemType>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Name).ToList();

            MediaItemTypeBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new MediaItemTypeBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(MediaItemType item)
        {
            Id = item.Id;           
            Name = item.Name;
        }

        internal MediaItemType MapData()
        {
            MediaItemType item = new MediaItemType();
            item.Id = Id;           
            item.Name = Name;
            item.EntityKey = new EntityKey("GmsrEntities.MediaItemTypeSet", "Id", Id);

            return item;
        }

        #endregion
    }
}