using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LoggingService.Services
{
    /// <summary>
    /// Deals with the injection of MassTransit Service
    /// </summary>
    public static class MassTransitService
    {
        /// <summary>
        /// Injects MassTransit with RabbitMQ connection
        /// </summary>
        /// <param name="services">the dependency injection container</param>
        /// <param name="configuration">the app configuration container</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureMassTransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            string rabbitMqHost = configuration.GetSection("ApplicationSettings:RabbitMQHost").Get<string>();
            string rabbitMqUser = configuration.GetSection("ApplicationSettings:RabbitMQUser").Get<string>();
            string rabbitMqPassword = configuration.GetSection("ApplicationSettings:RabbitMQPassword").Get<string>();
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://" + rabbitMqHost), h =>
                    {
                        h.Username(rabbitMqUser);
                        h.Password(rabbitMqPassword);
                    });
                }));
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
