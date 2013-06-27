using System;

namespace ParkingFrontEnd.Service
{
    public class SendMessageService : ISendMessageService
    {
        public void SendStartParkingMessage(int locationId, int duration, DateTime parkingStartTime)
        {
            MessagePublisherSingleton.SendMessage(string.Format("startparking {0} {1} {2}", locationId, duration, parkingStartTime));
        }

        public void SendExtendParkingMessage(int locationId, int duration)
        {
            throw new NotImplementedException();
        }

        public void SendStopParkingMessage(int locationId, DateTime parkingStopTime)
        {
            throw new NotImplementedException();
        }
    }
}