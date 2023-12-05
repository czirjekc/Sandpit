using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{

    public class TicketRemoteSupportItemBL
    {
        #region Properties

        public DateTime CreationDate { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Nature { get; set; }
        public string Qualification { get; set; }

        #endregion

        #region Public Methods

        public static List<TicketRemoteSupportItemBL> GetByMonth(DateTime dayInMonth)
        {
            List<TicketRemoteSupportItemBL> itemList_BL = new List<TicketRemoteSupportItemBL>();
            TicketRemoteSupportItemBL itemBL;
            dayInMonth = FirstDayAndTimeStripper(dayInMonth);

            GetSmcRemoteSupportItem.GetAll().Where(x => x.CreationDate.Year  == dayInMonth.Year && x.CreationDate.Month == dayInMonth.Month).ToList().ForEach(x => 
            {
                itemBL = new TicketRemoteSupportItemBL();
                itemBL.MapData(x);
                itemList_BL.Add(itemBL);
                
            });
         
            return itemList_BL;
        
        }

        #endregion

        #region Internal Methods

        internal void MapData(GetSmcRemoteSupportItem item) {
            CreationDate = item.CreationDate;
            Code = item.Code ;
            Title = item.Title;
            Nature = item.Nature;
            Qualification = item.Qualification;
        }

        internal static DateTime FirstDayAndTimeStripper(DateTime dateToStrip)
        {
            dateToStrip = Convert.ToDateTime(dateToStrip.ToShortDateString());
            dateToStrip = dateToStrip.AddDays(-dateToStrip.Day + 1);
            return dateToStrip;
        }

        #endregion

    }
}
