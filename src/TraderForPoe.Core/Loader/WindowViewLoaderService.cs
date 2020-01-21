using System;
using System.Collections.Generic;
using System.Windows;

namespace TraderForPoe.Core.Loader
{
    public class WindowViewLoaderService : IWindowViewLoaderService
    {
        private readonly Dictionary<Type, Type> _viewDictionary = new Dictionary<Type, Type>();

        private readonly IServiceProvider _serviceProvider;

        public WindowViewLoaderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Register(Type viewmodel, Type view)
        {
            _viewDictionary.Add(viewmodel, view);
        }

        public void Show(Type viewmodel)
        {
            try
            {
                var tmpWindows = (Window) _serviceProvider.GetService(_viewDictionary[viewmodel]);
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
                 foreach (Window item in Application.Current.Windows)
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
