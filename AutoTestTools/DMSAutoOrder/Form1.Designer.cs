namespace DMSAutoOrder
{
    partial class Form1
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
            this.B_Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_IP = new System.Windows.Forms.TextBox();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_DBIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RB_Server = new System.Windows.Forms.RadioButton();
            this.RB_Client = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CB_SameIP = new System.Windows.Forms.CheckBox();
            this.TB_DBPW = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_DBname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_DBUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CB_AutoStart = new System.Windows.Forms.CheckBox();
            this.B_Save = new System.Windows.Forms.Button();
            this.TB_SendTestCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_Start
            // 
            this.B_Start.Location = new System.Drawing.Point(618, 319);
            this.B_Start.Name = "B_Start";
            this.B_Start.Size = new System.Drawing.Size(140, 43);
            this.B_Start.TabIndex = 0;
            this.B_Start.Text = "Start";
            this.B_Start.UseVisualStyleBackColor = true;
            this.B_Start.Click += new System.EventHandler(this.B_Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address";
            // 
            // TB_IP
            // 
            this.TB_IP.Location = new System.Drawing.Point(132, 37);
            this.TB_IP.Name = "TB_IP";
            this.TB_IP.Size = new System.Drawing.Size(204, 28);
            this.TB_IP.TabIndex = 2;
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(132, 89);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(204, 28);
            this.TB_Port.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "PortNo";
            // 
            // TB_DBIP
            // 
            this.TB_DBIP.Location = new System.Drawing.Point(175, 69);
            this.TB_DBIP.Name = "TB_DBIP";
            this.TB_DBIP.Size = new System.Drawing.Size(218, 28);
            this.TB_DBIP.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Database IP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_SendTestCode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TB_IP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TB_Port);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 269);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DMS Connect";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RB_Server);
            this.panel1.Controls.Add(this.RB_Client);
            this.panel1.Location = new System.Drawing.Point(19, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 58);
            this.panel1.TabIndex = 5;
            // 
            // RB_Server
            // 
            this.RB_Server.AutoSize = true;
            this.RB_Server.Location = new System.Drawing.Point(185, 17);
            this.RB_Server.Name = "RB_Server";
            this.RB_Server.Size = new System.Drawing.Size(87, 22);
            this.RB_Server.TabIndex = 1;
            this.RB_Server.Text = "Server";
            this.RB_Server.UseVisualStyleBackColor = true;
            this.RB_Server.CheckedChanged += new System.EventHandler(this.RB_Server_CheckedChanged);
            // 
            // RB_Client
            // 
            this.RB_Client.AutoSize = true;
            this.RB_Client.Checked = true;
            this.RB_Client.Location = new System.Drawing.Point(27, 17);
            this.RB_Client.Name = "RB_Client";
            this.RB_Client.Size = new System.Drawing.Size(87, 22);
            this.RB_Client.TabIndex = 0;
            this.RB_Client.TabStop = true;
            this.RB_Client.Text = "Client";
            this.RB_Client.UseVisualStyleBackColor = true;
            this.RB_Client.CheckedChanged += new System.EventHandler(this.RB_Client_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CB_SameIP);
            this.groupBox2.Controls.Add(this.TB_DBPW);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TB_DBname);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TB_DBUser);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.TB_DBIP);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(434, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(751, 268);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DMS Database Connect";
            // 
            // CB_SameIP
            // 
            this.CB_SameIP.AutoSize = true;
            this.CB_SameIP.Location = new System.Drawing.Point(409, 72);
            this.CB_SameIP.Name = "CB_SameIP";
            this.CB_SameIP.Size = new System.Drawing.Size(250, 22);
            this.CB_SameIP.TabIndex = 15;
            this.CB_SameIP.Text = "DB IP Same As Connect IP";
            this.CB_SameIP.UseVisualStyleBackColor = true;
            this.CB_SameIP.CheckedChanged += new System.EventHandler(this.CB_SameIP_CheckedChanged);
            // 
            // TB_DBPW
            // 
            this.TB_DBPW.Location = new System.Drawing.Point(600, 129);
            this.TB_DBPW.Name = "TB_DBPW";
            this.TB_DBPW.Size = new System.Drawing.Size(145, 28);
            this.TB_DBPW.TabIndex = 14;
            this.TB_DBPW.Text = "lINEA_3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(406, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Database Password";
            // 
            // TB_DBname
            // 
            this.TB_DBname.Location = new System.Drawing.Point(175, 190);
            this.TB_DBname.Name = "TB_DBname";
            this.TB_DBname.Size = new System.Drawing.Size(218, 28);
            this.TB_DBname.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "DatabaseName";
            // 
            // TB_DBUser
            // 
            this.TB_DBUser.Location = new System.Drawing.Point(175, 129);
            this.TB_DBUser.Name = "TB_DBUser";
            this.TB_DBUser.Size = new System.Drawing.Size(218, 28);
            this.TB_DBUser.TabIndex = 10;
            this.TB_DBUser.Text = "root";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database UserName";
            // 
            // CB_AutoStart
            // 
            this.CB_AutoStart.AutoSize = true;
            this.CB_AutoStart.Location = new System.Drawing.Point(21, 287);
            this.CB_AutoStart.Name = "CB_AutoStart";
            this.CB_AutoStart.Size = new System.Drawing.Size(124, 22);
            this.CB_AutoStart.TabIndex = 16;
            this.CB_AutoStart.Text = "Auto Start";
            this.CB_AutoStart.UseVisualStyleBackColor = true;
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(344, 319);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(140, 43);
            this.B_Save.TabIndex = 17;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // TB_SendTestCode
            // 
            this.TB_SendTestCode.Location = new System.Drawing.Point(219, 223);
            this.TB_SendTestCode.Name = "TB_SendTestCode";
            this.TB_SendTestCode.Size = new System.Drawing.Size(145, 28);
            this.TB_SendTestCode.TabIndex = 17;
            this.TB_SendTestCode.Text = "PARK";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Send Add Test Code";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 388);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.CB_AutoStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.B_Start);
            this.Name = "Form1";
            this.Text = "DMSAutoOrder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_IP;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_DBIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RB_Server;
        private System.Windows.Forms.RadioButton RB_Client;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TB_DBPW;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_DBname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_DBUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CB_SameIP;
        private System.Windows.Forms.CheckBox CB_AutoStart;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.TextBox TB_SendTestCode;
        private System.Windows.Forms.Label label7;
    }
}

