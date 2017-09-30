namespace AutoTestTools.AutoValTestSelect
{
    partial class AutoValTestSelect
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
            this.DGV_TestList = new System.Windows.Forms.DataGridView();
            this.AutoVal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dS_Auto = new AutoTestTools.DS_Auto();
            this.testTableAdapter = new AutoTestTools.DS_AutoTableAdapters.TestTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.testGroupMemberTableAdapter = new AutoTestTools.DS_AutoTableAdapters.TestGroupMemberTableAdapter();
            this.tableAdapterManager = new AutoTestTools.DS_AutoTableAdapters.TableAdapterManager();
            this.TestGroupMember = new System.Windows.Forms.BindingSource(this.components);
            this.testBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_TestList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS_Auto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestGroupMember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV_TestList
            // 
            this.DGV_TestList.AllowUserToAddRows = false;
            this.DGV_TestList.AllowUserToDeleteRows = false;
            this.DGV_TestList.AllowUserToOrderColumns = true;
            this.DGV_TestList.AutoGenerateColumns = false;
            this.DGV_TestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_TestList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AutoVal,
            this.nameDataGridViewTextBoxColumn,
            this.descriptionEDataGridViewTextBoxColumn});
            this.DGV_TestList.DataSource = this.testBindingSource;
            this.DGV_TestList.Location = new System.Drawing.Point(20, 20);
            this.DGV_TestList.Margin = new System.Windows.Forms.Padding(4);
            this.DGV_TestList.Name = "DGV_TestList";
            this.DGV_TestList.RowTemplate.Height = 23;
            this.DGV_TestList.Size = new System.Drawing.Size(959, 648);
            this.DGV_TestList.TabIndex = 0;
            this.DGV_TestList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_TestList_CellClick);
            this.DGV_TestList.CurrentCellDirtyStateChanged += new System.EventHandler(this.DGV_TestList_CurrentCellDirtyStateChanged);
            // 
            // AutoVal
            // 
            this.AutoVal.FalseValue = "0";
            this.AutoVal.HeaderText = "AutoVal";
            this.AutoVal.IndeterminateValue = "0";
            this.AutoVal.Name = "AutoVal";
            this.AutoVal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AutoVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AutoVal.TrueValue = "1";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // descriptionEDataGridViewTextBoxColumn
            // 
            this.descriptionEDataGridViewTextBoxColumn.DataPropertyName = "Description_E";
            this.descriptionEDataGridViewTextBoxColumn.HeaderText = "Description_E";
            this.descriptionEDataGridViewTextBoxColumn.Name = "descriptionEDataGridViewTextBoxColumn";
            this.descriptionEDataGridViewTextBoxColumn.Width = 1000;
            // 
            // testBindingSource
            // 
            this.testBindingSource.DataMember = "Test";
            this.testBindingSource.DataSource = this.dS_Auto;
            // 
            // dS_Auto
            // 
            this.dS_Auto.DataSetName = "DS_Auto";
            this.dS_Auto.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // testTableAdapter
            // 
            this.testTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(996, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 106);
            this.button1.TabIndex = 1;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(996, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(213, 106);
            this.button2.TabIndex = 2;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // testGroupMemberTableAdapter
            // 
            this.testGroupMemberTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CodingSystemTableAdapter = null;
            this.tableAdapterManager.csv_tableheadTableAdapter = null;
            this.tableAdapterManager.DeltaNormTableAdapter = null;
            this.tableAdapterManager.gp_TranslatorTableAdapter = null;
            this.tableAdapterManager.InstrumentTableAdapter = null;
            this.tableAdapterManager.NormTableAdapter = null;
            this.tableAdapterManager.SampleTypeCodeTableAdapter = null;
            this.tableAdapterManager.TestCodeTableAdapter = null;
            this.tableAdapterManager.TestGroupMemberTableAdapter = this.testGroupMemberTableAdapter;
            this.tableAdapterManager.TestGroupTableAdapter = null;
            this.tableAdapterManager.TestTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = AutoTestTools.DS_AutoTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // TestGroupMember
            // 
            this.TestGroupMember.DataSource = this.dS_Auto;
            this.TestGroupMember.Position = 0;
            // 
            // testBindingSource1
            // 
            this.testBindingSource1.DataMember = "Test";
            this.testBindingSource1.DataSource = this.TestGroupMember;
            // 
            // AutoValTestSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 688);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DGV_TestList);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AutoValTestSelect";
            this.Text = "AutoValTestSelect";
            this.Load += new System.EventHandler(this.AutoValTestSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_TestList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS_Auto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestGroupMember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_TestList;
        private DS_Auto dS_Auto;
        private System.Windows.Forms.BindingSource testBindingSource;
        private DS_AutoTableAdapters.TestTableAdapter testTableAdapter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DS_AutoTableAdapters.TestGroupMemberTableAdapter testGroupMemberTableAdapter;
        private DS_AutoTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingSource TestGroupMember;
        private System.Windows.Forms.BindingSource testBindingSource1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AutoVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionEDataGridViewTextBoxColumn;
    }
}