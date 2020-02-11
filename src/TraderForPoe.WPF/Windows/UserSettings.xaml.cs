using System.Windows;
using Microsoft.Win32;
using TraderForPoe.WPF.ViewModel;
using TraderForPoe.WPF.ViewModel.UserSettings;

namespace TraderForPoe.WPF.Windows
{
    /// <summary>
    /// Interaktionslogik für UserSettings.xaml
    /// </summary>
    public partial class UserSettings : Window
    {
        public UserSettings(IUserSettingsViewModel userSettingsViewModel)
        {
            InitializeComponent();
            DataContext = userSettingsViewModel;
        }
        
        //TODO Implement mvvm 
        private void Click_SearchFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() != true) return;

            TxtPathToClientTxt.Text = openFileDialog.FileName;
            TxtPathToClientTxt.Focus();
            TxtPathToClientTxt.CaretIndex = TxtPathToClientTxt.Text.Length;
        }

    }
}
