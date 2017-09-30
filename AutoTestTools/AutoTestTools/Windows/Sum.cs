using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Threading;

namespace AutoTestTools.Windows
{
    public partial class Sum : Form
    {
        ClassLib.CommunityControl cc;
        public Sum()
        {
            InitializeComponent();
        }






        private void BT_ImportCSV_Click(object sender, EventArgs e)
        {
            string dbpath;
            DialogResult result = MessageBox.Show("是否确定重新导入数据，将会丢失所有之前数据，在此操作前请先备份数据库", "注意！", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {


                //Database.DatabaseMYSQL.CloseDatabase();
                Database.DatabaseMYSQL.DropTable();

                //FileInfo fi = new FileInfo("Auto.db");
                ////if (fi.Exists)
                ////{
                ////    try
                ////    {
                ////        File.Delete("Auto.db");
                ////    }
                ////    catch(Exception ex)
                ////    {
                ////        MessageBox.Show(ex.Message);
                ////        return;
                ////    }

                ////}
                //dbpath = fi.FullName;
                Database.DatabaseMYSQL.InitDatabase();
                Database.DatabaseMYSQL.LoadDataTable();
                //Tools.RegeditSet.SetRegeditKey(dbpath);
                result = MessageBox.Show("导入完成", "注意！");
            }
            
        }

        private void BT_AutoValTestSelect_Click(object sender, EventArgs e)
        {
            Form fr = new AutoValTestSelect.AutoValTestSelect();
            fr.Show();

        }

        private void B_InstrumentSet_Click(object sender, EventArgs e)
        {
            Form fr = new Windows.InstrumentList();
            fr.Show();
        }

        private void BT_CommunityTest_Click(object sender, EventArgs e)
        {
            Form fr = new Windows.CommTestControl();
            fr.Show();
        }

        private void Sum_Load(object sender, EventArgs e)
        {
            string connectstring = Database.DatabaseMYSQL.GetConnectString();
            if (connectstring!="")
            {
                Database.DatabaseMYSQL.connectstring = connectstring;
            }
            Database.DatabaseMYSQL.ConnectDb();
            ClassLib.GlobalValue.Init();
            
            StartInstrumentControl();
        }

        private void StartInstrumentControl()
        {
            
            cc = new ClassLib.CommunityControl();
            cc.StartThread();
            cc.StartThreadSample();
            ClassLib.GlobalValue.CC = cc;

        }

        private void Sum_FormClosing(object sender, FormClosingEventArgs e)
        {
            cc.StopThread();
            cc.StopThreadSample();
            
        }
    }
}
