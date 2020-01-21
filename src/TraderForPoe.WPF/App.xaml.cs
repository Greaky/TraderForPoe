using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Loader;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.ViewModel;
using TraderForPoe.WPF.Windows;

namespace TraderForPoe.WPF
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App
    {
        private TaskbarIcon _notifyIcon;
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
            // _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            FindResource("NotifyIcon");
            Application.Current.FindResource("NotifyIcon");
            //_notifyIcon = new TaskbarIcon();
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
