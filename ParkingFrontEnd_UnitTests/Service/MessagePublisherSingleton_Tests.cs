using System;
using NUnit.Framework;
using Shared_Transmission.PubSub;

namespace ParkingFrontEnd_UnitTests.Service
{
    public class MessagePublisherSingleton_Tests
    {
        public class When_I_send_a_message_without_configuring_the_message_publisher
        {
            private Exception _resultException;

            [SetUp]
            public void When()
            {
                try
                {
                    MessagePublisherSingleton.SendMessage("test message");
                }
                catch (Exception exception)
                {
                    _resultException = exception; 
                }
            }

            [Test]
            public void Then_it_should_throw_an_application_error()
            {
                Assert.That(_resultException, Is.Not.Null);
                Assert.That(_resultException.GetType(), Is.EqualTo(typeof(ApplicationException)));
            }
        }

        public class When_I_send_a_message_after_configuring_the_message_publisher
        {
            private Exception _resultException;

            [SetUp]
            public void When()
            {
                MessagePublisherSingleton.Configure(new MessagePublisherConfig{TestMode = true});

                try
                {
                    MessagePublisherSingleton.SendMessage("test message");
                }
                catch (Exception exception)
                {
                    _resultException = exception;
                }
            }

            [Test]
            public void Then_it_should_not_throw_an_error()
            {
                Assert.That(_resultException, Is.Null);
            }

            [TearDown]
            public void TearDown()
            {
                MessagePublisherSingleton.Dispose();
            }
        }
    }
}
