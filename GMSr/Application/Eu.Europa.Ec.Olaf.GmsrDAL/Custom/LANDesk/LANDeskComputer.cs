using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class LANDeskComputer
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region Public Methods

        public static List<LANDeskComputer> GetByGroupId(int Id)
        {
            List<LANDeskComputer> itemDL_List = new List<LANDeskComputer>();
            LANDeskComputer itemDL;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();


            MyConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LANDesk"].ConnectionString;
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT DeviceName Name FROM Computer INNER JOIN CustomGroupComputer Link ON Link.Member_Idn = Computer.Computer_Idn WHERE Link.CustomGroup_Idn = " + Id.ToString() + " GROUP BY DeviceName";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            foreach (DataRow MyRow in MyTable.Rows)
            {
                itemDL = new LANDeskComputer();
                itemDL.MapData(MyRow);
                itemDL_List.Add(itemDL);
            }


            return itemDL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(DataRow dataRow)         
        {
            Name = dataRow["Name"].ToString();
        }

        #endregion

    }
}
