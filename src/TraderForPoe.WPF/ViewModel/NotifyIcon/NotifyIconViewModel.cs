using System.Windows;
using TraderForPoe.Core.Loader;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.ViewModel.Base;
using TraderForPoe.WPF.ViewModel.TradeHistory;

namespace TraderForPoe.WPF.ViewModel
{
    public class NotifyIconViewModel : ViewModelBase, INotifyIconViewModel
    {
        #region Fields


        #endregion Fields

        #region Constructor

        public NotifyIconViewModel(IWindowViewLoaderService loaderService, IWpfResourceLocator wpfResourceLocator)
        {
            CmdHistory = new RelayCommand(() => loaderService.ShowSingle(typeof(ITradeHistoryViewModel)));

            CmdLog = new RelayCommand(() => loaderService.Show(typeof(ILogMonitorViewModel)));

            CmdSettings = new RelayCommand(() => loaderService.ShowSingle(typeof(IUserSettingsViewModel)));

            CmdRestart = new RelayCommand(RestartApp);

            CmdUpdate = new RelayCommand(Updater.CheckForUpdate);

            CmdAbout = new RelayCommand(() => loaderService.ShowSingle(typeof(IAboutViewModel)));

            CmdQuit = new RelayCommand(() => wpfResourceLocator.Shutdown());

        }

        #endregion Constructor

        #region Properties

        public RelayCommand CmdAbout { get; }
        public RelayCommand CmdHistory { get; }
        public RelayCommand CmdLog { get; }
        public RelayCommand CmdMonitor { get; private set; }
        public RelayCommand CmdQuit { get; }
        public RelayCommand CmdRestart { get; }
        public RelayCommand CmdSettings { get; }
        public RelayCommand CmdUpdate { get; }

        public bool UseClipboardMonitor
        {
            get => Settings.Default.UseClipboardMonitor;
            set
            {
                if (Settings.Default.UseClipboardMonitor == value) return;
                Settings.Default.UseClipboardMonitor = value;
                Settings.Default.Save();
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        private void RestartApp()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion Methods
    }
}
