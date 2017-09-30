using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.SQLite;


namespace AutoTestTools.Database
{
    static class DatabaseCreate
    {
        private static string[] filename = { "Test.csv", "TestGroup.csv", "TestGroupMember.csv", "DeltaNorm.csv", "Norm.csv", "gp_Translator.csv", "Instrument.csv", "CodingSystem.csv", "SampleTypeCode.csv", "TestCode.csv", "InstrumentTranslator.csv" };
        public static SQLiteConnection SQLConnect = null;
        public static void LoadDataTable()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择CSV所在文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ConnectDatabase();
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show( "文件夹路径不能为空", "提示");
                    return;
                }

                foreach(string item in filename)
                {
                    string filepath = dialog.SelectedPath + "\\" +  item;
                    FileInfo fi = new FileInfo(filepath);
                    if (!fi.Exists) continue;
                    DataTable dt =  Tools.CSVProcess.OpenCSV(filepath);
                    string sql = "";
                    string tablehead = "";
                    dt.TableName = item.Split('.')[0];

                    sql = "Drop table if exists " + dt.TableName ;
                    ExecuteSql(SQLConnect, sql);
                    sql = "";

                    sql += "Create Table if not exists  " + dt.TableName + " ( ";
                    foreach (DataColumn dc in dt.Columns)   //循环列 
                    {
                        string temp = Replacechar(dc.ColumnName);
                        string headsql = "insert into csv_tablehead (tablename,tablehead_before,tablehead_current) values ('" + dt.TableName + "','" + dc.ColumnName + "','" + temp + "')";
                        sql = sql + " " + temp + "   string" + ",";
                        ExecuteSql(SQLConnect, headsql);
                        tablehead += temp + ",";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")";
                    ExecuteSql(SQLConnect, sql);

                    tablehead = tablehead.Substring(0, tablehead.Length - 1);
                    InsertRecord(dt, dt.TableName, tablehead);



                    //MessageBox.Show(sql, "提示");
                }
                CreateTables();
                CheckInstrument();
                CheckLIS();
            } 
        }

        public static SQLiteConnection ConnectDatabase()
        {
            if (SQLConnect == null)
            {
                SQLConnect = new SQLiteConnection("Data Source = Auto.db");
                SQLConnect.Open();
                InitDatabase();
                return SQLConnect;
            }
            else
            {
                return SQLConnect;
            }
            
            //InitDataBase();
        }

        public static void CloseDatabase()
        {
            if (SQLConnect != null)
            {
                SQLConnect.Close();
            }
        }

        private static void InsertRecord(DataTable dt,string tbname,string tablehead)
        {
            string head = "insert into " + tbname + " ( " + tablehead + ") values (";
            string sql = "",tmp = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string values = string.Empty;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    tmp = ChangeValue(dt.Rows[i][j].ToString());
                    values += "'" + tmp + "',";
                }
                sql = head + values.Remove(values.Length - 1) + ")";
                //MessageBox.Show(sql);
                ExecuteSql(SQLConnect, sql);
            }
        }

        private static string Replacechar(string text)
        {
            string t = text;
            t = t.Replace('.', '_');
            t = t.Replace(":", "_");
            t = t.Replace("~", "_");
            t = t.Trim();
            if (t.ToLower() == "limit")
            {
                t += "s";
            }
            return t;
        }
        private static string ChangeValue(string text)
        {
            return text.Replace("'", "*");
        }

        public static void InitDatabase()
        {
            string sql = "Create table if not exists csv_tablehead( tablename string ,tablehead_before string ,tablehead_current string)";
            ExecuteSql(SQLConnect, sql);
        }

        public static void ExecuteSql(SQLiteConnection sqlconnect,string sql)
        {
            SQLiteCommand sqlcomm = new SQLiteCommand(sqlconnect);
            sqlcomm.CommandText = sql;
            try
            {
                sqlcomm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString() + "\r\n" + sql, "提示");
                
            }

        }

        
        private static void CreateTables()
        {
            string sql;
            sql = @"CREATE TABLE `samplemain` (
	                `seq`	INTEGER,
	                `sampleid`	TEXT,
	                `sampleda`	TEXT,
	                `sampletype`	TEXT,
	                `patientno`	TEXT,
	                `patientna`	TEXT,
	                `sex`	TEXT,
	                `birthday`	TEXT,
	                `createtime`	TEXT,
                    `orderda`   TEXT,
                    `ordered`   INTEGER DEFAULT 0,
                    `dispatched`    INTEGER DEFAULT 0,
	                 PRIMARY KEY(`seq`,`sampleid`,`sampleda`)
                      )";
            ExecuteSql(SQLConnect, sql);

            sql = @"CREATE TABLE `sampledetail` (
	                `seq`	INTEGER,
	                `sampleid`	TEXT,
	                `sampleda`	TEXT,
	                `tests`	TEXT,
	                `result`	TEXT,
	                PRIMARY KEY(`seq`,`sampleid`,`sampleda`,`tests`)
                    )";
            ExecuteSql(SQLConnect, sql);

            sql = @"CREATE TABLE `instrumentlist` (
	                `instrumentna`	TEXT,
                    `codingsystem`  TEXT,
	                `class`	TEXT,
	                `instrumenttype`	TEXT,
	                `connecttype`	TEXT,
	                `hostip`	TEXT,
	                `portno`	TEXT,
	                `instrumentenable`	TEXT,
	                PRIMARY KEY(`instrumentna`)
                    )";
            ExecuteSql(SQLConnect, sql);

            sql = @"CREATE TABLE `Setting` (
                    `SettingName` TEXT,
                    `SettingValue`   TEXT,
                    PRIMARY KEY(`SettingName`)
                    )";
            ExecuteSql(SQLConnect, sql);

        }


        private static void CheckInstrument()
        {
            string sql,hostip,portno,instrclass,option,connecttype;
            string instrumentna,codingsystem,translator;
            Boolean b_IsClV16;
            DataTable dt_instrument = new DataTable("Instrument");
            DataTable dt_instrumenttranslator = new DataTable("Instrumenttranslator");
            DataTable dt_translator = new DataTable("Translator");
            DataTable dt_table = new DataTable("table");
            sql = "select name from sqlite_master where type='table' order by name";
            dt_table = GetDataTable(sql, "table");

            if (dt_table.Select("name = 'InstrumentTranslator'").Length > 0)
            {
                sql = "select Instrument_Name,Translator_Name from InstrumentTranslator";
                dt_instrumenttranslator = GetDataTable(sql, "Instrumenttranslator");
                sql = "select Name,CodingSystem_Name from instrument ";
                dt_instrument = GetDataTable(sql, "Instrument");
                b_IsClV16 = true;
            }
            else
            {
                sql = "select Name,CodingSystem_Name,Translator_Name from instrument ";
                dt_instrument = GetDataTable(sql, "Instrument");
                b_IsClV16 = false;


            }
            foreach(DataRow e in dt_instrument.Rows)
            {
                instrumentna = e[0].ToString().Trim();
                codingsystem = e[1].ToString().Trim();
                if (b_IsClV16)
                {
                    DataRow[] dt = dt_instrumenttranslator.Select("instrument_name = '" + instrumentna + "'");
                    if (dt.Length > 0)
                    {
                        translator = dt_instrumenttranslator.Select("instrument_name = '" + instrumentna + "'")[0][1].ToString();

                    }
                    else
                    {
                        continue;
                    }

                }
                else
                {
                    translator = e[2].ToString().Trim();
                }
                CreateInstrumentTable(instrumentna);
                dt_translator.Clear();
                sql = "select Name,ExternalHost,ExternalPort,Driver,SpecificOptions from gp_Translator where Name = '" + translator + "'";
                dt_translator = GetDataTable(sql, "Translator");
                
                if (dt_translator.Rows[0]!= null)
                {
                    hostip = dt_translator.Rows[0][1].ToString().Trim();
                    portno = dt_translator.Rows[0][2].ToString().Trim();
                    instrclass = dt_translator.Rows[0][3].ToString().Trim();
                    option = dt_translator.Rows[0][4].ToString().Trim();
                    if (option.ToLower().IndexOf("-s") >= 0 )
                    {
                        connecttype = "C";
                        portno = option.Remove(0, 3);
                        hostip = "192.168.1.221";
                    }
                    else
                    {
                        connecttype = "S";
                        hostip = "";
                    }

                    sql = @"INSERT INTO instrumentlist(instrumentna,codingsystem,class,instrumenttype,connecttype,hostip,portno,instrumentenable)
                         values('"+ instrumentna + "','"  + codingsystem + "','" + instrclass + "','Analyzer','" + connecttype  + "','" + hostip + "','" + portno + "','0')";
                    ExecuteSql(SQLConnect, sql);
                }

            }
            
            

        }

        private static void CheckLIS()
        {
            string sql = "select Name,ExternalHost,ExternalPort,Driver,SpecificOptions from gp_Translator where Driver = 'asts'";
            string lisname, option,ipaddr,portno,connecttype;
            DataTable dt_lis = new DataTable("LIS");
            SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, SQLConnect);
            sda.Fill(dt_lis);
            foreach(DataRow e in dt_lis.Rows)
            {
                lisname = e[0].ToString().Trim();
                ipaddr  = e[1].ToString().Trim();
                portno = e[2].ToString().Trim();
                option = e[4].ToString().Trim();
                if (option.ToLower().IndexOf("-s") >= 0)
                {
                    connecttype = "C";
                    portno = option.Remove(0, 3);
                    ipaddr = "192.168.1.221";
                }
                else
                {
                    connecttype = "S";
                    ipaddr = "";
                }
                sql = @"INSERT INTO instrumentlist(instrumentna,codingsystem,class,instrumenttype,connecttype,hostip,portno,instrumentenable)
                         values('" + lisname + "','','asts','LIS','" + connecttype + "','" + ipaddr + "','" + portno + "','0')";
                ExecuteSql(SQLConnect, sql);

            }
                
        }

        public static void CreateInstrumentTable(string instrumentna)
        {
            string sql;
            sql = "CREATE TABLE `" + instrumentna + @"_order` (
	                `seq`	INTEGER,
	                `sampleid`	TEXT,
	                `sampleda`	TEXT,
	                `createtime`	TEXT,
                    `isrequested`  INTEGER DEFAULT 0,
                    `isresulted`   INTEGER DEFAULT 0,
                    `orderstring`   TEXT,
	                PRIMARY KEY(`seq`,`sampleid`,`sampleda`)
                    )";
            ExecuteSql(SQLConnect, sql);
        }

        public static DataTable GetDataTable(string sql,string tablename)
        {
            
            DataTable dt = new DataTable(tablename);
            SQLiteDataAdapter SDA = new SQLiteDataAdapter(sql, ConnectDatabase());
            try
            {
                SDA.Fill(dt);
            }
            catch(Exception e)
            {
                //DialogResult result = MessageBox.Show(e.Message, "");
            }
            
            return dt;
        }

        public static void DropTable()
        {
            string sql = "select name from sqlite_master where type='table' order by name";
            DataTable dt = GetDataTable(sql, "table");
            foreach(DataRow r in dt.Rows)
            {
                sql = "drop table " + r[0].ToString();
                ExecuteSql(SQLConnect, sql);
            }
        }

        
    }



}
