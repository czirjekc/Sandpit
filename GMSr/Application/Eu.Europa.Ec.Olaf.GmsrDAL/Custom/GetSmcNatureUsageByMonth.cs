using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcNatureUsageByMonth
    {
        #region Properties

        public Int32 Items { get; set; }
        public string NatureName { get; set; }
        public DateTime Month { get; set; }

        #endregion

        #region Public Methods

        public static List<GetSmcNatureUsageByMonth> GetTopTenNatures(DateTime month)
        {
            month = FirstDayAndTimeStripper(month);
            List<GetSmcNatureUsageByMonth> itemDL_List = new List<GetSmcNatureUsageByMonth>();
            GetSmcNatureUsageByMonth item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT TOP 10 count(*) Items, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) Months, Nature.ArtemisDescription Nature FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.IdNature";
            MyCommand.CommandText += " WHERE dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) = '" + month.GetDateTimeFormats()[68] + "'";
            MyCommand.CommandText += " GROUP BY ";
            MyCommand.CommandText += " dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))), Nature.ArtemisDescription";
            MyCommand.CommandText += " ORDER BY COUNT(*) DESC, Nature.ArtemisDescription";





            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcNatureUsageByMonth();
                item.MapData(MyRow);
                itemDL_List.Add(item);
            }


            return itemDL_List;

        }

        public static List<GetSmcNatureUsageByMonth> GetMiscNatures(DateTime month)
        {
            List<GetSmcNatureUsageByMonth> itemDL_List = new List<GetSmcNatureUsageByMonth>();
            GetSmcNatureUsageByMonth item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT COUNT(*) Items, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) Months, Nature.ArtemisDescription Nature FROM Helpdesk_Item Item ";
            MyCommand.CommandText += "INNER JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.IdNature ";
            MyCommand.CommandText += "WHERE Nature.ArtemisDescription LIKE '%misc%' ";
            MyCommand.CommandText += "GROUP BY ";
            MyCommand.CommandText += "dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))), Nature.ArtemisDescription ";
            MyCommand.CommandText += "ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))), Nature.ArtemisDescription ";



            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcNatureUsageByMonth();
                item.MapData(MyRow);
                itemDL_List.Add(item);
            }


            return itemDL_List;

        }

        #endregion

        #region Internal Methods

        internal void MapData(DataRow MyRow)
        {
            NatureName = MyRow["Nature"].ToString();
            Items = (Int32)MyRow["Items"];
            Month = (DateTime)MyRow["Months"];
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
