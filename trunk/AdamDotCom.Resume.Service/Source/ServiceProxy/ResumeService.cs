using System.Runtime.Serialization;
using System.ServiceModel;

[assembly: ContractNamespace("http://adam.kahtava.com/services/resume", ClrNamespace = "AdamDotCom.Resume.Service.Proxy")]
namespace AdamDotCom.Resume.Service.Proxy
{
    public class ResumeService: ClientBase<IResume>, IResume
    {
        public Resume ResumeXml(string firstnameLastname)
        {
            return base.Channel.ResumeXml(firstnameLastname);
        }

        public Resume ResumeJson(string firstnameLastname)
        {
            return base.Channel.ResumeJson(firstnameLastname);
        }
    }
}
