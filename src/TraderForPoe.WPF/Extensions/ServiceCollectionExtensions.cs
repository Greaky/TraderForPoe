using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Extensions;
using TraderForPoe.Input.Extensions;

namespace TraderForPoe.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {

            services.AddClipboardMonitor()
                .AddWindowViewService();

            return services;
        }
    }
}
