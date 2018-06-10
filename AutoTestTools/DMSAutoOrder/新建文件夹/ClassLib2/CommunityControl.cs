using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;



namespace AutoTestTools.ClassLib
{
    class CommunityControl
    {

        public List<ClassLib.IFInstrumentDriver> InstrumentList = new List<ClassLib.IFInstrumentDriver>();
        Thread tcc,tmakesample;
        public Boolean Start,Startsample;
        InstrumentPar ip;
        public void StartThread()
        {
            
            tcc = new Thread(Process);
            tcc.IsBackground = true;
            Start = true;
            tcc.Start();
            
        }
        public void StartThreadSample()
        {
            
                
                Startsample = true;
                tmakesample = new Thread(MakeSample);
                tmakesample.IsBackground = true;
                tmakesample.Start();
            
        }
        public void monitorthread()
        {
            try
            {
                while (Startsample)
                {
                    if (!tcc.IsAlive)
                    {
                        StartThread();
                    }
                    if (!tmakesample.IsAlive)
                    {
                        StartThreadSample();
                    }
                    Thread.Sleep(20000);
                }
            }
            catch (Exception e)
            {
                GlobalValue.WriteLog("cc" + e.Message, "Systemerror.log");
            }
        }
        public void StopThreadSample()
        {
            Startsample = false;
            
        }
        public void StopThread()
        {
            Startsample = false;
            Start = false;
            foreach(IFInstrumentDriver instrument in ip.InstrumentDriver)
            {
                instrument.StopThread();
            }
        }
        private void Process()
        {
            
            DataTable dt_instrumentlist;
            ip.Instrument = new List<string>();
            ip.ConnectPar = new List<string>();
            ip.InstrumentDriver = new List<IFInstrumentDriver>();
            string sql = "select instrumentna,codingsystem,class,instrumenttype,connecttype,hostip,portno,instrumentenable from instrumentlist";


            while (Start)
            {


                dt_instrumentlist = Database.DatabaseMYSQL.GetDataTable(sql, "instrumentlist");

                for (int i = 0; i < ip.InstrumentDriver.Count; i++)
                {
                    if (ip.InstrumentDriver[i].GetThread().IsAlive == false)
                    {
                        ip.InstrumentDriver[i].StopThread();
                        Thread.Sleep(5000);
                        ip.InstrumentDriver[i].StartThread();
                    }
                    //TCPConnect c = ip.InstrumentDriver[i].GetConnect();
                    //string instrumentna, instrumenttype, connecttype, hostip, portno;
                    //instrumentna = ip.InstrumentDriver[i].GetInstrumentName();
                    //connecttype = c.ConnectType;
                    //hostip = c.HostIpaddress;
                    //portno = c.PortNo;
                    //instrumenttype = ip.InstrumentDriver[i].GetInstrumentType();
                    //ip.InstrumentDriver[i].InitInstrument(instrumentna, connecttype, hostip, portno, instrumenttype);
                    //if (c.ConnectType == "C")
                    //{
                    //    if (c.client.Canwrite == false)
                    //    {
                    //        ip.InstrumentDriver[i].InitInstrument(instrumentna, connecttype, hostip, portno, instrumenttype);
                    //    }
                    //}
                    //else
                    //{
                    //    if (c.Server.Canwrite == false)
                    //    {
                    //        ip.InstrumentDriver[i].InitInstrument(instrumentna, connecttype, hostip, portno, instrumenttype);
                    //    }
                    //}


                }

                foreach (DataRow r in dt_instrumentlist.Rows)
                    {
                        string instrumentna, codingsystem, driver, instrumenttype, connecttype, hostip, portno, instrumentenable;
                        instrumentna = r[0].ToString();
                        codingsystem = r[1].ToString();
                        driver = r[2].ToString();
                        instrumenttype = r[3].ToString();
                        connecttype = r[4].ToString();
                        hostip = r[5].ToString();
                        portno = r[6].ToString();
                        instrumentenable = r[7].ToString();
                        if (instrumentenable == "0" || ip.Instrument.Contains(instrumentna) || ip.ConnectPar.Contains(connecttype + ";" + hostip + ";" + portno) || instrumenttype == "LIS")
                        {
                            continue;
                        }
                        else
                        {
                            ip.Instrument.Add(instrumentna);
                            ip.ConnectPar.Add(connecttype + ";" + hostip + ";" + portno);
                            IFInstrumentDriver instrumentdriver;
                            if (driver == "asts" && instrumenttype == "LIS_ORDER")
                            {
                                instrumentdriver = new InstrumentDrivers.LIS_ORDER();
                            }
                            else if (driver == "asts" && instrumenttype == "LIS_RESULT")
                            {
                                instrumentdriver = new InstrumentDrivers.LIS_RESULT();
                            }
                            else if (driver == "advc")
                            {
                                instrumentdriver = new InstrumentDrivers.Advia2400();
                            }
                            else
                            {
                                instrumentdriver = new InstrumentDrivers.CentaurXP();
                            }
                            instrumentdriver.InitInstrument(instrumentna, connecttype, hostip, portno, instrumenttype);
                            ip.InstrumentDriver.Add(instrumentdriver);
                        }

                    }


                    Thread.Sleep(60000);
                }
            }
        
