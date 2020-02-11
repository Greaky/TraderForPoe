using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaktionslogik für CustomTestCtrl.xaml
    /// </summary>
    public partial class CustomTestCtrl : UserControl
    {
        public static ObservableCollection<CustomTestCtrl> TradeControlList { get; set; } = new ObservableCollection<CustomTestCtrl>();

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

        public TradeObject TItem;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        //-----------------------------------------------------------------------------------------

        public CustomTestCtrl(TradeObject tItemArg)
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

            TradeControlList.Add(this);
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
            var currentTime = string.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, ts.Seconds);
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
        }

        private void SetupControls(TradeObject tItemArg)
        {
            switch (TItem.TradeType)
            {
                case TradeTypeEnum.BUY:
                    txt_Item.Foreground = Brushes.GreenYellow;
                    btn_InviteCustomer.Visibility = Visibility.Collapsed;
                    btn_StartTrade.Visibility = Visibility.Collapsed;
                    btn_SearchItem.Visibility = Visibility.Collapsed;
                    btn_AskIfInterested.Visibility = Visibility.Collapsed;
                    btn_SendBusyMessage.Visibility = Visibility.Collapsed;
                    btn_stash.Click -= ClickStashIsQuad;
                    break;
                case TradeTypeEnum.SELL:
                    txt_Item.Foreground = Brushes.OrangeRed;
                    btn_VisitCustomerHideout.Visibility = Visibility.Collapsed;
                    btn_VisitOwnHideout.Visibility = Visibility.Collapsed;
                    btn_SendWhisperAgain.Visibility = Visibility.Collapsed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            txt_Customer.Text = TItem.Customer;
            txt_Item.Text = TItem.Item.ItemAsString;
            spnl_CurrencyRatio.Visibility = Visibility.Collapsed;
            try
            {
                txt_Price.Text = TItem.Item.Price.ItemAsString;
            }
            catch (Exception)
            { }

            if (string.IsNullOrEmpty(TItem.Item.Price.ItemAsString))
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
                btn_stash.ToolTip = TItem.Position.ToString();
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

            img_Currency.Source = TItem.Item.Price.Image;
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
            var iSim = new InputSimulator();

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
            if (TItem.TradeType == TradeTypeEnum.SELL && TItem.Stash != "")
            {
                _stashGridHighlight.AddButton(TItem);
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

            iSim.Keyboard.TextEntry(TItem.Item.ItemAsString);

            iSim = null;
        }

        private void ClickSendWhisperAgain(object sender, RoutedEventArgs e)
        {
            var whisper = TItem.Whisper;
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
            _stashGridHighlight.RemoveStashControl(TItem);
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

            if (TItem.TradeType == TradeTypeEnum.SELL && Settings.Default.CloseItemAfterTrade)
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
            if (TItem.TradeType == TradeTypeEnum.BUY && Settings.Default.CloseItemAfterTrade)
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
            if (TItem.TradeType == TradeTypeEnum.SELL)
            {
                _stashGridHighlight.AddButton(TItem);
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
            SendInputToPoe("@" + TItem.Customer + " Hi, are you still interested in " + TItem.Item + " for " + TItem.Item.Price.ItemAsString + "?");
        }

        public static void RemoveTiCfromList(CustomTestCtrl tradeItemControl)
        {
            _intNumberItems--;

            if (_intNumberItems == 3)
            {
                OnEqualThreeItems();
            }
        }

        protected static void OnMoreThanThreeItems()
        {
            MoreThanThreeItems?.Invoke(typeof(CustomTestCtrl), EventArgs.Empty);
        }

        protected static void OnEqualThreeItems()
        {
            EqualThreeItems?.Invoke(typeof(CustomTestCtrl), EventArgs.Empty);
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
