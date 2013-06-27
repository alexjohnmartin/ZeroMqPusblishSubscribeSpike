using System.Configuration;

namespace ParkingFrontEnd.Service
{
    public class MessagePublisherConfig
    {
        public string Host 
        { 
            get { return ConfigurationManager.AppSettings["MessagePublisher.Host"]; }
        }
    }
}