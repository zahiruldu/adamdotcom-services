using System.ServiceModel.Channels;

namespace AdamDotCom.Common.Service.Infrastructure.JSONP
{
    public class JSONPMessageProperty : IMessageProperty
    {
        public IMessageProperty CreateCopy()
        {
            return new JSONPMessageProperty(this);
        }

        public JSONPMessageProperty()
        {
        }

        internal JSONPMessageProperty(JSONPMessageProperty other)
        {
            MethodName = other.MethodName;
        }

        public string MethodName { get; set; }
    }
}
