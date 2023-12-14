using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Sample
{
    public class SerialPortEventArgs : EventArgs
    {
        public bool IsConnected { get; }
        public SerialPort Port { get; }

        public SerialPortEventArgs(bool isConnected, SerialPort port)
        {
            IsConnected = isConnected;
            Port = port;
        }
    }
}
