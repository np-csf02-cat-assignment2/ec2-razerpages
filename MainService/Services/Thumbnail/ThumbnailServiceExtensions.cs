using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.Services.Thumbnail
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddThumbnailServices(this IServiceCollection services, IConfiguration options)
        {
            services.AddOptions<ThumbnailServiceOptions>()
                .Bind(options)
                .ValidateDataAnnotations();

            return services.AddTransient<ThumbnailService>();
        }
    }
}
