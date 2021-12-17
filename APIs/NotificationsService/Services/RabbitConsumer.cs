using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationsService.Consumers;
using System;

namespace NotificationsService.Services
{
    public static class RabbitConsumer
    {
        /// <summary>
        /// Injects a rabbit MQ consumer configuration
        /// </summary>
        /// <param name="services">the dependency injection container</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRabbitConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            string rabbitMqHost = configuration.GetSection("ApplicationSettings:RabbitMQHost").Get<string>();
            string rabbitMqUser = configuration.GetSection("ApplicationSettings:RabbitMQUser").Get<string>();
            string rabbitMqPassword = configuration.GetSection("ApplicationSettings:RabbitMQPassword").Get<string>();
            string rabbitMqExchange = configuration.GetSection("ApplicationSettings:RabbitMQExchange").Get<string>();
            string rabbitMqNotificationsQueue = configuration.GetSection("ApplicationSettings:RabbitMQNotificationsQueue").Get<string>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationsQueueConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri("rabbitmq://" + rabbitMqHost), h =>
                    {
                        h.Username(rabbitMqUser);
                        h.Password(rabbitMqPassword);
                    });
                    cfg.ReceiveEndpoint(rabbitMqNotificationsQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<NotificationsQueueConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
