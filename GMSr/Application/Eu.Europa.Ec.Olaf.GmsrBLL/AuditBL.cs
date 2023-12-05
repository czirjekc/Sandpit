using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;
using System.Xml.Linq;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class AuditBL : BaseBL
    {
        #region Properties

        public string Guid { get; set; }
        public DateTime? Timestamp { get; set; }
        public string EntitySet { get; set; }
        public string UserLogin { get; set; }
        public string Action { get; set; }
        public string OldDataXml { get; set; }
        public string NewDataXml { get; set; }
        public string ChangedPropertiesXml { get; set; }

        public List<NameValuePairBL> OldValues
        {
            get
            {
                return GetValuesByXml(OldDataXml);
            }
        }

        public List<NameValuePairBL> NewValues
        {
            get
            {
                return GetValuesByXml(NewDataXml);
            }
        }

        public List<NameValuePairBL> ChangedProperties
        {
            get
            {
                return GetValuesByXml(ChangedPropertiesXml);
            }
        }

        #endregion

        #region Public Methods

        public static List<AuditBL> GetByParameters(DateTime? timestampFrom,
                                                    DateTime? timestampTo,
                                                    string entitySet,
                                                    string userLogin,
                                                    string action
                                                   )
        {
            List<AuditBL> itemBL_List = new List<AuditBL>();
            List<Audit> itemList = new List<Audit>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<Audit>(x =>
                                                    (timestampFrom == null || x.RevisionStamp >= timestampFrom) &&
                                                    (timestampTo == null || x.RevisionStamp <= timestampTo) &&
                                                    (entitySet == "" || x.TableName.Contains(entitySet)) &&
                                                    (userLogin == "" || x.UserLogin.Contains(userLogin)) &&
                                                    (action == "" || x.Action.Contains(action.Substring(0, 1)))
                                                  ).ToList();

                itemList = itemList.OrderByDescending(x => x.RevisionStamp).ToList();

                AuditBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new AuditBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static AuditBL GetByGuid(string guid)
        {
            AuditBL itemBL = new AuditBL();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                Audit item = repository.Get<Audit>(x => x.Id == guid);
                itemBL.MapData(item);
            }

            return itemBL;
        }

        #endregion

        #region Internal Methods

        internal void MapData(Audit item)
        {
            Guid = item.Id;
            Timestamp = item.RevisionStamp;
            EntitySet = item.TableName;
            UserLogin = item.UserLogin;
            if (item.Action == "U")
                Action = "Update";
            else if (item.Action == "I")
                Action = "Insert";
            else if (item.Action == "D")
                Action = "Delete";
            OldDataXml = item.OldData;
            NewDataXml = item.NewData;
            ChangedPropertiesXml = item.ChangedColumns;
        }

        #endregion

        #region Private Methods

        private List<NameValuePairBL> GetValuesByXml(string xml)
        {
            if (xml == null)
            {
                return null;
            }
            else
            {
                List<NameValuePairBL> nameValuePairList = new List<NameValuePairBL>();

                // Load XML document
                XDocument doc = XDocument.Parse(xml);

                // Get all nodes of the root node
                IEnumerable<XNode> nodes =
                    from xmlNode in doc.Root.Nodes()
                    select xmlNode;

                NameValuePairBL nameValuePair;
                foreach (XNode node in nodes)
                {
                    XElement element = (XElement)node;

                    if (element.HasElements)
                    {
                        //
                    }
                    else
                    {
                        nameValuePair = new NameValuePairBL();
                        nameValuePair.Name = element.Name.ToString();
                        nameValuePair.Value = element.Value;

                        nameValuePairList.Add(nameValuePair);
                    }
                }
                return nameValuePairList;
            }
        }

        #endregion
    }
}