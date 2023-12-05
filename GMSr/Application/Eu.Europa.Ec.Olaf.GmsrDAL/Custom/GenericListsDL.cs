using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class NameDL
    {
        #region Properties

        public string Title { get; set; }

        #endregion

        #region Internal Methods

        internal void MapDataFromDataRow(DataRow dataRow)
        {
            Title = dataRow["Name"].ToString();
        }

        #endregion
    }

    public class NameAndTotalDL
    {
        #region Properties

        public string Title { get; set; }
        public Int32 Total { get; set; }

        #endregion

        #region Internal Methods

        internal new void MapDataFromDataRow(DataRow dataRow)
        {
            Title = dataRow["Name"].ToString();
            Total = (Int32)dataRow["Total"];
        }

        #endregion
    }
    public class NameAndTotalAndDateDL
    {
        #region Properties

        public DateTime Date { get; set; }
        public string Title { get; set; }
        public Int32 Total { get; set; }

        #endregion

        #region Internal Methods

        internal new void MapDataFromDataRow(DataRow dataRow)
        {
            Title = dataRow["Name"].ToString();
            Total = (Int32)dataRow["Total"];
            Date = (DateTime)dataRow["Date"];
        }

        #endregion
        
    }

    public class DateAndTotalDL
    {
        #region Properties

        public DateTime Date { get; set; }
        public Int32 Total { get; set; }

        #endregion

        #region Internal Methods

        internal new void MapDataFromDataRow(DataRow dataRow)
        {
            Date = (DateTime)dataRow["Date"];
            Total = (Int32)dataRow["Total"];
        }

        #endregion

    }
}
