using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class NameBL
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

    public class NameAndTotalBL
    {
        #region Properties

        public string Title { get; set; }
        public Int32 Total { get; set; }

        #endregion

        #region Internal Methods

        internal void MapDataFromDataRow(DataRow dataRow)
        {
            Title = dataRow["Name"].ToString();
            Total = (Int32)dataRow["Total"];
        }

        internal void MapDataFromNameAndTotalDL(NameAndTotalDL nameAndTotalDL)
        {
            Title = nameAndTotalDL.Title;
            Total = nameAndTotalDL.Total;
        }

        #endregion
    }
    public class NameAndTotalAndDateBL
    {
        #region Properties

        public DateTime Date { get; set; }
        public string Title { get; set; }
        public Int32 Total { get; set; }

        #endregion

        #region Internal Methods

        internal void MapDataFromDataRow(DataRow dataRow)
        {
            Title = dataRow["Name"].ToString();
            Total = (Int32)dataRow["Total"];
            Date = (DateTime)dataRow["Date"];
        }

        internal void MapDataFromNameAndTotalAndDateDL(NameAndTotalAndDateDL nameAndTotalAndDateDL)
        {
            Title = nameAndTotalAndDateDL.Title;
            Total = nameAndTotalAndDateDL.Total;
            Date = nameAndTotalAndDateDL.Date;
        }

        #endregion
    }
    public class DateAndTotalBL
    {
        #region Properties

        public DateTime Date { get; set; }
        public Int32 Total { get; set; }

        #endregion

        #region Internal Methods

        internal void MapDataFromDateAndTotalDL(DateAndTotalDL dateAndTotalDL) 
        {
            Date = dateAndTotalDL.Date;
            Total = dateAndTotalDL.Total;
        }

        #endregion
    }
}
