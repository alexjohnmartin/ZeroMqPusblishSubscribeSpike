using System;

namespace ParkingFrontEnd.Service
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IMessagePublisher _messagePublisher;

        public void SendStartParkingMessage(int locationId, int duration, DateTime parkingStartTime)
        {
            _messagePublisher.SendMessage(string.Format("startparking {0} {1} {2}", locationId, duration, parkingStartTime));
        }

        public void SendExtendParkingMessage(int locationId, int duration)
        {
            throw new NotImplementedException();
        }

        public void SendStopParkingMessage(int locationId, DateTime parkingStopTime)
        {
            throw new NotImplementedException();
        }

        public SendMessageService() : this(new MessagePublisher(new MessagePublisherConfig()))
        {}

        public SendMessageService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }
    }
}