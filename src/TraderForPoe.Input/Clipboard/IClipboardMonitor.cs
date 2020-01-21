using System;

namespace TraderForPoe.Input.Clipboard
{
    public interface IClipboardMonitor
    {
        void Start();
        void Stop();
        event EventHandler<ClipboardTextEventArgs> OnChange;
    }
}
