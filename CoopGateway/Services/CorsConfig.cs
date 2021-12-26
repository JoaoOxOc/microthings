using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoopGateway.Services
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            string[] corsUrl = configuration.GetSection("ApplicationSettings:Cors").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy("MicrothingsCorsPolicy",
                    builder => builder
                        .WithOrigins(corsUrl) // or AllowAnyOrigin() combined with the next commented line
                        //.SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }
    }
}
