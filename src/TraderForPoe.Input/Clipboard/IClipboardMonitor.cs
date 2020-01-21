using System;

namespace TraderForPoe.Input.Clipboard
{
    public interface IClipboardMonitor
    {
        public void Start();
        public void Stop();
        public event EventHandler<ClipboardTextEventArgs> OnChange;
    }
}
