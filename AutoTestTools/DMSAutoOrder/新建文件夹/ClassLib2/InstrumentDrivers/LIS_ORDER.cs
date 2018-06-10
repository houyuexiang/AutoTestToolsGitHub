using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.Threading;

namespace AutoTestTools.ClassLib.InstrumentDrivers
{
    class LIS_ORDER:InstrumentBase
    {

        private string FS = GlobalValue.FS,GS = GlobalValue.GS,CR = GlobalValue.CR;
        public ASTM LISASTM;
        protected override void LoadDataTable()
        {
            string sql = "select sampleid,sampleda,sampletype,patientno,patientna,sex,birthday,createtime,orderda,ordered,dispatched from samplemain where ordered = 0 or dispatched = 0 limit 20";
            dt_instrument_order = Database.DatabaseMYSQL.GetDataTable(sql, Instrumentname);

        }
        public override string MakeRequest()
        {
            string sampleid, sampleda, sampletype, patientno, patientna, sex, birthday, createtime,orderda;
            int  ordered,seed = 0,timeout = 0;
            string sendtext;
            string timestr;
            string Ordertype = "", Stransorder = "";
            DateTime dt_createtime,dt_orderda;
            List<string> list = new List<string>();
            string sql = "select sampleid,sampleda,sampletype,patientno,patientna,sex,birthday,createtime,orderda,ordered,dispatched from samplemain where ordered = 0 limit 20";
            dt_instrument_order = Database.DatabaseMYSQL.GetDataTable(sql, Instrumentname);
            foreach (DataRow e in dt_instrument_order.Rows)
            {
                
                sampleid = e[0].ToString();
                sampleda = Convert.ToDateTime( e[1].ToString()).ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                sampletype = e[2].ToString();
                patientno = e[3].ToString();
                patientna = e[4].ToString();
                sex = e[5].ToString();
                birthday = Convert.ToDateTime(e[6].ToString()).ToString("yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                createtime = e[7].ToString();
                ordered = Convert.ToInt16(e[9]);
                
                
                dt_createtime = Convert.ToDateTime(createtime,System.Globalization.CultureInfo.CurrentCulture);
                orderda = e[8].ToString();
                if (orderda == null || orderda == "")
                {
                    dt_orderda = DateTime.Now;
                }
                else
                {
                    dt_orderda = Convert.ToDateTime(orderda, System.Globalization.CultureInfo.CurrentCulture);
                }
                
                if (DateTime.Now.Subtract(dt_createtime).TotalSeconds < 100)
                {
                    continue;
                }
                if (ordered == 0)
                {
                    while (LISASTM.sendbuffer.Length > 0)
                    {
                        Thread.Sleep(10);
                        timeout++;
                        if (timeout > 10)
                        {
                            return "";
                        }
                    }
                    //if (LISASTM.sendbuffer.Length > 0) return "" ; 
                    list = GetSampleDetailList(sampleid, sampleda);
                    sendtext = FS + "[H|\\^&||||||||||P|1" + CR + "]";
                    sendtext += "[P|1|" + patientno + "|" + patientno + "||"  + patientna + "||" + birthday + "|" + sex + "" + CR + "]";
                    timestr = DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    Ordertype = "N";
                    Stransorder = LisOrdertrans(list);
                    sendtext += "[O|1|" + sampleid + "||" + Stransorder + "|R||" + timestr + "||||" + Ordertype + "||||"  + sampletype + "|" + CR + "]";
                    
                    foreach(string str in list)
                    {
                        
                        sendtext += MakePreResult(str,dt_createtime, seed + DateTime.Now.Millisecond + DateTime.Now.Second);
                        seed++;
                    }
                    sendtext += "[L|1" + CR + "]" + GS;
                    //int timeout = 0;
                    //while (LISASTM.sendbuffer.Length > 10000 && timeout < 100)
                    //{
                    //    Thread.Sleep(100);
                    //    timeout++;
                    //}
                    //if (timeout >= 100)
                    //{
                    //    LISASTM.Reset();

                    //    Thread.Sleep(10);
                    //    timeout = 0;
                    //    //LISASTM = new ASTM(connect.connect);
                    //}
                    //timeout = 0;
                    LISASTM.sendbuffer = sendtext;
                    int cycle = 0;
                    while (!LISASTM.SendComplete()&&cycle < 1000)
                    {
                        Thread.Sleep(5);
                        cycle++;
                    }
                    if (LISASTM.SendComplete())
                    {
                        sql = "update samplemain set ordered = 1 , orderda = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) + "' where  sampleid = '" + sampleid + "' and sampleda = '" + sampleda + "'";
                        Database.DatabaseMYSQL.ExecuteSQL(sql);
                    }
                    else
                    {
                        GlobalValue.WriteLog("Sample ID " + sampleid + " order Send Error", "SendError.log");
                    }
                }
                Thread.Sleep(10);
                //else
                //{
                //    if (orderda == null || orderda == "") continue;
                //    DateTime now = DateTime.Now;
                //    double i = now.Subtract(dt_orderda).TotalSeconds;
                //    if (now.Subtract(dt_orderda).TotalSeconds > GlobalValue.InstrumentRequestInterval)
                //    {
                //        InserInstrument( sampleid, sampleda);
                //        sql = "update samplemain set dispatched = 1 where  sampleid = '" + sampleid + "' and sampleda = '" + sampleda + "'";
                //        Database.DatabaseMYSQL.ExecuteSQL(sql);
                //    }
                    
                //}
            }
               
            return "";
        }

        private void InserInstrument(string sampleid,string sampleda)
        {
            DataTable dt_instrumentlist = new DataTable("instrumentlist");
            string sql = "select instrumentna,instrumenttype from instrumentlist where instrumentenable = '1'";
            string instrumentna;
            dt_instrumentlist = Database.DatabaseMYSQL.GetDataTable(sql, "instrumentlist");
            
            foreach(DataRow r in dt_instrumentlist.Rows)
            {
                instrumentna = r[0].ToString();
                if (r[1].ToString().IndexOf("LIS") >= 0) continue;
                sql = "insert into " + instrumentna + "_order(sampleid,sampleda,createtime) values(" + "'" + sampleid + "','" + sampleda + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) + "')";
                Database.DatabaseMYSQL.ExecuteSQL(sql);
            }
        }


