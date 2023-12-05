using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Eu.Europa.Ec.Olaf.GmsrDAL.Custom
{
    public class PrtgStorageItem
    {
        public int VhdObjectId { get; set; }
        public string VhdVhdPath { get; set; }
        public string VhdVhdSize { get; set; }
        public string VhdCurrentNode { get; set; }

        public int VolumeObjectId { get; set; }
        public string VolumeLunId { get; set; }
        public string VolumeVolumeName { get; set; }
        public string VolumeClusterName { get; set; }

        public int LunObjectId { get; set; }
        public string LunLunId { get; set; }
        public string LunLunSize { get; set; }
        public string LunLunPath { get; set; }
        public string LunLunVolumeName { get; set; }
        public string LunLunVolumeUsedSpace { get; set; }
        public string LunLunVolumeSize { get; set; }
        public string LunAggregateName { get; set; }
        public string LunAggregateUsedSpace { get; set; }
        public string LunAggregateSize { get; set; }
        public string LunSanName { get; set; }

        public static List<PrtgStorageItem> GetAll()
        {
            GmsrEntities GmsrDc = new GmsrEntities();
            PrtgStorageItem item;
            List<PrtgStorageItem> itemList = new List<PrtgStorageItem>();
            XDocument doc;
            GmsrDc.PrtgReportConfigSet.Where(x => x.Type == "Vhd").ToList().ForEach(x =>
            {
                item = new PrtgStorageItem();
                item.VhdObjectId = x.ObjectId;
                doc = XDocument.Load("http://158.166.176.171/api/getsensordetails.xml?id=" + item.VhdObjectId.ToString() + "&username=lsa&password=Prtgmonitoring");
                item.VhdVhdPath = doc.Root.Element("lastmessage").Value;
                item.VhdVhdSize = doc.Root.Element("lastvalue").Value;
                itemList.Add(item);
            });

            GmsrDc.PrtgReportConfigSet.Where(x => x.Type == "Volume").ToList().ForEach(x =>
            {
                doc = XDocument.Load("http://158.166.176.171/api/getsensordetails.xml?id=" + x.ObjectId.ToString() + "&username=lsa&password=Prtgmonitoring");

                itemList.Where(y => y.VhdVhdPath.ToLower().Contains("\\" + doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("volume=")).First().Split('=')[1].ToLower() + "\\")).ToList().ForEach(y =>
                {
                    y.VolumeObjectId = x.ObjectId;
                    y.VolumeLunId = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("lunid=")).First().Split('=')[1];
                    y.VolumeClusterName = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("Name=")).First().Split('=')[1];
                    y.VolumeVolumeName = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("volume=")).First().Split('=')[1];
                });
            });

            GmsrDc.PrtgReportConfigSet.Where(x => x.Type == "Lun").ToList().ForEach(x =>
            {
                doc = XDocument.Load("http://158.166.176.171/api/getsensordetails.xml?id=" + x.ObjectId.ToString() + "&username=lsa&password=Prtgmonitoring");

                itemList.Where(y => y.VolumeLunId == doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("lunid=")).First().Split('=')[1]).ToList().ForEach(y =>
                {
                    y.LunObjectId = x.ObjectId;
                    y.LunLunId = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("lunid=")).First().Split('=')[1];
                    y.LunLunSize = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("lunsize=")).First().Split('=')[1];
                    y.LunLunPath = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("path=")).First().Split('=')[1];
                    y.LunLunVolumeName = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("volname=")).First().Split('=')[1];
                    y.LunLunVolumeSize = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("VolSize=")).First().Split('=')[1];
                    y.LunLunVolumeUsedSpace = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("VolUsed=")).First().Split('=')[1];
                    y.LunAggregateName = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("aggregate=")).First().Split('=')[1];
                    y.LunAggregateSize = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("aggrSize =")).First().Split('=')[1];
                    y.LunAggregateUsedSpace = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("aggrUsed=")).First().Split('=')[1];
                    y.LunSanName = doc.Root.Element("lastmessage").Value.Split(';').ToList().Where(z => z.Contains("SanName=")).First().Split('=')[1];
                });
            });
            return itemList;
        }
    }
    public class PrtgOccupationAndAvailability 
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

        public static List<PrtgOccupationAndAvailability> GetAll() 
        {
            List<PrtgOccupationAndAvailability> itemList = new List<PrtgOccupationAndAvailability>();
            PrtgOccupationAndAvailability item;
            XDocument doc;
            doc = XDocument.Load("http://158.166.176.171/api/table.xml?id=3753&content=sensors&columns=objid,group,device,sensor,status,message,lastvalue,priority,favorite&username=lsa&password=Prtgmonitoring");

            doc.Root.Elements("item").ToList().ForEach(x => {
                item = new PrtgOccupationAndAvailability();
                item.ObjId = x.Element("objid") == null ? "" : x.Element("objid").Value;
                item.Group = x.Element("group") == null ? "" : x.Element("group").Value;
                item.Device = x.Element("device") == null ? "" : x.Element("device").Value;
                item.Sensor = x.Element("sensor") == null ? "" : x.Element("sensor").Value;
                item.Status = x.Element("status") == null ? "" : x.Element("status").Value;
                item.Message = x.Element("message") == null ? "" : x.Element("message").Value;
                item.LastValue = x.Element("lastvalue") == null ? "" : x.Element("lastvalue").Value;
                item.Priority = x.Element("priority") == null ? "" : x.Element("priority").Value;
                itemList.Add(item);

            });

            return itemList;
        }

        #endregion

        
    }
}
