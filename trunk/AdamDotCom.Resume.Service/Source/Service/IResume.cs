using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using AdamDotCom.Common.Service.Infrastructure.JSONP;

[assembly: ContractNamespace("http://adam.kahtava.com/services/resume", ClrNamespace = "AdamDotCom.Resume.Service")]
namespace AdamDotCom.Resume.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/resume")]
    public interface IResume
    {
        [OperationContract]
        [WebGet(UriTemplate = "linkedIn/{firstnameLastname}/xml")]
        Resume ResumeXml(string firstnameLastname);

        [OperationContract]
        [JSONPBehavior(callback = "callback")]
        [WebGet(UriTemplate = "linkedIn/{firstnameLastname}/json", ResponseFormat = WebMessageFormat.Json)]
        Resume ResumeJson(string firstnameLastname);
    }
}