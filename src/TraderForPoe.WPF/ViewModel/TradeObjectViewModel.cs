using System.Windows.Media.Imaging;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.ViewModel.Base;

namespace TraderForPoe.WPF.ViewModel
{
    public class TradeObjectViewModel : ViewModelBase
    {
        private readonly TradeObject _tradeObject;

        private StashGridViewModel _stashGridViewModel = StashGridViewModel.Instance;

        public TradeObjectViewModel(TradeObject tradeObject)
        {
            _tradeObject = tradeObject;
        }

        public string ItemName => _tradeObject.Item.ItemAsString;

        public decimal Amount => _tradeObject.Item.Amount;
        public decimal AmountPrice => _tradeObject.Item.Price.Amount;
        public BitmapImage Image => _tradeObject.Item.Price.Image;

        public string Customer => _tradeObject.Customer;
        public string Stash => _tradeObject.Stash;

        public string Position => _tradeObject.Position.ToString();



    }
}
