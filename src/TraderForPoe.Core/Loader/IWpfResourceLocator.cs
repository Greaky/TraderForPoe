using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TraderForPoe.Core.Loader
{
    public interface IWpfResourceLocator
    {
        object GetResource(string key);
        WindowCollection GetWindows();

        void Shutdown();
    }
}
