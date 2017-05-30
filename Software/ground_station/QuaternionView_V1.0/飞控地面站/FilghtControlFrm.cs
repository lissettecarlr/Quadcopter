using QuaternionView;
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
using System.Windows.Media.Media3D;

namespace 飞控地面站
{
    public partial class FilghtControlFrm : Form
    {
        private View3D view3D;
        private double angle = 0;
        public int SendPort = 9001;
        UdpClient udpClient;
        private string msg = string.Empty;

        public FilghtControlFrm()
        {
            InitializeComponent();
            view3D = new View3D();
            view3D.Model3DPath = @"Model\PilotFish_UAV.obj";
            elementHost1.Child = view3D;

        }

        public Quaternion ZRotation(double angleInRadians)
        {
            double halfAngleInRadians = (angleInRadians * 0.5);
            Quaternion q = new Quaternion(0, 0, Math.Sin(halfAngleInRadians), Math.Cos(halfAngleInRadians));
            return q;
        }


        /// <summary>
        /// 接收数据
        /// </summary>
        private void ReceiveMsg()
        {
            Console.WriteLine("县城启动");
            UdpClient UCR;
            try
            {
                UCR = new UdpClient(SendPort);
                //取0可接收其它端口的数据，这里上面new UdpClient(SendPort)的设计好象没有起到应有的作用。
                IPEndPoint Sender = new IPEndPoint(IPAddress.Any, SendPort);
                while (true)
                {
                    try
                    {
                        //Receive处于等待，但不影响发送
                        byte[] data = new byte[1024 * 4];
                        data = UCR.Receive(ref Sender);
                        if (data.Length == 0)
                        {
                            break;
                        }
                        DataService ds = new DataService();
                        if (ds.Check_FC_TO_HC_Head(data) == true)
                        {
                            if (ds.CheckSum(data) == data[data.Length - 1])
                            {
                                byte funcode = data[2];
                                switch (funcode)
                                {
                                    case 0x00:

                                        break;
                                    case 0x01:
                                        //修改
                                        this.Invoke(new Action(() =>
                                        {
                                            this.lab_roll.Text = (((data[4] << 8) + data[5]) / 100).ToString();
                                            lab_pitch.Text = (((data[6] << 8) + data[7]) / 100).ToString();
                                            lab_yaw.Text = (((data[8] << 8) + data[9]) / 100).ToString();
                                            if (data[15] == 0x01)
                                            {
                                                lab_lock.Text = "解锁";
                                            }
                                            else if (data[15] == 0x00)
                                            {
                                                lab_lock.Text = "加锁";
                                            }
                                        }));
                                        
                                       
                                        break;
                                    case 0x02:
                                        lab_acc_x.Text = (((data[4] << 8) + data[5]) / 100).ToString();
                                        lab_acc_y.Text = (((data[6] << 8) + data[7]) / 100).ToString();
                                        lab_acc_z.Text = (((data[8] << 8) + data[9]) / 100).ToString();

                                        

                                        lab_gyrd_x.Text = (((data[10] << 8) + data[11]) / 100).ToString();
                                        lab_gyrd_y.Text = (((data[12] << 8) + data[13]) / 100).ToString();
                                        lab_gyrd_z.Text = (((data[14] << 8) + data[15]) / 100).ToString();

                                        lab_mag_x.Text = (((data[16] << 8) + data[17]) / 100).ToString();
                                        lab_mag_y.Text = (((data[18] << 8) + data[19]) / 100).ToString();
                                        lab_mag_z.Text = (((data[20] << 8) + data[21]) / 100).ToString();
                                        this.FilghtControlFrm_Load(null,null);//修改
                                        break;
                                    case 0x03:
                                        tb_thr.Value = (int)(((data[4] << 8) + data[5]) / 100);
                                        tb_yaw.Value = (int)(((data[6] << 8) + data[7]) / 100);
                                        tb_roll.Value = (int)(((data[8] << 8) + data[9]) / 100);
                                        tb_pitch.Value = (int)(((data[10] << 8) + data[11]) / 100);
                                        break;
                                    case 0x06:

                                        break;
                                    case 0x0A:
                                        txt_pid_1_rol.Text = (((data[4] << 8) + data[5]) / 100).ToString();
                                        txt_pid_1_rol.Text = (((data[6] << 8) + data[7]) / 100).ToString();
                                        txt_pid_1_rol.Text = (((data[8] << 8) + data[9]) / 100).ToString();

                                        txt_pid_2_yaw.Text = (((data[10] << 8) + data[11]) / 100).ToString();
                                        txt_pid_2_yaw.Text = (((data[12] << 8) + data[13]) / 100).ToString();
                                        txt_pid_2_yaw.Text = (((data[14] << 8) + data[15]) / 100).ToString();

                                        txt_pid_3_pitch.Text = (((data[16] << 8) + data[17]) / 100).ToString();
                                        txt_pid_3_pitch.Text = (((data[18] << 8) + data[19]) / 100).ToString();
                                        txt_pid_3_pitch.Text = (((data[20] << 8) + data[21]) / 100).ToString();
                                        break;
                                    case 0x0B:
                                        txt_pid_4_rol.Text = (((data[4] << 8) + data[5]) / 100).ToString();
                                        txt_pid_4_rol.Text = (((data[6] << 8) + data[7]) / 100).ToString();
                                        txt_pid_4_rol.Text = (((data[8] << 8) + data[9]) / 100).ToString();

                                        txt_pid_5_yaw.Text = (((data[10] << 8) + data[11]) / 100).ToString();
                                        txt_pid_5_yaw.Text = (((data[12] << 8) + data[13]) / 100).ToString();
                                        txt_pid_5_yaw.Text = (((data[14] << 8) + data[15]) / 100).ToString();

                                        txt_pid_6_pitch.Text = (((data[16] << 8) + data[17]) / 100).ToString();
                                        txt_pid_6_pitch.Text = (((data[18] << 8) + data[19]) / 100).ToString();
                                        txt_pid_6_pitch.Text = (((data[20] << 8) + data[21]) / 100).ToString();
                                        break;
                                    case 0x0C:
                                        txt_pid_7_rol.Text = (((data[4] << 8) + data[5]) / 100).ToString();
                                        txt_pid_7_rol.Text = (((data[6] << 8) + data[7]) / 100).ToString();
                                        txt_pid_7_rol.Text = (((data[8] << 8) + data[9]) / 100).ToString();

                                        txt_pid_8_yaw.Text = (((data[10] << 8) + data[11]) / 100).ToString();
                                        txt_pid_8_yaw.Text = (((data[12] << 8) + data[13]) / 100).ToString();
                                        txt_pid_8_yaw.Text = (((data[14] << 8) + data[15]) / 100).ToString();

                                        txt_pid_9_pitch.Text = (((data[16] << 8) + data[17]) / 100).ToString();
                                        txt_pid_9_pitch.Text = (((data[18] << 8) + data[19]) / 100).ToString();
                                        txt_pid_9_pitch.Text = (((data[20] << 8) + data[21]) / 100).ToString();
                                        break;
                                    case 0xEF:

                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                lab_msg.Text = "和校验错误！";
                                lab_msg.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lab_msg.Text = "数据报错误！";
                            lab_msg.ForeColor = Color.Red;
                        }
                    }
                    catch (Exception s)
                    {
                        UCR.Close();
                        Console.WriteLine(s.ToString());
                    }

                }
                UCR.Close();
            }
            catch
            { }
            UCR = null;
           
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sendBytes"></param>
        private void SendMsg(byte[] sendBytes)
        {
            udpClient = new UdpClient(9000);
            udpClient.Connect("192.168.4.2", 9000);
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length);
                lab_msg.Text = msg+"数据已发送！";
                lab_msg.ForeColor = Color.Green;
                udpClient.Close();
            }
            catch (Exception ee)
            {
                lab_msg.Text = msg+"数据发送失败！";
                lab_msg.ForeColor = Color.Red;
                udpClient.Close();
                Console.WriteLine(ee.ToString());
            }
            //udpClient.Close();
        }

        /// <summary>
        /// 进入程序，开启线程，不停接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilghtControlFrm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            timer1.Enabled = true;
            cb_pid.SelectedIndex = 0;
            Initial();
            Thread th = new Thread(new ThreadStart(ReceiveMsg));
            th.IsBackground = true;
            th.Start();
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            
            Console.WriteLine("=============================================");

        }

