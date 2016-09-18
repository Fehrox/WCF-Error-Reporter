using DuplexService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace DuplexServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var host = new ServiceHost(typeof(Service)))
            {

                host.Open();
                Console.WriteLine("Service is started!");
                Console.WriteLine("Type messages to clients:");

                while (true) {
                    var stringToSend = Console.ReadLine();
                    foreach (var client in Service.Connections) {
                        client.RecieveMessage(stringToSend);
                    }
                }

            }
        }
    }
}
