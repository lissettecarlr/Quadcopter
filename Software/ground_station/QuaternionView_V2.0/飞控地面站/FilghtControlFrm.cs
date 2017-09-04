using QuaternionView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
        public int ReceivePort = 9000;
        public byte[] d = new byte[30];
        private bool state = false;
        private string msg = string.Empty;
        private string ip = "192.168.4.1";
        private static int port = 9001;

        public FilghtControlFrm()
        {
            InitializeComponent();
            view3D = new View3D();
            view3D.Model3DPath = @"Model\PilotFish_UAV.obj";
            elementHost1.Child = view3D;
            cb_pid.SelectedIndex = 0;
            Tb_Initial();//设置滑动条最大最小值
            btn_Initial_False();
            tb_pitch.Value = 1500;
            tb_roll.Value = 1500;
        }

        public Quaternion ZRotation(double angleInRadians)
        {
            double halfAngleInRadians = (angleInRadians * 0.5);
            Quaternion q = new Quaternion(0, 0, Math.Sin(halfAngleInRadians), Math.Cos(halfAngleInRadians));
            return q;
        }

        UdpClient UCR = new UdpClient(port);
        /// <summary>
        /// 接收数据
        /// </summary>
        private void ReceiveMsg()
        {
            Console.WriteLine("线程启动");
          //  UdpClient UCR;
            try
            {
             //   UCR = new UdpClient(ReceivePort);
                // UCR = new UdpClient(9002);
                //取0可接收其它端口的数据，这里上面new UdpClient(SendPort)的设计好象没有起到应有的作用。
                //IPEndPoint Sender = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
               
                IPEndPoint Sender = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    try
                    {
                        
                        //IPEndPoint Sender = null;
                        //Receive处于等待，但不影响发送
                        byte[] data = new byte[1024];
                        data = UCR.Receive(ref Sender); 
                        if (state==true)
                        {
                            SendMsg(ref d, UCR);
                        }
                        Console.WriteLine("{0},{1}", data.Length, BitConverter.ToString(data));//打印数据流
                        if (data.Length == 0)
                        {
                            break;
                        }
                        DataService ds = new DataService();
                        int cursor = 0;//游标指针
                        int copylength = 0;
                        int length = 0;

                        for (int i = 0; i < data.Length; i += length)
                        {
                            cursor = cursor + 1;
                            if ((data[i] == 0xaa) && (data[i + 1] == 0xaa))
                            {
                                int len = int.Parse(Convert.ToString(data[i + 3], 10));//数据正文长度
                                cursor = cursor + 3;
                                //减1的目的是保证数据在【有正文的时候，还有一位是和校验位】
                                if (data.Length - cursor > len)
                                {
                                    cursor = cursor + len;//完整的数据帧【除去和校验位】
                                    byte[] df = new byte[len+4];
                                    
                                    for (int j = 0; j < len + 4; j++)
                                    {
                                        df[j] = data[j+ copylength];//取出数据
                                    }
                                    
                                    if (ds.CheckSum(df) == data[cursor])
                                    {
                                        byte funcode = data[i + 2];
                                        #region 显示数据
                                        switch (funcode)
                                        {
                                            case 0x00:

                                                break;
                                            case 0x01:
                                                //修改
                                                this.Invoke(new Action(() =>
                                                {
                                                    short temp = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                    temp = (short)(temp ^ df[4]) ; //低8位
                                                    temp = (short)(temp << 8);    //高8位
                                                    temp = (short)(temp ^ df[5]); //在b2赋给s的低8位                                                 
                                                    lab_roll.Text = ((float)temp/100).ToString();


                                                    short temp2 = 0;
                                                    temp2 = (short)(temp2 ^ df[6]); //低8位
                                                    temp2 = (short)(temp2 << 8);    //高8位
                                                    temp2 = (short)(temp2 ^ df[7]); //在b2赋给s的低8位                             
                                                    lab_pitch.Text = ((float)temp2 / 100).ToString();

                                                    short temp3 = 0;
                                                    temp3 = (short)(temp3 ^ df[8]); //低8位
                                                    temp3 = (short)(temp3 << 8);    //高8位
                                                    temp3 = (short)(temp3 ^ df[9]); //在b2赋给s的低8位                   
                                                    lab_yaw.Text = ((float)temp3 / 100).ToString();

                                                    int pow = 0;
                                                    pow = (short)(pow ^ df[12]); //低8位
                                                    pow = (short)(pow << 8);    //高8位
                                                    pow = (short)(pow ^ df[13]); //在b2赋给s的低8位   
                                                    lab_butter.Text = (((float)(pow))/100).ToString();
                                                    double x, y, z, w;
                                                    w = Math.Cos(temp / 200 * 0.0174533) * Math.Cos(temp2 / 200 * 0.0174533) * Math.Cos(temp3 / 200 * 0.0174533) + Math.Sin(temp / 200 * 0.0174533) * Math.Sin(temp2 / 200 * 0.0174533) * Math.Sin(temp3 / 200 * 0.0174533);
                                                    x = Math.Sin(temp / 200 * 0.0174533) * Math.Cos(temp2 / 200 * 0.0174533) * Math.Cos(temp3 / 200 * 0.0174533) - Math.Cos(temp / 200 * 0.0174533) * Math.Sin(temp2 / 200 * 0.0174533) * Math.Sin(temp3 / 200 * 0.0174533);
                                                    y = Math.Cos(temp / 200 * 0.0174533) * Math.Sin(temp2 / 200 * 0.0174533) * Math.Cos(temp3 / 200 * 0.0174533) + Math.Sin(temp / 200 * 0.0174533) * Math.Cos(temp2 / 200 * 0.0174533) * Math.Sin(temp3 / 200 * 0.0174533);
                                                    z = Math.Cos(temp / 200 * 0.0174533) * Math.Cos(temp2 / 200 * 0.0174533) * Math.Sin(temp3 / 200 * 0.0174533) - Math.Sin(temp / 200 * 0.0174533) * Math.Sin(temp2 / 200 * 0.0174533) * Math.Cos(temp3 / 200 * 0.0174533);
                                                    view3D.SetQuaternion(x,y,z,w);

                                                    if (df[15] == 0x01)
                                                    {
                                                        lab_lock.Text = "解锁";
                                                    }
                                                    else if (df[15] == 0x00)
                                                    {
                                                        lab_lock.Text = "加锁";
                                                    }
                                                }));


                                                break;
                                            case 0x02:

                                                //9.80665/32767
                                                short temp4 = 0;                                     
                                                temp4 = (short)(temp4 ^ df[4]); //低8位
                                                temp4 = (short)(temp4 << 8);    //高8位
                                                temp4 = (short)(temp4 ^ df[5]); //在b2赋给s的低8位                                                 
                                                lab_acc_x.Text = temp4.ToString();

                                                short temp5 = 0;                                     
                                                temp5 = (short)(temp5 ^ df[6]); //低8位
                                                temp5 = (short)(temp5 << 8);    //高8位
                                                temp5 = (short)(temp5 ^ df[7]); //在b2赋给s的低8位                                                 
                                                lab_acc_y.Text = temp5.ToString();

                                                short temp6 = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                temp6 = (short)(temp6 ^ df[8]); //低8位
                                                temp6 = (short)(temp6 << 8);    //高8位
                                                temp6 = (short)(temp6 ^ df[9]); //在b2赋给s的低8位
                                                lab_acc_z.Text = temp6.ToString();
                                                //lab_acc_z.Text = ( (double)temp6* 9.80665 / 32767).ToString();

                                                //gyr
                                                short temp7 = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                temp7 = (short)(temp7 ^ df[10]); //低8位
                                                temp7 = (short)(temp7 << 8);    //高8位
                                                temp7 = (short)(temp7 ^ df[11]); //在b2赋给s的低8位                                                 
                                                lab_gyrd_x.Text = temp7.ToString();

                                                short temp8 = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                temp8 = (short)(temp8 ^ df[12]); //低8位
                                                temp8 = (short)(temp8 << 8);    //高8位
                                                temp8 = (short)(temp8 ^ df[13]); //在b2赋给s的低8位                                                 
                                                lab_gyrd_y.Text = temp8.ToString();

                                                short temp9 = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                temp9 = (short)(temp9 ^ df[14]); //低8位
                                                temp9 = (short)(temp9 << 8);    //高8位
                                                temp9 = (short)(temp9 ^ df[15]); //在b2赋给s的低8位                                                 
                                                lab_gyrd_z.Text = temp9.ToString();

                                                //
                                                //lab_gyrd_x.Text = (((df[10] << 8) + df[11]) ).ToString();
                                                //lab_gyrd_y.Text = (((df[12] << 8) + df[13]) ).ToString();
                                                //lab_gyrd_z.Text = (((df[14] << 8) + df[15]) ).ToString();

                                                short temp10 = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                temp10 = (short)(temp10 ^ df[16]); //低8位
                                                temp10 = (short)(temp10 << 8);    //高8位
                                                temp10 = (short)(temp10 ^ df[17]); //在b2赋给s的低8位                                                 
                                                lab_mag_x.Text = temp10.ToString();

                                                short temp11 = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                temp11 = (short)(temp11 ^ df[18]); //低8位
                                                temp11 = (short)(temp11 << 8);    //高8位
                                                temp11 = (short)(temp11 ^ df[19]); //在b2赋给s的低8位                                                 
                                                lab_mag_y.Text = temp11.ToString();

                                                short temp12 = 0;   //一个16位整形变量，初值为 0000 0000 0000 0000                                    
                                                temp12 = (short)(temp12 ^ df[20]); //低8位
                                                temp12 = (short)(temp12 << 8);    //高8位
                                                temp12 = (short)(temp12 ^ df[21]); //在b2赋给s的低8位                                                 
                                                lab_mag_z.Text = temp12.ToString();
                                    
                                                this.FilghtControlFrm_Load(null, null);//刷新
                                                break;
                                            case 0x03:
                                                txt_thr.Text = ((int)(((df[4] << 8) + df[5]) )).ToString();
                                                txt_yaw.Text = ((int)(((df[6] << 8) + df[7]) )).ToString();
                                                txt_roll.Text = ((int)(((df[8] << 8) + df[9]) )).ToString();
                                                txt_pitch.Text = ((int)(((df[10] << 8) + df[11]) )).ToString();

                                                short tempa = 0;
                                                tempa = (short)(tempa ^ df[12]);
                                                tempa = (short)(tempa << 8);
                                                tempa = (short)(tempa ^ df[13]);                                               
                                                txt_pid_7_rol.Text = tempa.ToString();

                                                short tempb = 0;
                                                tempb = (short)(tempb ^ df[14]);
                                                tempb = (short)(tempb << 8);
                                                tempb = (short)(tempb ^ df[15]);
                                                txt_pid_8_rol.Text = tempb.ToString();

                                                short tempc = 0;
                                                tempc = (short)(tempc ^ df[16]);
                                                tempc = (short)(tempc << 8);
                                                tempc = (short)(tempc ^ df[17]);
                                                txt_pid_9_rol.Text = tempc.ToString();

                                                short tempd = 0;
                                                tempd = (short)(tempd ^ df[18]);
                                                tempd = (short)(tempd << 8);
                                                tempd = (short)(tempd ^ df[19]);
                                                txt_pid_7_yaw.Text = tempd.ToString();

                                                short tempe = 0;
                                                tempe = (short)(tempe ^ df[20]);
                                                tempe = (short)(tempe << 8);
                                                tempe = (short)(tempe ^ df[21]);
                                                txt_pid_8_yaw.Text = tempe.ToString();

                                                short tempf = 0;
                                                tempf = (short)(tempf ^ df[22]);
                                                tempf = (short)(tempf << 8);
                                                tempf = (short)(tempf ^ df[23]);
                                                txt_pid_9_yaw.Text = tempf.ToString();




                                                break;
                                            case 0x06:

                                                break;
                                            case 0x10:
                                                txt_pid_1_rol.Text = (((df[4] << 8) + df[5]) ).ToString();
                                                txt_pid_2_rol.Text = (((df[6] << 8) + df[7]) ).ToString();
                                                txt_pid_3_rol.Text = (((df[8] << 8) + df[9]) ).ToString();

                                                txt_pid_1_yaw.Text = (((df[10] << 8) + df[11]) ).ToString();
                                                txt_pid_2_yaw.Text = (((df[12] << 8) + df[13]) ).ToString();
                                                txt_pid_3_yaw.Text = (((df[14] << 8) + df[15]) ).ToString();

                                                txt_pid_1_pitch.Text = (((df[16] << 8) + df[17]) ).ToString();
                                                txt_pid_2_pitch.Text = (((df[18] << 8) + df[19]) ).ToString();
                                                txt_pid_3_pitch.Text = (((df[20] << 8) + df[21]) ).ToString();
                                                break;
                                            case 0x11:
                                                txt_pid_4_rol.Text = (((df[4] << 8) + df[5]) ).ToString();
                                                txt_pid_5_rol.Text = (((df[6] << 8) + df[7]) ).ToString();
                                                txt_pid_6_rol.Text = (((df[8] << 8) + df[9]) ).ToString();

                                                txt_pid_4_yaw.Text = (((df[10] << 8) + df[11]) ).ToString();
                                                txt_pid_5_yaw.Text = (((df[12] << 8) + df[13]) ).ToString();
                                                txt_pid_6_yaw.Text = (((df[14] << 8) + df[15]) ).ToString();

                                                txt_pid_4_pitch.Text = (((df[16] << 8) + df[17]) ).ToString();
                                                txt_pid_5_pitch.Text = (((df[18] << 8) + df[19]) ).ToString();
                                                txt_pid_6_pitch.Text = (((df[20] << 8) + df[21]) ).ToString();
                                                break;
                                            case 0x12:
                                                txt_pid_7_rol.Text = (((df[4] << 8) + df[5]) ).ToString();
                                                txt_pid_8_rol.Text = (((df[6] << 8) + df[7]) ).ToString();
                                                txt_pid_9_rol.Text = (((df[8] << 8) + df[9]) ).ToString();

                                                txt_pid_7_yaw.Text = (((df[10] << 8) + df[11]) ).ToString();
                                                txt_pid_8_yaw.Text = (((df[12] << 8) + df[13]) ).ToString();
                                                txt_pid_9_yaw.Text = (((df[14] << 8) + df[15]) ).ToString();

                                                txt_pid_7_pitch.Text = (((df[16] << 8) + df[17]) ).ToString();
                                                txt_pid_8_pitch.Text = (((df[18] << 8) + df[19]) ).ToString();
                                                txt_pid_9_pitch.Text = (((df[20] << 8) + df[21]) ).ToString();
                                                break;
                                            case 0xEF:

                                                break;
                                            default:
                                                break;
                                        }
                                        #endregion
                                        cursor = cursor + 1;
                                        copylength = copylength+ len + 5;
                                        length = len + 5;
                                    }
                                    else
                                    {
                                        lab_msg.Text = "和校验错误！";
                                        lab_msg.ForeColor = Color.Red;
                                    }
                                }
                                else
                                {
                                    lab_msg.Text = "数据不完整！";
                                    lab_msg.ForeColor = Color.Red;
                                    break;
                                }
                            }
                            else
                            {
                                lab_msg.Text = "数据报错误！";
                                lab_msg.ForeColor = Color.Red;
                                break;
                            }


                        }
                        
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine(s.ToString());
                    }

                }
            }
            catch
            { }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sendBytes"></param>
        private void SendMsg(ref byte[] sendBytes, UdpClient UCR)
        {
            //UdpClient UCR = new UdpClient(SendPort);
           
            UCR.Connect("192.168.4.1", 9000);
            try
            {
                UCR.Send(sendBytes, sendBytes.Length);
                lab_msg.Text = msg+"数据已发送！";
                lab_msg.ForeColor = Color.Green;
              //  UCR.Close();
            }
            catch (Exception ee)
            {
                lab_msg.Text = msg+"数据发送失败！";
                lab_msg.ForeColor = Color.Red;
               // UCR.Close();
                Console.WriteLine(ee.ToString());
            }
      
           // UCR.Close();
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
            
            Console.WriteLine("===================load==============");
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            
            Console.WriteLine("=============================================");

        }

        private void Tb_Initial()
        {
            //
            tb_thr.Minimum = 1000;
            tb_thr.Maximum = 2000;

            tb_roll.Minimum = 1000;
            tb_roll.Maximum = 2000;

            tb_pitch.Minimum = 1000;
            tb_pitch.Maximum = 2000;

            tb_yaw.Minimum = 1000;
            tb_yaw.Maximum = 2000;
        }
        private void btn_Initial_False()
        {
            btn_ReviceMsg.Enabled = false;
            btnSendRemote.Enabled = false;
            btnSendPID.Enabled = false;
            btnGetPID.Enabled = false;
            btnGyrd.Enabled = false;
            btnAcc.Enabled = false;
            btnMag.Enabled = false;
            btn_lock.Enabled = false;
            btn_unlock.Enabled = false;
        }

        private void btn_Initial_True()
        {
            btn_ReviceMsg.Enabled = true;
            btnSendRemote.Enabled = true;
            btnSendPID.Enabled = true;
            btnGetPID.Enabled = true;
            btnGyrd.Enabled = true;
            btnAcc.Enabled = true;
            btnMag.Enabled = true;
            btn_lock.Enabled = true;
            btn_unlock.Enabled = true;
        }

        #region 上位机=========》飞控

        #region 遥控器

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
                Int16 thr = (Int16)(double.Parse(tb_thr.Value.ToString()) );

                Int16 yaw = (Int16)(double.Parse(tb_yaw.Value.ToString()) );

                Int16 roll = (Int16)(double.Parse(tb_roll.Value.ToString()) );

                Int16 pitch = (Int16)(double.Parse(tb_pitch.Value.ToString()) );

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
                //SendMsg(buffer);
                d = buffer;
                state = true;
                SendMsg(ref d, UCR);
                state = false;
                d = new byte[30];

            }
            catch(Exception es)
            {
                lab_msg.Text = msg+"数据发送失败！";
                lab_msg.ForeColor = Color.Red;
                Console.WriteLine(es.ToString());
               // udpClient.Close();
            }

        }

        #endregion

        #region 数据校准
        private void btnGyrd_Click(object sender, EventArgs e)
        {
            msg = "陀螺仪校准";
            Protocol p = new Protocol();
            // SendMsg(p.Pro_GYRD());
            d = p.Pro_GYRD();
            state = true;
            SendMsg(ref d, UCR);
            state = false;
            d = new byte[30];
            Console.WriteLine(BitConverter.ToString(p.Pro_GYRD()));

        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            msg = "加速度校准";
            Protocol p = new Protocol();
            // SendMsg(p.Pro_ACC());
            d = p.Pro_ACC();
            state = true;
            SendMsg(ref d, UCR);
            state = false;
            d = new byte[30];
            Console.WriteLine(BitConverter.ToString(p.Pro_ACC()));
        }

        private void btnMag_Click(object sender, EventArgs e)
        {
            msg = "磁力计校准";
            Protocol p = new Protocol();
            // SendMsg(p.Pro_MAG());
            d = p.Pro_MAG();
            state = true;
            SendMsg(ref d, UCR);
            state = false;
            d = new byte[30];
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
            //  SendMsg(p.Pro_GetPID());
            d = p.Pro_GetPID();
            state = true;
            SendMsg(ref d, UCR);
            state = false;
            d = new byte[30];
            Console.WriteLine(BitConverter.ToString(p.Pro_GetPID()));

        }

        private void PID_1()
        {
            msg = "PID_1";
            try
            {
                Int16 pid_1_rol = (Int16)(double.Parse(txt_pid_1_rol.Text) );
                Int16 pid_1_yaw = (Int16)(double.Parse(txt_pid_1_yaw.Text) );
                Int16 pid_1_pitch = (Int16)(double.Parse(txt_pid_1_pitch.Text) );

                Int16 pid_2_rol = (Int16)(double.Parse(txt_pid_2_rol.Text) );
                Int16 pid_2_yaw = (Int16)(double.Parse(txt_pid_2_yaw.Text) );
                Int16 pid_2_pitch = (Int16)(double.Parse(txt_pid_2_pitch.Text) );

                Int16 pid_3_rol = (Int16)(double.Parse(txt_pid_3_rol.Text) );
                Int16 pid_3_yaw = (Int16)(double.Parse(txt_pid_3_yaw.Text) );
                Int16 pid_3_pitch = (Int16)(double.Parse(txt_pid_3_pitch.Text) );

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

                byte[] data = new byte[18] { pid_1_rol_H, pid_1_rol_L,pid_2_rol_H, pid_2_rol_L,pid_3_rol_H, pid_3_rol_L,
                                             pid_1_yaw_H, pid_1_yaw_L,pid_2_yaw_H, pid_2_yaw_L, pid_3_yaw_H, pid_3_yaw_L,
                                             pid_1_pitch_H, pid_1_pitch_L,pid_2_pitch_H, pid_2_pitch_L,pid_3_pitch_H, pid_3_pitch_L };
                Protocol p = new Protocol();
                byte[] buffer = p.Pro_SendPID_1(data);
                Console.WriteLine(BitConverter.ToString(buffer));
                //SendMsg(buffer);
                d = buffer;
                state = true;
                SendMsg(ref d, UCR);
                state = false;
                d = new byte[30];
            }
            catch
            {
                //udpClient.Close();
                lab_msg.Text = "数据发送失败！";
                lab_msg.ForeColor = Color.Red;
            }
        }

        private void PID_2()
        {
            msg = "PID_2";
            try
            {
                Int16 pid_4_rol = (Int16)(int.Parse(txt_pid_4_rol.Text) );
                Int16 pid_4_yaw = (Int16)(int.Parse(txt_pid_4_yaw.Text) );
                Int16 pid_4_pitch = (Int16)(int.Parse(txt_pid_4_pitch.Text) );

                Int16 pid_5_rol = (Int16)(int.Parse(txt_pid_5_rol.Text) );
                Int16 pid_5_yaw = (Int16)(int.Parse(txt_pid_5_yaw.Text) );
                Int16 pid_5_pitch = (Int16)(int.Parse(txt_pid_5_pitch.Text) );

                Int16 pid_6_rol = (Int16)(int.Parse(txt_pid_6_rol.Text) );
                Int16 pid_6_yaw = (Int16)(int.Parse(txt_pid_6_yaw.Text) );
                Int16 pid_6_pitch = (Int16)(int.Parse(txt_pid_6_pitch.Text) );

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

               // byte[] data = new byte[18] { pid_4_rol_H, pid_4_rol_L, pid_4_yaw_H, pid_4_yaw_L, pid_4_pitch_H, pid_4_pitch_L, pid_5_rol_H, pid_5_rol_L, pid_5_yaw_H, pid_5_yaw_L, pid_5_pitch_H, pid_5_pitch_L, pid_6_rol_H, pid_6_rol_L, pid_6_yaw_H, pid_6_yaw_L, pid_6_pitch_H, pid_6_pitch_L };

                byte[] data = new byte[18] { pid_4_rol_H, pid_4_rol_L,pid_5_rol_H, pid_5_rol_L,pid_6_rol_H, pid_6_rol_L,
                                             pid_4_yaw_H, pid_4_yaw_L,pid_5_yaw_H, pid_5_yaw_L, pid_6_yaw_H, pid_6_yaw_L,
                                             pid_4_pitch_H, pid_4_pitch_L,pid_5_pitch_H, pid_5_pitch_L,pid_6_pitch_H, pid_6_pitch_L };

                Protocol p = new Protocol();
                byte[] buffer = p.Pro_SendPID_2(data);
                Console.WriteLine(BitConverter.ToString(buffer));
                //SendMsg(buffer);
                d = buffer;
                state = true;
                SendMsg(ref d, UCR);
                state = false;
                d = new byte[30];
            }
            catch
            {
                //udpClient.Close();
                lab_msg.Text = "数据发送失败！";
                lab_msg.ForeColor = Color.Red;
            }
        }

        private void PID_3()
        {
            msg = "PID_3";
            try
            {
                Int16 pid_7_rol = (Int16)(double.Parse(txt_pid_7_rol.Text) );
                Int16 pid_7_yaw = (Int16)(double.Parse(txt_pid_7_yaw.Text) );
                Int16 pid_7_pitch = (Int16)(double.Parse(txt_pid_7_pitch.Text) );

                Int16 pid_8_rol = (Int16)(double.Parse(txt_pid_8_rol.Text) );
                Int16 pid_8_yaw = (Int16)(double.Parse(txt_pid_8_yaw.Text) );
                Int16 pid_8_pitch = (Int16)(double.Parse(txt_pid_8_pitch.Text) );

                Int16 pid_9_rol = (Int16)(double.Parse(txt_pid_9_rol.Text) );
                Int16 pid_9_yaw = (Int16)(double.Parse(txt_pid_9_yaw.Text) );
                Int16 pid_9_pitch = (Int16)(double.Parse(txt_pid_9_pitch.Text) );

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
                //SendMsg(buffer);
                d = buffer;
                state = true;
                SendMsg(ref d, UCR);
                state = false;
                d = new byte[30];
            }
            catch
            {
                //udpClient.Close();
                lab_msg.Text = "数据发送失败！";
                lab_msg.ForeColor = Color.Red;
            }
        }
        #endregion

        #endregion


        private void timer1_Tick(object sender, EventArgs e)
        {
            lab_msg.Text = "-------";
            lab_msg.ForeColor = Color.Green;


        }
        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_lock_Click(object sender, EventArgs e)
        {
            state = true;
            msg = "加锁";
            Protocol p = new Protocol();
            //SendMsg(p.Pro_Lock());
            d = p.Pro_Lock();
            Console.WriteLine("加锁==========：",BitConverter.ToString(d));
            SendMsg(ref d, UCR);
            state = false;
            d = new byte[30];
            Console.WriteLine(BitConverter.ToString(p.Pro_Lock()));
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_unlock_Click(object sender, EventArgs e)
        {
            state = true;
            msg = "解锁";
            Protocol p = new Protocol();
            // SendMsg(p.Pro_UnLock());
            d = p.Pro_UnLock();
            SendMsg(ref d, UCR);
            state = false;
            d = new byte[30];
            Console.WriteLine(BitConverter.ToString(p.Pro_UnLock()));
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(ReceiveMsg));
            th.IsBackground = true;
            th.Start();
            btn_ReviceMsg.Enabled = false;
        }
        /// <summary>
        /// 开启系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {

            try
            {
                //ip = txtip.Text.Trim();
                //port = int.Parse(txtport.Text.Trim());
                  btn_Initial_True();
                        Protocol p = new Protocol();
                        d = p.Pro_Lock();
                        state = true;
                        SendMsg(ref d, UCR);
                        state = false;
                        d = new byte[30];
     
            }
            catch
            {
                lab_msg.Text = "IP或端口号不可用！";
                lab_msg.ForeColor = Color.Red;
            }
        }

        /// <summary>  
        /// 获取本机已被使用的网络端点  
        /// </summary>  
        public IList<IPEndPoint> GetUsedIPEndPoint()
        {
            //获取一个对象，该对象提供有关本地计算机的网络连接和通信统计数据的信息。  
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 侦听器的终结点信息。  
            IPEndPoint[] ipEndPointTCP = ipGlobalProperties.GetActiveTcpListeners();

            //获取有关本地计算机上的 Internet 协议版本 4 (IPv4) 用户数据报协议 (UDP) 侦听器的信息。  
            IPEndPoint[] ipEndPointUDP = ipGlobalProperties.GetActiveUdpListeners();

            //获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 连接的信息。  
            TcpConnectionInformation[] tcpConnectionInformation = ipGlobalProperties.GetActiveTcpConnections();

            IList<IPEndPoint> allIPEndPoint = new List<IPEndPoint>();
            foreach (IPEndPoint iep in ipEndPointTCP) allIPEndPoint.Add(iep);
            foreach (IPEndPoint iep in ipEndPointUDP) allIPEndPoint.Add(iep);
            foreach (TcpConnectionInformation tci in tcpConnectionInformation) allIPEndPoint.Add(tci.LocalEndPoint);

            return allIPEndPoint;
        }
        /// <summary>  
        /// 判断指定的网络端点（只判断端口）是否被使用  
        /// </summary>  
        public bool IsUsedIPEndPoint(int port)
        {
            foreach (IPEndPoint iep in GetUsedIPEndPoint())
            {
                if (iep.Port == port)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>  
        /// 判断指定的网络端点（判断IP和端口）是否被使用  
        /// </summary>  
        public bool IsUsedIPEndPoint(string ip, int port)
        {
            foreach (IPEndPoint iep in GetUsedIPEndPoint())
            {
                if (iep.Address.ToString() == ip && iep.Port == port)
                {
                    return true;
                }
            }
            return false;
        }

        private void FilghtControlFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UCR.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txt_thr_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
