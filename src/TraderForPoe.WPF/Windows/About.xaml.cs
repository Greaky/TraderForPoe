using System.Reflection;
using System.Windows;
using TraderForPoe.WPF.ViewModel;
using TraderForPoe.WPF.ViewModel.About;

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
            AppName.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void OnRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
