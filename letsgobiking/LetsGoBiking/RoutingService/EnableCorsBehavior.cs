using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace RoutingService
{
    internal class EnableCorsBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        private static readonly Dictionary<string, string> RequiredHeaders = new()
        {
            { "Access-Control-Allow-Origin", "*" },
            { "Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS" },
            { "Access-Control-Allow-Headers", "X-Requested-With,Content-Type,Accept" }
        };

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomHeaderMessageInspector(RequiredHeaders));
        }

        public void Validate(ServiceEndpoint endpoint) { }

        public override Type BehaviorType => typeof(EnableCorsBehavior);

        protected override object CreateBehavior()
        {
            return new EnableCorsBehavior();
        }
    }

    internal class CustomHeaderMessageInspector : IDispatchMessageInspector
    {
        private readonly Dictionary<string, string> _requiredHeaders;
        public CustomHeaderMessageInspector(Dictionary<string, string> headers)
        {
            _requiredHeaders = headers ?? new Dictionary<string, string>();
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var httpRequest = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
            return httpRequest.Method.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase);
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (correlationState is true)
            {
                reply = Message.CreateMessage(MessageVersion.None, "PreflightReturn");

                var httpResponse = new HttpResponseMessageProperty();
                reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);

                httpResponse.SuppressEntityBody = true;
                httpResponse.StatusCode = HttpStatusCode.OK;
            }

            var httpHeader = (HttpResponseMessageProperty)reply.Properties[HttpResponseMessageProperty.Name];
            foreach (var item in _requiredHeaders)
            {
                httpHeader.Headers.Add(item.Key, item.Value);
            }
        }
    }
}
