using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace AdamDotCom.Common.Service.Infrastructure.CSV
{
    public class CSV : CSVBehavior {}

    public class CSVBehavior : Attribute, IOperationBehavior
    {
        public const string Name = "CSVMessageProperty";

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters) { return; }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(new Inspector());
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new Inspector());
        }

        public void Validate(OperationDescription operationDescription) { return; }

        class Inspector : IParameterInspector
        {
            public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
            {
            }

            public object BeforeCall(string operationName, object[] inputs)
            {
                OperationContext.Current.OutgoingMessageProperties.Add(Name, new object());
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/csv; charset=utf-8";
                return null;
            }
        }
    }

    public static class CSVBehaviorExtensions
    {
        public static bool IsCSVBehavior(this Message message)
        {
            return message.Properties.ContainsKey(CSVBehavior.Name);
        }
    }
}