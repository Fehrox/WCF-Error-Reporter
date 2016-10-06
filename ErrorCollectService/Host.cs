using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ErrorCollectService
{
    class Host
    {
        static void Main(string[] args)
        {

            using (var host = new ServiceHost(typeof(ErrorCollectService)))
            {

                host.Open();
                Console.WriteLine("Error Colelction Service is running!");
                Console.WriteLine("Press any key to close.");
                Console.ReadKey();

                //while (true) {
                //    var stringToSend = Console.ReadLine();
                //    foreach (var client in Service.Connections) {
                //        client.RecieveMessage(stringToSend);
                //    }
                //}

            }
        }
    }
}
