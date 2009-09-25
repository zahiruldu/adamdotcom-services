using System.Runtime.Serialization;
using System.ServiceModel;

[assembly: ContractNamespace("http://adam.kahtava.com/services/whois", ClrNamespace = "AdamDotCom.Whois.Service.Proxy")]
namespace AdamDotCom.Whois.Service.Proxy
{
    public class WhoisService : ClientBase<IWhois>, IWhois
    {   
    }
}