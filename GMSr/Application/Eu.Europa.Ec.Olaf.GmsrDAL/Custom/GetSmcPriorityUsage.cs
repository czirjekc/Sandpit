using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcPriorityUsage
    {
        #region Properties

        public Int32 Items { get; set; }
        public string PriorityName { get; set; }

        #endregion

        #region Public Methods

        public static List<GetSmcPriorityUsage> GetPriorityUsageByMonth(DateTime month)
        {
            List<GetSmcPriorityUsage> itemDL_List = new List<GetSmcPriorityUsage>();
            GetSmcPriorityUsage item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT COUNT(*) AS 'Items', dbo.Helpdesk_Priority.Description 'Priority' FROM dbo.Helpdesk_Item item INNER JOIN dbo.Helpdesk_Priority ON item.IdPriority = dbo.Helpdesk_Priority.IdPriority WHERE dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(item.CreationDate), item.CreationDate)))) = '" + FirstDayAndTimeStripper(month).Year.ToString() + "-" + FirstDayAndTimeStripper(month).Month.ToString() + "-" + FirstDayAndTimeStripper(month).Day.ToString() + "' GROUP BY dbo.Helpdesk_Priority.Description";
            


            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcPriorityUsage();
                item.MapData(MyRow);
                itemDL_List.Add(item);
            }


            return itemDL_List;

        }

        #endregion

        #region Internal Methods

        internal void MapData(DataRow MyRow)
        {
            PriorityName = MyRow["Priority"].ToString();
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
