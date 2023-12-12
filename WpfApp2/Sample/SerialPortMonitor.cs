using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Sample
{
    using System;
    using System.Windows.Interop;

    public class SerialPortMonitor
    {
        private const int WM_DEVICECHANGE = 0x0219;
        private const int DBT_DEVICEARRIVAL = 0x8000;
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

        private HwndSource hwndSource;

        public event EventHandler<bool> SerialPortStateChanged;

        public SerialPortMonitor(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                throw new ArgumentException("Invalid IntPtr.", nameof(hwnd));
            }
            hwndSource = HwndSource.FromHwnd(hwnd);
            hwndSource.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DEVICECHANGE)
            {
                int eventType = wParam.ToInt32();
                if (eventType == DBT_DEVICEARRIVAL || eventType == DBT_DEVICEREMOVECOMPLETE)
                {
                    bool isConnected = (eventType == DBT_DEVICEARRIVAL);
                    SerialPortStateChanged?.Invoke(this, isConnected);
                }
            }

            return IntPtr.Zero;
        }

        //public void StopMonitoring()
        //{
        //    hwndSource.RemoveHook(WndProc);
        //    hwndSource.Dispose();
        //}
    }

}
