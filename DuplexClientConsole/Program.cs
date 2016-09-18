using DuplexClient;
using DuplexClientConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuplexClientConsole
{
    class Program 
    {
        static void Main(string[] args)
        {
            Console.Write("Connecting....");
            var clientImplimentation = new ClientImplimentation();
            var client = new Client();
            client.Connect("DuplexService/", clientImplimentation);
            Console.Write("Connected!\nRecieving:\n");
            Console.ReadKey();
        }

    }
}