using System;
using System.Collections.Generic;
using System.Text;

namespace TraderForPoe.Core.Reader
{
    public interface ILogReader
    {
        void Start();
        void Stop();

        event EventHandler<LogReaderLineEventArgs> OnLineAddition;

    }
}
