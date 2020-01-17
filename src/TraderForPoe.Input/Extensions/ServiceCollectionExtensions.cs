using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace TraderForPoe.Input.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClipboardMonitor(
            this IServiceCollection services)
        {
            services.AddTransient<IClipboardMonitor, ClipboardMonitor>();


            return services;
        }
    }
}
