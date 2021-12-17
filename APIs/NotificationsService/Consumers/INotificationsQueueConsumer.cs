using MassTransit;
using microthings_shared.Models;
using System.Threading.Tasks;

namespace NotificationsService.Consumers
{
    public interface INotificationsQueueConsumer
    {
        Task Consume(ConsumeContext<LoggingNotification> context);
    }
}
