using System.Text.RegularExpressions;
using System.Windows.Input;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.ViewModel.MainWindow;

namespace TraderForPoe.WPF.Windows.MainWindow
{
    public partial class MainWindow : IMainWindow
    {
        private Regex _customerJoinedRegEx = new Regex(".* : (.*) has joined the area");

        private Regex _customerLeftRegEx = new Regex(".* : (.*) has left the area");



        public MainWindow(IMainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
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
            if (MainTabControl == null) return;

            if (e.Delta < 0)
            {
                if (MainTabControl.SelectedIndex + 1 < MainTabControl.Items.Count)
                    MainTabControl.SelectedItem = MainTabControl.Items[MainTabControl.SelectedIndex + 1];
            }
            else
            {
                if (MainTabControl.SelectedIndex - 1 > -1)
                    MainTabControl.SelectedItem = MainTabControl.Items[MainTabControl.SelectedIndex - 1];
            }
        }
    }
}
