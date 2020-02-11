using System;
using System.Diagnostics;
using System.Globalization;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Properties;
using TraderForPoe.WPF.Windows;

namespace TraderForPoe.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für TradeItemPopup.xaml
    /// </summary>
    public partial class TradeItemControl : UserControl
    {

        private static int _intNumberItems = 0;

        public static event EventHandler MoreThanThreeItems;

        public static event EventHandler EqualThreeItems;


        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private static StashGridHighlight _stashGridHighlight;

        public TradeItemOld TItem;

        readonly Stopwatch _stopwatch = new Stopwatch();

        //-----------------------------------------------------------------------------------------

        public TradeItemControl(TradeItemOld tItemArg)
        {
            TItem = tItemArg;

            _intNumberItems++;

            if (_intNumberItems > 3)
            {
                OnMoreThanThreeItems();
            }

            InitializeComponent();


            LoadSettings();

            SetupControls(TItem);

            StartAnimatioin();

            OpenStashGridHighlightWindow();

            StartTime();
        }


        //-----------------------------------------------------------------------------------------

        private void StartTime()
        {

            var dispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };

            dispatcherTimer.Tick += Timer_Tick;

            _stopwatch.Start();
            dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var ts = _stopwatch.Elapsed;
            var currentTime = $"{(int) ts.TotalMinutes:00}:{ts.Seconds:00}";
            txt_Time.Text = currentTime;
        }

        private void OpenStashGridHighlightWindow()
        {
            if (_stashGridHighlight == null)
            {
                _stashGridHighlight = new StashGridHighlight();
            }
            else if (_stashGridHighlight.Visibility == Visibility.Collapsed || _stashGridHighlight.Visibility == Visibility.Hidden)
            {
                _stashGridHighlight.Visibility = Visibility.Visible;
            }

            _stashGridHighlight.Show();
        }

        private void LoadSettings()
        {
            if (Settings.Default.CollapsedItems)
            {
                CollapseExpandItem();
            }

            if (!Settings.Default.PlayNotificationSound) return;

            var player = new SoundPlayer(WPF.Resources.notification);
            player.Play();
            player.Dispose();
        }

        private void SetupControls(TradeItemOld tItemArg)
        {
            if (!string.IsNullOrEmpty(TItem.Item))
            {
                btn_Item.ToolTip = TItem.Item;
            }

            if (!string.IsNullOrEmpty(TItem.Price))
            {
                btn_Price.ToolTip = TItem.Price;
            }

            switch (TItem.TradeType)
            {
                case TradeItemOld.TradeTypes.BUY:
                    txt_Item.Foreground = Brushes.GreenYellow;
                    btn_InviteCustomer.Visibility = Visibility.Collapsed;
                    btn_StartTrade.Visibility = Visibility.Collapsed;
                    btn_SearchItem.Visibility = Visibility.Collapsed;
                    btn_AskIfInterested.Visibility = Visibility.Collapsed;
                    btn_SendBusyMessage.Visibility = Visibility.Collapsed;
                    btn_stash.Click -= ClickStashIsQuad;
                    break;
                case TradeItemOld.TradeTypes.SELL:
                    txt_Item.Foreground = Brushes.OrangeRed;
                    btn_VisitCustomerHideout.Visibility = Visibility.Collapsed;
                    btn_VisitOwnHideout.Visibility = Visibility.Collapsed;
                    btn_SendWhisperAgain.Visibility = Visibility.Collapsed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            if (TItem.ItemIsCurrency)
            {

                txt_Customer.Text = TItem.Customer;
                txt_Item.Text = TItem.ItemCurrencyQuant;
                img_ItemCurrency.Source = TItem.ItemCurrencyBitmap;
                txt_League.Text = TItem.League;

                try
                {
                    txt_Price.Text = TradeItemOld.ExtractPointNumberFromString(TItem.Price);
                }
                catch (Exception)
                { }


                if (TItem.Stash == null)
                {
                    btn_stash.Visibility = Visibility.Collapsed;
                    btn_stash.Visibility = Visibility.Hidden;
                }
                else
                {
                    txt_Stash.Text = TItem.Stash;
                    btn_stash.ToolTip = TItem.StashPosition.ToString();
                }

                double dblPrice = 0;
                double dblItemPrice = 0;
                try
                {
                    dblPrice = double.Parse(TradeItemOld.ExtractPointNumberFromString(TItem.Price));
                    dblItemPrice = double.Parse(TradeItemOld.ExtractPointNumberFromString(TItem.ItemCurrencyQuant));
                }
                catch (Exception) { }



                if ((dblItemPrice / dblPrice) >= 1)
                {
                    txt_Ratio1.Text = "1";
                    img_Ratio1.Source = TItem.PriceCurrencyBitmap;

                    txt_Ratio2.Text = (dblItemPrice / dblPrice).ToString(CultureInfo.InvariantCulture);
                    img_Ratio2.Source = TItem.ItemCurrencyBitmap;
                }
                else
                {
                    txt_Ratio1.Text = "1";
                    img_Ratio1.Source = TItem.ItemCurrencyBitmap;

                    txt_Ratio2.Text = (dblPrice / dblItemPrice).ToString(CultureInfo.InvariantCulture);
                    img_Ratio2.Source = TItem.PriceCurrencyBitmap;
                }

                btn_stash.Visibility = Visibility.Collapsed;
                btn_stash.Visibility = Visibility.Hidden;
            }





            else
            {
                txt_Customer.Text = TItem.Customer;
                txt_Item.Text = TItem.Item;
                spnl_CurrencyRatio.Visibility = Visibility.Collapsed;
                try
                {
                    txt_Price.Text = TradeItemOld.ExtractPointNumberFromString(TItem.Price);
                }
                catch (Exception)
                { }

                if (string.IsNullOrEmpty(TItem.Price))
                {
                    txt_Price.Text = "--";
                }

                txt_League.Text = TItem.League;

                if (TItem.Stash == null)
                {
                    btn_stash.Visibility = Visibility.Collapsed;
                    btn_stash.Visibility = Visibility.Hidden;
                }
                else
                {
                    txt_Stash.Text = TItem.Stash;
                    btn_stash.ToolTip = TItem.StashPosition.ToString();
                }
            }





            if (string.IsNullOrEmpty(TItem.AdditionalText))
            {
                btn_AdditionalText.Visibility = Visibility.Collapsed;
                btn_AdditionalText.Visibility = Visibility.Hidden;
            }
            else
            {
                txt_AdditionalText.Text = TItem.AdditionalText;
            }

            img_Currency.Source = TItem.PriceCurrencyBitmap;

        }

        private void StartAnimatioin()
        {
            var dAnim = new DoubleAnimation()
            {
                From = 0.0,
                To = Settings.Default.ControlOpacity,
                Duration = new Duration(TimeSpan.FromMilliseconds(180))
            };

            BeginAnimation(OpacityProperty, dAnim);
        }

        private void SendInputToPoe(string input)
        {
            // Get a handle to POE. The window class and window name were obtained using the Spy++ tool.
            var poeHandle = FindWindow("POEWindowClass", "Path of Exile");

            // Verify that POE is a running process.
            if (poeHandle == IntPtr.Zero)
            {
                // Show message box if POE is not running
                MessageBox.Show("Path of Exile is not running.");
                return;
            }

            var iSim = new InputSimulator();

            // Need to press ALT because the SetForegroundWindow sometimes does not work
            iSim.Keyboard.KeyPress(VirtualKeyCode.MENU);

            // Make POE the foreground application and send input
            SetForegroundWindow(poeHandle);

            iSim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            // Send the input
            iSim.Keyboard.TextEntry(input);

            // Send RETURN
            iSim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            iSim = null;

        }

        private void ClickToCollapseExpandItem(object sender, RoutedEventArgs e)
        {
            CollapseExpandItem();
        }

        private void CollapseExpandItem()
        {
            if (grd_SecondRow.Visibility == Visibility.Visible)
            {
                grd_SecondRow.Visibility = Visibility.Hidden;
                grd_SecondRow.Visibility = Visibility.Collapsed;
                btn_CollExp.Text = "⏷";
            }
            else
            {
                grd_SecondRow.Visibility = Visibility.Visible;
                btn_CollExp.Text = "⏶";
            }
        }

        private void ClickWhisperCustomer(object sender, RoutedEventArgs e)
        {
            // Get a handle to POE. The window class and window name were obtained using the Spy++ tool.
            var poeHandle = FindWindow("POEWindowClass", "Path of Exile");

            // Verify that POE is a running process.
            if (poeHandle == IntPtr.Zero)
            {
                MessageBox.Show("Path of Exile is not running.");
                return;
            }
            InputSimulator iSim = new InputSimulator();

            // Need to press ALT because the SetForegroundWindow sometimes does not work
            iSim.Keyboard.KeyPress(VirtualKeyCode.MENU);

            // Make POE the foreground application and send input
            SetForegroundWindow(poeHandle);

            iSim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            
            iSim.Keyboard.TextEntry("@" + TItem.Customer + " ");

            iSim = null;
        }

        private void ClickInviteCustomer(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("/invite " + TItem.Customer);
            if (TItem.TradeType == TradeItemOld.TradeTypes.SELL && TItem.Stash != "")
            {
                //stashGridHighlight.AddButton(tItem);
            }
        }

        private void ClickTradeInvite(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("/tradewith " + TItem.Customer);
        }

        private void ClickSearchItem(object sender, RoutedEventArgs e)
        {
            // Get a handle to POE. The window class and window name were obtained using the Spy++ tool.
            var poeHandle = FindWindow("POEWindowClass", "Path of Exile");

            // Verify that POE is a running process.
            if (poeHandle == IntPtr.Zero)
            {
                MessageBox.Show("Path of Exile is not running.");
                return;
            }

            InputSimulator iSim = new InputSimulator();

            // Need to press ALT because the SetForegroundWindow sometimes does not work
            iSim.Keyboard.KeyPress(VirtualKeyCode.MENU);

            // Make POE the foreground application and send input
            SetForegroundWindow(poeHandle);

            iSim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_F);

            iSim.Keyboard.Sleep(500);

            iSim.Keyboard.TextEntry(TItem.Item);

            iSim = null;
        }

        private void ClickSendWhisperAgain(object sender, RoutedEventArgs e)
        {
            var whisper = TItem.WhisperMessage;
            whisper = whisper.Remove(0, whisper.IndexOf(": ", StringComparison.Ordinal) + 2);
            SendInputToPoe("@" + TItem.Customer + " " + whisper);
        }

        private void ClickRemoveItem(object sender, RoutedEventArgs e)
        {
            RemoveItem();
        }

        private void RemoveItem()
        {
            ((StackPanel)Parent).Children.Remove(this);
            //stashGridHighlight.RemoveStashControl(tItem);
            RemoveTiCfromList(this);
            //TradeItem.RemoveItemFromList(tItem);
            _stashGridHighlight.ClearCanvas();
        }

        private void ClickThanksForTrade(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("@" + TItem.Customer + " " + Settings.Default.ThankYouWhisper);

            if (Settings.Default.CloseItemAfterThankYouWhisper)
            {
                RemoveItem();
            }
        }

        private void ClickKickMyself(object sender, RoutedEventArgs e)
        {

            SendInputToPoe("/kick " + Settings.Default.PlayerName);

            if (TItem.TradeType == TradeItemOld.TradeTypes.SELL && Settings.Default.CloseItemAfterTrade)
            {
                RemoveItem();
            }
        }

        private void ClickWhoisCustomer(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("/whois " + TItem.Customer);
        }

        private void ClickToWikiLeague(object sender, RoutedEventArgs e)
        {
            Process.Start("https://pathofexile.gamepedia.com/" + TItem.League);
        }

        private void ClickVisitHideout(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("/hideout " + TItem.Customer);
        }

        private void ClickVisitOwnHideout(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("/hideout");
            if (TItem.TradeType == TradeItemOld.TradeTypes.BUY && Settings.Default.CloseItemAfterTrade)
            {
                RemoveItem();
            }
        }

        private void ClickStashIsQuad(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show("Is this a quad stash?", "Quad stash?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (dialogResult)
            {
                case MessageBoxResult.Yes:
                {
                    if (!Settings.Default.QuadStash.Contains(TItem.Stash))
                    {
                        Settings.Default.QuadStash.Add(TItem.Stash);
                        Settings.Default.Save();
                    }

                    break;
                }
                case MessageBoxResult.No:
                {
                    if (Settings.Default.QuadStash.Contains(TItem.Stash))
                    {
                        Settings.Default.QuadStash.Remove(TItem.Stash);
                        Settings.Default.Save();
                    }

                    break;
                }
            }
        }

        private void ClickWhisperCustomerBusy(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("@" + TItem.Customer + " " + Settings.Default.ImBusyWhisper);

            if (Settings.Default.CloseItemAfterImBusyWhisper)
            {
                RemoveItem();
            }
        }

        private void ClickShowStashOverlay(object sender, RoutedEventArgs e)
        {
            if (TItem.TradeType == TradeItemOld.TradeTypes.SELL)
            {
                //stashGridHighlight.AddButton(tItem);
            }
        }

        private void ClickCustomWhisper1(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("@" + TItem.Customer + " " + Settings.Default.CustomWhisper1);

            if (Settings.Default.CloseItemAfterCustomWhisper1)
            {
                RemoveItem();
            }
        }

        private void ClickCustomWhisper2(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("@" + TItem.Customer + " " + Settings.Default.CustomWhisper2);

            if (Settings.Default.CloseItemAfterCustomWhisper2)
            {
                RemoveItem();
            }
        }

        private void ClickAskIfInterested(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("@" + TItem.Customer + " Hi, are you still interested in " + TItem.Item + " for " + TItem.Price + "?");
        }

        public static void RemoveTiCfromList(TradeItemControl tradeItemControl)
        {
            _intNumberItems--;

            if (_intNumberItems == 3)
            {
                OnEqualThreeItems();
            }
        }

        protected static void OnMoreThanThreeItems()
        {
            MoreThanThreeItems?.Invoke(typeof(TradeItemControl), EventArgs.Empty);
        }

        protected static void OnEqualThreeItems()
        {
            EqualThreeItems?.Invoke(typeof(TradeItemControl), EventArgs.Empty);
        }

        public void CustomerJoined()
        {
            txt_Customer.Foreground = Brushes.GreenYellow;
        }
        public void CustomerLeft()
        {
            txt_Customer.Foreground = Brushes.White;
        }

        private void RightClickKickCustomer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SendInputToPoe("/kick " + TItem.Customer);

            if (Settings.Default.CloseItemAfterTrade)
            {
                RemoveItem();
            }
        }

        private void ClickCustomWhisper3(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("@" + TItem.Customer + " " + Settings.Default.CustomWhisper3);

            if (Settings.Default.CloseItemAfterCustomWhisper3)
            {
                RemoveItem();
            }
        }

        private void ClickCustomWhisper4(object sender, RoutedEventArgs e)
        {
            SendInputToPoe("@" + TItem.Customer + " " + Settings.Default.CustomWhisper4);

            if (Settings.Default.CloseItemAfterCustomWhisper4)
            {
                RemoveItem();
            }
        }
    }


}
