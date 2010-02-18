using System;
using System.ServiceModel.Configuration;

// Yanked from: http://msdn.microsoft.com/en-us/library/cc716898.aspx
namespace AdamDotCom.Common.Service.Infrastructure.JSONP
{
    public class JsonpBindingExtension : BindingElementExtensionElement
    {
        public override Type BindingElementType
        {
            get { return typeof(JSONPBindingElement); }

        }

        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement()
        {
            return new JSONPBindingElement();
        }
    }
}