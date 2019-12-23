using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Threading;
using TraderForPoe.Properties;

namespace TraderForPoe.Classes
{
    public static class LogReader
    {
        #region Fields

        public static ObservableCollection<string> Lines { get; } = new ObservableCollection<string>();
        private static readonly string Delimiter = "\n";
        private static readonly string Path = Settings.Default.PathToClientTxt;
        private static readonly DispatcherTimer Timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
        private static string _buffer;
        private static bool _monitoring;
        private static long _size = new FileInfo(Path).Length;

        #endregion Fields

        #region MyRegion

        static LogReader()
        {
            Path = Settings.Default.PathToClientTxt;
            Timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
            Timer.Tick += Check;
            Start();
        }

        #endregion MyRegion

        #region Events

        public static event EventHandler<LogReaderLineEventArgs> OnLineAddition;

        #endregion Events

        #region Methods

        public static void Start()
        {
            if (Timer.IsEnabled)
            {
                return;
            }

            Timer.Start();
        }

        public static void Stop()
        {
            Timer.Stop();
        }

        private static void Check(object sender, EventArgs e)
        {
            if (!StartMonitoring()) return;

            var newSize = new FileInfo(Path).Length;

            if (_size >= newSize) return;

            using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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

            lock (Timer) _monitoring = false;
        }

        private static bool StartMonitoring()
        {
            lock (Timer)
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