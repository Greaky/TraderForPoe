using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Controls;

namespace TraderForPoe.WPF.Windows.MainWindow.v1
{
    /// <summary>
    /// Logique d'interaction pour MainOld.xaml
    /// </summary>
    public partial class MainOld : NotActivatableWindow
    {
        public MainOld()
        {
            InitializeComponent();

            var tradeObject = new TradeObject("@To Labooooooo: Hi, I would like to buy your Cybil's Paw Thresher Claw listed for 8 jewellers in Bestiary (stash tab \"~b / o 0 alt\"; position: left 23, top 8)");
            CustomTestCtrl uc = new CustomTestCtrl(tradeObject);
            stk_MainPnl.Children.Add(uc);
        }

        private void ClickCollapseExpandMainwindow(object sender, RoutedEventArgs e)
        {
            var mainWindowCollapsed = false;
            if (mainWindowCollapsed == false)
            {
                btn_collapseMainWindow.Width = this.Width;
                btn_collapseMainWindow.Content = "⏷";
                //stk_MainPnl.Visibility = Visibility.Collapsed;
                mainWindowCollapsed = true;
            }
            else
            {
                btn_collapseMainWindow.Width = btn_collapseMainWindow.MinWidth;
                btn_collapseMainWindow.Content = "⏶";
                //stk_MainPnl.Visibility = Visibility.Visible;
                mainWindowCollapsed = false;
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                this.DragMove();
        }

    }
}
