using System;

namespace TraderForPoe.Core.Loader
{
    public interface IWindowViewLoaderService
    {

        void Register(Type viewmodel, Type view);
        void Show(Type viewmodel);
        void ShowSingle(Type viewmodel);
    }
}
