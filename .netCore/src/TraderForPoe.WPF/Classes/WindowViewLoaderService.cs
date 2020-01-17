using System;
using System.Collections.Generic;
using System.Windows;

namespace TraderForPoe.Classes
{
    static class WindowViewLoaderService
    {
        private static Dictionary<Type, Type> _viewDictionary = new Dictionary<Type, Type>();

        public static void Register(Type viewmodel, Type view)
        {
            _viewDictionary.Add(viewmodel, view);
        }

        public static void Show(Type viewmodel)
        {
            try
            {
                var tmpWindows = (Window)Activator.CreateInstance(_viewDictionary[viewmodel]);
                tmpWindows.Show();
                tmpWindows.Activate();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while creating View in WindowsViewLoaderService\n" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void ShowSingle(Type viewmodel)
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
                var tmpWindow = (Window)Activator.CreateInstance(_viewDictionary[viewmodel]);
                tmpWindow.Show();
                tmpWindow.Activate();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while creating View in WindowsViewLoaderService\n" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
