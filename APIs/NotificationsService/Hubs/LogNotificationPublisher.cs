using Microsoft.AspNetCore.SignalR;
using microthings_shared.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsService.Hubs
{
    public class LogNotificationPublisher : Hub, ILogNotificationPublisher
    {
        public async Task PublishLogNotification(LoggingNotification logNotification)
        {

            await Clients.All.SendAsync("OnLogNotifyPublished", logNotification);
        }
    }
}
