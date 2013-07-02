using System;
using EnforcementFrontEnd.Logic;
using NUnit.Framework;
using Newtonsoft.Json;
using Shared_Contracts;
using Shared_Contracts.Parking;
using Shared_Transmission.PubSub;

namespace EnforcementFrontEnd_IntegrationTests
{
    public class MessageSubscriberSingleton_Tests
    {
        public class When_the_message_subscriber_receives_a_payment_start_event
        {
            private const string Host = "tcp://127.0.0.1:6000"; 

            [SetUp]
            public void When()
            {
                LocationsEventStore.Initialize(1000, 1);
                MessageSubscriberSingleton.Configure(new MessageSubscriberConfig{Host = Host});

                MessagePublisherSingleton.Configure(new MessagePublisherConfig{Host = Host, TestMode = false});
                System.Threading.Thread.Sleep(5000);

                SendParkingStartMessage(); 

                System.Threading.Thread.Sleep(500);
            }

            [Test]
            public void Then_it_should_update_the_event_store()
            {
                Assert.That(LocationsEventStore.EventsConsumed, Is.EqualTo(1));
            }

            [TearDown]
            public void Teardown()
            {
                MessageSubscriberSingleton.Dispose();
                MessagePublisherSingleton.Dispose();
            }

            private void SendParkingStartMessage()
            {
                var startParkingEvent = new StartParkingEvent
                {
                    DurationInMins = 15,
                    LocationId = 1000,
                    LicensePlate = "111AAA",
                    StartDateTime = DateTime.UtcNow
                };

                var envelope = CreateEnvelope(startParkingEvent, typeof(StartParkingEvent));
                MessagePublisherSingleton.SendMessage(string.Format("parkingEvent {0}", JsonConvert.SerializeObject(envelope)));
            }

            private static Envelope CreateEnvelope(object messageObject, Type messageType)
            {
                var envelope = new Envelope
                {
                    Header = new Header
                    {
                        CorrelationId = Guid.NewGuid(),
                        MessageId = Guid.NewGuid(),
                        SourceApp = "IntegrationTests",
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
}
