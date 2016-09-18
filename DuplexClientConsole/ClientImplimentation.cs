using DuplexClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuplexClientConsole
{
    class ClientImplimentation : IServiceCallback
    {
        public void RecieveMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
