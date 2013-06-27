using System;
using System.Text;
using EnforcementFrontEnd.Logic;
using NUnit.Framework;
using Newtonsoft.Json;
using Shared_Contracts;
using Shared_Contracts.Parking;
using ZMQ;

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

                using (var _context = new Context(1))
                {
                    using (var _socket = _context.Socket(SocketType.PUB))
                    {
                        _socket.Bind(Host);

                        System.Threading.Thread.Sleep(500);

                        ////TODO: This doesn't appear to actually be sending any messages - the console listener doesn't see any traffic
                        _socket.Send(GenerateParkingStartMessage(), Encoding.Unicode);

                        System.Threading.Thread.Sleep(500);
                    }
                }
            }

            [Test]
            public void Then_it_should_update_the_event_store()
            {
                Assert.That(LocationsEventStore.EventsConsumed, Is.EqualTo(1));
            }

            private string GenerateParkingStartMessage()
            {
                var parkingEvent = new StartParkingEvent
                                       {
                                           DurationInMins = 15,
                                           LocationId = 1000,
                                           LicensePlate = "111AAA",
                                           StartDateTime = DateTime.UtcNow
                                       };
                var envelope = new Envelope
                                   {
                                       Header = new Header
                                                    {
                                                        CorrelationId = Guid.NewGuid(),
                                                        MessageId = Guid.NewGuid(),
                                                        SourceApp = "Integration tests",
                                                        SourceServer = "localhost",
                                                        Timestamp = DateTime.UtcNow,
                                                        Type = typeof (StartParkingEvent)
                                                    },
                                       Message = JsonConvert.SerializeObject(parkingEvent)
                                   };
                return JsonConvert.SerializeObject(envelope); 
            }

            [TearDown]
            public void Teardown()
            {
                MessageSubscriberSingleton.Dispose();
            }
        }
    }
}
