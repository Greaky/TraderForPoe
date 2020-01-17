using System.Text.RegularExpressions;
using System.Windows.Input;
using TraderForPoe.Classes;
using TraderForPoe.ViewModel;
using TraderForPoe.WPF.Properties;

namespace TraderForPoe
{
    public partial class MainWindow : NotActivatableWindow
    {
        private Regex customerJoinedRegEx = new Regex(".* : (.*) has joined the area");

        private Regex customerLeftRegEx = new Regex(".* : (.*) has left the area");

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
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