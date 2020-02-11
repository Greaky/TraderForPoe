using System;
using System.Collections.Generic;
using System.Windows;

namespace TraderForPoe.Core.Loader
{
    public class WindowViewLoaderService : IWindowViewLoaderService
    {
        private readonly Dictionary<Type, Type> _viewDictionary = new Dictionary<Type, Type>();

        private readonly IServiceProvider _serviceProvider;

        private readonly IWpfResourceLocator _wpfResourceLocator;

        public WindowViewLoaderService(IServiceProvider serviceProvider, IWpfResourceLocator wpfResourceLocator)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _wpfResourceLocator = wpfResourceLocator ?? throw new ArgumentNullException(nameof(wpfResourceLocator));
        }

        public void Register(Type viewmodel, Type view)
        {
            _viewDictionary.Add(viewmodel, view);
        }

        public void Show(Type viewmodel)
        {
            try
            {
                var windowType = _viewDictionary[viewmodel];
                var tmpWindows = (Window) _serviceProvider.GetService(windowType);
                tmpWindows.Show();
                tmpWindows.Activate();
            }
            catch (Exception e)
            {
                var error = $"Error while creating View in WindowsViewLoaderService {e.Message} Error";
            }
        }

        public void ShowSingle(Type viewmodel)
        {
             try
             {
                 foreach (Window item in _wpfResourceLocator.GetWindows())
                 {
                     if (item.GetType() != _viewDictionary[viewmodel]) continue;

                     item.WindowState = WindowState.Normal;
                     item.Activate();
                     return;
                 }
                 Show(viewmodel);
             }
             catch (Exception e)
             {
                 var error = $"Error while creating View in WindowsViewLoaderService {e.Message} Error";
             }
        }
    }
}
