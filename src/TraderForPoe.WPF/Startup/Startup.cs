using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.WPF.Extensions;

namespace TraderForPoe.WPF.Startup
{
    public static class Startup
    {
        public static ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection().
                RegisterServices();

            services.AddTransient<ViewModel.MainWindowViewModel>();
            services.AddTransient<ViewModel.NotifyIconViewModel>();


            return services.BuildServiceProvider();
        }
    }
}
