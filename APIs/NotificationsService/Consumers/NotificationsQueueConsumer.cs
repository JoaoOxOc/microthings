using MassTransit;
using microthings_shared.Enums;
using microthings_shared.Models;
using NotificationsService.NotificationsWorkers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsService.Consumers
{
    public class NotificationsQueueConsumer : INotificationsQueueConsumer, IConsumer<LoggingNotification>
    {
        private readonly INotificationWorker _notificationWorker;

        public NotificationsQueueConsumer(INotificationWorker notificationWorker)
        {
            _notificationWorker = notificationWorker;
        }

        public async Task Consume(ConsumeContext<LoggingNotification> context)
        {
            NotificationModel notificationModel = new NotificationModel();

            notificationModel.LoggingNotification = context.Message;
            notificationModel.NotificationTypes = new List<NotificationTypesEnum>();

            if (notificationModel.LoggingNotification.NotificationType == "Slack")
            {
                notificationModel.NotificationTypes.Add(NotificationTypesEnum.Slack);
            }
            if (notificationModel.LoggingNotification.NotificationType == "SignalR")
            {
                notificationModel.NotificationTypes.Add(NotificationTypesEnum.SignalR);
            }
            if (notificationModel.LoggingNotification.NotificationType == "PushNotification")
            {
                notificationModel.NotificationTypes.Add(NotificationTypesEnum.PushNotification);
            }

            await _notificationWorker.PublishNotification(notificationModel);
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
