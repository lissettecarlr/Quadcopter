namespace 飞控地面站
{
    partial class FilghtControlFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lab_lock = new System.Windows.Forms.Label();
            this.lab_roll = new System.Windows.Forms.Label();
            this.lab_yaw = new System.Windows.Forms.Label();
            this.lab_pitch = new System.Windows.Forms.Label();
            this.lab_butter = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendRemote = new System.Windows.Forms.Button();
            this.txt_pitch = new System.Windows.Forms.TextBox();
            this.txt_roll = new System.Windows.Forms.TextBox();
            this.txt_yaw = new System.Windows.Forms.TextBox();
            this.txt_thr = new System.Windows.Forms.TextBox();
            this.tb_pitch = new System.Windows.Forms.TrackBar();
            this.tb_roll = new System.Windows.Forms.TrackBar();
            this.tb_yaw = new System.Windows.Forms.TrackBar();
            this.tb_thr = new System.Windows.Forms.TrackBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lab_mag_z = new System.Windows.Forms.Label();
            this.lab_mag_x = new System.Windows.Forms.Label();
            this.lab_mag_y = new System.Windows.Forms.Label();
            this.lab_gyrd_z = new System.Windows.Forms.Label();
            this.lab_gyrd_x = new System.Windows.Forms.Label();
            this.lab_gyrd_y = new System.Windows.Forms.Label();
            this.lab_acc_z = new System.Windows.Forms.Label();
            this.lab_acc_x = new System.Windows.Forms.Label();
            this.lab_acc_y = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_pid = new System.Windows.Forms.ComboBox();
            this.txt_pid_9_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_9_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_9_rol = new System.Windows.Forms.TextBox();
            this.txt_pid_8_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_8_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_8_rol = new System.Windows.Forms.TextBox();
            this.txt_pid_7_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_7_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_7_rol = new System.Windows.Forms.TextBox();
            this.txt_pid_6_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_6_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_6_rol = new System.Windows.Forms.TextBox();
            this.txt_pid_5_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_5_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_5_rol = new System.Windows.Forms.TextBox();
            this.txt_pid_4_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_4_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_4_rol = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnGetPID = new System.Windows.Forms.Button();
            this.btnSendPID = new System.Windows.Forms.Button();
            this.txt_pid_3_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_3_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_3_rol = new System.Windows.Forms.TextBox();
            this.txt_pid_2_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_2_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_2_rol = new System.Windows.Forms.TextBox();
            this.txt_pid_1_pitch = new System.Windows.Forms.TextBox();
            this.txt_pid_1_yaw = new System.Windows.Forms.TextBox();
            this.txt_pid_1_rol = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_unlock = new System.Windows.Forms.Button();
            this.btn_lock = new System.Windows.Forms.Button();
            this.btnMag = new System.Windows.Forms.Button();
            this.btnAcc = new System.Windows.Forms.Button();
            this.btnGyrd = new System.Windows.Forms.Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lab_msg = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_ReviceMsg = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_pitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_roll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_yaw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_thr)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 362);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "锁定状态";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(75, 541);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "电量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(65, 497);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "PITCH";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(85, 452);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "YAW";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(75, 404);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "ROLL";
            // 
            // lab_lock
            // 
            this.lab_lock.AutoSize = true;
            this.lab_lock.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_lock.Location = new System.Drawing.Point(193, 362);
            this.lab_lock.Name = "lab_lock";
            this.lab_lock.Size = new System.Drawing.Size(47, 19);
            this.lab_lock.TabIndex = 6;
            this.lab_lock.Text = "锁定";
            // 
            // lab_roll
            // 
            this.lab_roll.AutoSize = true;
            this.lab_roll.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_roll.Location = new System.Drawing.Point(193, 404);
            this.lab_roll.Name = "lab_roll";
            this.lab_roll.Size = new System.Drawing.Size(19, 19);
            this.lab_roll.TabIndex = 7;
            this.lab_roll.Text = "1";
            // 
            // lab_yaw
            // 
            this.lab_yaw.AutoSize = true;
            this.lab_yaw.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_yaw.Location = new System.Drawing.Point(193, 452);
            this.lab_yaw.Name = "lab_yaw";
            this.lab_yaw.Size = new System.Drawing.Size(19, 19);
            this.lab_yaw.TabIndex = 8;
            this.lab_yaw.Text = "2";
            // 
            // lab_pitch
            // 
            this.lab_pitch.AutoSize = true;
            this.lab_pitch.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_pitch.Location = new System.Drawing.Point(193, 497);
            this.lab_pitch.Name = "lab_pitch";
            this.lab_pitch.Size = new System.Drawing.Size(19, 19);
            this.lab_pitch.TabIndex = 9;
            this.lab_pitch.Text = "3";
            // 
            // lab_butter
            // 
            this.lab_butter.AutoSize = true;
            this.lab_butter.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_butter.Location = new System.Drawing.Point(191, 541);
            this.lab_butter.Name = "lab_butter";
            this.lab_butter.Size = new System.Drawing.Size(19, 19);
            this.lab_butter.TabIndex = 10;
            this.lab_butter.Text = "4";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSendRemote);
            this.groupBox1.Controls.Add(this.txt_pitch);
            this.groupBox1.Controls.Add(this.txt_roll);
            this.groupBox1.Controls.Add(this.txt_yaw);
            this.groupBox1.Controls.Add(this.txt_thr);
            this.groupBox1.Controls.Add(this.tb_pitch);
            this.groupBox1.Controls.Add(this.tb_roll);
            this.groupBox1.Controls.Add(this.tb_yaw);
            this.groupBox1.Controls.Add(this.tb_thr);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(331, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 369);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "遥控器";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnSendRemote
            // 
            this.btnSendRemote.Location = new System.Drawing.Point(22, 288);
            this.btnSendRemote.Name = "btnSendRemote";
            this.btnSendRemote.Size = new System.Drawing.Size(293, 40);
            this.btnSendRemote.TabIndex = 18;
            this.btnSendRemote.Text = "发送";
            this.btnSendRemote.UseVisualStyleBackColor = true;
            this.btnSendRemote.Click += new System.EventHandler(this.btnSendRemote_Click);
            // 
            // txt_pitch
            // 
            this.txt_pitch.Location = new System.Drawing.Point(267, 219);
            this.txt_pitch.Name = "txt_pitch";
            this.txt_pitch.Size = new System.Drawing.Size(58, 29);
            this.txt_pitch.TabIndex = 17;
            this.txt_pitch.Text = "0";
            this.txt_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // 
            // txt_roll
            // 
            this.txt_roll.Location = new System.Drawing.Point(267, 162);
            this.txt_roll.Name = "txt_roll";
            this.txt_roll.Size = new System.Drawing.Size(58, 29);
            this.txt_roll.TabIndex = 16;
            this.txt_roll.Text = "0";
            this.txt_roll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // 
            // txt_yaw
            // 
            this.txt_yaw.Location = new System.Drawing.Point(267, 105);
            this.txt_yaw.Name = "txt_yaw";
            this.txt_yaw.Size = new System.Drawing.Size(58, 29);
            this.txt_yaw.TabIndex = 15;
            this.txt_yaw.Text = "0";
            this.txt_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      
            // 
            // txt_thr
            // 
            this.txt_thr.Location = new System.Drawing.Point(267, 41);
            this.txt_thr.Name = "txt_thr";
            this.txt_thr.Size = new System.Drawing.Size(58, 29);
            this.txt_thr.TabIndex = 14;
            this.txt_thr.Text = "0";
            this.txt_thr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_thr.TextChanged += new System.EventHandler(this.txt_thr_TextChanged);
            // 
            // tb_pitch
            // 
            this.tb_pitch.LargeChange = 1;
            this.tb_pitch.Location = new System.Drawing.Point(101, 223);
            this.tb_pitch.Name = "tb_pitch";
            this.tb_pitch.Size = new System.Drawing.Size(136, 45);
            this.tb_pitch.TabIndex = 13;
            // 
            // tb_roll
            // 
            this.tb_roll.LargeChange = 1;
            this.tb_roll.Location = new System.Drawing.Point(101, 162);
            this.tb_roll.Name = "tb_roll";
            this.tb_roll.Size = new System.Drawing.Size(136, 45);
            this.tb_roll.TabIndex = 12;
            // 
            // tb_yaw
            // 
            this.tb_yaw.LargeChange = 1;
            this.tb_yaw.Location = new System.Drawing.Point(101, 110);
            this.tb_yaw.Name = "tb_yaw";
            this.tb_yaw.Size = new System.Drawing.Size(136, 45);
            this.tb_yaw.TabIndex = 11;
            // 
            // tb_thr
            // 
            this.tb_thr.LargeChange = 1;
            this.tb_thr.Location = new System.Drawing.Point(101, 41);
            this.tb_thr.Name = "tb_thr";
            this.tb_thr.Size = new System.Drawing.Size(136, 45);
            this.tb_thr.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(19, 223);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 16);
            this.label14.TabIndex = 9;
            this.label14.Text = "PITCH";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(29, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 16);
            this.label13.TabIndex = 8;
            this.label13.Text = "ROLL";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(39, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 7;
            this.label12.Text = "YAW";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(39, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 16);
            this.label11.TabIndex = 6;
            this.label11.Text = "THR";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lab_mag_z);
            this.groupBox2.Controls.Add(this.lab_mag_x);
            this.groupBox2.Controls.Add(this.lab_mag_y);
            this.groupBox2.Controls.Add(this.lab_gyrd_z);
            this.groupBox2.Controls.Add(this.lab_gyrd_x);
            this.groupBox2.Controls.Add(this.lab_gyrd_y);
            this.groupBox2.Controls.Add(this.lab_acc_z);
            this.groupBox2.Controls.Add(this.lab_acc_x);
            this.groupBox2.Controls.Add(this.lab_acc_y);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(331, 387);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 198);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "传感器数据";
            // 
            // lab_mag_z
            // 
            this.lab_mag_z.AutoSize = true;
            this.lab_mag_z.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_mag_z.Location = new System.Drawing.Point(273, 154);
            this.lab_mag_z.Name = "lab_mag_z";
            this.lab_mag_z.Size = new System.Drawing.Size(19, 19);
            this.lab_mag_z.TabIndex = 45;
            this.lab_mag_z.Text = "6";
            // 
            // lab_mag_x
            // 
            this.lab_mag_x.AutoSize = true;
            this.lab_mag_x.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_mag_x.Location = new System.Drawing.Point(146, 154);
            this.lab_mag_x.Name = "lab_mag_x";
            this.lab_mag_x.Size = new System.Drawing.Size(19, 19);
            this.lab_mag_x.TabIndex = 44;
            this.lab_mag_x.Text = "4";
            // 
            // lab_mag_y
            // 
            this.lab_mag_y.AutoSize = true;
            this.lab_mag_y.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_mag_y.Location = new System.Drawing.Point(203, 154);
            this.lab_mag_y.Name = "lab_mag_y";
            this.lab_mag_y.Size = new System.Drawing.Size(19, 19);
            this.lab_mag_y.TabIndex = 43;
            this.lab_mag_y.Text = "5";
            // 
            // lab_gyrd_z
            // 
            this.lab_gyrd_z.AutoSize = true;
            this.lab_gyrd_z.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gyrd_z.Location = new System.Drawing.Point(273, 110);
            this.lab_gyrd_z.Name = "lab_gyrd_z";
            this.lab_gyrd_z.Size = new System.Drawing.Size(19, 19);
            this.lab_gyrd_z.TabIndex = 42;
            this.lab_gyrd_z.Text = "3";
            // 
            // lab_gyrd_x
            // 
            this.lab_gyrd_x.AutoSize = true;
            this.lab_gyrd_x.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gyrd_x.Location = new System.Drawing.Point(146, 110);
            this.lab_gyrd_x.Name = "lab_gyrd_x";
            this.lab_gyrd_x.Size = new System.Drawing.Size(19, 19);
            this.lab_gyrd_x.TabIndex = 41;
            this.lab_gyrd_x.Text = "1";
            // 
            // lab_gyrd_y
            // 
            this.lab_gyrd_y.AutoSize = true;
            this.lab_gyrd_y.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gyrd_y.Location = new System.Drawing.Point(203, 110);
            this.lab_gyrd_y.Name = "lab_gyrd_y";
            this.lab_gyrd_y.Size = new System.Drawing.Size(19, 19);
            this.lab_gyrd_y.TabIndex = 40;
            this.lab_gyrd_y.Text = "2";
            // 
            // lab_acc_z
            // 
            this.lab_acc_z.AutoSize = true;
            this.lab_acc_z.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_acc_z.Location = new System.Drawing.Point(273, 65);
            this.lab_acc_z.Name = "lab_acc_z";
            this.lab_acc_z.Size = new System.Drawing.Size(19, 19);
            this.lab_acc_z.TabIndex = 33;
            this.lab_acc_z.Text = "3";
            // 
            // lab_acc_x
            // 
            this.lab_acc_x.AutoSize = true;
            this.lab_acc_x.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_acc_x.Location = new System.Drawing.Point(146, 65);
            this.lab_acc_x.Name = "lab_acc_x";
            this.lab_acc_x.Size = new System.Drawing.Size(19, 19);
            this.lab_acc_x.TabIndex = 32;
            this.lab_acc_x.Text = "1";
            // 
            // lab_acc_y
            // 
            this.lab_acc_y.AutoSize = true;
            this.lab_acc_y.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_acc_y.Location = new System.Drawing.Point(203, 65);
            this.lab_acc_y.Name = "lab_acc_y";
            this.lab_acc_y.Size = new System.Drawing.Size(19, 19);
            this.lab_acc_y.TabIndex = 31;
            this.lab_acc_y.Text = "2";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(273, 27);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 19);
            this.label20.TabIndex = 24;
            this.label20.Text = "Z";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(146, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 19);
            this.label19.TabIndex = 23;
            this.label19.Text = "X";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(203, 27);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 19);
            this.label18.TabIndex = 22;
            this.label18.Text = "Y";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(54, 153);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 19);
            this.label17.TabIndex = 21;
            this.label17.Text = "MAG";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(44, 110);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 19);
            this.label16.TabIndex = 20;
            this.label16.Text = "GYRD";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(54, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 19);
            this.label15.TabIndex = 19;
            this.label15.Text = "ACC";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.cb_pid);
            this.groupBox5.Controls.Add(this.txt_pid_9_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_9_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_9_rol);
            this.groupBox5.Controls.Add(this.txt_pid_8_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_8_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_8_rol);
            this.groupBox5.Controls.Add(this.txt_pid_7_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_7_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_7_rol);
            this.groupBox5.Controls.Add(this.txt_pid_6_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_6_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_6_rol);
            this.groupBox5.Controls.Add(this.txt_pid_5_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_5_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_5_rol);
            this.groupBox5.Controls.Add(this.txt_pid_4_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_4_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_4_rol);
            this.groupBox5.Controls.Add(this.groupBox9);
            this.groupBox5.Controls.Add(this.groupBox8);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.btnGetPID);
            this.groupBox5.Controls.Add(this.btnSendPID);
            this.groupBox5.Controls.Add(this.txt_pid_3_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_3_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_3_rol);
            this.groupBox5.Controls.Add(this.txt_pid_2_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_2_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_2_rol);
            this.groupBox5.Controls.Add(this.txt_pid_1_pitch);
            this.groupBox5.Controls.Add(this.txt_pid_1_yaw);
            this.groupBox5.Controls.Add(this.txt_pid_1_rol);
            this.groupBox5.Controls.Add(this.label43);
            this.groupBox5.Controls.Add(this.label40);
            this.groupBox5.Controls.Add(this.label37);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(684, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(318, 366);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "PID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(289, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 16);
            this.label9.TabIndex = 62;
            this.label9.Text = "P";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(289, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 61;
            this.label8.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 60;
            this.label7.Text = "R";
            // 
            // cb_pid
            // 
            this.cb_pid.FormattingEnabled = true;
            this.cb_pid.Items.AddRange(new object[] {
            "PID_1",
            "PID_2",
            "PID_3"});
            this.cb_pid.Location = new System.Drawing.Point(27, 324);
            this.cb_pid.Name = "cb_pid";
            this.cb_pid.Size = new System.Drawing.Size(66, 24);
            this.cb_pid.TabIndex = 59;
            // 
            // txt_pid_9_pitch
            // 
            this.txt_pid_9_pitch.Location = new System.Drawing.Point(223, 278);
            this.txt_pid_9_pitch.Multiline = true;
            this.txt_pid_9_pitch.Name = "txt_pid_9_pitch";
            this.txt_pid_9_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_9_pitch.TabIndex = 58;
            this.txt_pid_9_pitch.Text = "0";
            this.txt_pid_9_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_9_yaw
            // 
            this.txt_pid_9_yaw.Location = new System.Drawing.Point(223, 248);
            this.txt_pid_9_yaw.Multiline = true;
            this.txt_pid_9_yaw.Name = "txt_pid_9_yaw";
            this.txt_pid_9_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_9_yaw.TabIndex = 57;
            this.txt_pid_9_yaw.Text = "0";
            this.txt_pid_9_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_9_rol
            // 
            this.txt_pid_9_rol.Location = new System.Drawing.Point(223, 218);
            this.txt_pid_9_rol.Multiline = true;
            this.txt_pid_9_rol.Name = "txt_pid_9_rol";
            this.txt_pid_9_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_9_rol.TabIndex = 56;
            this.txt_pid_9_rol.Text = "0";
            this.txt_pid_9_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_8_pitch
            // 
            this.txt_pid_8_pitch.Location = new System.Drawing.Point(168, 278);
            this.txt_pid_8_pitch.Multiline = true;
            this.txt_pid_8_pitch.Name = "txt_pid_8_pitch";
            this.txt_pid_8_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_8_pitch.TabIndex = 55;
            this.txt_pid_8_pitch.Text = "0";
            this.txt_pid_8_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_8_yaw
            // 
            this.txt_pid_8_yaw.Location = new System.Drawing.Point(167, 248);
            this.txt_pid_8_yaw.Multiline = true;
            this.txt_pid_8_yaw.Name = "txt_pid_8_yaw";
            this.txt_pid_8_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_8_yaw.TabIndex = 54;
            this.txt_pid_8_yaw.Text = "0";
            this.txt_pid_8_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_8_rol
            // 
            this.txt_pid_8_rol.Location = new System.Drawing.Point(166, 218);
            this.txt_pid_8_rol.Multiline = true;
            this.txt_pid_8_rol.Name = "txt_pid_8_rol";
            this.txt_pid_8_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_8_rol.TabIndex = 53;
            this.txt_pid_8_rol.Text = "0";
            this.txt_pid_8_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_7_pitch
            // 
            this.txt_pid_7_pitch.Location = new System.Drawing.Point(111, 278);
            this.txt_pid_7_pitch.Multiline = true;
            this.txt_pid_7_pitch.Name = "txt_pid_7_pitch";
            this.txt_pid_7_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_7_pitch.TabIndex = 52;
            this.txt_pid_7_pitch.Text = "0";
            this.txt_pid_7_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_7_yaw
            // 
            this.txt_pid_7_yaw.Location = new System.Drawing.Point(111, 248);
            this.txt_pid_7_yaw.Multiline = true;
            this.txt_pid_7_yaw.Name = "txt_pid_7_yaw";
            this.txt_pid_7_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_7_yaw.TabIndex = 51;
            this.txt_pid_7_yaw.Text = "0";
            this.txt_pid_7_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_7_rol
            // 
            this.txt_pid_7_rol.Location = new System.Drawing.Point(111, 218);
            this.txt_pid_7_rol.Multiline = true;
            this.txt_pid_7_rol.Name = "txt_pid_7_rol";
            this.txt_pid_7_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_7_rol.TabIndex = 50;
            this.txt_pid_7_rol.Text = "0";
            this.txt_pid_7_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_6_pitch
            // 
            this.txt_pid_6_pitch.Location = new System.Drawing.Point(224, 183);
            this.txt_pid_6_pitch.Multiline = true;
            this.txt_pid_6_pitch.Name = "txt_pid_6_pitch";
            this.txt_pid_6_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_6_pitch.TabIndex = 49;
            this.txt_pid_6_pitch.Text = "0";
            this.txt_pid_6_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_6_yaw
            // 
            this.txt_pid_6_yaw.Location = new System.Drawing.Point(223, 153);
            this.txt_pid_6_yaw.Multiline = true;
            this.txt_pid_6_yaw.Name = "txt_pid_6_yaw";
            this.txt_pid_6_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_6_yaw.TabIndex = 48;
            this.txt_pid_6_yaw.Text = "0";
            this.txt_pid_6_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_6_rol
            // 
            this.txt_pid_6_rol.Location = new System.Drawing.Point(225, 123);
            this.txt_pid_6_rol.Multiline = true;
            this.txt_pid_6_rol.Name = "txt_pid_6_rol";
            this.txt_pid_6_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_6_rol.TabIndex = 47;
            this.txt_pid_6_rol.Text = "0";
            this.txt_pid_6_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_5_pitch
            // 
            this.txt_pid_5_pitch.Location = new System.Drawing.Point(167, 183);
            this.txt_pid_5_pitch.Multiline = true;
            this.txt_pid_5_pitch.Name = "txt_pid_5_pitch";
            this.txt_pid_5_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_5_pitch.TabIndex = 46;
            this.txt_pid_5_pitch.Text = "0";
            this.txt_pid_5_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_5_yaw
            // 
            this.txt_pid_5_yaw.Location = new System.Drawing.Point(167, 153);
            this.txt_pid_5_yaw.Multiline = true;
            this.txt_pid_5_yaw.Name = "txt_pid_5_yaw";
            this.txt_pid_5_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_5_yaw.TabIndex = 45;
            this.txt_pid_5_yaw.Text = "0";
            this.txt_pid_5_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_5_rol
            // 
            this.txt_pid_5_rol.Location = new System.Drawing.Point(168, 123);
            this.txt_pid_5_rol.Multiline = true;
            this.txt_pid_5_rol.Name = "txt_pid_5_rol";
            this.txt_pid_5_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_5_rol.TabIndex = 44;
            this.txt_pid_5_rol.Text = "0";
            this.txt_pid_5_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_4_pitch
            // 
            this.txt_pid_4_pitch.Location = new System.Drawing.Point(111, 183);
            this.txt_pid_4_pitch.Multiline = true;
            this.txt_pid_4_pitch.Name = "txt_pid_4_pitch";
            this.txt_pid_4_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_4_pitch.TabIndex = 43;
            this.txt_pid_4_pitch.Text = "0";
            this.txt_pid_4_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_4_yaw
            // 
            this.txt_pid_4_yaw.Location = new System.Drawing.Point(111, 153);
            this.txt_pid_4_yaw.Multiline = true;
            this.txt_pid_4_yaw.Name = "txt_pid_4_yaw";
            this.txt_pid_4_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_4_yaw.TabIndex = 42;
            this.txt_pid_4_yaw.Text = "0";
            this.txt_pid_4_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_4_rol
            // 
            this.txt_pid_4_rol.Location = new System.Drawing.Point(111, 123);
            this.txt_pid_4_rol.Multiline = true;
            this.txt_pid_4_rol.Name = "txt_pid_4_rol";
            this.txt_pid_4_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_4_rol.TabIndex = 41;
            this.txt_pid_4_rol.Text = "0";
            this.txt_pid_4_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox9
            // 
            this.groupBox9.Location = new System.Drawing.Point(3, 304);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(315, 10);
            this.groupBox9.TabIndex = 40;
            this.groupBox9.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(3, 202);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(315, 10);
            this.groupBox8.TabIndex = 39;
            this.groupBox8.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(3, 106);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(312, 10);
            this.groupBox7.TabIndex = 38;
            this.groupBox7.TabStop = false;
            // 
            // btnGetPID
            // 
            this.btnGetPID.Location = new System.Drawing.Point(232, 320);
            this.btnGetPID.Name = "btnGetPID";
            this.btnGetPID.Size = new System.Drawing.Size(61, 30);
            this.btnGetPID.TabIndex = 37;
            this.btnGetPID.Text = "读取";
            this.btnGetPID.UseVisualStyleBackColor = true;
            this.btnGetPID.Click += new System.EventHandler(this.btnGetPID_Click);
            // 
            // btnSendPID
            // 
            this.btnSendPID.Location = new System.Drawing.Point(111, 320);
            this.btnSendPID.Name = "btnSendPID";
            this.btnSendPID.Size = new System.Drawing.Size(61, 30);
            this.btnSendPID.TabIndex = 36;
            this.btnSendPID.Text = "发送";
            this.btnSendPID.UseVisualStyleBackColor = true;
            this.btnSendPID.Click += new System.EventHandler(this.btnSendPID_Click);
            // 
            // txt_pid_3_pitch
            // 
            this.txt_pid_3_pitch.Location = new System.Drawing.Point(224, 84);
            this.txt_pid_3_pitch.Multiline = true;
            this.txt_pid_3_pitch.Name = "txt_pid_3_pitch";
            this.txt_pid_3_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_3_pitch.TabIndex = 35;
            this.txt_pid_3_pitch.Text = "0";
            this.txt_pid_3_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_3_yaw
            // 
            this.txt_pid_3_yaw.Location = new System.Drawing.Point(225, 54);
            this.txt_pid_3_yaw.Multiline = true;
            this.txt_pid_3_yaw.Name = "txt_pid_3_yaw";
            this.txt_pid_3_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_3_yaw.TabIndex = 34;
            this.txt_pid_3_yaw.Text = "0";
            this.txt_pid_3_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_3_rol
            // 
            this.txt_pid_3_rol.Location = new System.Drawing.Point(225, 24);
            this.txt_pid_3_rol.Multiline = true;
            this.txt_pid_3_rol.Name = "txt_pid_3_rol";
            this.txt_pid_3_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_3_rol.TabIndex = 33;
            this.txt_pid_3_rol.Text = "0";
            this.txt_pid_3_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_2_pitch
            // 
            this.txt_pid_2_pitch.Location = new System.Drawing.Point(167, 84);
            this.txt_pid_2_pitch.Multiline = true;
            this.txt_pid_2_pitch.Name = "txt_pid_2_pitch";
            this.txt_pid_2_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_2_pitch.TabIndex = 32;
            this.txt_pid_2_pitch.Text = "0";
            this.txt_pid_2_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_2_yaw
            // 
            this.txt_pid_2_yaw.Location = new System.Drawing.Point(168, 54);
            this.txt_pid_2_yaw.Multiline = true;
            this.txt_pid_2_yaw.Name = "txt_pid_2_yaw";
            this.txt_pid_2_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_2_yaw.TabIndex = 31;
            this.txt_pid_2_yaw.Text = "0";
            this.txt_pid_2_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_2_rol
            // 
            this.txt_pid_2_rol.Location = new System.Drawing.Point(168, 24);
            this.txt_pid_2_rol.Multiline = true;
            this.txt_pid_2_rol.Name = "txt_pid_2_rol";
            this.txt_pid_2_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_2_rol.TabIndex = 30;
            this.txt_pid_2_rol.Text = "0";
            this.txt_pid_2_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_1_pitch
            // 
            this.txt_pid_1_pitch.Location = new System.Drawing.Point(111, 84);
            this.txt_pid_1_pitch.Multiline = true;
            this.txt_pid_1_pitch.Name = "txt_pid_1_pitch";
            this.txt_pid_1_pitch.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_1_pitch.TabIndex = 29;
            this.txt_pid_1_pitch.Text = "0";
            this.txt_pid_1_pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_1_yaw
            // 
            this.txt_pid_1_yaw.Location = new System.Drawing.Point(111, 54);
            this.txt_pid_1_yaw.Multiline = true;
            this.txt_pid_1_yaw.Name = "txt_pid_1_yaw";
            this.txt_pid_1_yaw.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_1_yaw.TabIndex = 28;
            this.txt_pid_1_yaw.Text = "0";
            this.txt_pid_1_yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pid_1_rol
            // 
            this.txt_pid_1_rol.Location = new System.Drawing.Point(111, 24);
            this.txt_pid_1_rol.Multiline = true;
            this.txt_pid_1_rol.Name = "txt_pid_1_rol";
            this.txt_pid_1_rol.Size = new System.Drawing.Size(58, 19);
            this.txt_pid_1_rol.TabIndex = 19;
            this.txt_pid_1_rol.Text = "0";
            this.txt_pid_1_rol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label43.Location = new System.Drawing.Point(23, 254);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(49, 19);
            this.label43.TabIndex = 26;
            this.label43.Text = "PID3";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label40.Location = new System.Drawing.Point(23, 150);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(49, 19);
            this.label40.TabIndex = 23;
            this.label40.Text = "PID2";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label37.Location = new System.Drawing.Point(23, 53);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(49, 19);
            this.label37.TabIndex = 20;
            this.label37.Text = "PID1";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_unlock);
            this.groupBox6.Controls.Add(this.btn_lock);
            this.groupBox6.Controls.Add(this.btnMag);
            this.groupBox6.Controls.Add(this.btnAcc);
            this.groupBox6.Controls.Add(this.btnGyrd);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(687, 387);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(318, 198);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "数据校准";
            // 
            // btn_unlock
            // 
            this.btn_unlock.Location = new System.Drawing.Point(184, 154);
            this.btn_unlock.Name = "btn_unlock";
            this.btn_unlock.Size = new System.Drawing.Size(106, 34);
            this.btn_unlock.TabIndex = 4;
            this.btn_unlock.Text = "解锁";
            this.btn_unlock.UseVisualStyleBackColor = true;
            this.btn_unlock.Click += new System.EventHandler(this.btn_unlock_Click);
            // 
            // btn_lock
            // 
            this.btn_lock.Location = new System.Drawing.Point(24, 153);
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.Size = new System.Drawing.Size(106, 34);
            this.btn_lock.TabIndex = 3;
            this.btn_lock.Text = "加锁";
            this.btn_lock.UseVisualStyleBackColor = true;
            this.btn_lock.Click += new System.EventHandler(this.btn_lock_Click);
            // 
            // btnMag
            // 
            this.btnMag.Location = new System.Drawing.Point(108, 103);
            this.btnMag.Name = "btnMag";
            this.btnMag.Size = new System.Drawing.Size(106, 35);
            this.btnMag.TabIndex = 2;
            this.btnMag.Text = "磁力计校准";
            this.btnMag.UseVisualStyleBackColor = true;
            this.btnMag.Click += new System.EventHandler(this.btnMag_Click);
            // 
            // btnAcc
            // 
            this.btnAcc.Location = new System.Drawing.Point(184, 50);
            this.btnAcc.Name = "btnAcc";
            this.btnAcc.Size = new System.Drawing.Size(106, 34);
            this.btnAcc.TabIndex = 1;
            this.btnAcc.Text = "加速度校准";
            this.btnAcc.UseVisualStyleBackColor = true;
            this.btnAcc.Click += new System.EventHandler(this.btnAcc_Click);
            // 
            // btnGyrd
            // 
            this.btnGyrd.Location = new System.Drawing.Point(24, 50);
            this.btnGyrd.Name = "btnGyrd";
            this.btnGyrd.Size = new System.Drawing.Size(106, 34);
            this.btnGyrd.TabIndex = 0;
            this.btnGyrd.Text = "陀螺仪校准";
            this.btnGyrd.UseVisualStyleBackColor = true;
            this.btnGyrd.Click += new System.EventHandler(this.btnGyrd_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(12, 12);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(313, 328);
            this.elementHost1.TabIndex = 15;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lab_msg
            // 
            this.lab_msg.AutoSize = true;
            this.lab_msg.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_msg.ForeColor = System.Drawing.Color.Red;
            this.lab_msg.Location = new System.Drawing.Point(160, 615);
            this.lab_msg.Name = "lab_msg";
            this.lab_msg.Size = new System.Drawing.Size(119, 19);
            this.lab_msg.TabIndex = 16;
            this.lab_msg.Text = "-----------";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(12, 615);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 19);
            this.label6.TabIndex = 17;
            this.label6.Text = "系统提示信息：";
            // 
            // btn_ReviceMsg
            // 
            this.btn_ReviceMsg.Location = new System.Drawing.Point(882, 591);
            this.btn_ReviceMsg.Name = "btn_ReviceMsg";
            this.btn_ReviceMsg.Size = new System.Drawing.Size(123, 43);
            this.btn_ReviceMsg.TabIndex = 18;
            this.btn_ReviceMsg.Text = "接收数据";
            this.btn_ReviceMsg.UseVisualStyleBackColor = true;
            this.btn_ReviceMsg.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(687, 591);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(123, 43);
            this.btn_start.TabIndex = 19;
            this.btn_start.Text = "开启系统";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // FilghtControlFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 644);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_ReviceMsg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lab_msg);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lab_butter);
            this.Controls.Add(this.lab_pitch);
            this.Controls.Add(this.lab_yaw);
            this.Controls.Add(this.lab_roll);
            this.Controls.Add(this.lab_lock);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FilghtControlFrm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FilghtControlFrm_FormClosed);
            this.Load += new System.EventHandler(this.FilghtControlFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_pitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_roll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_yaw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_thr)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_lock;
        private System.Windows.Forms.Label lab_roll;
        private System.Windows.Forms.Label lab_yaw;
        private System.Windows.Forms.Label lab_pitch;
        private System.Windows.Forms.Label lab_butter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSendRemote;
        private System.Windows.Forms.TextBox txt_pitch;
        private System.Windows.Forms.TextBox txt_roll;
        private System.Windows.Forms.TextBox txt_yaw;
        private System.Windows.Forms.TextBox txt_thr;
        private System.Windows.Forms.TrackBar tb_pitch;
        private System.Windows.Forms.TrackBar tb_roll;
        private System.Windows.Forms.TrackBar tb_yaw;
        private System.Windows.Forms.TrackBar tb_thr;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lab_mag_z;
        private System.Windows.Forms.Label lab_mag_x;
        private System.Windows.Forms.Label lab_mag_y;
        private System.Windows.Forms.Label lab_gyrd_z;
        private System.Windows.Forms.Label lab_gyrd_x;
        private System.Windows.Forms.Label lab_gyrd_y;
        private System.Windows.Forms.Label lab_acc_z;
        private System.Windows.Forms.Label lab_acc_x;
        private System.Windows.Forms.Label lab_acc_y;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnMag;
        private System.Windows.Forms.Button btnAcc;
        private System.Windows.Forms.Button btnGyrd;
        private System.Windows.Forms.Button btnGetPID;
        private System.Windows.Forms.Button btnSendPID;
        private System.Windows.Forms.TextBox txt_pid_3_pitch;
        private System.Windows.Forms.TextBox txt_pid_3_yaw;
        private System.Windows.Forms.TextBox txt_pid_3_rol;
        private System.Windows.Forms.TextBox txt_pid_2_pitch;
        private System.Windows.Forms.TextBox txt_pid_2_yaw;
        private System.Windows.Forms.TextBox txt_pid_2_rol;
        private System.Windows.Forms.TextBox txt_pid_1_pitch;
        private System.Windows.Forms.TextBox txt_pid_1_yaw;
        private System.Windows.Forms.TextBox txt_pid_1_rol;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txt_pid_9_pitch;
        private System.Windows.Forms.TextBox txt_pid_9_yaw;
        private System.Windows.Forms.TextBox txt_pid_9_rol;
        private System.Windows.Forms.TextBox txt_pid_8_pitch;
        private System.Windows.Forms.TextBox txt_pid_8_yaw;
        private System.Windows.Forms.TextBox txt_pid_8_rol;
        private System.Windows.Forms.TextBox txt_pid_7_pitch;
        private System.Windows.Forms.TextBox txt_pid_7_yaw;
        private System.Windows.Forms.TextBox txt_pid_7_rol;
        private System.Windows.Forms.TextBox txt_pid_6_pitch;
        private System.Windows.Forms.TextBox txt_pid_6_yaw;
        private System.Windows.Forms.TextBox txt_pid_6_rol;
        private System.Windows.Forms.TextBox txt_pid_5_pitch;
        private System.Windows.Forms.TextBox txt_pid_5_yaw;
        private System.Windows.Forms.TextBox txt_pid_5_rol;
        private System.Windows.Forms.TextBox txt_pid_4_pitch;
        private System.Windows.Forms.TextBox txt_pid_4_yaw;
        private System.Windows.Forms.TextBox txt_pid_4_rol;
        private System.Windows.Forms.ComboBox cb_pid;
        private System.Windows.Forms.Label lab_msg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_unlock;
        private System.Windows.Forms.Button btn_lock;
        private System.Windows.Forms.Button btn_ReviceMsg;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.IO.Ports.SerialPort serialPort1;
    }
}

