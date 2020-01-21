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


            return services;
        }
    }
}
