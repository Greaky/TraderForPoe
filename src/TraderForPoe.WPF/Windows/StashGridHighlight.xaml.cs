using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TraderForPoe.WPF.Classes;
using TraderForPoe.WPF.Controls;
using TraderForPoe.WPF.Properties;

namespace TraderForPoe.WPF.Windows
{
    /// <summary>
    /// Interaktionslogik für StashGridHighlight.xaml
    /// </summary>
    public partial class StashGridHighlight : Window
    {
        // Needed to prevent window getting focus
        const int GWL_EXSTYLE = -20;
        const int WS_EX_NOACTIVATE = 134217728;
        const int LSFW_LOCK = 1;

        readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        readonly IntPtr _poeHandle = WinApi.FindWindow("POEWindowClass", "Path of Exile");

        // Needed to determine if poe window location changed
        private readonly IntPtr _hWinEventHook;
        protected WinApi.WinEventDelegate WinEventDelegate;
        readonly uint _poeProcessId;

        public StashGridHighlight()
        {
            // Verify that POE is a running process.
            if (_poeHandle == IntPtr.Zero)
            {
                MessageBox.Show("Path of Exile is not running.");
                return;
            }

            InitializeComponent();

            Loaded += (object sender, RoutedEventArgs e) => SetNoActiveWindow();

            UpdateLocationAndSize();

            WinEventDelegate = new WinApi.WinEventDelegate(WinEventCallback);

            try
            {
                if (_poeHandle == IntPtr.Zero) return;

                var targetThreadId = WinApi.GetWindowThread(_poeHandle);
                UnsafeNativeMethods.GetWindowThreadProcessId(_poeHandle, out _poeProcessId);
                _hWinEventHook = WinApi.WinEventHookOne(WinApi.SwehEvents.EVENT_OBJECT_LOCATIONCHANGE,
                    WinEventDelegate,
                    _poeProcessId,
                    targetThreadId);

            }
            catch (Exception ex)
            {
                //ErrorManager.Logger(this, this.InitializeComponent(), ex.HResult, ex.Data, DateTime.Now);
                throw ex;
            }
        }

        protected void WinEventCallback(IntPtr hWinEventHook, WinApi.SwehEvents eventType, IntPtr hWnd, WinApi.SwehObjectId idObject, long idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hWnd == _poeHandle && eventType == WinApi.SwehEvents.EVENT_OBJECT_LOCATIONCHANGE && idObject == (WinApi.SwehObjectId)WinApi.SwehChildidSelf)
            {
                // Occurs when POE window is moved or size changed
                UpdateLocationAndSize();
            }
        }

