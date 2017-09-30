using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace AutoTestTools.ClassLib.InstrumentDrivers
{
    class CentaurXP: InstrumentBase
    {
        private string FS = GlobalValue.FS, GS = GlobalValue.GS;
        public ASTM LISASTM;
        private string order;

        protected override void LoadDataTable()
        {
            //string sql = "select `seq`,`sampleid`,`sampleda`,`createtime`,`isrequested`,`isresulted`,`orderstring` from " + Instrumentname + "_order where `isrequested` = 0 or `isresulted` = 0 limit 1";
            //string sql = "select `sampleid`,`sampleda`,`createtime`,`isrequested`,`isresulted`,`orderstring` from " + Instrumentname + "_order where `isrequested` = 0  limit 5";
            string sql = "select settingname,settingvalue from setting ";
            dt_instrument_order = Database.DatabaseMYSQL.GetDataTable(sql, Instrumentname);

        }


        public override string MakeRequest()
        {
            string sampleid;
            string isrequest;
            string sql = "select `sampleid`,`sampleda`,`createtime`,`isrequested`,`isresulted`,`orderstring` from " + Instrumentname + "_order where `isrequested` = 0 limit 5";
            LISASTM.LOG("GetRecord Start", "DataTable");
            Thread.Sleep(5);
            dt_instrument_order = Database.DatabaseMYSQL.GetDataTable(sql, Instrumentname);
            Thread.Sleep(5);
            LISASTM.LOG("GetRecord End ,Get Rows " + dt_instrument_order.Rows.Count, "DataTable");
            if (dt_instrument_order.Columns.Count < 6)
            {
                LISASTM.LOG("GetRecord Error ,Get Rows " + dt_instrument_order.Rows.Count, "DataTable");
                return"";
            }
            foreach (DataRow r in dt_instrument_order.Rows)
            {
                
                if (Convert.ToInt16(r[3]) != 0 ) continue ;
                if (LISASTM.sendbuffer.Length > 0) return "";
                sampleid = r[0].ToString();
                LISASTM.sendbuffer = FS+  Request(sampleid) + GS;
                int cycle = 0;
                while (!LISASTM.SendComplete() && cycle < 100)
                {
                    Thread.Sleep(5);
                    cycle++;
                }
                if (LISASTM.SendComplete())
                {

                    sql = "update " + Instrumentname + "_order set isrequested = 1 where sampleid = '" + sampleid + "'";
                    Database.DatabaseMYSQL.ExecuteSQL(sql);
                    Thread.Sleep(10);

                    return "";
                }
                else
                {
                    GlobalValue.WriteLog("Sample ID " + sampleid  + " " + Instrumentname + " Request Send Error", "SendError.log");
                }
            }
            return "";
        }

        public override string MakeResult()
        {
            GetOrder();

            return "";
            //return base.MakeResult();
        }

        private void GetOrder()
        {
            order += LISASTM.receivebuffer;
            string SampleString;
            int startpos, endpos;
            startpos = order.IndexOf(FS);
            endpos = order.IndexOf(GS);
            Thread.Sleep(1);
            while (startpos < 0 || endpos < 0)
            {
                order += LISASTM.receivebuffer;
                Thread.Sleep(1);
                startpos = order.IndexOf(FS);
                endpos = order.IndexOf(GS);
                while (order.Length > 0 && order.Substring(0, 1) == GS)
                {
                    order = order.Substring(1);
                    Thread.Sleep(1);
                }
                startpos = order.IndexOf(FS);
                endpos = order.IndexOf(GS);
                

                
                Thread.Sleep(1);
                if (order!= "")
                {
                    LISASTM.TcpConnect.LOG(order + ":" + startpos + ":" + endpos, "message1");
                }
               if (startpos < 0 && endpos < 0 && order.Length < 10)
                {
                    order = "";
                    return;
                }
                if (startpos < 0 && order.Length > 10) order = FS + order;
                if (endpos < 0 && order.Length > 10) order = order + GS;



                
            }
            while (order.Length > 0 && order.Substring(0,1) == GS)
            {
                order = order.Substring(1);
            }
            startpos = order.IndexOf(FS);
            endpos = order.IndexOf(GS);
            

            LISASTM.TcpConnect.LOG(order + ":" + startpos + ":" + endpos, "message2");
            if (startpos >= endpos && endpos >= 0 && order.Length > endpos + 1)
            {
                LISASTM.TcpConnect.LOG(order + ":" + startpos + ":" + endpos, "message3");
                order = order.Substring(endpos + 1);
                startpos = order.IndexOf(FS);
                endpos = order.IndexOf(GS);
                LISASTM.TcpConnect.LOG(order + ":" + startpos + ":" + endpos, "message4");
            }

            
            

            while (startpos >= 0 && endpos > 0)
            {
                string[] ASTMString;
                string Sampleno = "";

                int interpos;
                string tmp2;
                interpos = order.IndexOf(FS, startpos + 1);

                if (interpos > 0)
                {
                    tmp2 = order.Substring(0, interpos - 1) + GS + order.Substring(interpos);
                    order = tmp2;

                    Thread.Sleep(1);
                    startpos = order.IndexOf(FS);
                    endpos = order.IndexOf(GS);
                    LISASTM.TcpConnect.LOG(order + ":" + startpos + ":" + endpos, "message5");
                }

                while (order.IndexOf(FS + FS) >= 0)
                {
                    order = order.Replace(FS + FS, FS);
                }
                while (order.IndexOf(GS + GS) >= 0)
                {
                    order = order.Replace(GS + GS, GS);
                }
                startpos = order.IndexOf(FS);
                endpos = order.IndexOf(GS);
                if (startpos > endpos)
                {
                    if (endpos > 2)
                    {


                        SampleString = order.Substring(0, endpos - 1);
                        order = order.Substring(endpos + 1);
                    }
                    else
                    {
                        return;
                    }
                    
                }
                else
                {
                    startpos = order.IndexOf(FS);
                    endpos = order.IndexOf(GS);
                    
                    if (endpos > 0 && startpos < 0)
                    {
                        order = FS + order;
                        
                    }
                    else if(endpos == 0)
                    {
                        order = order.Substring(1);
                    }

                    if (order.Length == 0) return;
                    startpos = order.IndexOf(FS);
                    endpos = order.IndexOf(GS);
                    
                    try
                    {
                        SampleString = order.Substring(startpos + 1, endpos - 1);
                        order = order.Substring(endpos + 1);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("CentaurXP" + e.Message);
                        SampleString = "";
                        if (order == "") return ;
                    }
                    
                    
                   
                    
                }
                //舍弃未接收完的数据
                while (SampleString.IndexOf(FS) > 0)
                {
                    SampleString = SampleString.Substring(SampleString.IndexOf(GS) + 1);
                }
                SampleString = SampleString.Replace("][", FS);
                if (SampleString.IndexOf("[") >= 0 && SampleString.IndexOf("]") >= 0)
                {
                    SampleString = SampleString.Substring(1, SampleString.Length - 2);
                }
                if (SampleString.IndexOf(FS)> 0)
                { 
                    ASTMString = SampleString.Split(Convert.ToChar(FS));
                }
                else
                {
                    ASTMString = SampleString.Split(Convert.ToChar(GlobalValue.CR));
                }
                LISASTM.TcpConnect.LOG(SampleString, "message5");
                foreach (string S in ASTMString)
                {
                    if (S == "" || S == null) continue;
                    string tmp = S;//.Substring(1);
                    string flag = tmp.Substring(0, 1);
                    string[] splits;
                    string ordertest;
                    splits = tmp.Split(Convert.ToChar("|"));
                    if (flag == "O")
                    {
                        Sampleno = splits[2];
                        SampleString = SampleString.Replace(Convert.ToChar( "'"), ' ');
                        string sql = "update  " + Instrumentname + "_order set orderstring = '" + SampleString + "' where sampleid = '" + Sampleno + "'";
                        Database.DatabaseMYSQL.ExecuteSQL(sql);
                        ordertest = splits[4];

                        

                        string[] tests = ordertest.Split(Convert.ToChar("^"));
                        List<string> testlist = new List<string>();
                        foreach (string s in tests)
                        {
                            if (s!= null && s!= "")
                            {
                                testlist.Add(s.Replace("\\", " ").Trim());
                            }
                            
                        }
                        string sendtext;
                        sendtext = FS + result(Sampleno, testlist) + GS; ;
                        int i = 0;
                        while (LISASTM.sendbuffer != "" && i > 10)
                        {
                            Thread.Sleep(100);
                            i++;
                        }
                        LISASTM.sendbuffer = sendtext;
                        int cycle = 0;
                        while (!LISASTM.SendComplete() && cycle < 100)
                        {
                            Thread.Sleep(5);
                            cycle++;
                        }
                        if (LISASTM.SendComplete())
                        {
                            sql = "update  " + Instrumentname + "_order set isresulted = '1' where sampleid = '" + Sampleno + "'";
                            Database.DatabaseMYSQL.ExecuteSQL(sql);
                        }
                        else
                        {
                            GlobalValue.WriteLog("Sample ID " + Sampleno + " " + Instrumentname  + " result Send Error", "SendError.log");
                        }


                        Thread.Sleep(10);
                        continue;

                        //string[] tmps = splits[2].Split(Convert.ToChar("^"));
                        //string item = tmps[3], result = splits[3];
                        //string flags = splits[6];
                        //int index = DGV_Result.Rows.Add();
                        //DGV_Result.Rows[index].Cells[0].Value = item;
                        //DGV_Result.Rows[index].Cells[1].Value = result;
                        //DGV_Result.Rows[index].Cells[2].Value = flags;



                    }

                    //else if (flag == "O")
                    //{


                    //}
                }
                startpos = order.IndexOf(FS);
                endpos = order.IndexOf(GS);

            }
        }


        private string result(string sampleid,List<string> list)
        {
            string datetime = DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string Send = "", Oitem = "",temp, resultstring = "";
            Int32 seeds = 0;
            foreach(string s in list)
            {
                temp = s;
                if (temp.Length > 8) temp = temp.Substring(0, 8);
                Oitem += "\\^^^" + temp;
                resultstring += "[R|1|^^^" + temp + "^^^^DOSE|" + MakeRandomValue(seeds + DateTime.Now.Millisecond + DateTime.Now.Second) + "|ng/mL||||F\\Q\\R||||" + datetime + GlobalValue.CR + "]";
                seeds++;
            }
            if (Oitem == "") return "";
            Oitem = Oitem.Substring(1);
            Send = "[H|\\^&|||ADVCNT_LIS|||||LIS_ID||P|1" + GlobalValue.CR + "]";
            Send += "[P|12|A123-45-6789|||Jones^Mary^A||19540601|F|||||12B||||||||||||ER1" + GlobalValue.CR + "]";
            Send += "[O|1|" + sampleid + "||" + Oitem + "|R||||||N||||||||||||||O" + GlobalValue.CR + "]";
            Send += resultstring;
            Send += "[L|1|F" + GlobalValue.CR + "]";
            return Send;
        }

        protected override void Init()
        {
            LISASTM = new ASTM(connect.connect);
        }


        private string MakeRandomValue(int seed)
        {
            Random r = new Random(seed);
            
            string  values;
            
            values = (r.Next(0, 20) + r.NextDouble()).ToString();
            values = values.Substring(0, values.IndexOf(".") + 2);
            

            return values;
        }




        public string Request(string SampleID)
        {
            if (SampleID != "" && SampleID != null)
            {
                string Send = "";
                string datetime = DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                Send = "[H|\\^&|||ADVCNT_LIS|||||LIS_ID||P|1" + GlobalValue.CR + "]";
                Send += "[Q|1|^" + SampleID + "|^" + SampleID + "|ALL||||||||O" + GlobalValue.CR + "]";
                Send += "[L|1|F" + GlobalValue.CR + "]";
                return Send;
            }
            return "";
        }
        public string Result(string SampleID, DataGridView DGV_result)
        {
            int gridcount = DGV_result.Rows.Count, testnum = 0;
            string item, result, flag, resultstring = "", Send = "",Oitem = "";
            string datetime = DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            for (int i = 0; i < gridcount; i++)
            {
                if (DGV_result.Rows[i] != null)
                {
                    item = (string)DGV_result.Rows[i].Cells[0].Value;
                    result = (string)DGV_result.Rows[i].Cells[1].Value;
                    flag = (string)DGV_result.Rows[i].Cells[2].Value;
                    if (item != null && item != "" && result != null)
                    {
                        if (item.Length > 8) item = item.Substring(0, 8);
                        Oitem += "\\^^^" + item;
                        
                        if (result.Length > 15) result = result.Substring(0, 15);
                        if (flag.Length > 1) flag = flag.Substring(0, 1);

                        resultstring += "[R|1|^^^" + item + "^^^^DOSE|" + result + "|ng/mL||" + flag + "||F\\Q\\R||||" + datetime + GlobalValue.CR + "]";
                    }
                }
            }
            if (Oitem == "") return "";
            Oitem = Oitem.Substring(1);
            Send = "[H|\\^&|||ADVCNT_LIS|||||LIS_ID||P|1" + GlobalValue.CR + "]";
            Send += "[P|12|A123-45-6789|||Jones^Mary^A||19540601|F|||||12B||||||||||||ER1" + GlobalValue.CR + "]";
            Send += "[O|1|" + SampleID + "||" + Oitem + "|R||||||N||||||||||||||O" + GlobalValue.CR + "]";
            Send += resultstring;
            Send += "[L|1|F" + GlobalValue.CR + "]";
            return Send;
            
        }

    }
}
