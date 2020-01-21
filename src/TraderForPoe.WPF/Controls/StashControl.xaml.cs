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
            txt_StashName.Text = GetTItem.Stash;
        }

        public TradeObject GetTItem { get; set; }

    }



}
