using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TraderForPoe.WPF.Classes
{
    public class NotActivatableWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int GwlExstyle = -20;

        private const int WsExNoactivate = 0x08000000;

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            //Set the window style to noactivate.

            var helper = new WindowInteropHelper(this);

            SetWindowLong(helper.Handle, GwlExstyle,

            GetWindowLong(helper.Handle, GwlExstyle) | WsExNoactivate);
        }
    }
}
