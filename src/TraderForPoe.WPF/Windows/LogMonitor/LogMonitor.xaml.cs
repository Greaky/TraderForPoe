using System.Windows;
using TraderForPoe.WPF.ViewModel.LogMonitor;

namespace TraderForPoe.WPF.Windows.LogMonitor
{
    /// <summary>
    /// Interaktionslogik für LogReader.xaml
    /// </summary>
    public partial class LogMonitor : Window , ILogMonitor
    {
        public LogMonitor(ILogMonitorViewModel logMonitorViewModel)
        {
            InitializeComponent();
            DataContext = logMonitorViewModel;
        }
    }
}
