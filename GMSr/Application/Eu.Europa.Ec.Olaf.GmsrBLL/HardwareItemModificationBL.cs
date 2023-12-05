using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class HardwareItemModificationBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? RmtMatId { get; set; }
        public string InventoryNo { get; set; }
        public string Description { get; set; }
        public string NewUser { get; set; }
        public string OldUser { get; set; }
        public string Type { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {

            HardwareItemModification item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<HardwareItemModification>(id);
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

        public static List<HardwareItemModificationBL> GetAll()
        {
            List<HardwareItemModificationBL> itemBL_List = new List<HardwareItemModificationBL>();
            List<HardwareItemModification> itemList = new List<HardwareItemModification>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItemModification>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Id).ToList();

            HardwareItemModificationBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new HardwareItemModificationBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        public static List<HardwareItemModificationBL> GetByRmtMatId(decimal? rmtMatId)
        {
            List<HardwareItemModificationBL> itemBL_List = new List<HardwareItemModificationBL>();
            List<HardwareItemModification> itemList = new List<HardwareItemModification>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItemModification>(x => x.RmtMatId == rmtMatId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                HardwareItemModificationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemModificationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<HardwareItemModificationBL> GetByParameters(DateTime? dateFrom,
                                                                   DateTime? dateTo,
                                                                   string type
                                                                   )
        {
            List<HardwareItemModificationBL> itemBL_List = new List<HardwareItemModificationBL>();
            List<HardwareItemModification> itemList = new List<HardwareItemModification>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItemModification>(x =>
                                                                    (dateFrom == null || x.Date == null || x.Date >= dateFrom) &&
                                                                    (dateTo == null || x.Date <= dateTo) &&
                                                                    (type == "" || x.Type.Contains(type))
                                                                ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                HardwareItemModificationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemModificationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(HardwareItemModification item)
        {
            Id = item.Id;
            Date = item.Date;
            RmtMatId = item.RmtMatId;            
            InventoryNo = item.InventoryNo;            
            Description = item.Description;
            NewUser = item.NewUser;
            OldUser = item.OldUser;
            Type = item.Type;
        }

        internal HardwareItemModification MapData()
        {
            HardwareItemModification item = new HardwareItemModification();
            item.Id = Id;
            item.Date = Date;
            item.RmtMatId = RmtMatId;
            item.InventoryNo = InventoryNo;            
            item.Description = Description;
            item.NewUser = NewUser;
            item.OldUser = OldUser;
            item.Type = Type;

            item.EntityKey = new EntityKey("GmsrEntities.HardwareItemModificationSet", "Id", Id);

            return item;
        }

        #endregion

    }
}
