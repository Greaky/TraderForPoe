﻿using System.Collections.ObjectModel;
using TraderForPoe.Classes;
using TraderForPoe.WPF.Properties;
using TraderForPoe.ViewModel.Base;

namespace TraderForPoe.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private readonly ClipboardMonitor _clipboardMonitor = new ClipboardMonitor();
        private StashGridViewModel _stashGridViewModel = StashGridViewModel.Instance;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel()
        {
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
            WindowViewLoaderService.Show(typeof(StashGridViewModel));
        }

        #endregion Methods
    }
}