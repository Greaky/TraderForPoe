using System.Reflection;
using System.Windows;
using TraderForPoe.WPF.ViewModel;

namespace TraderForPoe.WPF.Windows
{
    /// <summary>
    /// Interaktionslogik für About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About(IAboutViewModel aboutViewModel)
        {
            if (aboutViewModel is null)
            {
                throw new System.ArgumentNullException(nameof(aboutViewModel));
            }

            InitializeComponent();
            DataContext = aboutViewModel;
            appName.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void OnRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
