using MassTransit;
using Microsoft.Extensions.Configuration;
using microthings_shared.Models;
using System;
using System.Threading.Tasks;

namespace LoggingService.Services
{
    /// <summary>
    /// Deals with RabbitMQ message sending or publishing
    /// </summary>
    public class RabbitMQSender : IRabbitMQSender
    {
        private readonly IConfiguration _configuration;
        private readonly IBus _bus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration">dependency injection of app configuration container</param>
        /// <param name="bus">dependency injection of MassTransit bus object</param>
        public RabbitMQSender(IConfiguration configuration, IBus bus)
        {
            _configuration = configuration;
            _bus = bus;
        }

        /// <summary>
        /// Async method to send message through RabbitMQ Exchange
        /// </summary>
        /// <param name="logNotification">The notification object to be sent</param>
        /// <returns></returns>
        public async Task<bool> PublishNotificationMessage(LoggingNotification logNotification)
        {
            bool success = false;
            try
            {
                string rabbitMqHost = _configuration.GetSection("ApplicationSettings:RabbitMQHost").Get<string>();
                string rabbitMqExchange = _configuration.GetSection("ApplicationSettings:RabbitMQExchange").Get<string>();
                
                Uri uri = new Uri("rabbitmq://" + rabbitMqHost + "/" + rabbitMqExchange);
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(logNotification);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }
    }
}
