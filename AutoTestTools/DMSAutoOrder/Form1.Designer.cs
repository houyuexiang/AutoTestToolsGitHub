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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.B_Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_IP = new System.Windows.Forms.TextBox();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_DBIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_SendTestCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.CBD_ModifyStatus = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TB_IgnoreDMSFlagList = new System.Windows.Forms.TextBox();
            this.CB_NoWorkOrder = new System.Windows.Forms.CheckBox();
            this.CB_EnablePitstopTableMonitor = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_Start
            // 
            this.B_Start.Location = new System.Drawing.Point(1468, 130);
            this.B_Start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.B_Start.Name = "B_Start";
            this.B_Start.Size = new System.Drawing.Size(186, 58);
            this.B_Start.TabIndex = 14;
            this.B_Start.Text = "Start";
            this.B_Start.UseVisualStyleBackColor = true;
            this.B_Start.Click += new System.EventHandler(this.B_Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address";
            // 
            // TB_IP
            // 
            this.TB_IP.Location = new System.Drawing.Point(176, 50);
            this.TB_IP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_IP.Name = "TB_IP";
            this.TB_IP.Size = new System.Drawing.Size(270, 35);
            this.TB_IP.TabIndex = 1;
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(176, 118);
            this.TB_Port.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(270, 35);
            this.TB_Port.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "PortNo";
            // 
            // TB_DBIP
            // 
            this.TB_DBIP.Location = new System.Drawing.Point(234, 46);
            this.TB_DBIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_DBIP.Name = "TB_DBIP";
            this.TB_DBIP.Size = new System.Drawing.Size(290, 35);
            this.TB_DBIP.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 24);
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
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(578, 422);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DMS Connect";
            // 
            // TB_SendTestCode
            // 
            this.TB_SendTestCode.Location = new System.Drawing.Point(278, 298);
            this.TB_SendTestCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_SendTestCode.Name = "TB_SendTestCode";
            this.TB_SendTestCode.Size = new System.Drawing.Size(192, 35);
            this.TB_SendTestCode.TabIndex = 5;
            this.TB_SendTestCode.Text = "PARK";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 302);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(226, 24);
            this.label7.TabIndex = 16;
            this.label7.Text = "Send Add Test Code";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RB_Server);
            this.panel1.Controls.Add(this.RB_Client);
            this.panel1.Location = new System.Drawing.Point(26, 186);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 78);
            this.panel1.TabIndex = 5;
            // 
            // RB_Server
            // 
            this.RB_Server.AutoSize = true;
            this.RB_Server.Location = new System.Drawing.Point(246, 22);
            this.RB_Server.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RB_Server.Name = "RB_Server";
            this.RB_Server.Size = new System.Drawing.Size(113, 28);
            this.RB_Server.TabIndex = 4;
            this.RB_Server.TabStop = true;
            this.RB_Server.Text = "Server";
            this.RB_Server.UseVisualStyleBackColor = true;
            this.RB_Server.CheckedChanged += new System.EventHandler(this.RB_Server_CheckedChanged);
            // 
            // RB_Client
            // 
            this.RB_Client.AutoSize = true;
            this.RB_Client.Checked = true;
            this.RB_Client.Location = new System.Drawing.Point(36, 22);
            this.RB_Client.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RB_Client.Name = "RB_Client";
            this.RB_Client.Size = new System.Drawing.Size(113, 28);
            this.RB_Client.TabIndex = 3;
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
            this.groupBox2.Location = new System.Drawing.Point(22, 476);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(572, 422);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DMS Database Connect";
            // 
            // CB_SameIP
            // 
            this.CB_SameIP.AutoSize = true;
            this.CB_SameIP.Location = new System.Drawing.Point(12, 110);
            this.CB_SameIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CB_SameIP.Name = "CB_SameIP";
            this.CB_SameIP.Size = new System.Drawing.Size(330, 28);
            this.CB_SameIP.TabIndex = 8;
            this.CB_SameIP.Text = "DB IP Same As Connect IP";
            this.CB_SameIP.UseVisualStyleBackColor = true;
            this.CB_SameIP.CheckedChanged += new System.EventHandler(this.CB_SameIP_CheckedChanged);
            // 
            // TB_DBPW
            // 
            this.TB_DBPW.Location = new System.Drawing.Point(234, 258);
            this.TB_DBPW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_DBPW.Name = "TB_DBPW";
            this.TB_DBPW.Size = new System.Drawing.Size(290, 35);
            this.TB_DBPW.TabIndex = 10;
            this.TB_DBPW.Text = "lINEA_3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 260);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(214, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "Database Password";
            // 
            // TB_DBname
            // 
            this.TB_DBname.Location = new System.Drawing.Point(234, 338);
            this.TB_DBname.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_DBname.Name = "TB_DBname";
            this.TB_DBname.Size = new System.Drawing.Size(290, 35);
            this.TB_DBname.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 342);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "DatabaseName";
            // 
            // TB_DBUser
            // 
            this.TB_DBUser.Location = new System.Drawing.Point(234, 170);
            this.TB_DBUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_DBUser.Name = "TB_DBUser";
            this.TB_DBUser.Size = new System.Drawing.Size(290, 35);
            this.TB_DBUser.TabIndex = 9;
            this.TB_DBUser.Text = "root";
            this.TB_DBUser.TextChanged += new System.EventHandler(this.TB_DBUser_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 174);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database UserName";
            // 
            // CB_AutoStart
            // 
            this.CB_AutoStart.AutoSize = true;
            this.CB_AutoStart.Location = new System.Drawing.Point(28, 120);
            this.CB_AutoStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CB_AutoStart.Name = "CB_AutoStart";
            this.CB_AutoStart.Size = new System.Drawing.Size(162, 28);
            this.CB_AutoStart.TabIndex = 12;
            this.CB_AutoStart.Text = "Auto Start";
            this.CB_AutoStart.UseVisualStyleBackColor = true;
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(1468, 40);
            this.B_Save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(186, 58);
            this.B_Save.TabIndex = 13;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "DMSAutoOrder";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 80);
            this.contextMenuStrip1.Text = "Menu";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(151, 38);
            this.toolStripMenuItem1.Text = "Show";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(151, 38);
            this.toolStripMenuItem2.Text = "Exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 40);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(454, 24);
            this.label8.TabIndex = 15;
            this.label8.Text = "Automaticly Modify Wrong Test Status:";
            // 
            // CBD_ModifyStatus
            // 
            this.CBD_ModifyStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBD_ModifyStatus.FormattingEnabled = true;
            this.CBD_ModifyStatus.Items.AddRange(new object[] {
            "Do Not Change The Status",
            "Modify Send To Host Status to 0",
            "Modify Test Status To Final"});
            this.CBD_ModifyStatus.Location = new System.Drawing.Point(26, 68);
            this.CBD_ModifyStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CBD_ModifyStatus.Name = "CBD_ModifyStatus";
            this.CBD_ModifyStatus.Size = new System.Drawing.Size(764, 32);
            this.CBD_ModifyStatus.Sorted = true;
            this.CBD_ModifyStatus.TabIndex = 16;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CB_EnablePitstopTableMonitor);
            this.groupBox3.Controls.Add(this.CB_NoWorkOrder);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.TB_IgnoreDMSFlagList);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.CBD_ModifyStatus);
            this.groupBox3.Controls.Add(this.CB_AutoStart);
            this.groupBox3.Location = new System.Drawing.Point(622, 24);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Size = new System.Drawing.Size(804, 874);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DMS Setting";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 192);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(466, 24);
            this.label10.TabIndex = 17;
            this.label10.Text = "Separated by a comma. e.g. flag1,flag2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 156);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(670, 24);
            this.label9.TabIndex = 15;
            this.label9.Text = "Auto Val Results when the following flags are present. ";
            // 
            // TB_IgnoreDMSFlagList
            // 
            this.TB_IgnoreDMSFlagList.Location = new System.Drawing.Point(26, 220);
            this.TB_IgnoreDMSFlagList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TB_IgnoreDMSFlagList.Name = "TB_IgnoreDMSFlagList";
            this.TB_IgnoreDMSFlagList.Size = new System.Drawing.Size(764, 35);
            this.TB_IgnoreDMSFlagList.TabIndex = 14;
            // 
            // CB_NoWorkOrder
            // 
            this.CB_NoWorkOrder.AutoSize = true;
            this.CB_NoWorkOrder.Location = new System.Drawing.Point(28, 293);
            this.CB_NoWorkOrder.Name = "CB_NoWorkOrder";
            this.CB_NoWorkOrder.Size = new System.Drawing.Size(330, 28);
            this.CB_NoWorkOrder.TabIndex = 18;
            this.CB_NoWorkOrder.Text = "EnableNoWorkOrderProcess";
            this.CB_NoWorkOrder.UseVisualStyleBackColor = true;
            // 
            // CB_EnablePitstopTableMonitor
            // 
            this.CB_EnablePitstopTableMonitor.AutoSize = true;
            this.CB_EnablePitstopTableMonitor.Location = new System.Drawing.Point(28, 344);
            this.CB_EnablePitstopTableMonitor.Name = "CB_EnablePitstopTableMonitor";
            this.CB_EnablePitstopTableMonitor.Size = new System.Drawing.Size(342, 28);
            this.CB_EnablePitstopTableMonitor.TabIndex = 19;
            this.CB_EnablePitstopTableMonitor.Text = "EnablePitstopTableMonitor";
            this.CB_EnablePitstopTableMonitor.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1680, 918);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.B_Start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DMSAutoOrder";
            this.MinimumSizeChanged += new System.EventHandler(this.Form1_MinimumSizeChanged);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_MinimumSizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CBD_ModifyStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TB_IgnoreDMSFlagList;
        private System.Windows.Forms.CheckBox CB_EnablePitstopTableMonitor;
        private System.Windows.Forms.CheckBox CB_NoWorkOrder;
    }
}

