using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Extensions;
using TraderForPoe.Input.Extensions;
using TraderForPoe.WPF.Extensions;
using TraderForPoe.WPF.Properties;

namespace TraderForPoe.WPF.Startup
{
    public static class Startup
    {
        public static ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection()
                .AddViewModels()
                .AddResourceLocator(Application.Current)
                .AddLogReader(Settings.Default.PathToClientTxt)
                .AddClipboardMonitor()
                .AddWindowViewService();
            return services.BuildServiceProvider();
        }
    }
}
