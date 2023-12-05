using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SoftwareLicenseTypeBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }        

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {

            SoftwareLicenseType item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<SoftwareLicenseType>(id);
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

        public static List<SoftwareLicenseTypeBL> GetAll()
        {
            List<SoftwareLicenseTypeBL> itemBL_List = new List<SoftwareLicenseTypeBL>();
            List<SoftwareLicenseType> itemList = new List<SoftwareLicenseType>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<SoftwareLicenseType>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Name).ToList();

            SoftwareLicenseTypeBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new SoftwareLicenseTypeBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SoftwareLicenseType item)
        {
            Id = item.Id;           
            Name = item.Name;
        }

        internal SoftwareLicenseType MapData()
        {
            SoftwareLicenseType item = new SoftwareLicenseType();
            item.Id = Id;           
            item.Name = Name;
            item.EntityKey = new EntityKey("GmsrEntities.SoftwareLicenseTypeSet", "Id", Id);

            return item;
        }

        #endregion
    }
}