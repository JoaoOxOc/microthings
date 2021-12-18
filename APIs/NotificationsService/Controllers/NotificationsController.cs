using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using microthings_shared.Models;
using NotificationsService.NotificationsWorkers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationWorker _notificationWorker;

        public NotificationsController(INotificationWorker notificationWorker)
        {
            _notificationWorker = notificationWorker;
        }

        /// <summary>
        /// Manual post of a notification; 
        /// notificationTypes: 1 for Signalr, 2 for Slack and 3 for Push Notification;
        /// use the following for all:
        /// "notificationTypes": [
        ///    1,2,3
        /// ],
        /// </summary>
        /// <returns>The response Model data</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /notifications/pushNotification
        ///     {
        ///          "notificationTypes": [
        ///            1
        ///          ],
        ///          "createdAt": "2021-12-18T14:45:12.817Z",
        ///          "createdById": 0,
        ///          "loggingNotification": {
        ///            "loggingId": 0,
        ///            "message": "string",
        ///            "trace": "string",
        ///            "severity": "string",
        ///            "microserviceIdentifier": "string",
        ///            "notificationType": "string",
        ///            "createdAt": "2021-12-18T14:45:12.818Z",
        ///            "createdById": 0
        ///          }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if sent successfully</response>
        /// <response code="500">Returns error if not sent</response>
        [HttpPost("~/[controller]/pushNotification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ObjectResult> PushNotification([FromBody]NotificationModel notification)
        {

            var response = await _notificationWorker.PublishNotification(notification);
            return StatusCode((int)System.Net.HttpStatusCode.OK, response);
        }
    }
}
