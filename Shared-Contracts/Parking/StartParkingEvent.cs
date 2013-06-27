using System;

namespace Shared_Contracts.Parking
{
    public class StartParkingEvent
    {
        public int LocationId { get; set; }
        public int DurationInMins { get; set; }
        public DateTime StartDateTime { get; set; }
        public string LicensePlate { get; set; }
    }
}