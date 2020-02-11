using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TraderForPoe.Input.Clipboard
{
    public sealed partial class ClipboardMonitor : IDisposable, IClipboardMonitor
    {

        /// <summary>
        /// Occurs when the clipboard content changes and the content is text.
        /// </summary>
        public event EventHandler<ClipboardTextEventArgs> OnChange;

        private readonly HwndSource _hwndSource = new HwndSource(0, 0, 0, 0, 0, 0, 0, null, NativeMethods.HwndMessage);

        public ClipboardMonitor()
        {
            _hwndSource.AddHook(WndProc);
            NativeMethods.AddClipboardFormatListener(_hwndSource.Handle);
        }

        public void Dispose()
        {
            NativeMethods.RemoveClipboardFormatListener(_hwndSource.Handle);
            _hwndSource.RemoveHook(WndProc);
            _hwndSource.Dispose();
        }

        public void Stop()
        {
            _hwndSource.RemoveHook(WndProc);
            NativeMethods.RemoveClipboardFormatListener(_hwndSource.Handle);
        }

        public void Start()
        {
            _hwndSource.AddHook(WndProc);
            NativeMethods.AddClipboardFormatListener(_hwndSource.Handle);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WmClipboardupdate)
            {
                if (System.Windows.Clipboard.ContainsText())
                {

                    for (var i = 0; i < 10; i++)
                    {
                        try
                        {
                            OnChange?.Invoke(this, new ClipboardTextEventArgs { Line = System.Windows.Clipboard.GetText(TextDataFormat.UnicodeText) });
                            break;
                        }
                        catch (COMException ex)
                        {
                            //fix for OpenClipboard Failed (Exception from HRESULT: 0x800401D0 (CLIPBRD_E_CANT_OPEN))
                            //https://stackoverflow.com/questions/12769264/openclipboard-failed-when-copy-pasting-data-from-wpf-datagrid
                            //https://stackoverflow.com/questions/68666/clipbrd-e-cant-open-error-when-setting-the-clipboard-from-net
                            if (ex.ErrorCode == -2147221040)
                                System.Threading.Thread.Sleep(10);
                            else
                                throw new Exception("Unable to get Clipboard text. Message: \n" + ex.Message);
                        }
                    }


                }
            }

            return IntPtr.Zero;
        }


    }
}
