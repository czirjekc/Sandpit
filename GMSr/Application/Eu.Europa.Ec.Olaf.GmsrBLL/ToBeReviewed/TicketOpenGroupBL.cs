using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SmtOpenGroupBL
    {
        #region Properties

        public string Code { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string Report { get; set; }

        #endregion

        #region Public Methods

        public static List<SmtOpenGroupBL> GetAll()
        {
            List<SmtOpenGroupBL> itemBL_List = new List<SmtOpenGroupBL>();
  
            SmtOpenGroupBL itemBL;
            GmsrDAL.Custom.GetSmcOpenGroup.GetData().ForEach(item =>
            {
                itemBL = new SmtOpenGroupBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }
        public static List<SmtOpenGroupBL> GetByGroupId(int id)
        {
            List<SmtOpenGroupBL> itemBL_List = new List<SmtOpenGroupBL>();

            SmtOpenGroupBL itemBL;
            GmsrDAL.Custom.GetSmcOpenGroup.GetByFunctionalServiceId(id).ForEach(item =>
            {
                itemBL = new SmtOpenGroupBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(GmsrDAL.Custom.GetSmcOpenGroup item)
        {
            Code = item.Code;
            State = item.State;
            Description = item.Description;
            Report = item.Report;
        }

        #endregion
    }
}
