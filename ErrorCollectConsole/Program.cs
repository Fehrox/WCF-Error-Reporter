using ErrorCollectClient;
using ErrorCollectClient.ServiceReference;
using ErrorCollectConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorCollectConsole
{
    public class Program 
    {
        static void Main(string[] args)
        {
            Console.Write("Connecting....");
            var client = new RemoteErrorCollect(
                "localhost",
                2358,
                "consoleclient", 
                "WindowsConsole", 
                "Console User");

            Console.Write("Connected!\nRecieving:\n");

            while (true) {
                Console.WriteLine("Press any key to send error to server.");
                Console.ReadKey();
                var exception = new Exception("Logged from the console client");
                client.LogError(exception);
                Console.WriteLine("Exception sent to server.");
            }
        }

    }
}