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
            Console.WriteLine("Subscribing to parking events...");

            //string filter = "startparking ";
            string filter = "s";

            using (var context = new Context(1))
            {
                using (Socket subscriber = context.Socket(SocketType.SUB))
                {
                    subscriber.Connect(ConfigurationManager.AppSettings["MessageSubscriber.Host"]);
                    subscriber.Subscribe(filter, Encoding.Unicode);

                    while (true)
                    {
                        string message = subscriber.Recv(Encoding.Unicode);
                        Console.WriteLine("message received - " + message);
                    }
                }
            }
        }
    }
}
