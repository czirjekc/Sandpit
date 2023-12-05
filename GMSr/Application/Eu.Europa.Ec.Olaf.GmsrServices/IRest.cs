using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Eu.Europa.Ec.Olaf.GmsrServices
{
    [ServiceContract]
    public interface IRest
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Tickets/GetOpenByUserLogin/{userLogin}")]
        List<Ticket> GetOpenByUserLogin(string userLogin);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Tickets/GetOpenByUserLoginXml/{userLogin}")]
        List<Ticket> GetOpenByUserLoginXml(string userLogin);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Tickets/GetResolvedByUserLogin/{userLogin}")]
        List<Ticket> GetResolvedByUserLogin(string userLogin);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Tickets/GetResolvedByUserLoginXml/{userLogin}")]
        List<Ticket> GetResolvedByUserLoginXml(string userLogin);
    }

    [DataContract]
    public class Ticket
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Nature { get; set; }
        [DataMember]
        public DateTime? CreationDate { get; set; }
        [DataMember]
        public DateTime? ActionGroupResolutionDate { get; set; }
        [DataMember]
        public string Concern { get; set; }
    }
}
