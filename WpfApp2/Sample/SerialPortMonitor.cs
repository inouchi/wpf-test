using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Sample
{
    using System;
    using System.IO.Ports;
    using System.Windows.Interop;

    public class SerialPortMonitor
    {
        private const int WM_DEVICECHANGE = 0x0219;
        private const int DBT_DEVICEARRIVAL = 0x8000;
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

        private SerialPort port;
        private HwndSource hwndSource;

        public event EventHandler<bool> SerialPortStateChanged;

        public SerialPortMonitor(ref SerialPort port, IntPtr hwnd)
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
            if (msg != WM_DEVICECHANGE)
            {
                return IntPtr.Zero;
            }

            int eventType = wParam.ToInt32();

            // USB接続時
            if (eventType == DBT_DEVICEARRIVAL)
            {
                if (port == null)
                {
                    port = new SerialPort("COM1");
                    SerialPortStateChanged.Invoke(this, true);
                }
            }

            // USB切断時
            if (eventType == DBT_DEVICEREMOVECOMPLETE)
            {
                if (port != null)
                {
                    try
                    {
                        port.Write("foo");
                    }
                    catch
                    {
                        port.Close();
                        port = null;
                        SerialPortStateChanged.Invoke(this, false);
                    }
                }
            }

            return IntPtr.Zero;
        }

        public void StopMonitoring()
        {
            hwndSource.RemoveHook(WndProc);
            hwndSource.Dispose();
        }
    }

}
