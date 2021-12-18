using CorePush.Google;
using Microsoft.Extensions.Configuration;
using NotificationsService.Models;
using NotificationsService.NotificationsWorkers.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static NotificationsService.Models.GoogleNotification;

namespace NotificationsService.NotificationsWorkers.Notificators
{
    public class PushNotificator: IPushNotificator
    {
        private readonly IConfiguration _configuration;

        public PushNotificator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseModel> SendNotification(FCMNotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (notificationModel.IsAndroidDevice)
                {
                    /* FCM Sender (Android Device) */
                    string senderId = _configuration.GetSection("ApplicationSettings:FCMSEnderId").Get<string>();
                    string serverKey = _configuration.GetSection("ApplicationSettings:FCMServerKey").Get<string>();
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = senderId,
                        ServerKey = serverKey
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }
}
