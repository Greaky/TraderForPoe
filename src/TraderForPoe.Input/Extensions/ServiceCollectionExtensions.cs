using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Input.Clipboard;

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
