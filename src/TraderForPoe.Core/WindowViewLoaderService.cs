using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace TraderForPoe.Core
{
    public class WindowViewLoaderService : IWindowViewLoaderService
    {
        private Dictionary<Type, Type> _viewDictionary = new Dictionary<Type, Type>();

        private readonly IServiceProvider serviceProvider;

        public WindowViewLoaderService(IServiceProvider ServiceProvider)
        {
            serviceProvider = ServiceProvider;
        }

        public void Register(Type viewmodel, Type view)
        {
            _viewDictionary.Add(viewmodel, view);
        }

        public void Show(Type viewmodel)
        {
            try
            {
                var tmpWindows = (Window) serviceProvider.GetService(_viewDictionary[viewmodel]);
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
                     if (item.GetType() == _viewDictionary[viewmodel])
                     {
                         item.WindowState = WindowState.Normal;
                         item.Activate();
                         return;
                     }
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
