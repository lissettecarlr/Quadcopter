using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 飞控地面站
{
    public partial class TestFrm : Form
    {
        public TestFrm()
        {
            InitializeComponent();
        }

        private void TestFrm_Load(object sender, EventArgs e)
        {

            Control.CheckForIllegalCrossThreadCalls = false;
        }
        Socket client;

        /// <summary>
        /// 接收发送给本机ip对应端口号的数据报
        /// </summary>
        public void ReciveMsg()
        {
            Console.WriteLine("客户端");
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                byte[] buffer = new byte[1024];
                int length = client.ReceiveFrom(buffer, ref point);//接收数据报
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                Console.WriteLine(point.ToString() + message);
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse("192.168.4.2"), 9001));
            Thread t = new Thread(ReciveMsg);
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte s = 0x45;
            richTextBox1.Text = Convert.ToString(s, 10);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sendBytes"></param>
        private void SendMsg(byte[] sendBytes)
        {
            UdpClient udpClient = new UdpClient(int.Parse(textBox2.Text));
            udpClient.Connect(textBox1.Text, int.Parse(textBox2.Text));
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length);
                //lab_msg.Text = msg + "数据已发送！";
                //lab_msg.ForeColor = Color.Green;
                udpClient.Close();
            }
            catch (Exception ee)
            {
                //lab_msg.Text = msg + "数据发送失败！";
                //lab_msg.ForeColor = Color.Red;
                udpClient.Close();
                Console.WriteLine(ee.ToString());
            }
            udpClient.Close();
        }
    }
}

