using System;
using System.Windows.Threading;
using TraderForPoe.Classes;
using TraderForPoe.ViewModel.Base;

namespace TraderForPoe.ViewModel
{
    internal class StashGridViewModel : ViewModelBase
    {
        private static StashGridViewModel _instance;

        private readonly DispatcherTimer _timerPoeLocation = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(500) };

        public StashGridViewModel()
        {
            _timerPoeLocation.Tick += TimerPoeLocation_Tick;
            _timerPoeLocation.Start();
        }

        private void TimerPoeLocation_Tick(object sender, EventArgs e)
        {
            WinApi.GetClientRect(Poe.GetHandle(), out var clientRect);
            WinApi.GetWindowRect(Poe.GetHandle(), out WinApi.Rect windowRect);

            double borderSize = (windowRect.Right - (windowRect.Left + clientRect.Right)) / 2;
            var titleBarSize = (windowRect.Bottom - (windowRect.Top + clientRect.Bottom)) - borderSize;

            var x = windowRect.Left + borderSize;
            var y = windowRect.Top + titleBarSize;
            double h = clientRect.Bottom;
            double w = clientRect.Right;
            
            if ((w / h) < 1.35)
            {
                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.011);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);
            }

            else if ((w / h) < 1.9)
            {
                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.0081);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);
            }
            else if ((w / h) < 1.99)
            {
                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.008);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);
            }

            else if ((w / h) < 2.3)
            {

                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.0075);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);
            }

            else if ((w / h) < 2.58)
            {
                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.0065);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);

            }

            else if ((w / h) < 2.8)
            {
                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.0055);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);

            }

            else if ((w / h) < 3.1)
            {
                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.0055);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);
            }

            else
            {
                WindowLocationTop = y + (h * 0.1500);

                WindowLocationLeft = x + (w * 0.005);

                WindowHeight = (h * 0.585);

                WindowWidth = (h * 0.585);
            }


        }

        public static StashGridViewModel Instance => _instance ?? (_instance = new StashGridViewModel());

        private double _windowLocationLeft;
        public double WindowLocationLeft
        {
            get => _windowLocationLeft;
            set
            {
                if (_windowLocationLeft != value)
                {
                    _windowLocationLeft = value;
                    OnPropertyChanged();
                }
            }
        }




        private double _windowLocationTop;
        public double WindowLocationTop
        {
            get => _windowLocationTop;
            set
            {
                if (_windowLocationTop != value)
                {
                    _windowLocationTop = value;
                    OnPropertyChanged();
                }
            }
        }



        private double _windowHeight;
        public double WindowHeight
        {
            get => _windowHeight;
            set
            {
                if (_windowHeight != value)
                {
                    _windowHeight = value;
                    OnPropertyChanged();
                }
            }
        }



        private double _windowWidth;
        public double WindowWidth
        {
            get => _windowWidth;
            set
            {
                if (_windowWidth != value)
                {
                    _windowWidth = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
