using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class MediaItemLocationBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }        

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {

            MediaItemLocation item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<MediaItemLocation>(id);
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

        public static List<MediaItemLocationBL> GetAll()
        {
            List<MediaItemLocationBL> itemBL_List = new List<MediaItemLocationBL>();
            List<MediaItemLocation> itemList = new List<MediaItemLocation>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<MediaItemLocation>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Name).ToList();

            MediaItemLocationBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new MediaItemLocationBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(MediaItemLocation item)
        {
            Id = item.Id;           
            Name = item.Name;
        }

        internal MediaItemLocation MapData()
        {
            MediaItemLocation item = new MediaItemLocation();
            item.Id = Id;           
            item.Name = Name;
            item.EntityKey = new EntityKey("GmsrEntities.MediaItemLocationSet", "Id", Id);

            return item;
        }

        #endregion 
    }
}