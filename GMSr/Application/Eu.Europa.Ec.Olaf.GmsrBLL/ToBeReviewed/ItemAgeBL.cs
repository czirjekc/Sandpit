using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class ItemAgeBL
    {
        #region Properties
        public int Week { get; set; }
        public int Items { get; set; }
        
        #endregion
        #region Public Methods
        public static List<ItemAgeBL> GetLastTwelveMonthsAgesByWeek(DateTime startMonth) 
        {
            startMonth = FirstDayAndTimeStripper(startMonth);
            List<ItemAgeBL> itemBL_List = new List<ItemAgeBL>();
            ItemAgeBL itemBL;
            List<GetSmcItemsAgeByWeek> item_List = new List<GetSmcItemsAgeByWeek>();

            item_List = GetSmcItemsAgeByWeek.GetLastTwelveMonthsAgesByWeek(startMonth);

            item_List = item_List.OrderBy(x => x.Week).ToList();

            item_List.ForEach(item =>
            {
                itemBL = new ItemAgeBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);

            });


            return itemBL_List;
        }

        #endregion
        #region Internal Methods
        internal void MapData(GetSmcItemsAgeByWeek item)
        {
            Week = item.Week;
            Items = item.Items;
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
