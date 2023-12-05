using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcItemCreation
    {
        #region Properties

        public DateTime Month { get; set; }
        public int Items { get; set; }

        #endregion

        #region Public Methods

        public static List<GetSmcItemCreation> GetAllByMonth()
        {
            
            List<GetSmcItemCreation> itemDL_List = new List<GetSmcItemCreation>();
            GetSmcItemCreation item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Items.CreationDate), Items.CreationDate)))) [Month], COUNT(*) 'Items' FROM Helpdesk_Item Items GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Items.CreationDate), Items.CreationDate)))) ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Items.CreationDate), Items.CreationDate))))";
         

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcItemCreation();
                item.MapData(MyRow);
                itemDL_List.Add(item);
            }


            return itemDL_List;

        }
                
        #endregion

        #region Internal Methods

        internal void MapData(DataRow MyRow)
        {
            Month = Convert.ToDateTime(MyRow["Month"]);
            Items = (Int32)MyRow["Items"];
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
