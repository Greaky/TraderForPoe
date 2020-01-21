using System.Collections.ObjectModel;
using TraderForPoe.Input.Clipboard;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.ViewModel.Base;

namespace TraderForPoe.WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private readonly IClipboardMonitor _clipboardMonitor;
        private StashGridViewModel _stashGridViewModel = StashGridViewModel.Instance;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel(IClipboardMonitor clipboardMonitor)
        {
            _clipboardMonitor = clipboardMonitor;
            SubscribeToEvents();
            SetUpStashGrid();
        }
        #endregion Constructors

        #region Properties

        public ObservableCollection<TradeObjectViewModel> TradeObjects { get; set; } = new ObservableCollection<TradeObjectViewModel>();

        public float ControlOpacity => Settings.Default.ControlOpacity;

        #endregion Properties

        #region Methods

        private void ClipMonitor_OnChange(object sender, ClipboardTextEventArgs e)
        {
            if (Settings.Default.UseClipboardMonitor)
            {
                if (TradeObject.IsTradeWhisper(e.Line))
                {
                    Poe.SendCommand(e.Line);
                }
            }
        }

        private void LogReader_OnLineAddition(object sender, LogReaderLineEventArgs e)
        {
            //TODO Implementieren
            if (TradeObject.IsLogTradeWhisper(e.Line))
            {
                var to = new TradeObject(e.Line);
                var tovm = new TradeObjectViewModel(to);
                TradeObjects.Add(tovm);
            }
        }

        private void SubscribeToEvents()
        {
            _clipboardMonitor.OnChange += ClipMonitor_OnChange;
            LogReader.OnLineAddition += LogReader_OnLineAddition;
        }

        private void SetUpStashGrid()
        {
           // WindowViewLoaderService.Show(typeof(StashGridViewModel));
        }

        #endregion Methods
    }
}
