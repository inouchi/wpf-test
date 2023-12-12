using ControlzEx.Standard;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Sample;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPortMonitor sm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr mainWindowHandle = new WindowInteropHelper(this).Handle;
            sm = new SerialPortMonitor(mainWindowHandle);
            sm.SerialPortStateChanged += UsbStateChanged;
        }

        private void UsbStateChanged(object sender, bool isUsbConnected)
        {
            if (isUsbConnected)
            {
                MessageBox.Show("USB connected.");
            }
            else
            {
                MessageBox.Show("USB disconnected.");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SerialPort port = new SerialPort("COM1");
            A a = new A(ref port);
            var foo = port.IsOpen;
            MessageBox.Show("ok");
        }
    }
}
