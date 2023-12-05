using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class GetSmcFunctionalService
    {
        #region Properties

        public int Id { get; set; }
        public string Code { get; set; }
        public string Valid { get; set; }
                
        #endregion

        #region Public Methods

        public static List<GetSmcFunctionalService> GetAll()
        {
            List<GetSmcFunctionalService> itemDL_List = new List<GetSmcFunctionalService>();

            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter();

            SqlCommand MyCommand = new SqlCommand();
            SqlConnection MyConnection = new SqlConnection();

            MyConnection.ConnectionString = "Data Source=S-OLAF-PROD17;Initial Catalog=GetSMC_SR5;User ID=wimt; Password=zaeE145E5FcZ";
            MyCommand.Connection = MyConnection;
            MyCommand.CommandText = "SELECT IdFunctionalService Id, Code Code, Valid Valid FROM Framework_FunctionalService";

            MyAdapter.SelectCommand = MyCommand;

            MyAdapter.Fill(MyTable);
            MyAdapter.SelectCommand.Connection.Close();
            MyAdapter.Dispose();

            GetSmcFunctionalService itemDL;

            foreach (DataRow MyRow in MyTable.Rows)
            {
                itemDL = new GetSmcFunctionalService();
                itemDL.MapData(MyRow);
                itemDL_List.Add(itemDL);
            }



            return itemDL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(DataRow myRow) 
        {
            Code = myRow["Code"].ToString();
            Valid = myRow["Valid"].ToString();
            Id = (int)myRow["Id"];

        }

        #endregion


    }
}
