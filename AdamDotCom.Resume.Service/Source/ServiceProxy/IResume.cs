using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdamDotCom.Resume.Service.Proxy
{
    [ServiceContract]
    public interface IResume
    {
        [OperationContract]
        [WebGet(UriTemplate = "linkedIn/{firstnameLastname}/xml")]
        Resume ResumeXml(string firstnameLastname);

        [WebGet(UriTemplate = "linkedIn/{firstnameLastname}/json", ResponseFormat = WebMessageFormat.Json)]
        Resume ResumeJson(string firstnameLastname);
    }
}