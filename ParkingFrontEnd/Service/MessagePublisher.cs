using System.Text;
using ZMQ; 

namespace ParkingFrontEnd.Service
{
    public class MessagePublisher : IMessagePublisher
    {
        public void SendMessage(string message)
        {
            using (var context = new Context(1))
            {
                using (Socket publisher = context.Socket(SocketType.PUB))
                {
                    publisher.Bind(_config.Host);

                    for (int i = 0; i < 3; i++)
                    {
                        System.Threading.Thread.Sleep(1000);
                        publisher.Send(message, Encoding.Unicode);
                    }
                }
            }
        }

        private readonly MessagePublisherConfig _config;

        public MessagePublisher(MessagePublisherConfig config)
        {
            _config = config;
        }
    }
}