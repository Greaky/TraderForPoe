using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.ViewModel.Base;

namespace TraderForPoe.WPF.ViewModel
{
    public class TradeHistoryViewModel : ViewModelBase
    {
        public ObservableCollection<TradeObject> TradeObjectsList { get; set; } = TradeObject.TradeObjectList;

        public TradeHistoryViewModel()
        {
            CmdTestObject = new RelayCommand(Add);
            CmdClear = new RelayCommand(() => TradeObjectsList.Clear());
        }

        private void Add()
        {
            var tradeObject = new TradeObject("@To Labooooooo: Hi, I would like to buy your Cybil's Paw Thresher Claw listed for 1 jewellers in Bestiary (stash tab \"~b / o 0 alt\"; position: left 23, top 8)");
        }

        public RelayCommand CmdTestObject { get; }
        public RelayCommand CmdClear { get; }
    }

    public class UriToBitmapConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;

            return new BitmapImage(new Uri(value as string ?? throw new InvalidOperationException())); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
