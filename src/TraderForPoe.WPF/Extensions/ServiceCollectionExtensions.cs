using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Extensions;
using TraderForPoe.Core.Loader;
using TraderForPoe.Input.Extensions;
using TraderForPoe.WPF.ViewModel;

namespace TraderForPoe.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddViewModels(
            this IServiceCollection services)
        {
            services.AddTransient<ViewModel.MainWindowViewModel>();
            services.AddTransient<NotifyIconViewModel>();

            return services;
        }

    }
}
