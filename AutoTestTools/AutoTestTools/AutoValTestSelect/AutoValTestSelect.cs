using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoTestTools.AutoValTestSelect
{
    public partial class AutoValTestSelect : Form
    {
        public AutoValTestSelect()
        {
            InitializeComponent();
        }

        private void AutoValTestSelect_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dS_Auto.Test”中。您可以根据需要移动或删除它。
            this.testTableAdapter.Fill(this.dS_Auto.Test);
            // TODO: 这行代码将数据加载到表“dS_Auto.Test”中。您可以根据需要移动或删除它。
            this.testTableAdapter.Fill(this.dS_Auto.Test);

        }

        private void DGV_TestList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewCell cell = DGV_TestList.Rows[e.RowIndex].Cells[0];
                if (cell.Value == null)
                {
                    
                    cell.Value = 1;
                }
                else
                {
                    if (cell.Value.ToString() != "1")
                    {
                        cell.Value = 1;
                    }
                    else
                    {
                        cell.Value = 0;
                    }
                }
                DGV_TestList.CommitEdit(DataGridViewDataErrorContexts.InitialValueRestoration);
                DGV_TestList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                
            }
        }

        private void SetFlag()
        {

        }

        private void DGV_TestList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DGV_TestList.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
