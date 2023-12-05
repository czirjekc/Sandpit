using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.GmsrServices
{
    public class RestWebService : IRest
    {
        #region Public Methods

        public List<Ticket> GetOpenByUserLogin(string userLogin)
        {
            List<Ticket> itemList = new List<Ticket>();
            List<TicketBL> itemBL_List = TicketBL.GetOpenByUserLogin(userLogin);

            Ticket item;
            itemBL_List.ForEach(itemBL =>
            {
                item = MapData(itemBL);
                itemList.Add(item);
            });

            return itemList;
        }

        public List<Ticket> GetOpenByUserLoginXml(string userLogin)
        {
            return GetOpenByUserLogin(userLogin);
        }

        public List<Ticket> GetResolvedByUserLogin(string userLogin)
        {
            List<Ticket> itemList = new List<Ticket>();
            List<TicketBL> itemBL_List = TicketBL.GetResolvedByUserLogin(userLogin);

            Ticket item;
            itemBL_List.ForEach(itemBL =>
            {
                item = MapData(itemBL);
                itemList.Add(item);
            });

            return itemList;
        }

        public List<Ticket> GetResolvedByUserLoginXml(string userLogin)
        {
            return GetResolvedByUserLogin(userLogin);
        }

        #endregion

        #region Internal Methods

        internal Ticket MapData(TicketBL itemBL)
        {
            Ticket item = new Ticket();

            item.Code = itemBL.Code;
            item.Title = itemBL.Title;
            item.Nature = itemBL.Nature;
            item.CreationDate = itemBL.CreationDate;
            item.Concern = itemBL.Concern;

            return item;
        }

        #endregion
    }
}
