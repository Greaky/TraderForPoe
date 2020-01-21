using System.Windows;
using TraderForPoe.WPF.ViewModel;

namespace TraderForPoe.WPF.Windows
{
    /// <summary>
    /// Interaktionslogik für TradeHistory.xaml
    /// </summary>
    public partial class TradeHistory : Window
    {
        public TradeHistory()
        {
            InitializeComponent();
            DataContext = new TradeHistoryViewModel();
        }
    }
}
