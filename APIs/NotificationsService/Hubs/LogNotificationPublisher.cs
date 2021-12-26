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
        protected IHubContext<LogNotificationPublisher> _context;

        public LogNotificationPublisher(IHubContext<LogNotificationPublisher> context)
        {
            _context = context;
        }

        public async Task PublishLogNotification(LoggingNotification logNotification)
        {

            await _context.Clients.All.SendAsync("OnLogNotifyPublished", logNotification);
        }

        public override async Task OnConnectedAsync()
        {
            await _context.Clients.All.SendAsync("Notify", $"{Context.ConnectionId} welcome to SignalR");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _context.Clients.All.SendAsync("Notify", $"{Context.ConnectionId} goodbye");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
