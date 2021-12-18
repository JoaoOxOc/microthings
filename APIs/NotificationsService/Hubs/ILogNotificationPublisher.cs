using Microsoft.AspNetCore.SignalR;
using microthings_shared.Models;
using System.Threading.Tasks;

namespace NotificationsService.Hubs
{
    public interface ILogNotificationPublisher
    {
        Task PublishLogNotification(LoggingNotification logNotification);
    }
}
