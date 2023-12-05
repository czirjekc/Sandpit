using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class PrtgLunSpaceItemBL
    {
        #region Properties

        public string ObjId { get; set; }
        public string Group { get; set; }
        public string Device { get; set; }
        public string Sensor { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string LastValue { get; set; }
        public string Priority { get; set; }

        #endregion

        #region Public Methods

        public static List<PrtgLunSpaceItemBL> GetAll()
        {
            List<PrtgLunSpaceItemBL> itemBL_List = new List<PrtgLunSpaceItemBL>();
            PrtgLunSpaceItemBL itemBL;

            GmsrDAL.Custom.PrtgLunSpace.GetData().ForEach(item =>
            {
                itemBL = new PrtgLunSpaceItemBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);

            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods
        internal void MapData(GmsrDAL.Custom.PrtgLunSpace item)
        {
            ObjId = item.ObjId;
            Group = item.Group;
            Device = item.Device;
            Sensor = item.Sensor;
            Status = item.Status;
            Message = item.Message;
            LastValue = item.LastValue;
            Priority = item.Priority;
        }

        #endregion
    }
}