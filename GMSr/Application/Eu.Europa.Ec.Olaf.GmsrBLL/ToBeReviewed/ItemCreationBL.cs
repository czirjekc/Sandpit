using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class ItemCreationBL
    {
        #region Properties

        public DateTime Month { get; set; }
        public int Items { get; set; }
        

        #endregion

        #region Public Methods

        public static List<ItemCreationBL> GetLastTwelveMonthByDate(DateTime month)
        {
            month = FirstDayAndTimeStripper(month);
            List<ItemCreationBL> itemBL_List = new List<ItemCreationBL>();
            ItemCreationBL itemBL;
            List<GetSmcItemCreation> item_List = new List<GetSmcItemCreation>();

            item_List = GetSmcItemCreation.GetAllByMonth().Where(x => FirstDayAndTimeStripper(x.Month) >= month.AddMonths(-11) && FirstDayAndTimeStripper(x.Month) <= month).ToList();

            item_List.ForEach(item =>
            {
                itemBL = new ItemCreationBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);

            });


            return itemBL_List;
        }
        #endregion

        #region Internal Methods
        internal void MapData(GetSmcItemCreation item)
        {
            Items = item.Items;
            Month = item.Month;
            
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
