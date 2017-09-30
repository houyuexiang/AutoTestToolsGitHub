using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoTestTools.Windows
{
    public partial class CommParmSet : Form
    {
        public CommParmSet()
        {
            InitializeComponent();
        }

        private void RB_Server_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisible(RB_Server.Checked);
        }

        private void ChangeVisible(Boolean visable)
        {
            P_Client.Visible = !visable;
            P_Server.Visible = visable;
        }

        private void RB_Client_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisible(!RB_Client.Checked);
        }
    }
}
