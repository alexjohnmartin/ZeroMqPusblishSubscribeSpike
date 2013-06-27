﻿using System;
using System.Collections.Generic;
using EnforcementFrontEnd.Models;
using Shared_Contracts.Parking;

namespace EnforcementFrontEnd.Logic
{
    public static class LocationsEventStore
    {
        private static IDictionary<int, Location> locations;
        private static int _eventsCount = 0;

        public static int EventsConsumed { get { return _eventsCount; } }

        public static void Consume(ParkingEvent parkingEvent)
        {
            if (parkingEvent.GetType().Equals(typeof (StartParkingEvent)))
            {
                _eventsCount++; 
                var startParkingEvent = (StartParkingEvent) parkingEvent;
                locations[startParkingEvent.LocationId].StartParking(startParkingEvent.LicensePlate,
                                                                     startParkingEvent.StartDateTime,
                                                                     startParkingEvent.DurationInMins);
            }
            else
            {
                throw new ApplicationException("unknown parking event type");
            }
        }

        public static void Initialize(int firstLocationId, int numberOfLocations)
        {
            locations = new Dictionary<int, Location>();
            for (int l = firstLocationId; l < firstLocationId + numberOfLocations; l++)
            {
                locations.Add(l, new Location{LocationId = l});
            }
        }
    }
}