using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Extensions;
using TraderForPoe.Core.Loader;
using TraderForPoe.Core.Reader;
using TraderForPoe.Input.Extensions;
using TraderForPoe.WPF.ViewModel;
using TraderForPoe.WPF.ViewModel.About;
using TraderForPoe.WPF.ViewModel.LogMonitor;
using TraderForPoe.WPF.ViewModel.MainWindow;
using TraderForPoe.WPF.ViewModel.NotifyIcon;
using TraderForPoe.WPF.ViewModel.TradeHistory;
using TraderForPoe.WPF.ViewModel.UserSettings;
using TraderForPoe.WPF.Windows;
using TraderForPoe.WPF.Windows.LogMonitor;
using TraderForPoe.WPF.Windows.MainWindow;
using TraderForPoe.WPF.Windows.TradeHistory;

namespace TraderForPoe.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddViewModels(
            this IServiceCollection services)
        {
            services.AddTransient<IMainWindowViewModel, MainWindowViewModel>();
            services.AddTransient<IMainWindow,MainWindow>();
            services.AddTransient<MainWindow>();

            services.AddTransient<INotifyIconViewModel,NotifyIconViewModel>();

            services.AddTransient<ILogMonitorViewModel, LogMonitorViewModel>();
            services.AddTransient<ILogMonitor,LogMonitor>();
            services.AddTransient<LogMonitor>();

            services.AddTransient<ITradeHistoryViewModel, TradeHistoryViewModel>();
            services.AddTransient<ITradeHistory, TradeHistory>();
            services.AddTransient<TradeHistory>();

            services.AddTransient<IAboutViewModel, AboutViewModel>();
            services.AddTransient<About>();

            services.AddTransient<IUserSettingsViewModel, UserSettingsViewModel>();
            services.AddTransient<UserSettings>();


            return services;
        }

    }
}
