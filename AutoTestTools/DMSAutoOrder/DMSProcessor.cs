using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMSAutoOrder
{
    public static class DMSProcessor
    {

        public static Boolean BAutoStart, BSameIP, BStart, BEnableNoWorkorderProcess, BEnablePitStopTableMonitor;
        public static string STestCode, SampleIDLen, AutoModifyTestStatus, IgnoreFlagList;
        public static Dictionary<string, string> DAutoModifyTestStatus = new Dictionary<string, string>();
        public static string inifile = AppDomain.CurrentDomain.BaseDirectory + "\\Setting.ini";
        public static DMSConnector dmsconnect;
        public static Dictionary<string, Dictionary<string, DateTime>> PitStopDictSum = new Dictionary<string, Dictionary<string, DateTime>>();
        public static string AptioConnectString, DeleteTableList, TestTriggerSampleDeletion;
        public static Boolean BSendCancelMessageToAptio;
        public static List<string> TableDeleteSql;
        public static Thread t;
      

        public static void LoadSetting()
        {
            GlobalValue.wlog.WriteEntry("LoadSetting:"+inifile, EventLogEntryType.Information);
            iniManager.fileName = inifile;
            FileInfo fi = new FileInfo(inifile);
            if (!fi.Exists)
            {
                GlobalValue.wlog.WriteEntry("INIFileNotExists,Create New File:" + inifile, EventLogEntryType.Warning);
                fi.Create().Close();
                
                
                InitSettingFile();
                GlobalValue.wlog.WriteEntry("Init New Setting File:" + inifile, EventLogEntryType.Warning);
                //return;
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

            BEnableNoWorkorderProcess = (iniManager.GetString("SYS", "EnableNoWorkorderProcess", "0") == "1");
            BEnablePitStopTableMonitor = (iniManager.GetString("SYS", "EnablePitStopTableMonitor", "0") == "1");

            SampleIDLen = iniManager.GetString("DMS", "SampleIDLen", "0");
            IgnoreFlagList = iniManager.GetString("DMS", "IgnoreFlagList", "");
            //1,don't modify status 2,change send to host status to 0,resend result 3,change test status to final
            AutoModifyTestStatus = iniManager.GetString("DMS", "AutoModifyTestStatus", "2");

            //20210903
            AptioConnectString = iniManager.GetString("Aptio", "AptioConnectString", "10.0.0.200:2055");
            DeleteTableList = iniManager.GetString("DMS", "DeleteTableList", "reqtestresult;reqtestresultrerun;reqtestresultunsolicited;reqtestresultrejected;attach;attachrerun;orders;orders_details;orders_tat;reqtube;reqtube_details;reqtube_automation;reqtube_tat;reqtest;reqtest_tat;duplicate;duplicate_tests;awos_hl7;reqtestresult_substance;reqtestresultrerun_substance;reqtestresultunsolicited_substance;reqtestresultrejected_substance;plate_mtp;reqtestresult_aspects;reqtestresultrerun_aspects;reqtestresultunsolicited_aspects;reqtestresultrejected_aspects");
            TestTriggerSampleDeletion = iniManager.GetString("DMS", "TestTriggerSampleDeletion", "");
            BSendCancelMessageToAptio = (iniManager.GetString("DMS", "SendCancelMessageToAptio", "0") == "1");
            GlobalValue.wlog.WriteEntry("LoadComplete", EventLogEntryType.Information);
        }

        public static void InitSettingFile()
        {
            try
            {
                iniManager.WriteString("DB", "DBName", "DMSCN");
                iniManager.WriteString("DB", "DBIP", "10.0.0.100");
                iniManager.WriteString("DB", "DBUser", "root");
                iniManager.WriteString("DB", "DBPasswd", "lINEA_3");
                iniManager.WriteString("DMS", "DMSIP", "10.0.0.100");
                iniManager.WriteString("DMS", "OrderPort", "3001");
                iniManager.WriteString("DMS", "TestCode", "PARK");
                iniManager.WriteString("SYS", "AutoStart", "1");
                iniManager.WriteString("SYS", "SameIP", "1");
                iniManager.WriteString("DMS", "ConnectMode", "1");
                iniManager.WriteString("DMS", "ConnectModeComment", "1=client,2=server");
                iniManager.WriteString("SYS", "EnableNoWorkorderProcess", "0");
                iniManager.WriteString("SYS", "EnablePitStopTableMonitor", "1");
                iniManager.WriteString("DMS", "AutoModifyTestStatus", "1");
                iniManager.WriteString("DMS", "AutoModifyTestStatusComment", "1=Do Not Change The Status,2=Change Hostflag status to 0,resent the result,3=Change Test Status From Val To F");
                iniManager.WriteString("DMS", "IgnoreFlagList", "");
                iniManager.WriteString("DMS", "IgnoreFlagListComment", "Auto Val Results when the following flags are present.  Separated by a comma. e.g. flag1,flag2");
                iniManager.WriteString("Aptio", "AptioConnectString", "10.0.0.200:2055");
                iniManager.WriteString("DMS", "TestTriggerSampleDeletion", "");
                iniManager.WriteString("DMS", "DeleteTableList", "reqtestresult;reqtestresultrerun;reqtestresultunsolicited;reqtestresultrejected;attach;attachrerun;orders;orders_details;orders_tat;reqtube;reqtube_details;reqtube_automation;reqtube_tat;reqtest;reqtest_tat;duplicate;duplicate_tests;awos_hl7;reqtestresult_substance;reqtestresultrerun_substance;reqtestresultunsolicited_substance;reqtestresultrejected_substance;plate_mtp;reqtestresult_aspects;reqtestresultrerun_aspects;reqtestresultunsolicited_aspects;reqtestresultrejected_aspects");
                iniManager.WriteString("DMS", "TestTriggerSampleDeletionComment", "Recieve Test Then Delete Sample From DMS,Format: [TestName]:[DelayTimeInMinutes] ; [TN]:[TIM]");
                iniManager.WriteString("DMS", "SendCancelMessageToAptio", "1");
            }
            catch (Exception ex) 
            {
                GlobalValue.wlog.WriteEntry("InitSettingFile With Error:" + ex.Message, EventLogEntryType.Error);
            }
        }
    


        public static void Start()
        {
            GlobalValue.wlog.WriteEntry("Processing Start", EventLogEntryType.Warning);
            BStart = true;
            dmsconnect = new DMSConnector();
            LoadSetting();
            t = new Thread(process);
            t.IsBackground = true;
            t.Start();

        }

        private static void process()
        {
            DbProcesser DdbProcesser = new DbProcesser(bathfunction);

            if (BEnableNoWorkorderProcess)
            {
                dmsconnect.MakeASTMConnect();
            }


            TableDeleteSql = MakeDeleteSql();

            while (BStart)
            {
                try
                {

                    IAsyncResult result = DdbProcesser.BeginInvoke(null, null);
                    //PitStopTableMonitor();


                    DdbProcesser.EndInvoke(result);

                    Thread.Sleep(6000);


                }
                catch (Exception e)
                {
                    GlobalValue.wlog.WriteEntry("Processing Error:" + e.Message, EventLogEntryType.Error);
                    
                }
            }

        }

        private static void bathfunction()
        {
            NoWorkOrderProcess();
            ChangeWrongStatusSampleToHostFlg();
            ValIgnoreFlagResult();
            PitStopTableMonitor();
            FTestTriggerSampleDeletion();
        }

        private delegate void DbProcesser();


        private static void PitStopTableMonitor()
        {

            if (!BEnablePitStopTableMonitor)
            {
                return;
            }
            try
            {

                string tablesql;
                tablesql = new StringBuilder("select table_name from information_schema.tables where table_schema = '").Append(MysqlClass.DBname).Append("' and table_name like 'pitstop%'").ToString();
                DataTable pitstoplist = MysqlClass.GetDataTable(tablesql, "pitstoptable");

                foreach (DataRow r in pitstoplist.Rows)
                {
                    string tablename = r[0].ToString();
                    if (!PitStopDictSum.ContainsKey(tablename))
                    {
                        PitStopDictSum.Add(tablename, new Dictionary<string, DateTime>());
                    }

                    string sqlr = new StringBuilder("select * from ").Append(MysqlClass.DBname).Append(".").Append(r[0].ToString()).Append(" where flgrunning <> ''").ToString();
                    DataTable dt = MysqlClass.GetDataTable(sqlr, "drows");

                    if (!(dt.Rows.Count > 0))
                    {
                        continue;
                    }


                    //string sql = new StringBuilder("select count(*),flgrunning,idpitstop from ").Append(MysqlClass.DBname).Append(".").Append(r[0].ToString()).Append(" where flgrunning <> ''").ToString();
                    //int i = 0, counts;
                    //counts = int.Parse(MysqlClass.GetDataTable(sql, "count").Rows[0][0].ToString());
                    List<string> idpitstoplist = new List<string>();
                    foreach (DataRow row in dt.Rows)
                    {

                        string idpitstop = row["idpitstop"].ToString();
                        idpitstoplist.Add(idpitstop);
                        DateTime dateTime = DateTime.UtcNow;
                        if (!PitStopDictSum[tablename].ContainsKey(idpitstop))
                        {
                            PitStopDictSum[tablename].Add(idpitstop, dateTime);
                        }
                        else
                        {
                            if ((dateTime - PitStopDictSum[tablename][idpitstop]).TotalMinutes > 1)
                            {
                                StringBuilder sbrowobject = new StringBuilder();
                                int columnindex = 0;
                                foreach (object dc in row.ItemArray)
                                {
                                    sbrowobject.Append(dt.Columns[columnindex].ColumnName).Append(":").Append(dc.ToString()).Append(",");
                                    columnindex++;
                                }
                                GlobalValue.WriteLog("PitStopTableMonitor : Table" + r[0].ToString() + " record {" + sbrowobject.ToString() + "} need clean...", "SystemError.log");
                                string excutesql = new StringBuilder("delete from ").Append(MysqlClass.DBname).Append(".").Append(r[0].ToString()).Append(" where flgrunning <> ''").ToString();
                                MysqlClass.ExecuteSQL(excutesql);
                                GlobalValue.WriteLog("PitStopTableMonitor : Table" + r[0].ToString() + " clean...", "SystemError.log");
                                PitStopDictSum[tablename].Remove(idpitstop);
                                idpitstoplist.Remove(idpitstop);
                            }
                        }


                    }
                    foreach (string id in PitStopDictSum[tablename].Keys)
                    {
                        if (!idpitstoplist.Contains(id))
                        {
                            PitStopDictSum[tablename].Remove(id);
                        }
                    }




                    //while (counts > 2 && i < 10)
                    //{
                    //    Thread.Sleep(500);
                    //    counts = int.Parse(MysqlClass.GetDataTable(sql, "count").Rows[0][0].ToString());
                    //    i++;

                    //}
                    //if (counts > 2)
                    //{
                    //    string sqlr = new StringBuilder("select * from ").Append(MysqlClass.DBname).Append(".").Append(r[0].ToString()).Append(" where flgrunning <> ''").ToString();
                    //    DataTable dt = MysqlClass.GetDataTable(sqlr, "drows");
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        foreach (DataRow dr in dt.Rows)
                    //        {
                    //            StringBuilder sbrowobject = new StringBuilder();
                    //            int columnindex = 0;
                    //            foreach(object dc in dr.ItemArray)
                    //            {
                    //                sbrowobject.Append(dt.Columns[columnindex].ColumnName).Append(":").Append(dc.ToString()).Append(",");
                    //                columnindex++;
                    //            }

                    //            GlobalValue.WriteLog("PitStopTableMonitor : Table" + r[0].ToString() + " record {" + sbrowobject.ToString() + "} need clean...", "SystemError.log");
                    //        }

                    //        string excutesql = new StringBuilder("delete from ").Append(MysqlClass.DBname).Append(".").Append(r[0].ToString()).Append(" where flgrunning <> ''").ToString();
                    //        MysqlClass.ExecuteSQL(excutesql);
                    //        GlobalValue.WriteLog("PitStopTableMonitor : Table" + r[0].ToString() + " clean...", "SystemError.log");
                    //    }
                    //}


                }
            }
            catch (Exception e)
            {
                
                GlobalValue.wlog.WriteEntry("PitStopTableMonitor Error:" + e.Message, EventLogEntryType.Error);
            }

        }

        private static void NoWorkOrderProcess()
        {
            if (!BEnableNoWorkorderProcess)
            {
                return;
            }
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
                    }
                    catch (Exception e)
                    {
                        GlobalValue.wlog.WriteEntry("NoWorkOrderProcess Error:" + e.Message, EventLogEntryType.Error);
                        
                    }
                    Thread.Sleep(50);
                }
            }
        }

        private static string GetSampleType(string SID)
        {
            string sql, sql2, sampletype, testcode;
            Boolean Bhasinstrumenttest;
            DataTable T_reqtube, T_reqtest, T_testlab, T_testinstrument, T_sampletype;
            sql = "Select codtypesample from " + MysqlClass.DBname + ".reqtube where codsid = '" + SID + "'";
            T_reqtube = MysqlClass.GetDataTable(sql, "tube");
            sampletype = T_reqtube.Rows[0][0].ToString();
            Bhasinstrumenttest = false;
            if (sampletype != "" && sampletype != null)
            {
                return sampletype;
            }
            sql = "select codtest from " + MysqlClass.DBname + ".reqtestresult where codsid = '" + SID + "'";
            T_reqtest = MysqlClass.GetDataTable(sql, "Test");
            foreach (DataRow r in T_reqtest.Rows)
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
                if (T_testlab.Rows.Count > 0)
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

        private static void CleanOrdersTable(string OID, string SID)
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
            sql = "delete from " + MysqlClass.DBname + ".reqtest where codsid = '" + SID + "' and codtest = '" + STestCode + "'";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
            sql = "delete from " + MysqlClass.DBname + ".reqtestresult where codsid = '" + SID + "' and codtest = '" + STestCode + "'";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
        }

        private static void ValTest(string SID)
        {
            string sql;
            sql = "Update " + MysqlClass.DBname + ".reqtestresult set flgstatus = 'V' where codsid = '" + SID + "' and flgstatus = 'H'";
            MysqlClass.ExecuteSQL(sql);
            sql = "Update " + MysqlClass.DBname + ".reqtest set flgstatus = 'V' where codsid = '" + SID + "' and flgstatus = 'H'";
            MysqlClass.ExecuteSQL(sql);
        }
        private static void ValTestResult(string SID, string Testcode)
        {
            string sql;
            sql = "Update " + MysqlClass.DBname + ".reqtestresult set flgstatus = 'V' where codsid = '" + SID + "' and codtest = '" + Testcode + "'";
            MysqlClass.ExecuteSQL(sql);
            sql = "Update " + MysqlClass.DBname + ".reqtest set flgstatus = 'V',flgtohost = 0  where codsid = '" + SID + "' and codtest = '" + Testcode + "'";
            MysqlClass.ExecuteSQL(sql);

        }

        private static void ChangeWrongStatusSampleToHostFlg()
        {
            string sql;
            sql = "delete from " + MysqlClass.DBname + ".reqtestresult where flgstatus in ('E','R','P')  or valresult1 = ''";
            GlobalValue.WriteLog(sql + "\r\n", "DeleteSample.log");
            MysqlClass.ExecuteSQL(sql);
            Thread.Sleep(20);

            if (AutoModifyTestStatus == "1")
            {
                return;
            }
            if (AutoModifyTestStatus == "2")
            {
                sql = "update " + MysqlClass.DBname + ".reqtest set flgtohost = 0 where flgstatus in ('V','X','Y','Z') and flgtohost <> 0";
                MysqlClass.ExecuteSQL(sql);
                Thread.Sleep(20);
                sql = "update " + MysqlClass.DBname + ".reqtest t, " + MysqlClass.DBname + ".reqtestresult r set t.flgtohost = 0, t.flgstatus = 'V' where  t.codsid = r.codsid and t.codtest = r.codtest and t.flgstatus <> r.flgstatus and t.flgstatus='F'";
                MysqlClass.ExecuteSQL(sql);
            }
            if (AutoModifyTestStatus == "3")
            {
                //sql = "update " + MysqlClass.DBname + ".reqtestresult set  " + MysqlClass.DBname + ".reqtestresult.flgstatus = 'F' " +
                //    "where " + MysqlClass.DBname + ".reqtestresult.codsid =  " + MysqlClass.DBname + ".reqtest.codsid " +
                //    " and " + MysqlClass.DBname + ".reqtestresult.codtest =  " + MysqlClass.DBname + ".reqtest.codtest " +
                //    " and (" + MysqlClass.DBname + ".reqtestresult.flgstatus = 'V' or " + MysqlClass.DBname + ".reqtestresult.flgstatus = 'X' or " + MysqlClass.DBname + ".reqtestresult.flgstatus = 'Y' or " + MysqlClass.DBname + ".reqtestresult.flgstatus = 'Z' ) " +
                //    "and " + MysqlClass.DBname + ".reqtest.flgtohost <> 0";
                sql = "update " + MysqlClass.DBname + ".reqtestresult," + MysqlClass.DBname + ".reqtest  set  reqtestresult.flgstatus = 'F' " +
                    "where reqtestresult.codsid = reqtest.codsid " +
                    " and reqtestresult.codtest = reqtest.codtest " +
                    " and reqtestresult.flgstatus in ('V','X','Y','Z' ) " +
                    "and reqtest.flgtohost <> 0";
                MysqlClass.ExecuteSQL(sql);
                Thread.Sleep(20);
                sql = "update " + MysqlClass.DBname + ".reqtest set flgstatus = 'F' where flgstatus in ('V','X','Y','Z') and flgtohost <> 0";

                MysqlClass.ExecuteSQL(sql);
            }

        }

        private static void ValIgnoreFlagResult()
        {
            if (IgnoreFlagList == "")
            {
                return;
            }
            string sql;
            string[] ingroeflaglist = IgnoreFlagList.Split(',');
            sql = "select codsid, codtest, jsnflaginstrument from " + MysqlClass.DBname + ".reqtestresult where flgstatus not in  ('F','V','X','Y','Z')";
            DataTable dt_holdresult = MysqlClass.GetDataTable(sql, "holdresult");
            foreach (DataRow r in dt_holdresult.Rows)
            {
                string codsid = r[0].ToString();
                string codtest = r[1].ToString();
                string jsnflaginstrument = r[2].ToString();
                Boolean B_NeedModify = false;
                if (jsnflaginstrument.IndexOf("[") == 0)
                {
                    jsnflaginstrument = jsnflaginstrument.Substring(1, jsnflaginstrument.Length - 2);
                    jsnflaginstrument = jsnflaginstrument.Replace('"'.ToString(), string.Empty);
                    string[] flagarray = jsnflaginstrument.Split(',');
                    foreach (string flag in flagarray)
                    {
                        if (ingroeflaglist.Contains(flag))
                        {
                            B_NeedModify = true;
                            break;
                        }
                    }
                    if (B_NeedModify)
                    {
                        ValTestResult(codsid, codtest);
                    }
                }
            }
        }



        private static void FTestTriggerSampleDeletion()
        {
            Dictionary<string, string> TestTimePire = new Dictionary<string, string>();
            try
            {
                Connect aptioconnect;
                if (BSendCancelMessageToAptio)
                {
                    string ip = AptioConnectString.Substring(0, AptioConnectString.IndexOf(":"));
                    int port = int.Parse(AptioConnectString.Substring(AptioConnectString.IndexOf(":") + 1));
                    aptioconnect = new SocketClient(ip, port);
                }
                string[] tests = TestTriggerSampleDeletion.Split(';');
                List<string> testlist = new List<string>();


                foreach (string test in tests)
                {
                    if (!string.IsNullOrEmpty(test))
                    {
                        string t = test.Substring(0, test.IndexOf(":"));
                        string tm = test.Substring(test.IndexOf(":") + 1);
                        if (!TestTimePire.ContainsKey(t))
                        {
                            TestTimePire.Add(t, tm);
                            testlist.Add(t);
                        }
                        else
                        {
                            TestTimePire[t] = tm;
                        }
                        string sql = "select * from " + MysqlClass.DBname + ".reqtest where codtest = '" + t + "' and timestampdiff(MINUTE,datrequest,now()) > " + tm;
                        DataTable dt = MysqlClass.GetDataTable(sql, "reqtest");
                        foreach (DataRow r in dt.Rows)
                        {
                            string sid = r["codsid"].ToString();
                            string oid = r["codoid"].ToString();
                            DeleteSample(sid, oid);

                        }
                    }
                }






            }
            catch (Exception e)
            {
                
                GlobalValue.wlog.WriteEntry("FTestTriggerSampleDeletion Error:" + e.Message, EventLogEntryType.Error);
            }


        }

        private static void button1_Click(object sender, EventArgs e)
        {
            TableDeleteSql = MakeDeleteSql();
            FTestTriggerSampleDeletion();
        }

        private static void DeleteSample(string sid, string oid)
        {
            GlobalValue.WriteLog("Test Trigger DeleteSample SID:" + sid + "\r\n", "DeleteSample.log");
            foreach (string sql in TableDeleteSql)
            {
                MysqlClass.ExecuteSQL(string.Format(sql, sid, oid));
                Thread.Sleep(10);
            }

        }



        private static List<string> MakeDeleteSql()
        {
            string sql = "select COLUMN_NAME,table_name  FROM information_schema.columns where column_name in ('codsid','codoid') and table_name in (select table_name from information_schema.tables where table_type = 'BASE TABLE' and table_schema = '" + MysqlClass.DBname + "') group by COLUMN_NAME,table_name  order by table_name";
            DataTable dt = MysqlClass.GetDataTable(sql, "tableinfo");
            List<string> delstring = new List<string>();
            string tablename = "", sqltmp = "";

            foreach (DataRow r in dt.Rows)
            {
                if (tablename != r["table_name"].ToString())
                {
                    if (sqltmp != "")
                    {
                        delstring.Add(sqltmp);
                        sqltmp = "";
                    }

                    tablename = r["table_name"].ToString();
                    sqltmp = "delete from " + MysqlClass.DBname + "." + tablename;
                    if (r["COLUMN_NAME"].ToString().ToUpper() == "CODSID")
                    {
                        sqltmp += " where codsid = '{0}'";
                    }
                    if (r["COLUMN_NAME"].ToString().ToUpper() == "CODOID")
                    {
                        sqltmp += " where codoid = '{1}'";
                    }

                }
                else
                {
                    if (r["COLUMN_NAME"].ToString().ToUpper() == "CODSID")
                    {
                        sqltmp += " and codsid = '{0}'";
                    }
                    if (r["COLUMN_NAME"].ToString().ToUpper() == "CODOID")
                    {
                        sqltmp += " and codoid = '{1}'";
                    }
                }
            }

            if (sqltmp != "")
            {
                delstring.Add(sqltmp);
            }
            return delstring;
        }

        public static void Stop()
        {
            BStart = false;
            try
            {

                dmsconnect.StopConnector();
            }
            catch
            {

            }

            GlobalValue.wlog.WriteEntry("Processing Stoped", EventLogEntryType.Warning);
        }

    }
}
