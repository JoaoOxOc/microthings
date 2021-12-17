using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace NotificationsService.Services
{
    /// <summary>
    /// Injects Swagger with it's configuration
    /// </summary>
    public static class SwaggerService
    {
        /// <summary>
        /// Injects Swagger with it's configuration
        /// </summary>
        /// <param name="services">the dependency injection container</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Notifications Service API",
                    Description = "API that deals with notifications - for Slack, Push Notifications (firebase and apple) and SignalR clients",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                try
                {
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                }
                catch (Exception) { }
            });
            return services;
        }
    }
}
