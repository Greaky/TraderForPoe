using System.Windows;
using TraderForPoe.ViewModel;

namespace TraderForPoe.Windows
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