        private void Initial()
        {
            tb_thr.Minimum = 0;
            tb_thr.Maximum = 100;

            tb_roll.Minimum = 0;
            tb_roll.Maximum = 100;

            tb_pitch.Minimum = 0;
            tb_pitch.Maximum = 100;

            tb_yaw.Minimum = 0;
            tb_yaw.Maximum = 100;
        }


        #region 上位机=========》飞控

        #region 遥控器
        private void tb_thr_Scroll(object sender, EventArgs e)
        {
            txt_thr.Text = tb_thr.Value.ToString();
        }

        private void tb_yaw_Scroll(object sender, EventArgs e)
        {
            txt_yaw.Text = tb_yaw.Value.ToString();
        }

        private void tb_roll_Scroll(object sender, EventArgs e)
        {
            txt_roll.Text = tb_roll.Value.ToString();
        }

        private void tb_pitch_Scroll(object sender, EventArgs e)
        {
            txt_pitch.Text = tb_pitch.Value.ToString();
        }

        private void txt_thr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                double s = double.Parse(txt_thr.Text);
                tb_thr.Value = (int)s;
            }
        }

        private void txt_yaw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                double s = double.Parse(txt_yaw.Text);
                tb_yaw.Value = (int)s;
            }
        }

        private void txt_roll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                double s = double.Parse(txt_roll.Text);
                tb_roll.Value = (int)s;
            }
        }

        private void txt_pitch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                double s = double.Parse(txt_pitch.Text);
                tb_pitch.Value = (int)s;
            }
        }
        /// <summary>
        /// 发送遥控器数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendRemote_Click(object sender, EventArgs e)
        {
            msg = "遥控器";
            try
            {
                Int16 thr = (Int16)(double.Parse(txt_thr.Text) * 1000);

                Int16 yaw = (Int16)(double.Parse(txt_yaw.Text) * 1000);

                Int16 roll = (Int16)(double.Parse(txt_roll.Text) * 1000);

                Int16 pitch = (Int16)(double.Parse(txt_pitch.Text) * 1000);

                byte thr_L = (byte)(thr & 0xff);//低八位
                byte thr_H = (byte)(thr >> 8);//高八位
                byte yaw_L = (byte)(yaw & 0xff);
                byte yaw_H = (byte)(yaw >> 8);
                byte roll_L = (byte)(roll & 0xff);
                byte roll_H = (byte)(roll >> 8);
                byte pitch_L = (byte)(pitch & 0xff);
                byte pitch_H = (byte)(pitch >> 8);
                byte[] data = new byte[8] { thr_H, thr_L, yaw_H, yaw_L, roll_H, roll_L, pitch_H, pitch_L };
                Protocol p = new Protocol();
                byte[] buffer = new byte[25];
                buffer= p.Pro_SendRemoteData(data);
                Console.WriteLine(BitConverter.ToString(buffer));
                SendMsg(buffer);
                
            }
            catch(Exception es)
            {
                lab_msg.Text = msg+"数据发送失败！";
                lab_msg.ForeColor = Color.Red;
                Console.WriteLine(es.ToString());
                udpClient.Close();
            }

        }

        #endregion

        #region 数据校准
        private void btnGyrd_Click(object sender, EventArgs e)
        {
            msg = "陀螺仪校准";
            Protocol p = new Protocol();
            SendMsg(p.Pro_GYRD());
            Console.WriteLine(BitConverter.ToString(p.Pro_GYRD()));

        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            msg = "加速度校准";
            Protocol p = new Protocol();
            SendMsg(p.Pro_ACC());
            Console.WriteLine(BitConverter.ToString(p.Pro_ACC()));
        }

        private void btnMag_Click(object sender, EventArgs e)
        {
            msg = "磁力计校准";
            Protocol p = new Protocol();
            SendMsg(p.Pro_MAG());
            Console.WriteLine(BitConverter.ToString(p.Pro_MAG()));
        }
        #endregion

        #region PID的发送与读取
        private void btnSendPID_Click(object sender, EventArgs e)
        {
            switch (cb_pid.SelectedIndex)
            {
                case 0:
                    PID_1();
                    break;
                case 1:
                    PID_2();
                    break;
                case 2:
                    PID_3();
                    break;

                default:
                    PID_1();
                    break;
            }

        }

        private void btnGetPID_Click(object sender, EventArgs e)
        {
            msg = "读取PID请求";
            Protocol p = new Protocol();
            SendMsg(p.Pro_GetPID());
            Console.WriteLine(BitConverter.ToString(p.Pro_GetPID()));

        }

        private void PID_1()
        {
            msg = "PID_1";
            try
            {
                Int16 pid_1_rol = (Int16)(double.Parse(txt_pid_1_rol.Text) * 1000);
                Int16 pid_1_yaw = (Int16)(double.Parse(txt_pid_1_yaw.Text) * 1000);
                Int16 pid_1_pitch = (Int16)(double.Parse(txt_pid_1_pitch.Text) * 1000);

                Int16 pid_2_rol = (Int16)(double.Parse(txt_pid_2_rol.Text) * 1000);
                Int16 pid_2_yaw = (Int16)(double.Parse(txt_pid_2_yaw.Text) * 1000);
                Int16 pid_2_pitch = (Int16)(double.Parse(txt_pid_2_pitch.Text) * 1000);

                Int16 pid_3_rol = (Int16)(double.Parse(txt_pid_3_rol.Text) * 1000);
                Int16 pid_3_yaw = (Int16)(double.Parse(txt_pid_3_yaw.Text) * 1000);
                Int16 pid_3_pitch = (Int16)(double.Parse(txt_pid_3_pitch.Text) * 1000);

                byte pid_1_rol_L = (byte)(pid_1_rol & 0xff);//低八位
                byte pid_1_rol_H = (byte)(pid_1_rol >> 8);//高八位
                byte pid_1_yaw_L = (byte)(pid_1_yaw & 0xff);
                byte pid_1_yaw_H = (byte)(pid_1_yaw >> 8);
                byte pid_1_pitch_L = (byte)(pid_1_pitch & 0xff);
                byte pid_1_pitch_H = (byte)(pid_1_pitch >> 8);

                byte pid_2_rol_L = (byte)(pid_2_rol & 0xff);
                byte pid_2_rol_H = (byte)(pid_2_rol >> 8);
                byte pid_2_yaw_L = (byte)(pid_2_yaw & 0xff);
                byte pid_2_yaw_H = (byte)(pid_2_yaw >> 8);
                byte pid_2_pitch_L = (byte)(pid_2_pitch & 0xff);
                byte pid_2_pitch_H = (byte)(pid_2_pitch >> 8);

                byte pid_3_rol_L = (byte)(pid_3_rol & 0xff);
                byte pid_3_rol_H = (byte)(pid_3_rol >> 8);
                byte pid_3_yaw_L = (byte)(pid_3_yaw & 0xff);
                byte pid_3_yaw_H = (byte)(pid_3_yaw >> 8);
                byte pid_3_pitch_L = (byte)(pid_3_pitch & 0xff);
                byte pid_3_pitch_H = (byte)(pid_3_pitch >> 8);

                byte[] data = new byte[18] { pid_1_rol_H, pid_1_rol_L, pid_1_yaw_H, pid_1_yaw_L, pid_1_pitch_H, pid_1_pitch_L, pid_2_rol_H, pid_2_rol_L, pid_2_yaw_H, pid_2_yaw_L, pid_2_pitch_H, pid_2_pitch_L, pid_3_rol_H, pid_3_rol_L, pid_3_yaw_H, pid_3_yaw_L, pid_3_pitch_H, pid_3_pitch_L };
                Protocol p = new Protocol();
                byte[] buffer = p.Pro_SendPID_1(data);
                Console.WriteLine(BitConverter.ToString(buffer));
                SendMsg(buffer);
                
            }
            catch
            {
                udpClient.Close();
                lab_msg.Text = "数据发送失败！";
            }
        }

        private void PID_2()
        {
            msg = "PID_2";
            try
            {
                Int16 pid_4_rol = (Int16)(double.Parse(txt_pid_4_rol.Text) * 1000);
                Int16 pid_4_yaw = (Int16)(double.Parse(txt_pid_4_yaw.Text) * 1000);
                Int16 pid_4_pitch = (Int16)(double.Parse(txt_pid_4_pitch.Text) * 1000);

                Int16 pid_5_rol = (Int16)(double.Parse(txt_pid_5_rol.Text) * 1000);
                Int16 pid_5_yaw = (Int16)(double.Parse(txt_pid_5_yaw.Text) * 1000);
                Int16 pid_5_pitch = (Int16)(double.Parse(txt_pid_5_pitch.Text) * 1000);

                Int16 pid_6_rol = (Int16)(double.Parse(txt_pid_6_rol.Text) * 1000);
                Int16 pid_6_yaw = (Int16)(double.Parse(txt_pid_6_yaw.Text) * 1000);
                Int16 pid_6_pitch = (Int16)(double.Parse(txt_pid_6_pitch.Text) * 1000);

                byte pid_4_rol_L = (byte)(pid_4_rol & 0xff);//低八位
                byte pid_4_rol_H = (byte)(pid_4_rol >> 8);//高八位
                byte pid_4_yaw_L = (byte)(pid_4_yaw & 0xff);
                byte pid_4_yaw_H = (byte)(pid_4_yaw >> 8);
                byte pid_4_pitch_L = (byte)(pid_4_pitch & 0xff);
                byte pid_4_pitch_H = (byte)(pid_4_pitch >> 8);

                byte pid_5_rol_L = (byte)(pid_5_rol & 0xff);
                byte pid_5_rol_H = (byte)(pid_5_rol >> 8);
                byte pid_5_yaw_L = (byte)(pid_5_yaw & 0xff);
                byte pid_5_yaw_H = (byte)(pid_5_yaw >> 8);
                byte pid_5_pitch_L = (byte)(pid_5_pitch & 0xff);
                byte pid_5_pitch_H = (byte)(pid_5_pitch >> 8);

                byte pid_6_rol_L = (byte)(pid_6_rol & 0xff);
                byte pid_6_rol_H = (byte)(pid_6_rol >> 8);
                byte pid_6_yaw_L = (byte)(pid_6_yaw & 0xff);
                byte pid_6_yaw_H = (byte)(pid_6_yaw >> 8);
                byte pid_6_pitch_L = (byte)(pid_6_pitch & 0xff);
                byte pid_6_pitch_H = (byte)(pid_6_pitch >> 8);

                byte[] data = new byte[18] { pid_4_rol_H, pid_4_rol_L, pid_4_yaw_H, pid_4_yaw_L, pid_4_pitch_H, pid_4_pitch_L, pid_5_rol_H, pid_5_rol_L, pid_5_yaw_H, pid_5_yaw_L, pid_5_pitch_H, pid_5_pitch_L, pid_6_rol_H, pid_6_rol_L, pid_6_yaw_H, pid_6_yaw_L, pid_6_pitch_H, pid_6_pitch_L };
                Protocol p = new Protocol();
                byte[] buffer = p.Pro_SendPID_2(data);
                Console.WriteLine(BitConverter.ToString(buffer));
                SendMsg(buffer);
            }
            catch
            {
                udpClient.Close();
                lab_msg.Text = "数据发送失败！";
            }
        }

        private void PID_3()
        {
            msg = "PID_3";
            try
            {
                Int16 pid_7_rol = (Int16)(double.Parse(txt_pid_7_rol.Text) * 1000);
                Int16 pid_7_yaw = (Int16)(double.Parse(txt_pid_7_yaw.Text) * 1000);
                Int16 pid_7_pitch = (Int16)(double.Parse(txt_pid_7_pitch.Text) * 1000);

                Int16 pid_8_rol = (Int16)(double.Parse(txt_pid_8_rol.Text) * 1000);
                Int16 pid_8_yaw = (Int16)(double.Parse(txt_pid_8_yaw.Text) * 1000);
                Int16 pid_8_pitch = (Int16)(double.Parse(txt_pid_8_pitch.Text) * 1000);

                Int16 pid_9_rol = (Int16)(double.Parse(txt_pid_9_rol.Text) * 1000);
                Int16 pid_9_yaw = (Int16)(double.Parse(txt_pid_9_yaw.Text) * 1000);
                Int16 pid_9_pitch = (Int16)(double.Parse(txt_pid_9_pitch.Text) * 1000);

                byte pid_7_rol_L = (byte)(pid_7_rol & 0xff);//低八位
                byte pid_7_rol_H = (byte)(pid_7_rol >> 8);//高八位
                byte pid_7_yaw_L = (byte)(pid_7_yaw & 0xff);
                byte pid_7_yaw_H = (byte)(pid_7_yaw >> 8);
                byte pid_7_pitch_L = (byte)(pid_7_pitch & 0xff);
                byte pid_7_pitch_H = (byte)(pid_7_pitch >> 8);

                byte pid_8_rol_L = (byte)(pid_8_rol & 0xff);
                byte pid_8_rol_H = (byte)(pid_8_rol >> 8);
                byte pid_8_yaw_L = (byte)(pid_8_yaw & 0xff);
                byte pid_8_yaw_H = (byte)(pid_8_yaw >> 8);
                byte pid_8_pitch_L = (byte)(pid_8_pitch & 0xff);
                byte pid_8_pitch_H = (byte)(pid_8_pitch >> 8);

                byte pid_9_rol_L = (byte)(pid_9_rol & 0xff);
                byte pid_9_rol_H = (byte)(pid_9_rol >> 8);
                byte pid_9_yaw_L = (byte)(pid_9_yaw & 0xff);
                byte pid_9_yaw_H = (byte)(pid_9_yaw >> 8);
                byte pid_9_pitch_L = (byte)(pid_9_pitch & 0xff);
                byte pid_9_pitch_H = (byte)(pid_9_pitch >> 8);

                byte[] data = new byte[18] { pid_7_rol_H, pid_7_rol_L, pid_7_yaw_H, pid_7_yaw_L, pid_7_pitch_H, pid_7_pitch_L, pid_8_rol_H, pid_8_rol_L, pid_8_yaw_H, pid_8_yaw_L, pid_8_pitch_H, pid_8_pitch_L, pid_9_rol_H, pid_9_rol_L, pid_9_yaw_H, pid_9_yaw_L, pid_9_pitch_H, pid_9_pitch_L };
                Protocol p = new Protocol();
                byte[] buffer = p.Pro_SendPID_3(data);
                Console.WriteLine(BitConverter.ToString(buffer));
                SendMsg(buffer);
            }
            catch
            {
                udpClient.Close();
                lab_msg.Text = "数据发送失败！";
            }
        }
        #endregion

        #endregion


        #region 定义事件，实时刷新界面
        //委托
        public delegate void dDownloadList(Label lab, string msg);
        //事件
        public event dDownloadList onDownLoadList;

        public void frmServer_onDownLoadList(Label lab,string msg)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new FilghtControlFrm.dDownloadList(frmServer_onDownLoadList), new object[] { msg });
            }
            else
            {
                lab.Text = msg;
                Application.DoEvents();
            }
        }


        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            angle += 1;
            Quaternion q = ZRotation(angle * Math.PI / 180.0);
            view3D.SetQuaternion(q.X, q.Y, q.Z, q.W);

        }
    }
}
