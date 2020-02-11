using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TraderForPoe.Core.Loader
{
    public class WpfResourceLocator : IWpfResourceLocator
    {

        private readonly Application _application;

        public WpfResourceLocator(Application application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            _application = application;
        }

        public object GetResource(string key)
        {
            return _application.TryFindResource(key);
        }

        public WindowCollection GetWindows()
        {
            return _application.Windows;
        }

        public void Shutdown()
        {
             _application.Shutdown();
        }

    }
}
