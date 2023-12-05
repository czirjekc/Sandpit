using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcGeneric
    {
        #region Public Methods

        public static List<NameAndTotalDL> GetNet1IncidentNaturesFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalDL> itemList = new List<NameAndTotalDL>();
            NameAndTotalDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;


            MyCommand.CommandText = " SELECT Nature.ArtemisDescription as Name, Count(*) as Total  FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription NOT LIKE '%cbis%' and ItemType.Description LIKE 'Incident' And ATQ.Description LIKE 'incident'";
            MyCommand.CommandText += " GROUP BY Nature.ArtemisDescription";
            MyCommand.CommandText += " ORDER BY Count(*) DESC";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            foreach (DataRow row in MyTable.Rows)
            {
                item = new NameAndTotalDL();
                item.MapDataFromDataRow(row);
                itemList.Add(item);
            }


            return itemList;
        }

        public static List<NameAndTotalDL> GetCbisIncidentNaturesFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalDL> itemList = new List<NameAndTotalDL>();
            NameAndTotalDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;


            MyCommand.CommandText = " SELECT Nature.ArtemisDescription as Name, Count(*) as Total  FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription LIKE '%cbis%' and ItemType.Description LIKE 'Incident' And ATQ.Description LIKE 'incident'";
            MyCommand.CommandText += " GROUP BY Nature.ArtemisDescription";
            MyCommand.CommandText += " ORDER BY Count(*) DESC";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            foreach (DataRow row in MyTable.Rows)
            {
                item = new NameAndTotalDL();
                item.MapDataFromDataRow(row);
                itemList.Add(item);
            }


            return itemList;
        }

        public static List<NameAndTotalAndDateDL> GetCbisIncidentNaturesByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalAndDateDL> itemList = new List<NameAndTotalAndDateDL>();
            NameAndTotalAndDateDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as [Date], COUNT(*) as Total, Nature.ArtemisDescription as Name";
            MyCommand.CommandText += " FROM Helpdesk_Item Item ";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription LIKE '%cbis%' and ItemType.Description LIKE 'Incident' And ATQ.Description LIKE 'incident'";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))), Nature.ArtemisDescription";
            MyCommand.CommandText += " ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new NameAndTotalAndDateDL();
                item.MapDataFromDataRow(x);
                itemList.Add(item);
            });

            return itemList;
        }

        public static List<NameAndTotalAndDateDL> GetNet1IncidentNaturesByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalAndDateDL> itemList = new List<NameAndTotalAndDateDL>();
            NameAndTotalAndDateDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as [Date], COUNT(*) as Total, Nature.ArtemisDescription as Name";
            MyCommand.CommandText += " FROM Helpdesk_Item Item ";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription NOT LIKE '%cbis%' and ItemType.Description LIKE 'Incident' And ATQ.Description LIKE 'incident'";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))), Nature.ArtemisDescription";
            MyCommand.CommandText += " ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new NameAndTotalAndDateDL();
                item.MapDataFromDataRow(x);
                itemList.Add(item);
            });

            return itemList;
        }

        public static List<DateAndTotalDL> GetNet1IncidentItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalDL> itemList = new List<DateAndTotalDL>();
            DateAndTotalDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as [Date], COUNT(*) as Total";
            MyCommand.CommandText += " FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription NOT LIKE '%cbis%' and ItemType.Description LIKE 'Incident' And ATQ.Description LIKE 'incident'";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";
            MyCommand.CommandText += " ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new DateAndTotalDL();
                item.MapDataFromDataRow(x);
                itemList.Add(item);
            }
            );


            return itemList;
        }

        public static List<DateAndTotalDL> GetCbisIncidentItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalDL> itemList = new List<DateAndTotalDL>();
            DateAndTotalDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as [Date], COUNT(*) as Total";
            MyCommand.CommandText += " FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription LIKE '%cbis%' and ItemType.Description LIKE 'Incident' And ATQ.Description LIKE 'incident'";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";
            MyCommand.CommandText += " ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new DateAndTotalDL();
                item.MapDataFromDataRow(x);
                itemList.Add(item);
            }
            );

            return itemList;
        }

        public static List<DateAndTotalDL> GetNet1ItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalDL> itemList = new List<DateAndTotalDL>();
            DateAndTotalDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as [Date], COUNT(*) as Total";
            MyCommand.CommandText += " FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription NOT LIKE '%cbis%'";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";
            MyCommand.CommandText += " ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new DateAndTotalDL();
                item.MapDataFromDataRow(x);
                itemList.Add(item);
            }
            );

            return itemList;
        }

        public static List<DateAndTotalDL> GetCbisItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalDL> itemList = new List<DateAndTotalDL>();
            DateAndTotalDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as [Date], COUNT(*) as Total";
            MyCommand.CommandText += " FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_ItemType ItemType on ItemType.IdItemType = Item.idItemType";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Project Project ON ProjecT.IdProject = Item.Idproject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription LIKE '%cbis%'";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";
            MyCommand.CommandText += " ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new DateAndTotalDL();
                item.MapDataFromDataRow(x);
                itemList.Add(item);
            }
            );

            return itemList;
        }

        public static List<DateAndTotalDL> GetRemoteSupportItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalDL> itemList = new List<DateAndTotalDL>();
            DateAndTotalDL item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate)))) as [Date], COUNT(*) as Total";
            MyCommand.CommandText += " FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_Project Project on Project.IdProject = Item.IdProject";
            MyCommand.CommandText += " WHERE Item.CreationDate > '" + startDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "' AND Item.CreationDate < '" + endDate.GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[93] + "'";
            MyCommand.CommandText += " AND Project.ArtemisDescription LIKE '%/O/ OIT1 - Remote Support%'";
            MyCommand.CommandText += " GROUP BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";
            MyCommand.CommandText += " ORDER BY dateadd(dd,0, datediff(dd,0,(DATEADD(DD, 1 - DAY(Item.CreationDate), Item.CreationDate))))";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new DateAndTotalDL();
                item.MapDataFromDataRow(x);
                itemList.Add(item);
            }
            );

            return itemList;
        }

        #endregion

    }

    public class GetSmcRemoteSupportItem
    {
        #region Properties

        public DateTime CreationDate { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Nature { get; set; }
        public string Qualification { get; set; }

        #endregion

        #region Public Methods 

        public static List<GetSmcRemoteSupportItem> GetAll() 
        {
            List<GetSmcRemoteSupportItem> itemList = new List<GetSmcRemoteSupportItem>();
            GetSmcRemoteSupportItem item;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;

            MyCommand.CommandText = "SELECT Item.CreationDate CreationDate, Item.Code Code, Item.Title Title, ATQ.ArtemisDescription Qualification, Nature.ArtemisDescription Nature";
            MyCommand.CommandText += " FROM Helpdesk_Item Item";
            MyCommand.CommandText += " INNER JOIN Helpdesk_Project Project on Project.IdProject= Item.IdProject";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_Nature Nature ON Nature.IdNature = Item.idNature";
            MyCommand.CommandText += " LEFT JOIN Helpdesk_AnnexTableQUalification ATQ ON ATQ.id = Item.IdAnnexTableQualification";
            MyCommand.CommandText += " WHERE Project.ArtemisDescription LIKE '%/O/ OIT1 - Remote Support%'";
            
            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            MyTable.AsEnumerable().ToList().ForEach(x =>
            {
                item = new GetSmcRemoteSupportItem();
                item.MapData(x);
                itemList.Add(item);
            }
            );

            return itemList;

      
        }

        #endregion

        #region Internal Methods

        internal void MapData(DataRow dataRow) 
        {
            CreationDate = DateTime.Parse(dataRow["CreationDate"].ToString());
            Code = dataRow["Code"].ToString();
            Title = dataRow["Title"].ToString();
            Nature = dataRow["Nature"].ToString();
            Qualification = dataRow["Qualification"].ToString();
        }

        #endregion
    }
}
