using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;

namespace AutoTestTools.Database
{
    static class DatabaseMYSQL
    {
        //static string  connectstring = "DRIVER={MySQL ODBC 5.3 Unicode Driver};" +
        //                        "SERVER=localhost;" +
        //                        "DATABASE=sys;" +
        //                        "UID=root;" +
        //                        "PASSWORD=root;" +
        //                        "OPTION=3";
        public static string connectstring = "server=127.0.0.1;user id=root;password=root;database=sys";
        public static MySqlConnection dbconnect;
        public static MySqlCommand dbcommand;
        
        public static string DBname = "Auto";
        private static string[] filename = { "Test.csv", "TestGroup.csv", "TestGroupMember.csv", "DeltaNorm.csv", "Norm.csv", "gp_Translator.csv", "Instrument.csv", "CodingSystem.csv", "SampleTypeCode.csv", "TestCode.csv", "InstrumentTranslator.csv" };

        public static void LoadDataTable()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择CSV所在文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ConnectDb();
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }

                foreach (string item in filename)
                {
                    string filepath = dialog.SelectedPath + "\\" + item;
                    FileInfo fi = new FileInfo(filepath);
                    if (!fi.Exists) continue;
                    DataTable dt = Tools.CSVProcess.OpenCSV(filepath);
                    string sql = "";
                    string tablehead = "";
                    dt.TableName = item.Split('.')[0];

                    sql = "Drop table if exists " + dt.TableName;
                    ExecuteSQL(sql, true);
                    sql = "";

                    sql += "Create Table if not exists  " + dt.TableName + " ( ";
                    
                    foreach (DataColumn dc in dt.Columns)   //循环列 
                    {
                        string temp = Replacechar(dc.ColumnName);
                        string headsql = "insert into csv_tablehead (tablename,tablehead_before,tablehead_current) values ('" + dt.TableName + "','" + dc.ColumnName + "','" + temp + "')";
                        sql = sql + " " + temp + "   LONGTEXT,";
                        ExecuteSQL(headsql, true);
                        tablehead += temp + ",";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")";
                    ExecuteSQL(sql, true);

                    tablehead = tablehead.Substring(0, tablehead.Length - 1);
                    InsertRecord(dt, dt.TableName, tablehead);



                    //MessageBox.Show(sql, "提示");
                }
                
                CheckInstrument();
                CheckLIS();
            }
        }

        private static void CheckInstrument()
        {
            string sql, hostip, portno, instrclass, option, connecttype;
            string instrumentna, codingsystem, translator;
            Boolean b_IsClV16;
            DataTable dt_instrument = new DataTable();
            DataTable dt_instrumenttranslator = new DataTable();
            DataTable dt_translator = new DataTable();
            DataTable dt_table = new DataTable();
            sql = "select table_name from information_schema.tables where table_schema='" + DBname + "' and table_type='base table'  order by table_name";
            dt_table = GetDataTable(sql, "table_schema");

            if (dt_table.Select("table_name = 'InstrumentTranslator'").Length > 0)
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
            foreach (DataRow e in dt_instrument.Rows)
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
                if (dt_translator.Rows.Count == 0) continue;
                if (dt_translator.Rows[0] != null)
                {
                    hostip = dt_translator.Rows[0][1].ToString().Trim();
                    portno = dt_translator.Rows[0][2].ToString().Trim();
                    instrclass = dt_translator.Rows[0][3].ToString().Trim();
                    option = dt_translator.Rows[0][4].ToString().Trim();
                    if (option.ToLower().IndexOf("-s") >= 0)
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
                         values('" + instrumentna + "','" + codingsystem + "','" + instrclass + "','Analyzer','" + connecttype + "','" + hostip + "','" + portno + "','0')";
                    ExecuteSQL(sql);
                }

            }



        }

        private static void CheckLIS()
        {
            string sql = "select Name,ExternalHost,ExternalPort,Driver,SpecificOptions from gp_Translator where Driver = 'asts'";
            string lisname, option, ipaddr, portno, connecttype;
            DataTable dt_lis = GetDataTable(sql, "LIS");
            foreach (DataRow e in dt_lis.Rows)
            {
                lisname = e[0].ToString().Trim();
                ipaddr = e[1].ToString().Trim();
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
                ExecuteSQL(sql);

            }

        }

        public static void CreateInstrumentTable(string instrumentna)
        {
            string sql;
            sql = "CREATE TABLE `" + instrumentna + @"_order` (
	                
	                `sampleid`	varchar(20) NOT NULL,
	                `sampleda`	DATETIME NOT NULL,
	                `createtime`	DATETIME,
                    `isrequested`  INT DEFAULT 0,
                    `isresulted`   INT DEFAULT 0,
                    `orderstring`   LONGTEXT,
	                PRIMARY KEY(`sampleid`,`sampleda`)
                    )
                      ENGINE = InnoDB";
            ExecuteSQL(sql,true);
        }

        private static void InsertRecord(DataTable dt, string tbname, string tablehead)
        {
            string head = "insert into " + DBname + "." + tbname + " ( " + tablehead + ") values (";
            string sql = "", tmp = "";
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
                ExecuteSQL(sql);
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


        public static MySqlConnection ConnectDb()
        {
            if (dbconnect == null)
            {
                try
                {
                    dbconnect = new MySqlConnection(connectstring);
                    dbconnect.Open();
                    InitDatabase();
                }
                catch(Exception e)
                {
                    ClassLib.GlobalValue.WriteLog(e.Message + "\r\n", "SQLERRORLOADTABLE.log");
                    return null;
                }
            }
            
            return dbconnect;
        }

        public static MySqlConnection GetNewConnect(string DBname)
        {
            MySqlConnection dbnconnect;
            Boolean success = false;
            int timeout = 0;
            while (!success)
            {
                try
                {


                    dbnconnect = new MySqlConnection(connectstring);

                    dbnconnect.Open();
                    dbnconnect.ChangeDatabase(DBname);
                    success = true;
                    return dbnconnect;
                }
                catch (Exception e)
                {

                    timeout++;
                    if (timeout > 10)
                    {
                        ClassLib.GlobalValue.WriteLog(e.Message + "\r\n", "SQLERRORLOADTABLE.log");
                        return null;
                    }


                }
            }
            return dbconnect;

            
        }


        public static void InitDatabase()
        {
            string SQL;
            SQL = "create database if not exists " + DBname;
            dbcommand = new MySqlCommand(SQL, dbconnect);
            dbcommand.ExecuteNonQuery();
            ExecuteSQL(SQL, true);
            dbconnect.ChangeDatabase(DBname);
            SQL = @"Create table if not exists "+DBname+ @".csv_tablehead( 
                    tablename varchar(40) ,
                    tablehead_before varchar(40) ,
                    tablehead_current varchar(40))
                     ENGINE = MyISAM";
            ExecuteSQL(SQL,true);
            SQL = @"CREATE TABLE if not exists  " + DBname + @".Setting(
                    `SettingName` varchar(40),
                    `SettingValue`   LONGTEXT,
                    PRIMARY KEY(`SettingName`)
                    )
                    ENGINE = InnoDB";
            ExecuteSQL(SQL, true);

            SQL = @"CREATE TABLE if not exists " + DBname + @".samplemain (
	                
	                `sampleid`	varchar(20) NOT NULL,
	                `sampleda`	DATETIME NOT NULL,
	                `sampletype`	varchar(20),
	                `patientno`	varchar(20),
	                `patientna`	varchar(20),
	                `sex`	varchar(20),
	                `birthday`	DATETIME,
	                `createtime`	DATETIME,
                    `orderda`   DATETIME,
                    `ordered`   INT DEFAULT 0,
                    `dispatched`    INT DEFAULT 0,
	                 PRIMARY KEY(`sampleid`,`sampleda`)
                      )
                        ENGINE = InnoDB";
            ExecuteSQL(SQL, true);

            SQL = @"CREATE TABLE if not exists " + DBname + @".sampledetail (
	                
	                `sampleid`	varchar(20) NOT NULL,
	                `sampleda`	DATETIME NOT NULL,
	                `tests`	varchar(20) NOT NULL,
	                `result`	LONGTEXT,
	                PRIMARY KEY(`sampleid`,`sampleda`,`tests`)
                    )
                     ENGINE = InnoDB";
            ExecuteSQL(SQL, true);

            SQL = @"CREATE TABLE if not exists " + DBname + @".instrumentlist (
	                `instrumentna`	varchar(40),
                    `codingsystem`  varchar(20),
	                `class`	varchar(20),
	                `instrumenttype`	varchar(20),
	                `connecttype`	varchar(20),
	                `hostip`	varchar(20),
	                `portno`	varchar(20),
	                `instrumentenable`	INT DEFAULT 0,
	                PRIMARY KEY(`instrumentna`)
                    )
                     ENGINE = InnoDB";
            ExecuteSQL(SQL, true);

        }

        public static void CloseDatabase()
        {
            if (dbconnect != null)
            {
                dbconnect.Close();
            }
        }

        public static DataTable GetDataTable(string sql,string tablename,Boolean ShowErrorMessage = false)
        {

            MySqlConnection MSC = GetNewConnect(DBname);
            DataTable dt = new DataTable(tablename);//= new DataTable(tablename);
            DataSet ds = new DataSet(tablename);
            Boolean success = false;
            int trycycle = 0;
            while (!success)
            {
                try
                {
                    if (MSC == null)
                    {
                        MSC = GetNewConnect(DBname);
                    }
                    MySqlDataAdapter MSDA = new MySqlDataAdapter(sql, MSC);
                    int i = MSDA.Fill(ds, tablename);
                    dt = ds.Tables[tablename];
                    success = true;
                    ds.Dispose();
                    MSC.Close();
                    MSC.Dispose();
                    MSDA.Dispose();
                }
                catch (Exception e)
                {
                    if (ShowErrorMessage)
                    {
                        DialogResult result = MessageBox.Show(e.Message + "\r\n" + sql, "Error");
                    }
                    trycycle++;
                    if (trycycle > 10)
                    {
                        ClassLib.GlobalValue.WriteLog(e.Message + "\r\n" + sql, "SQLERRORLOADTABLE.log");
                        Thread.Sleep(5);
                        //if (MSC != null)
                        //{
                        //    MSC.Close();
                        //    MSC.Dispose();
                        //}
                        return dt;
                    }
                }
            }
            



            return dt;
        }

        public static int ExecuteSQL(string sql,Boolean ShowErrormessage = false)
        {
            MySqlConnection MSC = GetNewConnect(DBname); ;
            Boolean success = false;
            int trycycle = 0;
            while (!success)
            {
                try
                {
                    if (MSC == null)
                    {
                        MSC = GetNewConnect(DBname);
                    }
                    
                    dbcommand = new MySqlCommand(sql, MSC);
                    dbcommand.ExecuteNonQuery();
                    success = true;
                    MSC.Close();
                    MSC.Dispose();
                    //dbcommand.BeginExecuteNonQuery
                }
                catch (Exception e)
                {
                    if (ShowErrormessage)
                    {
                        DialogResult result = MessageBox.Show(e.Message + "\r\n" + sql, "SQLError");
                    }
                    
                    trycycle++;
                    if (trycycle > 10)
                    {
                        ClassLib.GlobalValue.WriteLog(e.Message + "\r\n" + sql, "SQLERROR.log");
                        //if (MSC!=null)
                        //{
                        //    MSC.Close();
                        //    MSC.Dispose();
                        //}
                        
                        return -1;
                    }
                    
                    
                }
                
            }
            

            return 0;
        }

        public static string GetConnectString()
        {
            string strLine = "";
            string total = "";
            Encoding encoding = Encoding.ASCII;
            FileInfo fi = new FileInfo("SQLConnect.txt");
            if (!fi.Exists) return "";
            FileStream fs = new FileStream("SQLConnect.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, encoding);
            while ((strLine = sr.ReadLine()) != null)
            {
                total += strLine;
            }
            
            sr.Close();
            fs.Close();
            return total;
        }

        public static void DropTable()
        {
            string sql = "select table_name from information_schema.tables where table_schema='" + DBname + "' and table_type='base table'  order by table_name";
            DataTable dt = GetDataTable(sql, "table");
            foreach (DataRow r in dt.Rows)
            {
                sql = "drop table " + r[0].ToString();
                ExecuteSQL(sql);
            }
        }
    }
}
