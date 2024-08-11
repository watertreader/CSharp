using System.Net.Sockets;
using System.Net;
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
using System.Security.RightsManagement;

namespace Button2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public struct ErrorMsg
        {
            public ErrorMsg()
            {
                byte header1 = 0xFD;
                byte header2 = 0x07;
                byte FCC_type;
                byte FCC_error;
                short checksum;
            }
            // define structure of message
        }

        public MainWindow()
        {
            InitializeComponent();

            var btn1 = new Button { Content = "Send_Fault" };
        }

        String GetFCCValue()
        {
            return FCC.Text;
        }

        String GetErrorType()
        {
            return FCC.Text;
        }


        void OnClick(object sender, RoutedEventArgs e)
        {
            ErrorMsg error;

            // get value from combo box fcc type
            String s = GetFCCValue();

            if (FCC_Label.Foreground == Brushes.Yellow)
            {
                FCC_Label.Foreground = Brushes.Green;
            }
            else if(FCC_Label.Foreground == Brushes.Green)
            {
                FCC_Label.Foreground = Brushes.Red;
            }
            else
            {
                FCC_Label.Foreground = Brushes.Yellow;
            }

            //Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //IPAddress serverAddr = IPAddress.Parse("192.168.2.255");

            //IPEndPoint endPoint = new IPEndPoint(serverAddr, 11000);

            //string text = "Hello";
            //byte[] send_buffer = Encoding.ASCII.GetBytes(text);

            //sock.SendTo(send_buffer, endPoint);
        }

        public void SendMessage(byte[] msg)
        {
            //var data = Encoding.Default.GetBytes(message);
            using (var udpClient = new UdpClient(AddressFamily.InterNetwork))
            {
                var address = IPAddress.Parse("224.100.0.1");
                var ipEndPoint = new IPEndPoint(address, 8088);
                udpClient.JoinMulticastGroup(address);
                udpClient.Send(msg, msg.Length, ipEndPoint);
                udpClient.Close();
            }
        }
    }
}