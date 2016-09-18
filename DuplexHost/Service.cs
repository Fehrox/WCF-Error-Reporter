using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DuplexService
{
    
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class Service : IService
    {
        public static List<IMessageClient> Connections = new List<IMessageClient>();

        void IService.Connect()
        {
            var client = OperationContext.Current.
                GetCallbackChannel<IMessageClient>();
            Connections.Add(client);

            OperationContext.Current.Channel.Closed += (sender, eventArgs)
                => OnClientDisconnected(sender, eventArgs, client);

            OperationContext.Current.Channel.Faulted += (sender, eventArgs)
                => OnClientDisconnected(sender, eventArgs, client);
        }

        void OnClientDisconnected(object sender, EventArgs e, IMessageClient client)
        {
            Connections.Remove(client);
        }
    }
}
