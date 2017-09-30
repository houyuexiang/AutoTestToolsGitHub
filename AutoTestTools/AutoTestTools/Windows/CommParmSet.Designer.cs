namespace AutoTestTools.Windows
{
    partial class CommParmSet
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
            this.RB_Client = new System.Windows.Forms.RadioButton();
            this.RB_Server = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.P_Server = new System.Windows.Forms.Panel();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.P_Client = new System.Windows.Forms.Panel();
            this.TB_RPort = new System.Windows.Forms.TextBox();
            this.TB_RIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.B_Save = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.P_Server.SuspendLayout();
            this.P_Client.SuspendLayout();
            this.SuspendLayout();
            // 
            // RB_Client
            // 
            this.RB_Client.AutoSize = true;
            this.RB_Client.Location = new System.Drawing.Point(197, 20);
            this.RB_Client.Name = "RB_Client";
            this.RB_Client.Size = new System.Drawing.Size(87, 22);
            this.RB_Client.TabIndex = 0;
            this.RB_Client.Text = "Client";
            this.RB_Client.UseVisualStyleBackColor = true;
            this.RB_Client.CheckedChanged += new System.EventHandler(this.RB_Client_CheckedChanged);
            // 
            // RB_Server
            // 
            this.RB_Server.AutoSize = true;
            this.RB_Server.Checked = true;
            this.RB_Server.Location = new System.Drawing.Point(22, 20);
            this.RB_Server.Name = "RB_Server";
            this.RB_Server.Size = new System.Drawing.Size(87, 22);
            this.RB_Server.TabIndex = 1;
            this.RB_Server.TabStop = true;
            this.RB_Server.Text = "Server";
            this.RB_Server.UseVisualStyleBackColor = true;
            this.RB_Server.CheckedChanged += new System.EventHandler(this.RB_Server_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.P_Server);
            this.panel1.Controls.Add(this.P_Client);
            this.panel1.Controls.Add(this.RB_Server);
            this.panel1.Controls.Add(this.RB_Client);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 232);
            this.panel1.TabIndex = 2;
            // 
            // P_Server
            // 
            this.P_Server.Controls.Add(this.TB_Port);
            this.P_Server.Controls.Add(this.label1);
            this.P_Server.Location = new System.Drawing.Point(13, 48);
            this.P_Server.Name = "P_Server";
            this.P_Server.Size = new System.Drawing.Size(311, 161);
            this.P_Server.TabIndex = 2;
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(121, 75);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(174, 28);
            this.TB_Port.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "PortNo";
            // 
            // P_Client
            // 
            this.P_Client.Controls.Add(this.TB_RPort);
            this.P_Client.Controls.Add(this.TB_RIP);
            this.P_Client.Controls.Add(this.label3);
            this.P_Client.Controls.Add(this.label2);
            this.P_Client.Location = new System.Drawing.Point(13, 48);
            this.P_Client.Name = "P_Client";
            this.P_Client.Size = new System.Drawing.Size(311, 161);
            this.P_Client.TabIndex = 3;
            // 
            // TB_RPort
            // 
            this.TB_RPort.Location = new System.Drawing.Point(123, 75);
            this.TB_RPort.Name = "TB_RPort";
            this.TB_RPort.Size = new System.Drawing.Size(174, 28);
            this.TB_RPort.TabIndex = 3;
            // 
            // TB_RIP
            // 
            this.TB_RIP.Location = new System.Drawing.Point(123, 27);
            this.TB_RIP.Name = "TB_RIP";
            this.TB_RIP.Size = new System.Drawing.Size(174, 28);
            this.TB_RIP.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "RemotePort";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "RemoteIP";
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(121, 261);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(122, 40);
            this.B_Save.TabIndex = 3;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            // 
            // CommParmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 322);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "CommParmSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CommParmSet";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.P_Server.ResumeLayout(false);
            this.P_Server.PerformLayout();
            this.P_Client.ResumeLayout(false);
            this.P_Client.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton RB_Client;
        private System.Windows.Forms.RadioButton RB_Server;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel P_Server;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel P_Client;
        private System.Windows.Forms.TextBox TB_RPort;
        private System.Windows.Forms.TextBox TB_RIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_Save;
    }
}