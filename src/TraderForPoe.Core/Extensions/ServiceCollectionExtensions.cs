using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Loader;
using TraderForPoe.Core.Reader;

namespace TraderForPoe.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWindowViewService(
            this IServiceCollection services)
        {
            services.AddSingleton<IWindowViewLoaderService, WindowViewLoaderService>();
            services.AddSingleton<IResourceLocator, ResourceLocator>();

            return services;
        }

        public static IServiceCollection AddResourceLocator(
            this IServiceCollection services, Application application)
        {
            services.AddSingleton<IResourceLocator>(x => new ResourceLocator(application));

            return services;
        }

        public static IServiceCollection AddLogReader(
            this IServiceCollection services, string path)
        {
            services.AddSingleton<ILogReader>(x => new LogReader(path));

            return services;
        }
    }
}
