using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Loader;

namespace TraderForPoe.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWindowViewService(
            this IServiceCollection services)
        {
            services.AddTransient<IWindowViewLoaderService, WindowViewLoaderService>();
            services.AddSingleton<IResourceLocator, ResourceLocator>();

            return services;
        }

        public static IServiceCollection AddResourceLocator(
            this IServiceCollection services, Application application)
        {
            services.AddSingleton<IResourceLocator>(x => new ResourceLocator(application));

            return services;
        }


    }
}