        private void UpdateLocationAndSize()
        {

            WinApi.GetClientRect(_poeHandle, out var clientRect);
            WinApi.GetWindowRect(_poeHandle, out var windowRect);

            double borderSize = (windowRect.Right - (windowRect.Left + clientRect.Right)) / 2;
            var titleBarSize = (windowRect.Bottom - (windowRect.Top + clientRect.Bottom)) - borderSize;

            var x = windowRect.Left + borderSize;
            var y = windowRect.Top + titleBarSize;
            double h = clientRect.Bottom;
            double w = clientRect.Right;

            Row1.Height = new GridLength(h * 0.02);
            Row2.Height = new GridLength(h * 0.035);
            Row3.Height = GridLength.Auto;

            if ((w / h) < 1.35)
            {
                Top = y + (h * 0.094);

                Left = x + (w * 0.011);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);
            }
            else if ((w / h) < 1.9)
            {
                Top = y + (h * 0.094);

                Left = x + (w * 0.0088);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);
            }
            else if ((w / h) < 1.99)
            {
                Top = y + (h * 0.097);

                Left = x + (w * 0.008);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);
            }

            else if ((w / h) < 2.3)
            {

                Top = y + (h * 0.097);

                Left = x + (w * 0.007);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);
            }

            else if ((w / h) < 2.58)
            {
                Top = y + (h * 0.096);

                Left = x + (w * 0.006);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);

            }

            else if ((w / h) < 2.8)
            {
                Top = y + (h * 0.0959);

                Left = x + (w * 0.0055);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);

            }

            else if ((w / h) < 3.1)
            {
                Top = y + (h * 0.0955);

                Left = x + (w * 0.005);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);
            }

            else
            {
                Top = y + (h * 0.094);

                Left = x + (w * 0.004);

                Height = (h * 0.585) + Row1.Height.Value + Row2.Height.Value;

                Width = (h * 0.585);
            }

            FrontCanvas.Width = Width;

            FrontCanvas.Height = Height;
        }

        public void AddButton(TradeObject tItemArgs)
        {
            UpdateLocationAndSize();

            if (ContainsItem(tItemArgs) || string.IsNullOrEmpty(tItemArgs.Stash)) return;

            var sCtrl = new StashControl(tItemArgs);

            sCtrl.MouseEnter += MouseEnterDrawRectangle;

            SpnlButtons.Children.Add(sCtrl);

        }

        private bool ContainsItem(TradeObject tItemArgs)
        {
            return SpnlButtons.Children.Cast<StashControl>()
                .Any(item => item.GetTItem.Item.ItemAsString == tItemArgs.Item.ItemAsString
                             && item.GetTItem.Customer == tItemArgs.Customer
                             && item.GetTItem.Item.Price.ItemAsString == tItemArgs.Item.Price.ItemAsString
                             && item.GetTItem.Position == tItemArgs.Position);
        }

        private void SetNoActiveWindow()
        {
            var helper = new WindowInteropHelper(this);
            WinApi.SetWindowLong(helper.Handle, GWL_EXSTYLE, WS_EX_NOACTIVATE);
            WinApi.LockSetForegroundWindow(LSFW_LOCK);
        }

        private void StartDispatcherTimer()
        {
            _dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 5);
            _dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            FrontCanvas.Children.Clear();
        }

        private void MouseEnterDrawRectangle(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Clear canvas before drawing new rectangle
            FrontCanvas.Children.Clear();

            // Start timer to clear canvas after some time
            StartDispatcherTimer();

            // Cast sender as StashControl
            var stashControl = (StashControl)sender;

            var x = stashControl.GetTItem.Position.X;

            var y = stashControl.GetTItem.Position.Y;

            // Nomber of stash columns
            var nbrRectStash = 12;

            // Set rectangle size by dividing canvas by number of columns
            var rectDimensionX = ((FrontCanvas.Width) / 12);

            // Check if stash is quad. If true divide rectangle size and multiply number of columns by 2
            if ((x > 12 && x < 25) || (y > 12 && y < 25))
            {
                rectDimensionX = rectDimensionX / 2;
                nbrRectStash = nbrRectStash * 2;
            }
            else
            {
                foreach (var item in Settings.Default.QuadStash)
                {
                    if (item == stashControl.GetTItem.Stash)
                    {
                        rectDimensionX = rectDimensionX / 2;
                        nbrRectStash = nbrRectStash * 2;
                    }
                }
            }


            for (var iX = 1; iX <= nbrRectStash; iX++)
            {
                // Create the rectangle
                var rectangleHighlight = new Rectangle()
                {
                    Width = rectDimensionX,
                    Height = rectDimensionX,
                    Stroke = Brushes.Red,
                    StrokeThickness = 1,
                };

                if (iX == x)
                {

                    for (var iY = 1; iY <= nbrRectStash; iY++)
                    {
                        if (iY == y)
                        {
                            FrontCanvas.Children.Add(rectangleHighlight);
                            Canvas.SetLeft(rectangleHighlight, (iX - 1) * rectDimensionX);
                            Canvas.SetTop(rectangleHighlight, (iY - 1) * rectDimensionX);
                        }

                    }
                }

            }
        }

        public void ClearCanvas()
        {
            FrontCanvas.Children.Clear();
        }

        internal void RemoveStashControl(TradeObject tItemArgs)
        {
            foreach (StashControl item in SpnlButtons.Children)
            {
                if (item.GetTItem.Item == tItemArgs.Item && item.GetTItem.Customer == tItemArgs.Customer && item.GetTItem.Item.Price.ItemAsString == tItemArgs.Item.Price.ItemAsString)
                {
                    SpnlButtons.Children.Remove(item);
                    break; //important step
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Unhook event when closing
            WinApi.WinEventUnhook(_hWinEventHook);
        }
    }
}
