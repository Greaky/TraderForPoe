using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.Core.Loader;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.ViewModel;
using TraderForPoe.WPF.ViewModel.TradeHistory;
using TraderForPoe.WPF.Windows;

namespace TraderForPoe.WPF
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App
    {
        private TaskbarIcon _notifyIcon;
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            _serviceProvider = WPF.Startup.Startup.InitializeServices();

            CheckForSettingsUpgrade();
            RegisterViewModel();
            CheckForLogFile();
            OpenMainWindow();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var resourceLocator = _serviceProvider.GetService<IWpfResourceLocator>();
            var model = _serviceProvider.GetService<INotifyIconViewModel>();

            _notifyIcon = (TaskbarIcon) resourceLocator.GetResource("NotifyIcon");
            _notifyIcon.DataContext = model;
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
            var windowViewLoaderService = _serviceProvider.GetService<IWindowViewLoaderService>();
            windowViewLoaderService.Show(typeof(IMainWindowViewModel));
        }

        private void RegisterViewModel()
        {
            var windowViewLoaderService = _serviceProvider.GetService<IWindowViewLoaderService>();

            windowViewLoaderService.Register(typeof(IAboutViewModel), typeof(About));
            windowViewLoaderService.Register(typeof(ILogMonitorViewModel), typeof(LogMonitor));
            windowViewLoaderService.Register(typeof(IMainWindowViewModel), typeof(MainWindow));
            windowViewLoaderService.Register(typeof(StashGridViewModel), typeof(StashGrid));
            windowViewLoaderService.Register(typeof(ITradeHistoryViewModel), typeof(TradeHistory));
            windowViewLoaderService.Register(typeof(IUserSettingsViewModel), typeof(UserSettings));
        }
    }
}
