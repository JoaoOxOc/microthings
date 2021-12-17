using System;

namespace microthings_shared.Models
{
    /// <summary>
    /// Logging Notification Model
    /// </summary>
    public class LoggingNotification
    {
        public int LoggingId { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
        public string Severity { get; set; }
        public string MicroserviceIdentifier { get; set; }
        public string NotificationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedById { get; set; }

    }
}
