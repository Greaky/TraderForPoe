using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TraderForPoe.Classes;
using TraderForPoe.Core;
using TraderForPoe.ViewModel;
using TraderForPoe.Windows;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.Startup;

namespace TraderForPoe
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        private ServiceProvider serviceProvider;
        public App()
        {
            serviceProvider = WPF.Startup.Startup.InitializeServices();

            CheckForSettingsUpgrade();
            RegisterViewModel();
            CheckForLogFile();
            OpenMainWindow();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            FindResource("NotifyIcon");
        }

        private void CheckForLogFile()
        {
            //TODO Add option to turn off check
            LogFileCheck.CheckForClientTxt();
        }

        private void CheckForSettingsUpgrade()
        {
            // Check if settings upgrade is needed
            if (Settings.Default.UpgradeSettingsRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeSettingsRequired = false;
                Settings.Default.Save();
            }
        }

        private void OpenMainWindow()
        {
            var windowViewLoaderService = serviceProvider.GetService<IWindowViewLoaderService>();
            windowViewLoaderService.Show(typeof(MainWindowViewModel));
        }

        private void RegisterViewModel()
        {
            var windowViewLoaderService = serviceProvider.GetService<IWindowViewLoaderService>();

            windowViewLoaderService.Register(typeof(AboutViewModel), typeof(About));
            windowViewLoaderService.Register(typeof(LogMonitorViewModel), typeof(LogMonitor));
            windowViewLoaderService.Register(typeof(MainWindowViewModel), typeof(MainWindow));
            windowViewLoaderService.Register(typeof(StashGridViewModel), typeof(StashGrid));
            windowViewLoaderService.Register(typeof(TradeHistoryViewModel), typeof(TradeHistory));
            windowViewLoaderService.Register(typeof(UserSettingsViewModel), typeof(UserSettings));
        }
    }
}
