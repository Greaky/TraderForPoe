using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using TraderForPoe.Core.Reader;
using TraderForPoe.WPF.ViewModel.Base;

namespace TraderForPoe.WPF.ViewModel.LogMonitor
{
    public class LogMonitorViewModel : ViewModelBase, ILogMonitorViewModel
    {
        #region Fields

        private string _filter;
        private readonly ICollectionView _linesView;

        #endregion Fields

        #region Contructors

        public LogMonitorViewModel(ILogReader logReader)
        {
            logReader.OnLineAddition += LogReader_OnLineAddition;

            CmdStart = new RelayCommand(
                logReader.Start);

            CmdStop = new RelayCommand(
                logReader.EndRead);

            CmdClear = new RelayCommand(
                () => Lines.Clear());

            _linesView = CollectionViewSource.GetDefaultView(Lines);
            _linesView.Filter = UserFilter;
        }

        #endregion Contructors
        
        #region Properties

        public RelayCommand CmdClear { get; }

        public RelayCommand CmdStart { get; }

        public RelayCommand CmdStop { get; }

        public string Filter
        {
            get => _filter;
            set
            {
                if (value == _filter) return;
                _filter = value;
                _linesView.Refresh();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Lines { get; } = new ObservableCollection<string>();

        #endregion Properties

        #region Methods

        private void LogReader_OnLineAddition(object sender, LogReaderLineEventArgs e)
        {
            Lines.Add(e.Line);
        }

        private bool UserFilter(object item)
        {
            return string.IsNullOrEmpty(Filter) || ((string)item).ToLower().Contains(Filter.ToLower());
        }

        #endregion Methods
    }
}
