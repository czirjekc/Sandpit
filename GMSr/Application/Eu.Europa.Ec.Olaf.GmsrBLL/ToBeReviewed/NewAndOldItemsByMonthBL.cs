using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class NewAndOldItemsByMonthBL
    {
        #region Properties

        public DateTime Month { get; set; }
        public int New { get; set; }
        public int Old { get; set; }

        #endregion

        #region Public Methods

        public static List<NewAndOldItemsByMonthBL> GetLastTwelveMonthsNewAndOldItemsByMonth(DateTime startMonth)
        {
            startMonth = FirstDayAndTimeStripper(startMonth);
            List<NewAndOldItemsByMonthBL> itemBL_List = new List<NewAndOldItemsByMonthBL>();
            NewAndOldItemsByMonthBL itemBL;
            List<GetSmcNewAndOldItemsByMonth> item_List = new List<GetSmcNewAndOldItemsByMonth>();

            item_List = GetSmcNewAndOldItemsByMonth.GetNewAndOldItemsByMonth(startMonth).Where(x => x.Month >= startMonth.AddMonths(-11) && x.Month <= startMonth).ToList();

            item_List = item_List.OrderBy(x => x.Month).ToList();

            item_List.ForEach(item =>
            {
                itemBL = new NewAndOldItemsByMonthBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);

            });


            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(GetSmcNewAndOldItemsByMonth item)
        {
            Month  = item.Month;
            Old = item.Old;
            New = item.New;
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
