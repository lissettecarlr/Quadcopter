using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            DataService ds = new DataService();
            byte[] d = new byte[17] { 0xaa, 0xaa, 0x01, 0x0c, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x02, 0x01, 0x00 };
            d[16] = ds.CheckSum(d);
            Console.WriteLine(BitConverter.ToString(d));
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            i++;
            if (i == 100)
            {
                i = 0;
            }
            this.lab_roll.Text = i.ToString();
            lab_pitch.Text = i.ToString();
            lab_yaw.Text = i.ToString();
            if (i % 2 == 0)
            {
                lab_lock.Text = "解锁";
            }
            else if (i % 2 == 1)
            {
                lab_lock.Text = "加锁";
            }

        }
    }
}
