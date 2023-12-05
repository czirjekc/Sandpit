using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcOpenGroup
    {
        #region Properties

        public string Code { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string Report { get; set; }

        #endregion

        #region Public Methods

        public static List<GetSmcOpenGroup> GetData()
        {
            List<GetSmcOpenGroup> ItemList = new List<GetSmcOpenGroup>();

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT Item.Code 'Code', State.Description 'State', AG.Description 'Description', AG.Report 'Report' FROM Helpdesk_ActionGroup AG INNER JOIN Helpdesk_State State ON State.IdState = Ag.IdState INNER JOIN Helpdesk_Item Item ON Item.IdItem = AG.IdItem WHERE (AG.IdState = 1 ) AND AG.IdFunctionalServiceAffected = 70";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            GetSmcOpenGroup itemDAL;

            foreach (DataRow MyRow in MyTable.Rows)
            {
                itemDAL = new GetSmcOpenGroup();
                itemDAL.MapData(MyRow);
                ItemList.Add(itemDAL);
            }

            return ItemList;
        }

        public static List<GetSmcOpenGroup> GetByFunctionalServiceId(int id)
        {
            List<GetSmcOpenGroup> ItemList = new List<GetSmcOpenGroup>();

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT Item.Code 'Code', State.Description 'State', AG.Description 'Description', AG.Report 'Report' FROM Helpdesk_ActionGroup AG INNER JOIN Helpdesk_State State ON State.IdState = Ag.IdState INNER JOIN Helpdesk_Item Item ON Item.IdItem = AG.IdItem WHERE (AG.IdState = 1 ) AND AG.IdFunctionalServiceAffected = " + id.ToString();

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            GetSmcOpenGroup itemDAL;

            foreach (DataRow MyRow in MyTable.Rows)
            {
                itemDAL = new GetSmcOpenGroup();
                itemDAL.MapData(MyRow);
                ItemList.Add(itemDAL);
            }

            return ItemList;
        }

        #endregion

        #region Internal Methods

        internal void MapData(DataRow MyRow)
        {
            Code = MyRow["Code"].ToString();
            State = MyRow["State"].ToString();
            Description = MyRow["Description"].ToString();
            Report = MyRow["Report"].ToString();
        }

        #endregion
    }
}
