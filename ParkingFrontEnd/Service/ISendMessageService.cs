using System;

namespace ParkingFrontEnd.Service
{
    public interface ISendMessageService
    {
        void SendStartParkingMessage(int locationId, int duration, DateTime parkingStartTime);
        void SendExtendParkingMessage(int locationId, int duration);
        void SendStopParkingMessage(int locationId, DateTime parkingStopTime); 
    }
}