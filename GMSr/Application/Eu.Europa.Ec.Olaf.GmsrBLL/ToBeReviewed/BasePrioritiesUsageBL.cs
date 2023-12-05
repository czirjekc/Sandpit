using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class BasePrioritiesUsageBL
    {
        #region Properties
        public DateTime Month { get; set; }
        public int NormalPriority { get; set; }
        public int HighPriority { get; set; }
        public int VeryHighPriority { get; set; }
        public int CriticalPriority { get; set; }
        #endregion

        #region Public Methods
        public static List<BasePrioritiesUsageBL> GetBasePrioritiesUsageByMonth(DateTime month)
        {
            month = FirstDayAndTimeStripper(month);
            List<BasePrioritiesUsageBL> itemBL_List = new List<BasePrioritiesUsageBL>();
            BasePrioritiesUsageBL itemBL;
            List<GetSmcBasePrioritiesUsage> item_List = new List<GetSmcBasePrioritiesUsage>();

            item_List = GetSmcBasePrioritiesUsage.GetBasePrioritiesUsageByMonth(month);

            item_List.ForEach(item =>
            {
                itemBL = new BasePrioritiesUsageBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);

            });


            return itemBL_List;
        }
        public static List<BasePrioritiesUsageBL> GetLastTwelveMonths(DateTime startMonth) 
        {
            startMonth = FirstDayAndTimeStripper(startMonth);
            List<BasePrioritiesUsageBL> itemBL_List = new List<BasePrioritiesUsageBL>();
            BasePrioritiesUsageBL itemBL;
            List<GetSmcBasePrioritiesUsage> item_List = new List<GetSmcBasePrioritiesUsage>();

            item_List = GetSmcBasePrioritiesUsage.GetAll().Where(x => FirstDayAndTimeStripper(x.Month) >= startMonth.AddMonths(-11) && FirstDayAndTimeStripper(x.Month) <= startMonth ).ToList();

            item_List.ForEach(item =>
            {
                itemBL = new BasePrioritiesUsageBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);

            });


            return itemBL_List;
        
        }


        #endregion

        #region Internal Methods
        internal void MapData(GetSmcBasePrioritiesUsage item)
        {
            Month = item.Month;
            NormalPriority = item.NormalPriority;
            HighPriority = item.HighPriority;
            VeryHighPriority = item.VeryHighPriority;
            CriticalPriority = item.CriticalPriority;
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
