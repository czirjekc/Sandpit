using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class PrtgLunSpace
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
        #region Public methods
        public static List<PrtgLunSpace> GetData()
        {
            string username = System.Threading.Thread.CurrentPrincipal.Identity.Name.Replace("\\", ".");
            List<PrtgLunSpace> itemDL_list = new List<PrtgLunSpace>();
            PrtgLunSpace itemBL;

            System.Net.WebClient MyWC = new System.Net.WebClient();
            MyWC.DownloadFile("http://158.166.176.171/api/table.xml?id=3753&content=sensors&columns=objid,group,device,sensor,status,message,lastvalue,priority,favorite&username=lsa&password=Prtgmonitoring", System.Web.HttpContext.Current.Server.MapPath("~") + "\\Temp\\PRTGLUNSPACE_" + username + ".xml");
            var s = (from sensors in XElement.Load(System.Web.HttpContext.Current.Server.MapPath("~") + "\\Temp\\PRTGLUNSPACE_"+ username+".xml").Elements("item") select sensors).ToList();
            s.ForEach(sensor =>
            {
                itemBL = new PrtgLunSpace();
                itemBL.MapData(sensor);
                itemDL_list.Add(itemBL);
            });

            return itemDL_list;

        }

        #endregion
        #region Internal methods
        internal void MapData(XElement item)
        {
            ObjId = item.Element("objid").Value;
            Group = item.Element("group").Value;
            Device = item.Element("device").Value;
            Sensor = item.Element("sensor").Value;
            Status = item.Element("status").Value;
            Message = item.Element("message_raw").Value;
            LastValue = item.Element("lastvalue").Value;
            Priority = item.Element("priority").Value;

        }
        #endregion

    }
}
