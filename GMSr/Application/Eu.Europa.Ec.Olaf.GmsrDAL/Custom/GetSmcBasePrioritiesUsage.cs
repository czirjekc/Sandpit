using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcBasePrioritiesUsage
    {
        #region Properties
        public DateTime Month { get; set; }
        public int NormalPriority { get; set; }
        public int HighPriority { get; set; }
        public int VeryHighPriority { get; set; }
        public int CriticalPriority { get; set; }
        #endregion

        #region Public Methods

        public static List<GetSmcBasePrioritiesUsage> GetBasePrioritiesUsageByMonth(DateTime month)
        {
            month = FirstDayAndTimeStripper(month);
            List<GetSmcBasePrioritiesUsage> itemDL_List = new List<GetSmcBasePrioritiesUsage>();

            GetSmcBasePrioritiesUsage item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY('" + month.Year.ToString() + "-" + month.Month.ToString() + "-" + month.Day.ToString() + "'), '" + month.Year.ToString() + "-" + month.Month.ToString() + "-" + month.Day.ToString() + "')))) as [Month],";
            MyCommand.CommandText += "(SELECT COUNT(*) FROM Helpdesk_Item WHERE IdPriority = 6 AND dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate)))) = '" + month.Year.ToString() + "-" + month.Month.ToString() + "-" + month.Day.ToString() + "') as NormalPriority,";
            MyCommand.CommandText += "(SELECT COUNT(*) FROM Helpdesk_Item WHERE IdPriority = 26 AND dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate)))) = '" + month.Year.ToString() + "-" + month.Month.ToString() + "-" + month.Day.ToString() + "') as HighPriority,";
            MyCommand.CommandText += "(SELECT COUNT(*) FROM Helpdesk_Item WHERE IdPriority = 5 AND dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate)))) = '" + month.Year.ToString() + "-" + month.Month.ToString() + "-" + month.Day.ToString() + "') as VeryHighPriority,";
            MyCommand.CommandText += "(SELECT COUNT(*) FROM Helpdesk_Item WHERE IdPriority = 4 AND dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate)))) = '" + month.Year.ToString() + "-" + month.Month.ToString() + "-" + month.Day.ToString() + "') as CriticalPriority";



            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcBasePrioritiesUsage();
                item.MapData(MyRow);
                itemDL_List.Add(item);
            }


            return itemDL_List;
        }

        public static List<GetSmcBasePrioritiesUsage> GetAll()
        {
            List<GetSmcBasePrioritiesUsage> itemDL_List = new List<GetSmcBasePrioritiesUsage>();

            GetSmcBasePrioritiesUsage item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = " SELECT Months.Months, ISNULL(NP.NP, 0) NormalPriority, ISNULL(HP.HP, 0) HighPriority, ISNULL(VHP.VHP, 0) VeryHighPriority, ISNULL(CP.CP,0) CriticalPriority FROM (";
            MyCommand.CommandText += " SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate)))) [Months] FROM Helpdesk_Item Item";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(CreationDate), CreationDate))))";
            MyCommand.CommandText += " ) As Months";
            MyCommand.CommandText += " LEFT JOIN (SELECT COUNT(*) as NP, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as Months FROM Helpdesk_Item Item WHERE Item.IdPriority=6 GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))) As NP";
            MyCommand.CommandText += " ON NP.Months = Months.Months";
            MyCommand.CommandText += " LEFT JOIN (SELECT COUNT(*) as HP, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as Months FROM Helpdesk_Item Item WHERE Item.IdPriority=26 GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))) As HP";
            MyCommand.CommandText += " ON HP.Months = Months.Months";
            MyCommand.CommandText += " LEFT JOIN (SELECT COUNT(*) as VHP, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as Months FROM Helpdesk_Item Item WHERE Item.IdPriority=5 GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))) As VHP";
            MyCommand.CommandText += " ON VHP.Months = Months.Months";
            MyCommand.CommandText += " LEFT JOIN (SELECT COUNT(*) as CP, dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as Months FROM Helpdesk_Item Item WHERE Item.IdPriority=4 GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))) As CP";
            MyCommand.CommandText += " ON CP.Months = Months.Months";
            MyCommand.CommandText += " ORDER BY Months.Months";


            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();



            foreach (DataRow MyRow in MyTable.Rows)
            {
                item = new GetSmcBasePrioritiesUsage();
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
            NormalPriority = (int)MyRow["NormalPriority"];
            HighPriority = (int)MyRow["HighPriority"];
            VeryHighPriority = (int)MyRow["VeryHighPriority"];
            CriticalPriority = (int)MyRow["CriticalPriority"];
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
