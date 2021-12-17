using microthings_shared.Models;
using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers.Interfaces
{
    public interface INotificationWorker
    {
        Task PublishNotification(NotificationModel notification);
    }
}
