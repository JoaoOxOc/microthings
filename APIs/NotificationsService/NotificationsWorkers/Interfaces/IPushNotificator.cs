using NotificationsService.Models;
using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers.Interfaces
{
    public interface IPushNotificator
    {
        Task<ResponseModel> SendNotification(FCMNotificationModel notificationModel);
    }
}
