using TraderForPoe.Classes;
using TraderForPoe.ViewModel.Base;

namespace TraderForPoe.ViewModel
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
    }
}