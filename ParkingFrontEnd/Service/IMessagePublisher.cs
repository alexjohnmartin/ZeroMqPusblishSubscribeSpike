namespace ParkingFrontEnd.Service
{
    public interface IMessagePublisher
    {
        void SendMessage(string message);
    }
}