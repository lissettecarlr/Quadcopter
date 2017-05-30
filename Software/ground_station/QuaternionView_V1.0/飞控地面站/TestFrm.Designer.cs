namespace 飞控地面站
{
    partial class TestFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lab_roll = new System.Windows.Forms.Label();
            this.lab_pitch = new System.Windows.Forms.Label();
            this.lab_yaw = new System.Windows.Forms.Label();
            this.lab_lock = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lab_roll
            // 
            this.lab_roll.AutoSize = true;
            this.lab_roll.Location = new System.Drawing.Point(112, 58);
            this.lab_roll.Name = "lab_roll";
            this.lab_roll.Size = new System.Drawing.Size(41, 12);
            this.lab_roll.TabIndex = 0;
            this.lab_roll.Text = "label1";
            // 
            // lab_pitch
            // 
            this.lab_pitch.AutoSize = true;
            this.lab_pitch.Location = new System.Drawing.Point(112, 112);
            this.lab_pitch.Name = "lab_pitch";
            this.lab_pitch.Size = new System.Drawing.Size(41, 12);
            this.lab_pitch.TabIndex = 1;
            this.lab_pitch.Text = "label2";
            // 
            // lab_yaw
            // 
            this.lab_yaw.AutoSize = true;
            this.lab_yaw.Location = new System.Drawing.Point(112, 170);
            this.lab_yaw.Name = "lab_yaw";
            this.lab_yaw.Size = new System.Drawing.Size(41, 12);
            this.lab_yaw.TabIndex = 2;
            this.lab_yaw.Text = "label3";
            // 
            // lab_lock
            // 
            this.lab_lock.AutoSize = true;
            this.lab_lock.Location = new System.Drawing.Point(112, 224);
            this.lab_lock.Name = "lab_lock";
            this.lab_lock.Size = new System.Drawing.Size(41, 12);
            this.lab_lock.TabIndex = 3;
            this.lab_lock.Text = "label4";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TestFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 261);
            this.Controls.Add(this.lab_lock);
            this.Controls.Add(this.lab_yaw);
            this.Controls.Add(this.lab_pitch);
            this.Controls.Add(this.lab_roll);
            this.Name = "TestFrm";
            this.Text = "TestFrm";
            this.Load += new System.EventHandler(this.TestFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_roll;
        private System.Windows.Forms.Label lab_pitch;
        private System.Windows.Forms.Label lab_yaw;
        private System.Windows.Forms.Label lab_lock;
        private System.Windows.Forms.Timer timer1;
    }
}