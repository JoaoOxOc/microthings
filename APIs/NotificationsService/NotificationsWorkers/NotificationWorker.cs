using microthings_shared.Enums;
using microthings_shared.Models;
using NotificationsService.NotificationsWorkers.Interfaces;
using System;
using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers
{
    public class NotificationWorker : INotificationWorker
    {
        private readonly IPushNotificator _pushNotificator;
        private readonly ISignalrNotificator _signalrNotificator;
        private readonly ISlackNotificator _slackNotificator;

        public NotificationWorker(IPushNotificator pushNotificator, ISignalrNotificator signalrNotificator, ISlackNotificator slackNotificator)
        {
            _pushNotificator = pushNotificator;
            _signalrNotificator = signalrNotificator;
            _slackNotificator = slackNotificator;
        }


        public async Task PublishNotification(NotificationModel notification)
        {
            foreach(NotificationTypesEnum notificationType in notification.NotificationTypes)
            {
                switch (notificationType)
                {
                    case NotificationTypesEnum.Slack:
                        {
                            await SendSlackNotification(notification);
                        } break;
                    case NotificationTypesEnum.PushNotification:
                        {
                            await SendPushNotification(notification);
                        }
                        break;
                    case NotificationTypesEnum.SignalR:
                        {
                            await SendSignalRNotification(notification);
                        }
                        break;
                }
            }
        }


        private async Task SendSlackNotification(NotificationModel notification)
        {
            try
            {
                await _slackNotificator.SendSlackMessageToChannelAsync(notification.LoggingNotification.Message);
            }
            catch(Exception)
            {

            }
        }

        private async Task SendPushNotification(NotificationModel notification)
        {

        }

        private async Task SendSignalRNotification(NotificationModel notification)
        {
            try
            {
                await _signalrNotificator.PublishLogNotificationAsync(notification.LoggingNotification);
            }
            catch (Exception)
            {

            }
        }
    }
}
