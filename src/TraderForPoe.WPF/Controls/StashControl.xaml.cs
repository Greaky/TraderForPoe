using System.Windows.Controls;
using TraderForPoe.WPF.Classes;

namespace TraderForPoe.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für StashControl.xaml
    /// </summary>
    public partial class StashControl : UserControl
    {
        public StashControl(TradeObject tItemArgs)
        {
            InitializeComponent();
            GetTItem = tItemArgs;
            TxtStashName.Text = GetTItem.Stash;
        }

        public TradeObject GetTItem { get; set; }

    }



}
