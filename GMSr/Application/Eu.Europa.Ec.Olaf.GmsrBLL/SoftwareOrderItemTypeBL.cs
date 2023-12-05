using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SoftwareOrderItemTypeBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {

            SoftwareOrderItemType item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<SoftwareOrderItemType>(id);
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

        public static List<SoftwareOrderItemTypeBL> GetAll()
        {
            List<SoftwareOrderItemTypeBL> itemBL_List = new List<SoftwareOrderItemTypeBL>();
            List<SoftwareOrderItemType> itemList = new List<SoftwareOrderItemType>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareOrderItemType>(x => x.Id <= 3).ToList(); //todo: cleanup the related Database table and remove the requirement <=3
            }

            SoftwareOrderItemTypeBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new SoftwareOrderItemTypeBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SoftwareOrderItemType item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        internal SoftwareOrderItemType MapData()
        {
            SoftwareOrderItemType item = new SoftwareOrderItemType();
            item.Id = Id;
            item.Name = Name;
            item.EntityKey = new EntityKey("GmsrEntities.SoftwareOrderTypeSet", "Id", Id);

            return item;
        }

        #endregion
    }
}