using LoggingService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using microthings_shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRabbitMQSender _rabbitMqSender;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRabbitMQSender rabbitMqSender)
        {
            _logger = logger;
            _rabbitMqSender = rabbitMqSender;
        }

        /// <summary>
        /// Gets weather forecast data
        /// </summary>
        /// <returns>Array of weather forecast data</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /weatherforecast/get
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns always OK since it is a simple API method</response>
        [HttpGet("~/[controller]/get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public ObjectResult Get()
        {
            var rng = new Random();
            var weatherData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return StatusCode((int)System.Net.HttpStatusCode.OK, weatherData);
        }

        /// <summary>
        /// Test RabbitMQ message publish
        /// </summary>
        /// <returns>Just success true or false</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /weatherforecast/publishLog
        ///     { 
        ///         CreatedAt = "2021-12-18 15:00:00", 
        ///         CreatedById = 1, 
        ///         LoggingId = 1, 
        ///         Message="My first log notification", 
        ///         Trace="Will stay in the queue until someone consumes",
        ///         Severity="High",
        ///         MicroserviceIdentifier="LoggingService",
        ///         NotificationType="Slack"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if sent successfully</response>
        /// <response code="500">Returns error if not sent</response>
        [HttpPost("~/[controller]/publishLog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ObjectResult> SendLogNotification([FromBody] LoggingNotification logNotification)
        {
            bool success = await _rabbitMqSender.PublishNotificationMessage(logNotification);
            if (success)
            {
                return StatusCode((int)System.Net.HttpStatusCode.OK, success);
            }
            else
            {

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, success);
            }
        }
    }
}
