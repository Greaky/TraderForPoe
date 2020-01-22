using System;
using System.Collections.Generic;
using System.Text;

namespace TraderForPoe.Core.Loader
{
    public interface IResourceLocator
    {
        object GetResource(string key);
    }
}
