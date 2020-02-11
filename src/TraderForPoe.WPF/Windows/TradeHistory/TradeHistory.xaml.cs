using System.Windows;
using TraderForPoe.WPF.ViewModel;
using TraderForPoe.WPF.ViewModel.TradeHistory;

namespace TraderForPoe.WPF.Windows
{
    /// <summary>
    /// Interaktionslogik für TradeHistory.xaml
    /// </summary>
    public partial class TradeHistory : Window, ITradeHistory
    {
        public TradeHistory(ITradeHistoryViewModel tradeHistoryViewModel)
        {
            InitializeComponent();
            DataContext = tradeHistoryViewModel;
        }
    }
}
