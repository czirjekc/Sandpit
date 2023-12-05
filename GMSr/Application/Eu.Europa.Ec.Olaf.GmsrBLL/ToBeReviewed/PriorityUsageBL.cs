using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class PriorityUsageBL
    {
        #region Properties
public string PriorityName { get; set; }
        public Int32 Items { get; set; }
        

        #endregion

        #region Public Methods

        public static List<PriorityUsageBL> GetPriorityUsageByMonth(DateTime month)
        {
            List<PriorityUsageBL> itemBL_List = new List<PriorityUsageBL>();
            PriorityUsageBL itemBL;
            List<GetSmcPriorityUsage> item_List = new List<GetSmcPriorityUsage>();


            item_List = GetSmcPriorityUsage.GetPriorityUsageByMonth(FirstDayAndTimeStripper(month));

            item_List.ForEach(item =>
            {
                itemBL = new PriorityUsageBL();
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

        internal void MapData(GetSmcPriorityUsage item)
        {
            Items = item.Items;
            PriorityName = item.PriorityName;
        }

        #endregion


    }
}

