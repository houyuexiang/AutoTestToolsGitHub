using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;


namespace AutoTestTools.Windows
{
    public partial class CommTestControl : Form
    {
        private MySqlConnection SQLconnect;
        
        public CommTestControl()
        {
            InitializeComponent();
            SQLconnect =  Database.DatabaseMYSQL.ConnectDb();
        }
        private void LoadInstrumentList()
        {
            string sql;
            DataTable dt_instrumentlist;
            sql = "select instrumentna,instrumenttype,connecttype,hostip,portno,'        ' as connectstatue from instrumentlist";
            dt_instrumentlist = Database.DatabaseMYSQL.GetDataTable(sql, "instrumentlist");
            DGV_instrumentLIST.DataSource = dt_instrumentlist;
            DGV_instrumentLIST.Columns[0].HeaderText = "Instrument Name";
            DGV_instrumentLIST.Columns[1].HeaderText = "Instrument Type";
            DGV_instrumentLIST.Columns[2].HeaderText = "Connect Type";
            DGV_instrumentLIST.Columns[3].HeaderText = "Host IP";
            DGV_instrumentLIST.Columns[4].HeaderText = "Port No.";
            DGV_instrumentLIST.Columns[5].HeaderText = "Comment";
        }
        private void LoadTestList()
        {
            string sql;
            DataTable dt_test;
            sql = "select name,Description_E,SampleType_Name,OmitOnLASUpdate,PatientDataType,ControlDataType from test ";
            dt_test = Database.DatabaseMYSQL.GetDataTable(sql, "test");
            DGV_tests.DataSource = dt_test;
        }
        

        private void CommTestControl_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dS_Auto.Test”中。您可以根据需要移动或删除它。
            tb_startsampleid.Text = ClassLib.GlobalValue.StartSampleno;
            tb_endsampleid.Text = ClassLib.GlobalValue.EndSampleno;
            tb_InstrumentRequestInterval.Text = ClassLib.GlobalValue.InstrumentRequestInterval.ToString();
            LoadInstrumentList();
            LoadTestList();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            
            int InstrumentRequestInterval;
            List<string> list = new List<string>();
            try
            {
                InstrumentRequestInterval = Convert.ToInt32(tb_InstrumentRequestInterval.Text);
            }
            catch
            {
                DialogResult r = MessageBox.Show("仪器询问间隔必须为整数，单位为妙", "错误");
                return;
            }
            DialogResult result = MessageBox.Show("是否清除之前标本？", "注意", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string sql = "delete from samplemain ";
                Database.DatabaseMYSQL.ExecuteSQL(sql);
                sql = "delete from sampledetail ";
                Database.DatabaseMYSQL.ExecuteSQL(sql);
            }
            ClassLib.GlobalValue.CC.StopThreadSample();
            //ClassLib.GlobalValue.testlist.Clear();
            ClassLib.GlobalValue.StartSampleno = tb_startsampleid.Text;
            ClassLib.GlobalValue.EndSampleno = tb_endsampleid.Text;
            ClassLib.GlobalValue.InstrumentRequestInterval = InstrumentRequestInterval;
            foreach (DataGridViewRow r in DGV_tests.Rows)
            {
                if (r.Cells["Select"].Value == null) continue;
                if (r.Cells["Select"].Value.ToString() == "1")
                {
                    list.Add(r.Cells[1].Value.ToString());
                    //ClassLib.GlobalValue.testlist.Add(r.Cells[1].Value.ToString());
                }
            }
            if (list.Count > 0) ClassLib.GlobalValue.testlist = list;
            SaveSetting();
            ClassLib.GlobalValue.CurrentSampleno = ClassLib.GlobalValue.StartSampleno;
            if (ClassLib.GlobalValue.testlist.Count > 0)
            {
                ClassLib.GlobalValue.CC.StartThreadSample();
            }

            
            
        }
        private void SaveSetting()
        {
            ClassLib.GlobalValue.SaveSetting("StartSampleno", tb_startsampleid.Text);
            ClassLib.GlobalValue.SaveSetting("EndSampleno", tb_endsampleid.Text);
            ClassLib.GlobalValue.SaveSetting("InstrumentRequestInterval", tb_InstrumentRequestInterval.Text);
            string tmp = "";
            foreach(string s in ClassLib.GlobalValue.testlist)
            {
                tmp += s + "|";
            }
            if (tmp.Length > 0)
            {
                tmp = tmp.Substring(0, tmp.Length - 1);
            }
            ClassLib.GlobalValue.SaveSetting("testlist", tmp);

            
        }
    }
}
