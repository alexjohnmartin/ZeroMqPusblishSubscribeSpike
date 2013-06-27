using System;
using Newtonsoft.Json;
using Shared_Contracts;
using Shared_Contracts.Parking;

namespace ParkingFrontEnd.Service
{
    public class SendMessageService : ISendMessageService
    {
        public void SendStartParkingMessage(int locationId, int duration, DateTime parkingStartTime)
        {
            var startParkingEvent = new StartParkingEvent
                                        {
                                            DurationInMins = duration,
                                            LocationId = locationId,
                                            LicensePlate = "111AAA",
                                            StartDateTime = parkingStartTime
                                        };

            var envelope = CreateEnvelope(startParkingEvent, typeof(StartParkingEvent));
            MessagePublisherSingleton.SendMessage(string.Format("parkingEvent {0}", JsonConvert.SerializeObject(envelope)));
        }

        public void SendExtendParkingMessage(int locationId, int duration)
        {
            throw new NotImplementedException();
        }

        public void SendStopParkingMessage(int locationId, DateTime parkingStopTime)
        {
            throw new NotImplementedException();
        }

        private static Envelope CreateEnvelope(object messageObject, Type messageType)
        {
            var envelope = new Envelope
            {
                Header = new Header
                {
                    CorrelationId = Guid.NewGuid(),
                    MessageId = Guid.NewGuid(),
                    SourceApp = "ParkingPubSubSpike",
                    SourceServer = Environment.MachineName,
                    Timestamp = DateTime.UtcNow,
                    Type = messageType
                },
                Message = JsonConvert.SerializeObject(messageObject)
            };
            return envelope;
        }
    }
}