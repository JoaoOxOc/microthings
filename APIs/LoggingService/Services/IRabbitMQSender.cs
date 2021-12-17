using System.Threading.Tasks;
using microthings_shared.Models;

namespace LoggingService.Services
{
    public interface IRabbitMQSender
    {
        Task<bool> PublishNotificationMessage(LoggingNotification logNotification);
    }
}
