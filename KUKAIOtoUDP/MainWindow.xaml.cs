using KUKARoboter.KRCModel.CrossKrc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KUKAIOtoUDP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint endPoint;
        string databuffer = "";

        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void Start_Stop_Click(object sender, RoutedEventArgs e)
        {
            if (Timer.IsEnabled)
            {
                Timer.Stop();
            }
            else
            {
                IPAddress serverAddr = IPAddress.Parse(this.ip.Text);
                endPoint = new IPEndPoint(serverAddr, Convert.ToInt32(this.port.Text));


                Timer.Tick += sendUDP_Tick;
                Timer.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(this.CycleTime.Text));
                Timer.Start();
            }
        }

        private void sendUDP_Tick(object sender, EventArgs e)
        {
            string data = "";

            try
            {
                //string xval = KrcVariableCommands.ShowVar("$POS_ACT.X");
                //string yval = KrcVariableCommands.ShowVar("$POS_ACT.Y");
                //string zval = KrcVariableCommands.ShowVar("$POS_ACT.Z");
                //string aval = KrcVariableCommands.ShowVar("$POS_ACT.A");
                //string bval = KrcVariableCommands.ShowVar("$POS_ACT.B");
                //string cval = KrcVariableCommands.ShowVar("$POS_ACT.C");

                //data = "{ \"xval\":" + xval + ",\"yval\":" + yval + ",\"zval\":" + zval + ",\"aval\":" + aval + ",\"bval\":" + bval + ",\"cval\":" + cval + "}";
                //datadisplay.Content = data + " at " + DateTime.Now.ToLongTimeString();

                string xval = KrcVariableCommands.ShowVar("$AXIS_ACT.A1");
                string yval = KrcVariableCommands.ShowVar("$AXIS_ACT.A2");
                string zval = KrcVariableCommands.ShowVar("$AXIS_ACT.A3");
                string aval = KrcVariableCommands.ShowVar("$AXIS_ACT.A4");
                string bval = KrcVariableCommands.ShowVar("$AXIS_ACT.A5");
                string cval = KrcVariableCommands.ShowVar("$AXIS_ACT.A6");

                data = xval + "," + yval + "," + zval + "," + aval + "," + bval + "," + cval;

                //data = "{ \"xval\":" + xval + ",\"yval\":" + yval + ",\"zval\":" + zval + ",\"aval\":" + aval + ",\"bval\":" + bval + ",\"cval\":" + cval + "}";
                datadisplay.Content = data + " at " + DateTime.Now.ToLongTimeString();
            }
            catch (Exception)
            {
                datadisplay.Content = "Failed to get variable. Are tool/base set?";
            }

            if (!data.Equals(databuffer) & !data.Equals(""))
            {
                byte[] send_buffer = Encoding.ASCII.GetBytes(data);
                socket.SendTo(send_buffer, endPoint);
                databuffer = data;
            }
        }
    }
}
