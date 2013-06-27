namespace Shared_Contracts
{
    public class Envelope: IMessage
    {
        public Header Header { get; set; }
        public string Message { get; set; }
    }
}