using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Threading;

namespace DMSAutoOrder
{
    public partial class Form1 : Form
    {
        public Boolean BAutoStart, BSameIP, BStart;
        public string STestCode,SampleIDLen;
        public string inifile = System.Environment.CurrentDirectory + "\\Setting.ini";
        public DMSConnector dmsconnect;
        public Form1()
        {
            InitializeComponent();
        }


        public void LoadSetting()
        {
            iniManager.fileName = inifile;
            FileInfo fi = new FileInfo(inifile);
            if (!fi.Exists)
            {
                return;
            }

            MysqlClass.DBname = iniManager.GetString("DB", "DBName", "DMS");
            MysqlClass.DBIP = iniManager.GetString("DB", "DBIP", "127.0.0.1");
            MysqlClass.DBUserName = iniManager.GetString("DB", "DBUser", "root");
            MysqlClass.DBPassword = iniManager.GetString("DB", "DBPasswd", "lINEA_3");
            dmsconnect.IPaddr = iniManager.GetString("DMS", "DMSIP", "127.0.0.1");
            dmsconnect.PortNo = iniManager.GetString("DMS", "OrderPort", "3001");
            dmsconnect.ConnectMode = (iniManager.GetString("DMS", "ConnectMode", "1") == "2");
            STestCode = iniManager.GetString("DMS", "TestCode", "PARK");
            BAutoStart = (iniManager.GetString("SYS", "AutoStart", "0") == "1");
            BSameIP = (iniManager.GetString("SYS", "SameIP", "0") == "1");
            SampleIDLen = iniManager.GetString("DMS", "SampleIDLen", "0");
        }

