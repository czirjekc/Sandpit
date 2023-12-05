using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class BacklogItemBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }                
        public string Name { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
                
        #endregion

        #region Public Methods

        public static List<BacklogItemBL> GetAll()
        {
            List<BacklogItemBL> itemBL_List = new List<BacklogItemBL>();
            List<BacklogItem> itemList = new List<BacklogItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<BacklogItem>(x => 1 == 1).ToList();

                itemList = itemList.OrderByDescending(x => x.BacklogItemStatus.Id).ToList();

                BacklogItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new BacklogItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<BacklogItemBL> GetByParameters(string name, string status)
        {
            List<BacklogItemBL> itemBL_List = new List<BacklogItemBL>();
            List<BacklogItem> itemList = new List<BacklogItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<BacklogItem>(x =>                                                                                                        
                                                       (name == "" || x.Name.Contains(name)) &&
                                                       (status == "" || x.BacklogItemStatus.Name.Contains(status))
                                                       ).ToList();

                itemList = itemList.OrderByDescending(x => x.BacklogItemStatus.Id).ToList();

                BacklogItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new BacklogItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(BacklogItem item)
        {
            Id = item.Id;
            Name = item.Name;
            Status = item.BacklogItemStatus.Name;
            StatusId = item.BacklogItemStatus.Id;
            DateStart = item.DateStart;
            DateEnd = item.DateEnd;
        }

        #endregion
    }
}