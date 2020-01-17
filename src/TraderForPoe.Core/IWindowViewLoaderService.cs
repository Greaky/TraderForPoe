using System;
using System.Collections.Generic;
using System.Text;

namespace TraderForPoe.Core
{
    public interface IWindowViewLoaderService
    {

        void Register(Type viewmodel, Type view);
        void Show(Type viewmodel);
        void ShowSingle(Type viewmodel);
    }
}