        struct InstrumentPar
        {
            public List<string> Instrument;
            public List<string> ConnectPar;
            public List<IFInstrumentDriver> InstrumentDriver;
        }

        public void MakeSample()
        {
            Random r = new Random();
            string sampleda;
            try
            {
                while (Startsample)
                {
                    if (Database.DatabaseMYSQL.ConnectDb() == null) continue;
                    if (GlobalValue.CurrentSampleno == GlobalValue.EndSampleno || GlobalValue.testlist.Count == 0)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    if (GlobalValue.StartSampleno != "" && GlobalValue.EndSampleno != "")
                    {
                        if (GlobalValue.CurrentSampleno == "" || GlobalValue.CurrentSampleno == null)
                        {
                            GlobalValue.CurrentSampleno = GlobalValue.StartSampleno;
                        }
                        else
                        {
                            GlobalValue.CurrentSampleno = SamplenoAdd(GlobalValue.CurrentSampleno);
                        }
                        sampleda = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                        string sql = @"insert into samplemain(sampleid,sampleda,sampletype,patientno,patientna,sex,birthday,createtime,ordered,dispatched) values(
                            '" + GlobalValue.CurrentSampleno + "','" + sampleda +
                                "','Serum','" + r.Next(0, 999999) + "','HuangLiZhu','" + Makesex() + "','"
                                + r.Next(1930, 2017).ToString() + "-" + Right("0" + r.Next(1, 12).ToString(), 2) + "-" + Right("0" + r.Next(1, 28).ToString(), 2) + " 00:00:00" + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) + "',0,0)";
                        //System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(sql);
                        Database.DatabaseMYSQL.ExecuteSQL(sql);
                        //Thread.Sleep(1);
                        foreach (string s in GlobalValue.testlist)
                        {

                            sql = "insert into sampledetail(sampleid,sampleda,tests) values('" + GlobalValue.CurrentSampleno + "','" + sampleda + "','" + s + "')";
                            Database.DatabaseMYSQL.ExecuteSQL(sql);
                            //Thread.Sleep(1);
                        }

                    }
                    GlobalValue.SaveSetting("CurrentSampleno", GlobalValue.CurrentSampleno);
                    Thread.Sleep(1);
                    //if (GlobalValue.CurrentSampleno == GlobalValue.EndSampleno && !tcc.IsAlive)
                    //{
                    //    StartThread();
                    //}
                }
            }
            catch (Exception e)
            {
                GlobalValue.WriteLog("cc" + e.Message, "Systemerror.log");
            }
        }

        
        private string Makesex()
        {
            Random r = new Random(DateTime.Now.Millisecond + DateTime.Now.Second);
            int sexint = r.Next();
            if (sexint%2 == 0)
            {
                return "M";
            }
            else
            {
                return "F";
            }
        }

        private string Right(string text,int rightlength)
        {
            return text.Substring(text.Length - rightlength);
        }
        private string SamplenoAdd(string sampleno)
        {
            Boolean isnumber = false;
            string tmp = sampleno,prestring = "";
            Int32 isampleno = 0;
            while (!isnumber)
            {
                try
                {
                    isampleno = Convert.ToInt32(tmp);
                    isnumber = true;
                }
                catch
                {
                    prestring += tmp.Substring(0, 1);
                    tmp = tmp.Substring(1);
                }

            }
            isampleno++;
            return prestring + isampleno.ToString();
        }
    }

    
}
