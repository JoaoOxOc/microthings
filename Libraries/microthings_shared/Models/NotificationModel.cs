using microthings_shared.Enums;
using microthings_shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace microthings_shared.Models
{
    public class NotificationModel
    {
        public List<NotificationTypesEnum> NotificationTypes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public LoggingNotification LoggingNotification { get; set; }
    }
}
