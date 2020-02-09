using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TraderForPoe.Core.Loader
{
    public class ResourceLocator : IResourceLocator
    {

        private readonly Application _application;

        public ResourceLocator() : this(Application.Current)
        {
        }

        public ResourceLocator(Application application)
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
    }
}