        private void RB_Client_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Client.Checked)
            {
                RB_Server.Checked = false;
            }
            else
            {
                RB_Server.Checked = true;
            }
        }

        private void RB_Server_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Server.Checked)
            {
                RB_Client.Checked = false;
            }
            else
            {
                RB_Client.Checked = true;
            }
        }

        private void ModifyStatus(Boolean canmodify)
        {
            TB_DBname.ReadOnly = canmodify;
            TB_DBPW.ReadOnly = canmodify;
            TB_DBUser.ReadOnly = canmodify;
            TB_IP.ReadOnly = canmodify;
            TB_Port.ReadOnly = canmodify;
            TB_SendTestCode.ReadOnly = canmodify;
            RB_Client.Enabled = !canmodify;
            RB_Server.Enabled = !canmodify;
            CB_AutoStart.Enabled = !canmodify;
            CB_SameIP.Enabled = !canmodify;
            if (CB_SameIP.Checked)
            {
                TB_DBIP.ReadOnly = false;
            }
            else
            {
                TB_DBIP.ReadOnly = canmodify;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dmsconnect = new DMSConnector();
            LoadSetting();
            TB_IP.Text = dmsconnect.IPaddr;
            TB_Port.Text = dmsconnect.PortNo;
            TB_DBIP.Text = MysqlClass.DBIP;
            TB_DBname.Text = MysqlClass.DBname;
            TB_DBUser.Text = MysqlClass.DBUserName;
            TB_DBPW.Text = MysqlClass.DBPassword;
            CB_AutoStart.Checked = BAutoStart;
            CB_SameIP.Checked = BSameIP;
            TB_SendTestCode.Text = STestCode;
            RB_Client.Checked = !dmsconnect.ConnectMode;
            RB_Server.Checked = dmsconnect.ConnectMode;
            BStart = false;
            if (BAutoStart)
            {
                Start();
                this.WindowState = FormWindowState.Minimized;

            }
        }

        private void CB_SameIP_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_SameIP.Checked)
            {
                TB_DBIP.Text = TB_IP.Text;
                TB_DBIP.ReadOnly = true;
            }
            else
            {
                TB_DBIP.ReadOnly = false;
            }
        }

        private void B_Start_Click(object sender, EventArgs e)
        {
            if (B_Start.Text == "Start")
            {
                SaveSetting();
                Start();
            }
            else
            {
                Stop();
            }
            
        }

        private void Start()
        {
            BStart = true;
            B_Start.Text = "Stop";
            ModifyStatus(true);
            dmsconnect.MakeASTMConnect();
            Thread t = new Thread(process);
            t.IsBackground = true;
            t.Start();

        }

        private void process()
        {
            while (BStart)
            {
                try
                {
                    string sid, sampletype, oid;
                    string sql = "select codsid,codoid from " + MysqlClass.DBname + ".reqtube where codoid in (select " + MysqlClass.DBname + ".orders.codoid from " + MysqlClass.DBname + ".orders," + MysqlClass.DBname + ".anagpatient where " + MysqlClass.DBname + ".orders.codaid = " + MysqlClass.DBname + ".anagpatient.codaid and " + MysqlClass.DBname + ".anagpatient.codpid = 'UNKNOWN') ";
                    sql += " and codsid in (select " + MysqlClass.DBname + ".reqtest.codsid from " + MysqlClass.DBname + ".reqtest where " + MysqlClass.DBname + ".reqtest.codtest <> 'UNKN')";
                    sql += " or codoid not in ( select " + MysqlClass.DBname + ".orders.codoid from " + MysqlClass.DBname + ".orders )";
                    DataTable dt = MysqlClass.GetDataTable(sql, "Orders");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            try
                            {
                                while (dmsconnect.SendBuffer != "")
                                {
                                    Thread.Sleep(10);
                                }

                                oid = r[1].ToString();
                                sid = r[0].ToString();

                                if (oid == null)
                                {
                                    oid = "";
                                }
                                string ASTMmessage;
                                string timestr = DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                ValTest(sid);
                                sampletype = GetSampleType(sid);
                                if (sampletype == null)
                                {
                                    continue;
                                }
                                CleanOrdersTable(oid, sid);
                                ASTMmessage = GlobalValue.FS + "[H|\\^&||||||||||P|1|" + timestr + GlobalValue.CR;
                                ASTMmessage += "P|1|" + oid + "|" + oid + "||Shougong|||F" + GlobalValue.CR;
                                ASTMmessage += "C|1|L|note:<nlbl:demographic_changed>|G" + GlobalValue.CR;
                                ASTMmessage += "O|1|" + sid + "||^^^" + STestCode + "|R||" + timestr + "||||N||||" + sampletype + "||U||||||||O" + GlobalValue.CR;
                                ASTMmessage += "L|1|N" + GlobalValue.CR + "]" + GlobalValue.GS;
                                dmsconnect.SendBuffer = ASTMmessage;
                                ASTMmessage = "";
                                notifyIcon1.BalloonTipTitle = "Notification";
                                notifyIcon1.BalloonTipText = "Processing Manual Sample :" + sid + " !";
                                notifyIcon1.ShowBalloonTip(100);
                            }
                            catch (Exception e)
                            {
                                GlobalValue.WriteLog("Form1.process" + e.Message + "\r\n", "SystemError.log");
                            }
                            Thread.Sleep(50);
                        }
                    }
                    Thread.Sleep(6000);
                    ChangeWrongStatusSampleToHostFlg();
                }
                catch(Exception e)
                {
                    GlobalValue.WriteLog("Form1.process" + e.Message + "\r\n", "SystemError.log");
                }
            }
            
        }

        private string GetSampleType(string SID)
        {
            string sql,sql2,sampletype,testcode;
            Boolean Bhasinstrumenttest;
            DataTable T_reqtube, T_reqtest, T_testlab,T_testinstrument,T_sampletype;
            sql = "Select codtypesample from " + MysqlClass.DBname + ".reqtube where codsid = '" + SID + "'";
            T_reqtube = MysqlClass.GetDataTable(sql, "tube");
            sampletype = T_reqtube.Rows[0][0].ToString();
            Bhasinstrumenttest = false;
            if (sampletype !="" && sampletype != null)
            {
                return sampletype;
            }
            sql = "select codtest from " + MysqlClass.DBname + ".reqtestresult where codsid = '" + SID + "'";
            T_reqtest = MysqlClass.GetDataTable(sql, "Test");
            foreach(DataRow r in T_reqtest.Rows)
            {
                testcode = r[0].ToString();
                sql = "select codtypesample from " + MysqlClass.DBname + ".testlab where codtest = '" + testcode + "' and codresult <> 'SORTMANUAL'";
                T_testlab = MysqlClass.GetDataTable(sql, "testlab");
                sql2 = "select * from " + MysqlClass.DBname + ".testinstrument where codtest = '" + testcode + "' and codiid <> 'HOSTASTMmCH' and codiid <> 'INPECO'";
                T_testinstrument = MysqlClass.GetDataTable(sql, "testinstrument");
                if (T_testinstrument.Rows.Count > 0)
                {
                    Bhasinstrumenttest = true;
                }
                else
                {
                    continue;
                }
                if (T_testlab.Rows.Count > 0 )
                {
                    sampletype = T_testlab.Rows[0][0].ToString().Trim();
                    if (sampletype != "" && sampletype != null)
                    {
                        return sampletype;
                    }
                }
               
            }
            if (Bhasinstrumenttest)
            {
                sql = "select codtypesample from " + MysqlClass.DBname + ".typesample where flgdefault = '1'";
                T_sampletype = MysqlClass.GetDataTable(sql, "sampletype");
                if (T_sampletype.Rows.Count > 0)
                {
                    sampletype = T_sampletype.Rows[0][0].ToString();
                    if (sampletype != "" && sampletype != null)
                    {
                        return sampletype;
                    }
                }
            }
            else
            {
                return null;
            }
            sql = "";
            return "";
        }

        private void CleanOrdersTable(string OID,string SID)
        {
            string sql;
            sql = "delete from " + MysqlClass.DBname + ".orders where codoid = '" + OID + "'";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
            sql = "delete from " + MysqlClass.DBname + ".orders_details where codoid = '" + OID + "'";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
            sql = "delete from " + MysqlClass.DBname + ".orders_tat where codoid = '" + OID + "'";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
            sql = "delete from " + MysqlClass.DBname + ".reqtest where codsid = '" + SID + "' and codtest = '"+ STestCode+"'";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
            sql = "delete from " + MysqlClass.DBname + ".reqtestresult where codsid = '" + SID + "' and codtest = '"+STestCode+"'";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
        }

        private void ValTest(string SID)
        {
            string sql;
            sql = "Update " + MysqlClass.DBname + ".reqtestresult set flgstatus = 'V' where codsid = '" + SID + "' and flgstatus = 'H'";
            MysqlClass.ExecuteSQL(sql);
            sql = "Update " + MysqlClass.DBname + ".reqtest set flgstatus = 'V' where codsid = '" + SID + "' and flgstatus = 'H'";
            MysqlClass.ExecuteSQL(sql);
        }

        private void ChangeWrongStatusSampleToHostFlg()
        {
            string sql;
            sql = "update " + MysqlClass.DBname + ".reqtest set flgtohost = 0 where(flgstatus = 'V' or flgstatus = 'X' or flgstatus = 'Y' or flgstatus = 'Z') and flgtohost <> 0";
            MysqlClass.ExecuteSQL(sql);
        }

        private void TB_DBUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();   //隐藏窗体
                this.Visible = false;
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true; //使托盘图标可见
                toolStripMenuItem1.Text = "Show";
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            toolStripMenuItem1.Text = "Hide";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.Hide();   //隐藏窗体
                this.Visible = false;
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true; //使托盘图标可见
                this.WindowState = FormWindowState.Minimized;
                toolStripMenuItem1.Text = "Show";
            }
            else
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                toolStripMenuItem1.Text = "Hide";
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Stop();
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            SaveSetting();
        }

        private void Stop()
        {
            BStart = false;
            B_Start.Text = "Start";
            ModifyStatus(false);
            dmsconnect.StopConnector();

        }
        private void SaveSetting()
        {
            iniManager.WriteString("DB", "DBName", TB_DBname.Text);
            iniManager.WriteString("DB", "DBIP", TB_DBIP.Text);
            iniManager.WriteString("DB", "DBUser", TB_DBUser.Text);
            iniManager.WriteString("DB", "DBPasswd", TB_DBPW.Text);
            iniManager.WriteString("DMS", "DMSIP", TB_IP.Text);
            iniManager.WriteString("DMS", "OrderPort", TB_Port.Text);
            iniManager.WriteString("DMS", "TestCode", TB_SendTestCode.Text);

            if (CB_AutoStart.Checked)
            {
                iniManager.WriteString("SYS", "AutoStart", "1");
            }
            else
            {
                iniManager.WriteString("SYS", "AutoStart", "0");
            }

            if (CB_SameIP.Checked)
            {
                iniManager.WriteString("SYS", "SameIP", "1");
            }
            else
            {
                iniManager.WriteString("SYS", "SameIP", "0");
            }

            if (RB_Server.Checked)
            {
                iniManager.WriteString("DMS", "ConnectMode", "2");
            }
            else
            {
                iniManager.WriteString("DMS", "ConnectMode", "1");
            }
            LoadSetting();
        }
    }
}
