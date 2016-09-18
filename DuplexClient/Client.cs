using DuplexClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace DuplexClient
{
    public class Client
    {

        private static ServiceClient _proxy;

        public void Connect(string endPoint, IServiceCallback contract)
        {
            InstanceContext context = new InstanceContext(contract);
            var address = new EndpointAddress("net.tcp://localhost:2358/" + endPoint);
            var binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            _proxy = new ServiceClient(context, binding, address);
            _proxy.Connect();
        }
    }

}
