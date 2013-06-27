using System;

namespace EnforcementFrontEnd.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LicensePlate { get; set; }

        public void StartParking(string licensePlate, DateTime startDateTime, int durationInMins)
        {
            LicensePlate = licensePlate; 
        }
    }
}