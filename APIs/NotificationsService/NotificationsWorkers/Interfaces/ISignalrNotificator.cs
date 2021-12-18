using microthings_shared.Models;
using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers.Interfaces
{
    public interface ISignalrNotificator
    {
        Task PublishLogNotificationAsync(LoggingNotification logNotification);
    }
}
