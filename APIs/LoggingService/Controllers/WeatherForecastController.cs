using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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
    }
}
