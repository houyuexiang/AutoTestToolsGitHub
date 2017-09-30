namespace AutoTestTools.Windows
{
    partial class Sum
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
            this.BT_ImportCSV = new System.Windows.Forms.Button();
            this.BT_NormEdit = new System.Windows.Forms.Button();
            this.BT_DNormEdit = new System.Windows.Forms.Button();
            this.BT_AutoValTestSelect = new System.Windows.Forms.Button();
            this.B_InstrumentSet = new System.Windows.Forms.Button();
            this.BT_CommunityTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BT_ImportCSV
            // 
            this.BT_ImportCSV.Location = new System.Drawing.Point(18, 18);
            this.BT_ImportCSV.Margin = new System.Windows.Forms.Padding(4);
            this.BT_ImportCSV.Name = "BT_ImportCSV";
            this.BT_ImportCSV.Size = new System.Drawing.Size(1071, 78);
            this.BT_ImportCSV.TabIndex = 0;
            this.BT_ImportCSV.Text = "导入CSV（会丢失已有数据）";
            this.BT_ImportCSV.UseVisualStyleBackColor = true;
            this.BT_ImportCSV.Click += new System.EventHandler(this.BT_ImportCSV_Click);
            // 
            // BT_NormEdit
            // 
            this.BT_NormEdit.Enabled = false;
            this.BT_NormEdit.Location = new System.Drawing.Point(18, 202);
            this.BT_NormEdit.Margin = new System.Windows.Forms.Padding(4);
            this.BT_NormEdit.Name = "BT_NormEdit";
            this.BT_NormEdit.Size = new System.Drawing.Size(1071, 88);
            this.BT_NormEdit.TabIndex = 1;
            this.BT_NormEdit.Text = "Norm编辑";
            this.BT_NormEdit.UseVisualStyleBackColor = true;
            // 
            // BT_DNormEdit
            // 
            this.BT_DNormEdit.Enabled = false;
            this.BT_DNormEdit.Location = new System.Drawing.Point(18, 300);
            this.BT_DNormEdit.Margin = new System.Windows.Forms.Padding(4);
            this.BT_DNormEdit.Name = "BT_DNormEdit";
            this.BT_DNormEdit.Size = new System.Drawing.Size(1071, 88);
            this.BT_DNormEdit.TabIndex = 2;
            this.BT_DNormEdit.Text = "DeltaNorm编辑";
            this.BT_DNormEdit.UseVisualStyleBackColor = true;
            // 
            // BT_AutoValTestSelect
            // 
            this.BT_AutoValTestSelect.Enabled = false;
            this.BT_AutoValTestSelect.Location = new System.Drawing.Point(18, 105);
            this.BT_AutoValTestSelect.Margin = new System.Windows.Forms.Padding(4);
            this.BT_AutoValTestSelect.Name = "BT_AutoValTestSelect";
            this.BT_AutoValTestSelect.Size = new System.Drawing.Size(1071, 88);
            this.BT_AutoValTestSelect.TabIndex = 3;
            this.BT_AutoValTestSelect.Text = "自动审核项目选择";
            this.BT_AutoValTestSelect.UseVisualStyleBackColor = true;
            this.BT_AutoValTestSelect.Click += new System.EventHandler(this.BT_AutoValTestSelect_Click);
            // 
            // B_InstrumentSet
            // 
            this.B_InstrumentSet.Location = new System.Drawing.Point(18, 394);
            this.B_InstrumentSet.Margin = new System.Windows.Forms.Padding(4);
            this.B_InstrumentSet.Name = "B_InstrumentSet";
            this.B_InstrumentSet.Size = new System.Drawing.Size(1071, 88);
            this.B_InstrumentSet.TabIndex = 4;
            this.B_InstrumentSet.Text = "仪器设置";
            this.B_InstrumentSet.UseVisualStyleBackColor = true;
            this.B_InstrumentSet.Click += new System.EventHandler(this.B_InstrumentSet_Click);
            // 
            // BT_CommunityTest
            // 
            this.BT_CommunityTest.Location = new System.Drawing.Point(18, 492);
            this.BT_CommunityTest.Margin = new System.Windows.Forms.Padding(4);
            this.BT_CommunityTest.Name = "BT_CommunityTest";
            this.BT_CommunityTest.Size = new System.Drawing.Size(1071, 88);
            this.BT_CommunityTest.TabIndex = 5;
            this.BT_CommunityTest.Text = "通讯测试";
            this.BT_CommunityTest.UseVisualStyleBackColor = true;
            this.BT_CommunityTest.Click += new System.EventHandler(this.BT_CommunityTest_Click);
            // 
            // Sum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 591);
            this.Controls.Add(this.BT_CommunityTest);
            this.Controls.Add(this.B_InstrumentSet);
            this.Controls.Add(this.BT_AutoValTestSelect);
            this.Controls.Add(this.BT_DNormEdit);
            this.Controls.Add(this.BT_NormEdit);
            this.Controls.Add(this.BT_ImportCSV);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Sum";
            this.Text = "Sum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sum_FormClosing);
            this.Load += new System.EventHandler(this.Sum_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BT_ImportCSV;
        private System.Windows.Forms.Button BT_NormEdit;
        private System.Windows.Forms.Button BT_DNormEdit;
        private System.Windows.Forms.Button BT_AutoValTestSelect;
        private System.Windows.Forms.Button B_InstrumentSet;
        private System.Windows.Forms.Button BT_CommunityTest;
    }
}