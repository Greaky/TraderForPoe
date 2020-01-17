using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core;

namespace TraderForPoe.Input.Extensions
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
