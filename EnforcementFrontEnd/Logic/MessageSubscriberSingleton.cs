using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Shared_Contracts;
using Shared_Contracts.Parking;
using ZMQ;

namespace EnforcementFrontEnd.Logic
{
    public static class MessageSubscriberSingleton
    {
        public static void Configure(MessageSubscriberConfig config)
        {
            _config = config;
            string filter = "parkingEvent ";

            _context = new Context(1);
            _socket = _context.Socket(SocketType.SUB); 
            _socket.Connect(_config.Host);
            _socket.Subscribe(filter, Encoding.Unicode);

            _receiveThread = new Thread(ReceiveMessages); 
            _receiveThread.Start();
        }

        public static void ReceiveMessages()
        {
            while (true)
            {
                string message = _socket.Recv(Encoding.Unicode);
                string jsonData = message.Substring(13);
                var envelope = JsonConvert.DeserializeObject<Envelope>(jsonData);
                switch (envelope.Header.Type.ToString())
                {
                    case "Shared_Contracts.Parking.StartParkingEvent":
                        LocationsEventStore.Consume(JsonConvert.DeserializeObject<StartParkingEvent>(envelope.Message));
                        break;
                    default:
                        throw new ApplicationException("unknown type: " + envelope.Header.Type);
                }
            }
        }

        public static void Dispose()
        {
            _receiveThread.Abort();
            _socket.Dispose();
            _context.Dispose();
        }

        private static MessageSubscriberConfig _config;
        private static Thread _receiveThread; 
        private static Context _context;
        private static Socket _socket;
    }
}