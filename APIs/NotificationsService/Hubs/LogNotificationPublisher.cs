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

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} welcome to SignalR");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} goodbye");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
