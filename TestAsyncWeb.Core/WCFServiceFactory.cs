using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace TestAsyncWeb.Core
{

    public class CustomServiceHostFactory : ServiceHostFactory
    {


        public CustomServiceHostFactory()
        {
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, System.Uri[] baseAddresses)
        {

            ServiceHost host = new ServiceHost(serviceType, baseAddresses);


            foreach (Uri uri in baseAddresses)
            {

                if (uri.Scheme != "net.tcp")
                {
                    WebHttpBinding whb = new WebHttpBinding(WebHttpSecurityMode.None);
                    
                    whb.AllowCookies = true;
                    whb.CrossDomainScriptAccessEnabled = false;
                    whb.MaxReceivedMessageSize = 16 * 1024 * 1024;
                    if (uri.Scheme == "https")
                    {
                        whb.Security.Mode = WebHttpSecurityMode.Transport;
                    }

                    WebHttpBehavior behavior = new WebHttpBehavior();
                    
                    behavior.DefaultOutgoingResponseFormat = WebMessageFormat.Json;
                    behavior.DefaultOutgoingRequestFormat = WebMessageFormat.Json;
                    behavior.DefaultBodyStyle = WebMessageBodyStyle.WrappedRequest;

                    ServiceEndpoint endpoint = host.AddServiceEndpoint(GetContractType(serviceType), whb, string.Empty);
                    endpoint.Behaviors.Add(behavior);

                }

            }

            foreach (ServiceEndpoint ep in host.Description.Endpoints)
            {
                foreach (OperationDescription op in ep.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = 2147483646;
                    }
                }
            }

            return host;

        }

        private Type GetContractType(Type serviceType)
        {
            if (HasServiceContract(serviceType))
            {
                return serviceType;
            }

            List<Type> possibleContractTypesList = new List<Type>();

            foreach (var ifce in  serviceType.GetType().GetInterfaces())
            {
                if(HasServiceContract(ifce))
                {
                    possibleContractTypesList.Add(ifce);
                }
            }

            Type[] possibleContractTypes = possibleContractTypesList.ToArray();

            switch (possibleContractTypes.Length)
            {
                case 0:
                    throw new InvalidOperationException("Service type " + Convert.ToString(serviceType.FullName) + " does not implement any interface decorated with the ServiceContractAttribute.");
                case 1:
                    return possibleContractTypes[0];
                default:
                    throw new InvalidOperationException("Service type " + Convert.ToString(serviceType.FullName) + " implements multiple interfaces decorated with the ServiceContractAttribute, not supported by this factory.");
            }
        }

        private static bool HasServiceContract(Type type)
        {
            return Attribute.IsDefined(type, typeof(ServiceContractAttribute), false);
        }

    }

}
