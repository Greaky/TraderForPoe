using System.Windows;
using TraderForPoe.WPF.ViewModel;

namespace TraderForPoe.WPF.Windows
{
    /// <summary>
    /// Interaktionslogik für LogReader.xaml
    /// </summary>
    public partial class LogMonitor : Window
    {
        public LogMonitor()
        {
            InitializeComponent();
            DataContext = new LogMonitorViewModel();
        }
    }
}
