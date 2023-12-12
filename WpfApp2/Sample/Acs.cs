using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Sample
{
    public class A
    {
        public A(ref SerialPort port)
        {
            if (port != null)
            {
                port.Close();
                port.Dispose();
            }
        }
    }
}
