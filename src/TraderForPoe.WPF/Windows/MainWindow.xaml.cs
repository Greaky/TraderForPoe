using System.Text.RegularExpressions;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.ViewModel;

namespace TraderForPoe.WPF.Windows
{
    public partial class MainWindow : NotActivatableWindow
    {
        private Regex customerJoinedRegEx = new Regex(".* : (.*) has joined the area");

        private Regex customerLeftRegEx = new Regex(".* : (.*) has left the area");

        public static ServiceProvider ServiceProvider;


        public MainWindow()
        {
            InitializeComponent();

            var mainWindowViewModel = ServiceProvider.GetService<MainWindowViewModel>();

            DataContext = mainWindowViewModel;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.Save();
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void NotActivatableWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (mainTabControl != null)
            {
                if (e.Delta < 0)
                {
                    if (mainTabControl.SelectedIndex + 1 < mainTabControl.Items.Count)
                        mainTabControl.SelectedItem = mainTabControl.Items[mainTabControl.SelectedIndex + 1];
                }
                else
                {
                    if (mainTabControl.SelectedIndex - 1 > -1)
                        mainTabControl.SelectedItem = mainTabControl.Items[mainTabControl.SelectedIndex - 1];
                }
            }
        }
    }
}
