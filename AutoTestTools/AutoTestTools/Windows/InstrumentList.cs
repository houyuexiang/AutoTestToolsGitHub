using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace AutoTestTools.Windows
{
    public partial class InstrumentList : Form
    {
       
        private DataTable dt_InstrumentList;
        private Boolean B_Modify;
        public InstrumentList()
        {
            InitializeComponent();
        }

        private void InstrumentList_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dS_Auto.CodingSystem”中。您可以根据需要移动或删除它。
            //this.codingSystemTableAdapter.Fill(this.dS_Auto.CodingSystem);
            //CheckConnect();
            LoadInsterList();
            B_Modify = false;
        }

        

        private void LoadInsterList()
        {
            string sql = @"select InstrumentNa,InstrumentType,class,connecttype,Hostip,portno,instrumentenable,codingsystem from instrumentlist";
            dt_InstrumentList = Database.DatabaseMYSQL.GetDataTable(sql, "instrumentlist");
            
            DGV_InstrumentList.DataSource = dt_InstrumentList;
            Thread.Sleep(10);
            DGV_InstrumentList.Columns[0].HeaderText = "InstrumentName";
            DGV_InstrumentList.Columns[0].ReadOnly = true;
            DGV_InstrumentList.Columns[1].HeaderText = "InstrumentType";
            DGV_InstrumentList.Columns[1].ReadOnly = true;
            DGV_InstrumentList.Columns[2].Visible = false;
            DGV_InstrumentList.Columns[3].Visible = false;
            DGV_InstrumentList.Columns[4].Visible = false;
            DGV_InstrumentList.Columns[5].Visible = false;
            DGV_InstrumentList.Columns[6].Visible = false;
            DGV_InstrumentList.Columns[7].Visible = false;
        }

        private void DGV_InstrumentList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (DGV_InstrumentListSelectChange(e.RowIndex) == 0) return;

        }

        private  int DGV_InstrumentListSelectChange(int index)
        {

            string InstrumentNa, InstrumentType;
            DataRow[] InstrumentRecord;
            if (B_Modify)
            {
                DialogResult result = MessageBox.Show("设置已经更改，是否确认保存?", "注意", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (Save() == -1) return -1;
                }
                else if (result == DialogResult.Cancel)
                {
                    return -1;
                }
            }

            InstrumentNa = DGV_InstrumentList.Rows[index].Cells[0].Value.ToString();
            InstrumentType = DGV_InstrumentList.Rows[index].Cells[1].Value.ToString();
            InstrumentRecord = dt_InstrumentList.Select("instrumentna = '" + InstrumentNa + "'");
            if (InstrumentRecord.Length == 0)
            {
                return -1;
            }

            tb_Instrumentna.Text = InstrumentNa;
            cbx_instrumenttype.Text = InstrumentType;
            cbx_driver.Text = InstrumentRecord[0][2].ToString();
            if (InstrumentRecord[0][6].ToString() == "1")
            {
                ckb_instrumentenable.Checked = true;
            }
            else
            {
                ckb_instrumentenable.Checked = false;
            }
            if (InstrumentType.IndexOf("LIS") >= 0)
            {
                cbx_driver.Enabled = false;
            }
            else
            {
                cbx_driver.Enabled = true;
            }
            if (InstrumentRecord[0][3].ToString() == "S")
            {
                rdb_server.Checked = true;
                rdb_client.Checked = false;
                tb_ipaddress.Enabled = false;
            }
            else
            {
                rdb_server.Checked = false;
                rdb_client.Checked = true;
                tb_ipaddress.Enabled = false;
            }
            tb_ipaddress.Text = InstrumentRecord[0][4].ToString();
            tb_portno.Text = InstrumentRecord[0][5].ToString();
            cbx_codingsystem.Text = InstrumentRecord[0][7].ToString();
            B_Modify = false;
            return 0;

            //DialogResult r = MessageBox.Show(InstrumentRecord[0][0].ToString());
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("设置已经更改，是否确认保存?", "注意",  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Save();
            }
            
        }
        private int Save()
        {
            string InstrumentNa, InstrumentType,driver,ipaddr,portno,connecttype,instrumentenable,codingsystem;
            string sql;
            DataRow[] DataTableRecord;
            InstrumentNa = tb_Instrumentna.Text.Trim();
            InstrumentType = cbx_instrumenttype.Text.Trim();
            driver = cbx_driver.Text.Trim();
            ipaddr = tb_ipaddress.Text.Trim();
            portno = tb_portno.Text.Trim();
            codingsystem = cbx_codingsystem.Text.Trim();
            if (rdb_server.Checked)
            {
                connecttype = "S";
            }
            else
            {
                connecttype = "C";
            }
            if (ckb_instrumentenable.Checked)
            {
                instrumentenable = "1";
            }
            else
            {
                instrumentenable = "0";
            }
            if (connecttype == "C" && (ipaddr.Trim() == "" ||  CheckIP(ipaddr) < 0))
            {
                DialogResult result = MessageBox.Show("请检查主机IP设置！", "错误", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return -1;
            }
            if (!(InstrumentType.IndexOf("LIS") >= 0 && driver == "asts" || InstrumentType.IndexOf("LIS") <= 0&&(driver == "cent" || driver == "advc")))
            {

                DialogResult result = MessageBox.Show("仪器类型与Driver设置错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            DataTableRecord = dt_InstrumentList.Select("instrumentna = '" + InstrumentNa + "'");
            if (DataTableRecord.Length > 0)
            {
                DataTableRecord[0][1] = InstrumentType;
                DataTableRecord[0][2] = driver;
                DataTableRecord[0][3] = connecttype;
                DataTableRecord[0][4] = ipaddr;
                DataTableRecord[0][5] = portno;
                DataTableRecord[0][6] = instrumentenable;
                DataTableRecord[0][7] = codingsystem;
                sql = "update instrumentlist set " + @"
                    InstrumentType = '" + InstrumentType + @"' , 
                    class = '" + driver + @"' ,
                    connecttype = '" + connecttype + @"' ,
                    Hostip = '" + ipaddr + @"' ,
                    portno = '" + portno + @"' ,
                    instrumentenable = '" + instrumentenable + @"' ,
                    codingsystem = '" + codingsystem + @"' where 
                    InstrumentNa = '" + InstrumentNa + "'";

            }
            else
            {
                
                DataRow newrow = dt_InstrumentList.Rows.Add(dt_InstrumentList.Rows.Count);
                newrow[0] = InstrumentNa;
                newrow[1] = InstrumentType;
                newrow[2] = driver;
                newrow[3] = connecttype;
                newrow[4] = ipaddr;
                newrow[5] = portno;
                newrow[6] = instrumentenable;
                newrow[7] = codingsystem;
                sql = @"insert into instrumentlist(InstrumentNa,InstrumentType,class,connecttype,Hostip,portno,instrumentenable,codingsystem) values(
                        '" + InstrumentNa + "','" + InstrumentType + "','" + driver + "','" + connecttype + "','" + ipaddr + "','" + portno + "','" + instrumentenable + "','" + codingsystem + "')";
                Database.DatabaseMYSQL.CreateInstrumentTable(InstrumentNa);
            }
            Database.DatabaseMYSQL.ExecuteSQL(sql);


            DialogResult result2 = MessageBox.Show("保存完成", "注意");


            B_Modify = false;
            return 0;
        }

        private int CheckIP(string ipaddr)
        {
            string[] ipsub;
            int ip;
            ipsub = ipaddr.Split('.');
            if (ipsub.Length < 4)
            {
                return -1;
            }
            foreach(string s in ipsub)
            {
                try
                {
                    ip = Convert.ToInt16(s);
                }
                catch
                {
                    return -1;
                }
                if (ip< 0 || ip >255)
                {
                    return -1;
                }
            }
            return 0;
        }

        private void Modify(object sender,EventArgs e)
        {
            B_Modify = true;
            if (rdb_server.Checked)
            {
                tb_ipaddress.Enabled = false;
            }
            else
            {
                tb_ipaddress.Enabled = true;
            }
            if (cbx_instrumenttype.Text.IndexOf("LIS") >= 0)
            {
                cbx_driver.Enabled = false;
            }
            else
            {
                cbx_driver.Enabled = true;
            }
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确定删除此记录?", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            string sql,instrumentna,instrumenttype;
            
            if (result == DialogResult.Yes)
            {
                instrumentna = tb_Instrumentna.Text.Trim();
                instrumenttype = cbx_instrumenttype.Text;
                DataRow[] rows = dt_InstrumentList.Select("instrumentna = '" + instrumentna + "'");
                if (rows.Length > 0)
                {
                    rows[0].Delete();
                    sql = "delete from instrumentlist where instrumentna = '" + instrumentna + "'";
                    Database.DatabaseMYSQL.ExecuteSQL(sql);
                    if (instrumenttype.IndexOf("LIS") <0)
                    {
                        sql = "Drop table " + instrumentna + "_order";
                    }
                     
                }
                if (DGV_InstrumentList.Rows.Count > 0)
                {
                    DGV_InstrumentListSelectChange(0);
                }
                else
                {
                    Clear();
                }

            }
        }

        private void bt_new_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            tb_Instrumentna.Text = "";
            cbx_instrumenttype.Text = "";
            cbx_driver.Text = "Analyzer";
            cbx_driver.Enabled = true;
            tb_ipaddress.Text = "";
            tb_portno.Text = "";
            cbx_codingsystem.Text = "";
            rdb_server.Checked = true;
            tb_ipaddress.Enabled = false;
        }
    }
}
