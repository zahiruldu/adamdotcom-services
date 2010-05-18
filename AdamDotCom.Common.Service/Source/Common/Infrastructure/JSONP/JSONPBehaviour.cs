using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace AdamDotCom.Common.Service.Infrastructure.JSONP
{
    public class JSONP : JSONPBehavior {}

    public class JSONPBehavior : Attribute, IOperationBehavior
    {
        public string callback;
        public const string Name = "JSONPMessageProperty";

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters) { return; }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(new Inspector(callback));
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new Inspector(callback));
        }

        public void Validate(OperationDescription operationDescription) { return; }
       
        class Inspector : IParameterInspector
        {
            string callback;
            public Inspector(string callback)
            {
                this.callback = callback;
            }

            public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
            {
            }

            public object BeforeCall(string operationName, object[] inputs)
            {
                string methodName = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters[callback];
                if (methodName != null)
                {                    
                    JSONPMessageProperty property = new JSONPMessageProperty
                                                        {
                                                            MethodName = methodName
                                                        };
                    OperationContext.Current.OutgoingMessageProperties.Add(Name, property);
                }
                return null;
            }
        }
    }

    public static class JSONPBehaviorExtensions
    {
        public static bool IsJSONPBehavior(this Message message)
        {
            return message.Properties.ContainsKey(JSONPBehavior.Name);
        }
    }
}