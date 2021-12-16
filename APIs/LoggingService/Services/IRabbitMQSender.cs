using LoggingService.Models;
using System.Threading.Tasks;

namespace LoggingService.Services
{
    public interface IRabbitMQSender
    {
        Task<bool> PublishNotificationMessage(LoggingNotification logNotification);
    }
}
