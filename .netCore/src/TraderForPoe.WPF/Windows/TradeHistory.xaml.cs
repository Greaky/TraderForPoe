using System.Windows;
using TraderForPoe.ViewModel;

namespace TraderForPoe.Windows
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
