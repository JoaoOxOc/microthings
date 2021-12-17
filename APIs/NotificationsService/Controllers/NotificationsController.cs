using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public NotificationsController()
        {
        }

        /// <summary>
        /// Manual post of a notification
        /// </summary>
        /// <returns>Just success true or false</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /notifications/pushNotification
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if sent successfully</response>
        /// <response code="500">Returns error if not sent</response>
        [HttpPost("~/[controller]/pushNotification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ObjectResult> PushNotification()
        {
             return StatusCode((int)System.Net.HttpStatusCode.OK, null);
        }
    }
}
