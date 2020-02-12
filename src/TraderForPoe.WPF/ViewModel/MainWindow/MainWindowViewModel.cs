using System.Collections.ObjectModel;
using TraderForPoe.Core.Loader;
using TraderForPoe.Core.Reader;
using TraderForPoe.Input.Clipboard;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Controls;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.ViewModel.Base;

namespace TraderForPoe.WPF.ViewModel.MainWindow
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        #region Fields

        private readonly IClipboardMonitor _clipboardMonitor;
        private readonly ILogReader _logReader;
        private readonly IWindowViewLoaderService _viewLoaderService;

        private StashGridViewModel _stashGridViewModel = StashGridViewModel.Instance;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel(IClipboardMonitor clipboardMonitor, ILogReader logReader, IWindowViewLoaderService viewLoaderService)
        {
            _clipboardMonitor = clipboardMonitor;
            _logReader = logReader;
            _viewLoaderService = viewLoaderService;
            SubscribeToEvents();
            SetUpStashGrid();

            var tradeObject = new TradeObject("@To Labooooooo: Hi, I would like to buy your Cybil's Paw Thresher Claw listed for 8 jewellers in Bestiary (stash tab \"~b / o 0 alt\"; position: left 23, top 8)");
            var tradeObjectViewModel = new TradeObjectViewModel(tradeObject);
            //CustomTestCtrl uc = new CustomTestCtrl(tradeObject);
            //stk_MainPnl.Children.Add(uc);

            TradeObjects.Add(tradeObjectViewModel);


        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<TradeObjectViewModel> TradeObjects { get; set; } = new ObservableCollection<TradeObjectViewModel>();

        public float ControlOpacity => Settings.Default.ControlOpacity;

        #endregion Properties

        #region Methods

        private void ClipMonitor_OnChange(object sender, ClipboardTextEventArgs e)
        {
            if (!Settings.Default.UseClipboardMonitor) return;
            if (TradeObject.IsTradeWhisper(e.Line))
            {
                Poe.SendCommand(e.Line);
            }
        }

        private void LogReader_OnLineAddition(object sender, LogReaderLineEventArgs e)
        {
            //TODO Implementieren
            if (!TradeObject.IsLogTradeWhisper(e.Line)) return;
            var to = new TradeObject(e.Line);
            var tovm = new TradeObjectViewModel(to);
            TradeObjects.Add(tovm);
        }

        private void SubscribeToEvents()
        {
            _clipboardMonitor.OnChange += ClipMonitor_OnChange;
            _logReader.OnLineAddition += LogReader_OnLineAddition;
        }

        private void SetUpStashGrid()
        {
            _viewLoaderService.Show(typeof(StashGridViewModel));
        }

        #endregion Methods
    }
}
