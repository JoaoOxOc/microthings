using microthings_shared.Models;
using NotificationsService.Hubs;
using NotificationsService.NotificationsWorkers.Interfaces;
using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers.Notificators
{
    public class SignalrNotificator : ISignalrNotificator
    {
        private readonly ILogNotificationPublisher _logNotificationPublisher;

        public SignalrNotificator(ILogNotificationPublisher logNotifyPublisher)
        {
            _logNotificationPublisher = logNotifyPublisher;
        }

        public async Task PublishLogNotificationAsync(LoggingNotification logNotification)
        {
            await _logNotificationPublisher.PublishLogNotification(logNotification);
        }
    }
}
