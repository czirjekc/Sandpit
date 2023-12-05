using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SoftwareProductStatusBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }        

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {

            SoftwareProductStatus item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<SoftwareProductStatus>(id);
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

        public static List<SoftwareProductStatusBL> GetAll()
        {
            List<SoftwareProductStatusBL> itemBL_List = new List<SoftwareProductStatusBL>();
            List<SoftwareProductStatus> itemList = new List<SoftwareProductStatus>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareProductStatus>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Id).ToList();

            SoftwareProductStatusBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new SoftwareProductStatusBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SoftwareProductStatus item)
        {
            Id = item.Id;           
            Name = item.Name;
        }

        internal SoftwareProductStatus MapData()
        {
            SoftwareProductStatus item = new SoftwareProductStatus();
            item.Id = Id;           
            item.Name = Name;
            item.EntityKey = new EntityKey("GmsrEntities.SoftwareProductStatusSet", "Id", Id);

            return item;
        }

        #endregion
    }
}