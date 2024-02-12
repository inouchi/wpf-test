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
using System.Management;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using Wpf.Ui.Common;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort port;
        private SerialPortMonitor sm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr mainWindowHandle = new WindowInteropHelper(this).Handle;
            sm = new SerialPortMonitor(port, mainWindowHandle);
            sm.SerialPortStateChanged += UsbStateChanged;
        }

        private void CardAction_Click(object sender, RoutedEventArgs e)
        {
            RootSnackbar.Timeout = 3000;
            var snackbarService = new SnackbarService();
            snackbarService.SetSnackbarControl(RootSnackbar);
            snackbarService.Show("本日はありがとうございます。\n1番 田中様\n○○号車");
            
        }

        private void UsbStateChanged(object sender, SerialPortEventArgs args)
        {
            if (args.IsConnected)
            {
                MessageBox.Show("USB connected.");
                port = args.Port;
                Console.Write(port);
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
