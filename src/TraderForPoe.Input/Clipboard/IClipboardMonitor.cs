using System;
using System.Collections.Generic;
using System.Text;

namespace TraderForPoe.Input
{
    public interface IClipboardMonitor
    {
        public void Start();
        public void Stop();
        public event EventHandler<ClipboardTextEventArgs> OnChange;
    }
}
