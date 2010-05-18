using System;
using System.ServiceModel.Configuration;

namespace AdamDotCom.Common.Service.Infrastructure
{
    public class CustomBindingExtension : BindingElementExtensionElement
    {
        public override Type BindingElementType
        {
            get { return typeof(CustomBindingElement); }
        }

        protected override System.ServiceModel.Channels.BindingElement CreateBindingElement()
        {
            return new CustomBindingElement();
        }
    }
}