using microthings_shared.Models;
using NotificationsService.Models;
using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers.Interfaces
{
    public interface INotificationWorker
    {
        Task<ResponseModel> PublishNotification(NotificationModel notification);
    }
}
