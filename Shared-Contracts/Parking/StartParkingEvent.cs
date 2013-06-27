using System;

namespace Shared_Contracts.Parking
{
    public class StartParkingEvent : ParkingEvent
    {
        public int DurationInMins { get; set; }
        public DateTime StartDateTime { get; set; }
    }
}