using System;
using System.ServiceModel.Configuration;

namespace AdamDotCom.Common.Service.Infrastructure
{
    public class WebHttpWithExceptionsElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof (WebHttpWithExceptions); }
        }

        protected override object CreateBehavior()
        {
            return new WebHttpWithExceptions();
        }
    }
}