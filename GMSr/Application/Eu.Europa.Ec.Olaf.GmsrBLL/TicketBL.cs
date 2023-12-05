using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class TicketBL : BaseBL
    {
        #region Properties

        public string Code { get; set; }
        public string Title { get; set; }
        public string Nature { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ActionGroupResolutionDate { get; set; }
        public string Concern { get; set; }

        #endregion

        #region Public Methods 

        public static List<TicketBL> GetOpenByUserLogin(string userLogin)
        {
            List<TicketBL> itemBL_List = new List<TicketBL>();
            List<HelpdeskItem> itemList = new List<HelpdeskItem>();

            using (IRepository repository = RepositoryFactory.CreateWithGetSmcEntities())
            {
                itemList = repository.Find<HelpdeskItem>(x => x.ConcernDescription.StartsWith(userLogin) &&
                                                                x.IdState != 2
                                                                ).ToList();

                itemList = itemList.OrderByDescending(x => x.CreationDate).ToList();

                TicketBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new TicketBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }
        
        public static List<TicketBL> GetResolvedByUserLogin(string userLogin)
        {
            List<TicketBL> itemBL_List = new List<TicketBL>();
            List<HelpdeskActionGroup> itemList = new List<HelpdeskActionGroup>();

            using (IRepository repository = RepositoryFactory.CreateWithGetSmcEntities())
            {
                itemList = repository.Find<HelpdeskActionGroup>(x => x.HelpdeskItem.ConcernDescription.StartsWith(userLogin) &&
                                                                x.HelpdeskItem.IdState == 2 &&
                                                                x.IdFunctionalServiceAffected == 59 /*&&
                                                                x.ResolutionDate != null */
                                                                ).ToList();
                
                itemList = itemList.OrderByDescending(x => x.ResolutionDate).ToList();

                TicketBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new TicketBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods 

        internal void MapData(HelpdeskItem item)
        {
            Code = item.Code;
            Title = item.Title;
            Nature = item.HelpdeskNature == null ? null : item.HelpdeskNature.ArtemisDescription;
            CreationDate = item.CreationDate;
            Concern = item.ConcernDescription;         
        }
        
        internal void MapData(HelpdeskActionGroup item)
        {
            Code = item.HelpdeskItem == null ? null : item.HelpdeskItem.Code;
            Title = item.HelpdeskItem == null ? null : item.HelpdeskItem.Title;
            Nature = (item.HelpdeskItem == null || item.HelpdeskItem.HelpdeskNature == null) ? null : item.HelpdeskItem.HelpdeskNature.ArtemisDescription;
            ActionGroupResolutionDate = item.ResolutionDate;
            Concern = item.HelpdeskItem == null ? null : item.HelpdeskItem.ConcernDescription;
        }

        #endregion
    }
}