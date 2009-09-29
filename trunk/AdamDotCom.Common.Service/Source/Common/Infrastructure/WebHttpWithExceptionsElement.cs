using System;
using System.ServiceModel.Configuration;
using AdamDotCom.Common.Service.Infrastructure;

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