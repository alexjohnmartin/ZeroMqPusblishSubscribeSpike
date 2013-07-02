using System;
using System.Configuration;
using System.Text;
using ZMQ;

namespace ConsoleListener
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = ConfigurationManager.AppSettings["MessageSubscriber.Host"];
            string filter = "parkingEvent ";
            int messageCount = 0; 

            Console.WriteLine("Subscribing to parking events on " + host);
            Console.WriteLine("******************************************************************");
   
            using (var context = new Context(1))
            {
                using (Socket subscriber = context.Socket(SocketType.SUB))
                {
                    subscriber.Connect(host);
                    subscriber.Subscribe(filter, Encoding.Unicode);

                    while (true)
                    {
                        string message = subscriber.Recv(Encoding.Unicode);
                        messageCount++; 
                        Console.WriteLine();
                        Console.WriteLine("message {0} - {1}", messageCount, message);
                    }
                }
            }
        }
    }
}
