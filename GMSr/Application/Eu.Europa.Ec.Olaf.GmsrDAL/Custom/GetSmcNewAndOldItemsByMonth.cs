using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcNewAndOldItemsByMonth
    {
        #region Properties
        public DateTime Month { get; set; }
        public int New { get; set; }
        public int Old { get; set; }
        #endregion 

        #region Public Methods
        public static List<GetSmcNewAndOldItemsByMonth> GetNewAndOldItemsByMonth(DateTime startMonth)
        {
            startMonth = FirstDayAndTimeStripper(startMonth).AddMonths(-11);
            List<GetSmcNewAndOldItemsByMonth> itemDL_List = new List<GetSmcNewAndOldItemsByMonth>();

            GetSmcNewAndOldItemsByMonth item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = " SELECT Months.Months, ISNULL(Old.Items, 0) Old, ISNULL(New.Items, 0) New FROM";
            MyCommand.CommandText += " (SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate)))) [Months] FROM Helpdesk_Item Item";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate))))";
            MyCommand.CommandText += " ) As Months LEFT JOIN";
            MyCommand.CommandText += " (SELECT COUNT(*) Items, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.ClosureDate), Item.ClosureDate)))) Months FROM Helpdesk_Item Item";
            MyCommand.CommandText += " WHERE dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) <> dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.ClosureDate), Item.ClosureDate))))";
            MyCommand.CommandText += " AND Item.ClosureDate IS NOT NULL";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.ClosureDate), Item.ClosureDate))))";
            MyCommand.CommandText += " )  As Old ON Old.Months = Months.Months";
            MyCommand.CommandText += " LEFT JOIN";
            MyCommand.CommandText += " (SELECT COUNT(*) Items, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.ClosureDate), Item.ClosureDate)))) Months FROM Helpdesk_Item Item";
            MyCommand.CommandText += " WHERE dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) = dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.ClosureDate), Item.ClosureDate))))";
            MyCommand.CommandText += " AND Item.ClosureDate IS NOT NULL";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.ClosureDate), Item.ClosureDate))))";
            MyCommand.CommandText += " )As New ON New.Months = Months.Months";



            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcNewAndOldItemsByMonth();
                item.MapData(MyRow);
                itemDL_List.Add(item);
            }


            return itemDL_List;
        }
        #endregion

        #region Internal Methods
        internal void MapData(DataRow MyRow)
        {
            Month = (DateTime)MyRow["Months"];
            Old = (int)MyRow["Old"];
            New = (int)MyRow["New"];
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