        private string LisOrdertrans(List<string> test)
        {
            string Stemp = "";
            foreach (string S in test)
            {
                if (S != "" && S != null)
                {
                    Stemp += "\\^^^" + S + "^";
                }
            }
            if (Stemp.Length > 0)
            {
                Stemp = Stemp.Substring(1);
            }
            
            return Stemp;
        }
        private string MakePreResult(string test,DateTime dt_sampleda,int seed)
        {
            Random r = new Random(seed);
            DateTime dt_presampleda;
            int days;
            string timestr,resulttext,values;
            days = r.Next(-31, -1);
            dt_presampleda = dt_sampleda.AddDays(days);
            timestr = dt_presampleda.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            values = (r.Next(0, 20) + r.NextDouble()).ToString();
            values = values.Substring(0, values.IndexOf(".") + 2);
            resulttext = "[R|1|^^^" + test + "|" + values + "||||||||" + timestr+ "|" + CR + "]";
            
            return resulttext;
        }

        private List<string> GetSampleDetailList(string sampleid,string sampleda)
        {
            string sql;
            List<string> list = new List<string>();
            DataTable tb = new DataTable("sampledetail");
            sql = "select sampleid,sampleda,tests from sampledetail where  sampleid = '" + sampleid + "' and sampleda = '" + sampleda + "'";
            tb = Database.DatabaseMYSQL.GetDataTable(sql, "sampledetail");
            Thread.Sleep(10);
            foreach(DataRow r in tb.Rows)
            {
                list.Add(r[2].ToString());
            }
            return list;
        }

        protected override void Init()
        {
            LISASTM = new ASTM(connect.connect);
        }

        public override string MakeResult()
        {
            string sampleid, sampleda, sampletype, patientno, patientna, sex, birthday, createtime, orderda;
            int ordered;
            DateTime dt_createtime, dt_orderda;
            string sql = "select sampleid,sampleda,sampletype,patientno,patientna,sex,birthday,createtime,orderda,ordered,dispatched from samplemain where ordered = 1 and dispatched = 0 limit 100";
            dt_instrument_order = Database.DatabaseMYSQL.GetDataTable(sql, Instrumentname);
            foreach (DataRow e in dt_instrument_order.Rows)
            {

                sampleid = e[0].ToString();
                sampleda = Convert.ToDateTime(e[1].ToString()).ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                
                sampletype = e[2].ToString();
                patientno = e[3].ToString();
                patientna = e[4].ToString();
                sex = e[5].ToString();
                birthday = e[6].ToString();
                createtime = Convert.ToDateTime(e[7].ToString()).ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                
                ordered = Convert.ToInt16(e[9]);


                dt_createtime = Convert.ToDateTime(createtime);
                orderda = e[8].ToString();
                if (orderda == null || orderda == "")
                {
                    dt_orderda = DateTime.Now;
                }
                else
                {
                    dt_orderda = Convert.ToDateTime(orderda);
                }

                if (DateTime.Now.Subtract(dt_createtime).TotalSeconds < 100)
                {
                    continue;
                }


                if (orderda == null || orderda == "") continue;
                DateTime now = DateTime.Now;
                double i = now.Subtract(dt_orderda).TotalSeconds;
                if (now.Subtract(dt_orderda).TotalSeconds > GlobalValue.InstrumentRequestInterval)
                {
                    InserInstrument(sampleid, sampleda);
                    sql = "update samplemain set dispatched = 1 where  sampleid = '" + sampleid + "' and sampleda = '" + sampleda + "'";
                    Database.DatabaseMYSQL.ExecuteSQL(sql);
                    //return "";
                }

            }
            return "";
        }
    }
}
