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
        public static IServiceCollection AddViewModels(
            this IServiceCollection services)
        {
            services.AddTransient<ViewModel.MainWindowViewModel>();
            services.AddTransient<ViewModel.NotifyIconViewModel>();

            return services;
        }

    }
}
