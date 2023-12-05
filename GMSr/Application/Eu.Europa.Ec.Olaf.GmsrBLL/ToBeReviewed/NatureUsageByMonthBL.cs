using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class NatureUsageByMonthBL
    {
        #region Properties
public DateTime Month { get; set; }
   public string NatureName { get; set; }
        public Int32 Items { get; set; }
     
        

        #endregion

        #region Public Methods

        public static List<NatureUsageByMonthBL> GetMonthTopTen(DateTime month) 
        {
            month = FirstDayAndTimeStripper(month);
            List<NatureUsageByMonthBL> itemBL_List = new List<NatureUsageByMonthBL>();
            NatureUsageByMonthBL itemBL;
            List<GetSmcNatureUsageByMonth> item_List = new List<GetSmcNatureUsageByMonth>();
            
            
            item_List = GetSmcNatureUsageByMonth.GetTopTenNatures(month);
            
            item_List.ForEach(item => 
            {
                itemBL = new NatureUsageByMonthBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
                 
            });

            
            return itemBL_List;
        }

        public static List<NatureUsageByMonthBL> GetMonthMiscs(DateTime month)
        {
            month = FirstDayAndTimeStripper(month);
            List<NatureUsageByMonthBL> itemBL_List = new List<NatureUsageByMonthBL>();
            NatureUsageByMonthBL itemBL;
            List<GetSmcNatureUsageByMonth> item_List = new List<GetSmcNatureUsageByMonth>();


            item_List = GetSmcNatureUsageByMonth.GetMiscNatures(month).OrderBy(x => x.Month).Where(x=> x.Month == month).ToList();

            item_List.ForEach(item =>
            {
                itemBL = new NatureUsageByMonthBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);

            });
            
            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal static DateTime FirstDayAndTimeStripper(DateTime dateToStrip)
        {
            dateToStrip = Convert.ToDateTime(dateToStrip.ToShortDateString());
            dateToStrip = dateToStrip.AddDays(-dateToStrip.Day + 1);
            return dateToStrip;
        }

        internal void MapData(GetSmcNatureUsageByMonth item) 
        {
            Items = item.Items;
            NatureName = item.NatureName;
            Month = item.Month;
        }

        #endregion

        
    }
}
