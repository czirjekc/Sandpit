using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class OlafUserModificationBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public DateTime? Date { get; set; }
        public decimal? PeoId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string User { get; set; }
        public string OldOffice { get; set; }
        public string NewOffice { get; set; }
        public string Type { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {

            OlafUserModification item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<OlafUserModification>(id);
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

        public static List<OlafUserModificationBL> GetAll()
        {
            List<OlafUserModificationBL> itemBL_List = new List<OlafUserModificationBL>();
            List<OlafUserModification> itemList = new List<OlafUserModification>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OlafUserModification>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Id).ToList();

            OlafUserModificationBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new OlafUserModificationBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        public static List<OlafUserModificationBL> GetByPeoId(decimal? peoId)
        {
            List<OlafUserModificationBL> itemBL_List = new List<OlafUserModificationBL>();
            List<OlafUserModification> itemList = new List<OlafUserModification>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OlafUserModification>(x => x.PeoId == peoId).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                OlafUserModificationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OlafUserModificationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OlafUserModificationBL> GetByParameters(DateTime? dateFrom,
                                                                   DateTime? dateTo,
                                                                   string type
                                                                   )
        {
            List<OlafUserModificationBL> itemBL_List = new List<OlafUserModificationBL>();
            List<OlafUserModification> itemList = new List<OlafUserModification>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OlafUserModification>(x =>
                                                                    (dateFrom == null || x.Date == null || x.Date >= dateFrom) &&
                                                                    (dateTo == null || x.Date <= dateTo) &&
                                                                    (type == "" || x.Type.Contains(type))        
                                                                ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                OlafUserModificationBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OlafUserModificationBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(OlafUserModification item)
        {
            Id = item.Id;
            Date = item.Date;
            PeoId = item.PeoId;
            LastName = item.LastName;
            FirstName = item.FirstName;
            User = item.User;
            OldOffice = item.OldOffice;
            NewOffice = item.NewOffice;
            Type = item.Type;
        }

        internal OlafUserModification MapData()
        {
            OlafUserModification item = new OlafUserModification();
            item.Id = Id;
            item.Date = Date;
            item.PeoId = PeoId;
            item.LastName = LastName;
            item.FirstName = FirstName;
            item.User = User;
            item.OldOffice = OldOffice;
            item.NewOffice = NewOffice;
            item.Type = Type;

            item.EntityKey = new EntityKey("GmsrEntities.OlafUserModificationSet", "Id", Id);

            return item;
        }

        #endregion
    }
}