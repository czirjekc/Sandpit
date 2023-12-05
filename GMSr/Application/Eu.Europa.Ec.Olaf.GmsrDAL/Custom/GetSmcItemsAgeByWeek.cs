using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcItemsAgeByWeek
    {
        #region Properties

        public int Items { get; set; }
        public int Week { get; set; }

        #endregion
        #region Public Methods

        public static List<GetSmcItemsAgeByWeek> GetLastTwelveMonthsAgesByWeek(DateTime startMonth)
        {
            startMonth = FirstDayAndTimeStripper(startMonth).AddMonths(-11);
            List<GetSmcItemsAgeByWeek> itemDL_List = new List<GetSmcItemsAgeByWeek>();

            GetSmcItemsAgeByWeek item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;
      
            MyCommand.CommandText = "SELECT TOP 7 Count(*) as NumberOfItems, (DATEDIFF(day,item.CreationDate, item.ClosureDate) / 5)as WeeksBetween FROM Helpdesk_item AS Item";
            MyCommand.CommandText += " WHERE Item.ClosureDate IS NOT NULL";
            MyCommand.CommandText += " AND dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(item.CreationDate), item.CreationDate)))) >= '" + startMonth.Year.ToString() + "-" + startMonth.Month.ToString() + "-" + startMonth.Day.ToString() + "'";
            MyCommand.CommandText += " GROUP BY (DATEDIFF(day,item.CreationDate, item.ClosureDate) / 5)";
            MyCommand.CommandText += " ORDER BY WeeksBetween";


            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcItemsAgeByWeek();
                item.MapData(MyRow);
                itemDL_List.Add(item);
            }


            return itemDL_List;
        }

        #endregion
        #region Internal Methods
        internal void MapData(DataRow MyRow)
        {
            Items = (int)MyRow["NumberOfItems"];
            Week = (int)MyRow["WeeksBetween"];
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
