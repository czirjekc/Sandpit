using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class GlobalQualityIndicatorBL
    {
        #region Properties

        public DateTime Month { get; set; }
        public Decimal Value { get; set; }


        #endregion

        #region Public Methods
        public static List<GlobalQualityIndicatorBL> GetLastTwelveByDate(DateTime startDate)
        {
            List<GlobalQualityIndicatorBL> itemList_BL = new List<GlobalQualityIndicatorBL>();
            List<ServiceQualityIndicatorHistoryItem> itemList = new List<ServiceQualityIndicatorHistoryItem>();

            startDate = FirstDayAndTimeStripper(startDate);

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ServiceQualityIndicatorHistoryItem>(x => 1 == 1).ToList();
                
                GlobalQualityIndicatorBL itemBL;

                List<ServiceQualityIndicatorHistoryItem> justThisMonthItemsList = new List<ServiceQualityIndicatorHistoryItem>();
                for (int i = 0; i < 12; i++)
                {
                    itemBL = new GlobalQualityIndicatorBL();
                    itemBL.Month = startDate.AddMonths(-i);
                    itemBL.Value = 0;

                    justThisMonthItemsList = (from items in itemList where FirstDayAndTimeStripper(items.ComputationDate) == startDate.AddMonths(-i) select items).ToList();

                    justThisMonthItemsList.ForEach(x =>
                    {
                        itemBL.Value += ((x.AverageProfiledSQI_Value) * Convert.ToDecimal(x.ServiceQualityIndicatorType.POINTS) / 90);

                    });
                    itemBL.Value = Math.Round(itemBL.Value, 2);
                    itemList_BL.Add(itemBL);

                }


            }

            itemList_BL =itemList_BL.OrderBy(x => x.Month).ToList();
            return itemList_BL;
        }
        #endregion

        #region Internal Methods

        internal static DateTime FirstDayAndTimeStripper(DateTime dateToStrip)
        {
            dateToStrip = Convert.ToDateTime(dateToStrip.ToShortDateString());
            dateToStrip = dateToStrip.AddDays(-dateToStrip.Day + 1);
            return dateToStrip;
        }

        #endregion
    }
}
