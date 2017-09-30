namespace AutoTestTools.Windows
{
    partial class InstrumentList
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
            this.DGV_InstrumentList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbx_codingsystem = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ckb_instrumentenable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_portno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdb_server = new System.Windows.Forms.RadioButton();
            this.rdb_client = new System.Windows.Forms.RadioButton();
            this.tb_ipaddress = new System.Windows.Forms.TextBox();
            this.cbx_driver = new System.Windows.Forms.ComboBox();
            this.cbx_instrumenttype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Instrumentna = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_new = new System.Windows.Forms.Button();
            this.bt_delete = new System.Windows.Forms.Button();
            this.bt_save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_InstrumentList)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV_InstrumentList
            // 
            this.DGV_InstrumentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_InstrumentList.Location = new System.Drawing.Point(12, 12);
            this.DGV_InstrumentList.Name = "DGV_InstrumentList";
            this.DGV_InstrumentList.RowTemplate.Height = 30;
            this.DGV_InstrumentList.Size = new System.Drawing.Size(412, 483);
            this.DGV_InstrumentList.TabIndex = 0;
            this.DGV_InstrumentList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_InstrumentList_CellClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbx_codingsystem);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ckb_instrumentenable);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cbx_driver);
            this.panel1.Controls.Add(this.cbx_instrumenttype);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_Instrumentna);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(432, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 526);
            this.panel1.TabIndex = 1;
            // 
            // cbx_codingsystem
            // 
            this.cbx_codingsystem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbx_codingsystem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbx_codingsystem.FormattingEnabled = true;
            this.cbx_codingsystem.Location = new System.Drawing.Point(213, 216);
            this.cbx_codingsystem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbx_codingsystem.Name = "cbx_codingsystem";
            this.cbx_codingsystem.Size = new System.Drawing.Size(247, 26);
            this.cbx_codingsystem.TabIndex = 15;
            this.cbx_codingsystem.ValueMember = "Name";
            this.cbx_codingsystem.SelectedIndexChanged += new System.EventHandler(this.Modify);
            this.cbx_codingsystem.TextChanged += new System.EventHandler(this.Modify);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 220);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 18);
            this.label6.TabIndex = 14;
            this.label6.Text = "CodingSystem";
            // 
            // ckb_instrumentenable
            // 
            this.ckb_instrumentenable.AutoSize = true;
            this.ckb_instrumentenable.Location = new System.Drawing.Point(30, 268);
            this.ckb_instrumentenable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckb_instrumentenable.Name = "ckb_instrumentenable";
            this.ckb_instrumentenable.Size = new System.Drawing.Size(88, 22);
            this.ckb_instrumentenable.TabIndex = 13;
            this.ckb_instrumentenable.Text = "Enable";
            this.ckb_instrumentenable.UseVisualStyleBackColor = true;
            this.ckb_instrumentenable.CheckedChanged += new System.EventHandler(this.Modify);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_portno);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rdb_server);
            this.groupBox1.Controls.Add(this.rdb_client);
            this.groupBox1.Controls.Add(this.tb_ipaddress);
            this.groupBox1.Location = new System.Drawing.Point(22, 315);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(477, 192);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect Parameters";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 146);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "PortNo";
            // 
            // tb_portno
            // 
            this.tb_portno.Location = new System.Drawing.Point(208, 141);
            this.tb_portno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_portno.Name = "tb_portno";
            this.tb_portno.Size = new System.Drawing.Size(247, 28);
            this.tb_portno.TabIndex = 14;
            this.tb_portno.TextChanged += new System.EventHandler(this.Modify);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "IP Address";
            // 
            // rdb_server
            // 
            this.rdb_server.AutoSize = true;
            this.rdb_server.Location = new System.Drawing.Point(45, 44);
            this.rdb_server.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdb_server.Name = "rdb_server";
            this.rdb_server.Size = new System.Drawing.Size(87, 22);
            this.rdb_server.TabIndex = 10;
            this.rdb_server.TabStop = true;
            this.rdb_server.Text = "Server";
            this.rdb_server.UseVisualStyleBackColor = true;
            this.rdb_server.CheckedChanged += new System.EventHandler(this.Modify);
            // 
            // rdb_client
            // 
            this.rdb_client.AutoSize = true;
            this.rdb_client.Location = new System.Drawing.Point(228, 44);
            this.rdb_client.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdb_client.Name = "rdb_client";
            this.rdb_client.Size = new System.Drawing.Size(87, 22);
            this.rdb_client.TabIndex = 11;
            this.rdb_client.TabStop = true;
            this.rdb_client.Text = "Client";
            this.rdb_client.UseVisualStyleBackColor = true;
            this.rdb_client.CheckedChanged += new System.EventHandler(this.Modify);
            // 
            // tb_ipaddress
            // 
            this.tb_ipaddress.Location = new System.Drawing.Point(208, 82);
            this.tb_ipaddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_ipaddress.Name = "tb_ipaddress";
            this.tb_ipaddress.Size = new System.Drawing.Size(247, 28);
            this.tb_ipaddress.TabIndex = 5;
            this.tb_ipaddress.TextChanged += new System.EventHandler(this.Modify);
            // 
            // cbx_driver
            // 
            this.cbx_driver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_driver.FormattingEnabled = true;
            this.cbx_driver.Items.AddRange(new object[] {
            "asts",
            "advc",
            "cent"});
            this.cbx_driver.Location = new System.Drawing.Point(213, 150);
            this.cbx_driver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbx_driver.Name = "cbx_driver";
            this.cbx_driver.Size = new System.Drawing.Size(247, 26);
            this.cbx_driver.TabIndex = 9;
            this.cbx_driver.TextUpdate += new System.EventHandler(this.Modify);
            this.cbx_driver.TextChanged += new System.EventHandler(this.Modify);
            // 
            // cbx_instrumenttype
            // 
            this.cbx_instrumenttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_instrumenttype.FormattingEnabled = true;
            this.cbx_instrumenttype.Items.AddRange(new object[] {
            "Analyzer",
            "LIS_ORDER",
            "LIS_RESULT",
            "LIS"});
            this.cbx_instrumenttype.Location = new System.Drawing.Point(213, 86);
            this.cbx_instrumenttype.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbx_instrumenttype.Name = "cbx_instrumenttype";
            this.cbx_instrumenttype.Size = new System.Drawing.Size(247, 26);
            this.cbx_instrumenttype.TabIndex = 8;
            this.cbx_instrumenttype.TextUpdate += new System.EventHandler(this.Modify);
            this.cbx_instrumenttype.TextChanged += new System.EventHandler(this.Modify);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 154);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Driver";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Instrument Type";
            // 
            // tb_Instrumentna
            // 
            this.tb_Instrumentna.Location = new System.Drawing.Point(213, 27);
            this.tb_Instrumentna.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_Instrumentna.Name = "tb_Instrumentna";
            this.tb_Instrumentna.Size = new System.Drawing.Size(247, 28);
            this.tb_Instrumentna.TabIndex = 1;
            this.tb_Instrumentna.TextChanged += new System.EventHandler(this.Modify);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Instrument Name";
            // 
            // bt_new
            // 
            this.bt_new.Location = new System.Drawing.Point(45, 548);
            this.bt_new.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_new.Name = "bt_new";
            this.bt_new.Size = new System.Drawing.Size(112, 34);
            this.bt_new.TabIndex = 2;
            this.bt_new.Text = "New";
            this.bt_new.UseVisualStyleBackColor = true;
            this.bt_new.Click += new System.EventHandler(this.bt_new_Click);
            // 
            // bt_delete
            // 
            this.bt_delete.Location = new System.Drawing.Point(368, 548);
            this.bt_delete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(112, 34);
            this.bt_delete.TabIndex = 3;
            this.bt_delete.Text = "Delete";
            this.bt_delete.UseVisualStyleBackColor = true;
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(696, 548);
            this.bt_save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(112, 34);
            this.bt_save.TabIndex = 4;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // InstrumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.bt_delete);
            this.Controls.Add(this.bt_new);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DGV_InstrumentList);
            this.Name = "InstrumentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InstrumentList";
            this.Load += new System.EventHandler(this.InstrumentList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_InstrumentList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_InstrumentList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckb_instrumentenable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_portno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdb_server;
        private System.Windows.Forms.RadioButton rdb_client;
        private System.Windows.Forms.TextBox tb_ipaddress;
        private System.Windows.Forms.ComboBox cbx_driver;
        private System.Windows.Forms.ComboBox cbx_instrumenttype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Instrumentna;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_new;
        private System.Windows.Forms.Button bt_delete;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.ComboBox cbx_codingsystem;
        private System.Windows.Forms.Label label6;
    }
}