using TraderForPoe.WPF.ViewModel;

namespace TraderForPoe.WPF.Windows
{
    /// <summary>
    /// Interaktionslogik für StashGrid.xaml
    /// </summary>
    public partial class StashGrid
    {
        public StashGrid()
        {
            InitializeComponent();
            DataContext = StashGridViewModel.Instance;
        }
    }
}
