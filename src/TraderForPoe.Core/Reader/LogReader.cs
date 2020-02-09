using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Threading;

namespace TraderForPoe.Core.Reader
{
    public class LogReader : ILogReader
    {
        #region Fields

        public ObservableCollection<string> Lines { get; } = new ObservableCollection<string>();
        private const string Delimiter = "\n";
        private readonly string _path;
        private readonly DispatcherTimer _timer;
        private  string _buffer;
        private  bool _monitoring;
        private long _size;

        #endregion Fields

        #region MyRegion

        public LogReader(string path)
        {
            _path = path;
            _size = new FileInfo(_path).Length;
            _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
            _timer.Tick += Check;
            Start();
        }

        #endregion MyRegion

        #region Events

        public event EventHandler<LogReaderLineEventArgs> OnLineAddition;

        #endregion Events

        #region Methods

        public void Start()
        {
            if (_timer.IsEnabled)
            {
                return;
            }

            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void Check(object sender, EventArgs e)
        {
            if (!StartMonitoring()) return;

            var newSize = new FileInfo(_path).Length;

            if (_size >= newSize) return;

            using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                sr.BaseStream.Seek(_size, SeekOrigin.Begin);

                var data = _buffer + sr.ReadToEnd();

                if (!data.EndsWith(Delimiter))
                {
                    if (data.IndexOf(Delimiter, StringComparison.Ordinal) == -1)
                    {
                        _buffer += data;

                        data = string.Empty;
                    }
                    else
                    {
                        var pos = data.LastIndexOf(Delimiter, StringComparison.Ordinal) + Delimiter.Length;

                        _buffer = data.Substring(pos);

                        data = data.Substring(0, pos);
                    }
                }

                var lines = data.Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    if (OnLineAddition != null)
                    {
                        OnLineAddition(null, new LogReaderLineEventArgs { Line = line.Trim() });
                    }

                    Lines.Add(line.Trim());
                }
            }

            _size = newSize;

            lock (_timer) _monitoring = false;
        }

        private bool StartMonitoring()
        {
            lock (_timer)
            {
                if (_monitoring) return true;
                _monitoring = true;
                return false;
            }
        }
    }

    #endregion Methods

    public class LogReaderLineEventArgs : EventArgs
    {
        public string Line { get; set; }
    }
}
