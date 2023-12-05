using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;


namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class WebEventBL : BaseBL
    {
        #region Properties

        public string Guid { get; set; }
        public DateTime TimeUtc { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public decimal Sequence { get; set; }
        public decimal Occurrence { get; set; }
        public int Code { get; set; }
        public int DetailCode { get; set; }
        public string Message { get; set; }
        public string ApplicationPath { get; set; }
        public string ApplicationVirtualPath { get; set; }
        public string MachineName { get; set; }
        public string RequestUrl { get; set; }
        public string ExceptionType { get; set; }
        public string DetailsInRichTextFormat { get; set; }

        public string Details
        {
            get
            {               
                return DetailsInRichTextFormat.Replace("\n", Environment.NewLine);
            }
        }

        public bool isError
        {
            get
            {
                return (3000 <= Code && Code < 4000);
            }
        }


        #endregion

        #region Public Methods

        public static List<WebEventBL> GetByParameters(DateTime? timeFrom,
                                                       DateTime? timeTo
                                                      )
        {
            List<WebEventBL> itemBL_List = new List<WebEventBL>();
            List<WebEvent> itemList = new List<WebEvent>();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                itemList = repository.Find<WebEvent>(x =>
                                                    (timeFrom == null || x.Time >= timeFrom) &&
                                                    (timeTo == null || x.Time <= timeTo)
                                                    ).ToList();

                itemList = itemList.OrderByDescending(x => x.Time).ToList();

                WebEventBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new WebEventBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static WebEventBL GetByGuid(string guid)
        {
            WebEventBL itemBL = new WebEventBL();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                WebEvent item = repository.Get<WebEvent>(x => x.Id == guid);
                itemBL.MapData(item);
            }

            return itemBL;
        }

        #endregion

        #region Internal Methods

        internal void MapData(WebEvent item)
        {
            Guid = item.Id;
            TimeUtc = item.TimeUtc;
            Time = item.Time;
            Type = item.Type;
            Sequence = item.Sequence;
            Occurrence = item.Occurrence;
            Code = item.Code;
            DetailCode = item.DetailCode;
            Message = item.Message;
            ApplicationPath = item.ApplicationPath;
            ApplicationVirtualPath = item.ApplicationVirtualPath;
            MachineName = item.MachineName;
            RequestUrl = item.RequestUrl;
            ExceptionType = item.ExceptionType;
            DetailsInRichTextFormat = item.Details;
        }

        #endregion
    }
}