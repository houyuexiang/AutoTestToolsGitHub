namespace AutoTestTools.Windows
{
    partial class CommTestControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_startsampleid = new System.Windows.Forms.TextBox();
            this.tb_endsampleid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DGV_instrumentLIST = new System.Windows.Forms.DataGridView();
            this.bt_save = new System.Windows.Forms.Button();
            this.DGV_tests = new System.Windows.Forms.DataGridView();
            this.tb_InstrumentRequestInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_instrumentLIST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_tests)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 405);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "起始标本号";
            // 
            // tb_startsampleid
            // 
            this.tb_startsampleid.Location = new System.Drawing.Point(176, 398);
            this.tb_startsampleid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_startsampleid.Name = "tb_startsampleid";
            this.tb_startsampleid.Size = new System.Drawing.Size(148, 28);
            this.tb_startsampleid.TabIndex = 1;
            // 
            // tb_endsampleid
            // 
            this.tb_endsampleid.Location = new System.Drawing.Point(548, 398);
            this.tb_endsampleid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_endsampleid.Name = "tb_endsampleid";
            this.tb_endsampleid.Size = new System.Drawing.Size(148, 28);
            this.tb_endsampleid.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 405);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束标本号";
            // 
            // DGV_instrumentLIST
            // 
            this.DGV_instrumentLIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_instrumentLIST.Location = new System.Drawing.Point(18, 18);
            this.DGV_instrumentLIST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV_instrumentLIST.Name = "DGV_instrumentLIST";
            this.DGV_instrumentLIST.RowTemplate.Height = 23;
            this.DGV_instrumentLIST.Size = new System.Drawing.Size(1782, 357);
            this.DGV_instrumentLIST.TabIndex = 8;
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(1326, 794);
            this.bt_save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(476, 84);
            this.bt_save.TabIndex = 9;
            this.bt_save.Text = "保存";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // DGV_tests
            // 
            this.DGV_tests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_tests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select});
            this.DGV_tests.Location = new System.Drawing.Point(20, 454);
            this.DGV_tests.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV_tests.Name = "DGV_tests";
            this.DGV_tests.RowTemplate.Height = 23;
            this.DGV_tests.Size = new System.Drawing.Size(1780, 312);
            this.DGV_tests.TabIndex = 10;
            // 
            // tb_InstrumentRequestInterval
            // 
            this.tb_InstrumentRequestInterval.Location = new System.Drawing.Point(960, 398);
            this.tb_InstrumentRequestInterval.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_InstrumentRequestInterval.Name = "tb_InstrumentRequestInterval";
            this.tb_InstrumentRequestInterval.Size = new System.Drawing.Size(148, 28);
            this.tb_InstrumentRequestInterval.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(774, 405);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(395, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = " 仪器Request间隔                        (S)";
            // 
            // Select
            // 
            this.Select.FalseValue = "0";
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.TrueValue = "1";
            // 
            // CommTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1820, 896);
            this.Controls.Add(this.tb_InstrumentRequestInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DGV_tests);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.DGV_instrumentLIST);
            this.Controls.Add(this.tb_endsampleid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_startsampleid);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CommTestControl";
            this.Text = "CommTestControl";
            this.Load += new System.EventHandler(this.CommTestControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_instrumentLIST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_tests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_startsampleid;
        private System.Windows.Forms.TextBox tb_endsampleid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DGV_instrumentLIST;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.DataGridView DGV_tests;
        private System.Windows.Forms.TextBox tb_InstrumentRequestInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
    }
}