using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NotificationsService.Consumers;
using NotificationsService.Hubs;
using NotificationsService.NotificationsWorkers;
using NotificationsService.NotificationsWorkers.Interfaces;
using NotificationsService.NotificationsWorkers.Notificators;
using NotificationsService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<INotificationsQueueConsumer, NotificationsQueueConsumer>();
            services.AddControllers();
            services.ConfigureSwagger();

            // .NET Native DI Abstraction
            RegisterServices(services);
            services.ConfigureRabbitConsumer(Configuration);

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotificationsService v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LogNotificationPublisher>("/log-notification");
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            services.TryAddSingleton<INotificationWorker, NotificationWorker>();
            services.TryAddSingleton<IPushNotificator, PushNotificator>();
            services.TryAddSingleton<ISignalrNotificator, SignalrNotificator>();
            services.TryAddSingleton<ISlackNotificator, SlackNotificator>();
            services.TryAddSingleton<ILogNotificationPublisher, LogNotificationPublisher>();
        }
    }
}
