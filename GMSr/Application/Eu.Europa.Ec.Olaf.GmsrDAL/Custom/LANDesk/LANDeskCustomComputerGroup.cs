using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class LANDeskCustomComputerGroup
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region Public Methods

        public static List<LANDeskCustomComputerGroup> GetAll() 
        {
            List<LANDeskCustomComputerGroup> itemDL_List = new List<LANDeskCustomComputerGroup>();
            LANDeskCustomComputerGroup itemDL;

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();


            MyConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LANDesk"].ConnectionString;
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT CustomGroup.CustomGroup_Idn Id, CustomGroup.Name Name FROM CustomGroup INNER JOIN CustomGroupComputer ON CustomGroup.CustomGroup_Idn = CustomGroupComputer.CustomGroup_Idn GROUP BY CustomGroup.Name, CustomGroup.CustomGroup_Idn ORDER BY CustomGroup.Name";

            

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            foreach (DataRow MyRow in MyTable.Rows)
            {
                itemDL = new LANDeskCustomComputerGroup();
                itemDL.MapData(MyRow);
                itemDL_List.Add(itemDL);
            }


            return itemDL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(DataRow myRow)
        {
            Name = myRow["Name"].ToString();
            Id = (int)myRow["Id"];
        }

        #endregion
    }
}
