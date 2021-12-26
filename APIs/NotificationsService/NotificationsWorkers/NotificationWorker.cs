using microthings_shared.Enums;
using microthings_shared.Models;
using NotificationsService.Models;
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


        public async Task<ResponseModel> PublishNotification(NotificationModel notification)
        {
            var response = new ResponseModel();
            foreach(NotificationTypesEnum notificationType in notification.NotificationTypes)
            {
                switch (notificationType)
                {
                    case NotificationTypesEnum.Slack:
                        {
                            response = await SendSlackNotification(notification);
                        } break;
                    case NotificationTypesEnum.PushNotification:
                        {
                            response = await SendPushNotification(notification);
                        }
                        break;
                    case NotificationTypesEnum.SignalR:
                        {
                            response = await SendSignalRNotification(notification);
                        }
                        break;
                }
            }
            return response;
        }


        private async Task<ResponseModel> SendSlackNotification(NotificationModel notification)
        {
            try
            {
                await _slackNotificator.SendSlackMessageToChannelAsync(notification.LoggingNotification.Message);
                return new ResponseModel() { IsSuccess = true, Message = "Success" };
            }
            catch(Exception ex)
            {
                return new ResponseModel() { IsSuccess = false, Message = ex.Message + "|Trace: " + ex.StackTrace };
            }
        }

        private async Task<ResponseModel> SendPushNotification(NotificationModel notification)
        {
            var responseFCM = await _pushNotificator.SendNotification(new FCMNotificationModel()
            {
                DeviceId="test",
                IsAndroidDevice = true,
                Title=notification.LoggingNotification.Message,
                Body=notification.LoggingNotification.Trace
            });
            return responseFCM;
        }

        private async Task<ResponseModel> SendSignalRNotification(NotificationModel notification)
        {
            try
            {
                await _signalrNotificator.PublishLogNotificationAsync(notification.LoggingNotification);
                return new ResponseModel() { IsSuccess = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                return new ResponseModel() { IsSuccess = false, Message = ex.Message + "|data: " + ex.Data + "|source: " + ex.Source + "|inner: " + ex.InnerException + "|Trace: " + ex.StackTrace };
            }
        }
    }
}
