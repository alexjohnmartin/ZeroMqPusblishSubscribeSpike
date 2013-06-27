using System;

namespace Shared_Contracts
{
    public class Header
    {
        public string SourceApp { get; set; }
        public string SourceServer { get; set; }
        public DateTime Timestamp { get; set; }
        public Type Type { get; set; }

        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }

        public int RetryAttempts { get; set; }
    }
}