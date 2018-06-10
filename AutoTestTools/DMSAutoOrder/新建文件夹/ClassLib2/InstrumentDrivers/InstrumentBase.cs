using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using System.Data.SQLite;

namespace AutoTestTools.ClassLib.InstrumentDrivers
{
    class InstrumentBase:IFInstrumentDriver
    {
        protected string Instrumentname, InstrumentType;
        protected string ConnectType, IPaddress;
        protected int Portno;
        protected TCPConnect connect;
        public Thread tprocess;
        protected Boolean Threadstart;
        protected DataTable dt_instrument_order;
        public string CodingSystemName;
        public string log = "";

        public int InitInstrument(string InstrumentName, string ConnectType, string IPaddress, string Portno, string InstrumentType)
        {
            StopThread();
            if (connect!=null)
            {
                connect.CloseConnect();
            }
            
            
            Thread.Sleep(10);

            this.InstrumentType = InstrumentType;
            this.ConnectType = ConnectType;
            this.IPaddress = IPaddress;
            this.Portno = Convert.ToInt32(Portno);

            Instrumentname = InstrumentName;
            connect = new TCPConnect();
            connect.ConnectType = ConnectType;
            connect.HostIpaddress = IPaddress;
            connect.PortNo = Portno;
            connect.TCPConnectInit();
            Init();
            StartThread();

            return 0;
        }
        public Thread GetThread()
        {
            return tprocess;
        }
        public TCPConnect GetConnect()
        {
            return connect;
        }
        public string GetInstrumentName()
        {
            return Instrumentname;
        }
        public string GetInstrumentType()
        {
            return InstrumentType;
        }

        protected void ProcessThread()
        {
            Random r = new Random();
            while (Threadstart)
            {
                LoadDataTable();
                if (dt_instrument_order.Rows.Count > 0)
                {
                    MakeRequest();
                    Thread.Sleep(50);
                    MakeResult();
                    if (connect.ConnectType == "C"&& null!= connect.client)
                    {
                        log += connect.client.LOGTEXT;
                    }
                    if (connect.ConnectType == "S" && null != connect.Server)
                    {
                        log += connect.Server.LOGTEXT;
                    }
                    if (log.Length > 40000)
                    {
                        log = log.Substring(20000);
                    }
                    
                }
                dt_instrument_order.Dispose();
                
                
                Thread.Sleep(r.Next(100,500));
            }

        }
        public void StopThread()
        {
            Threadstart = false;
            int cycle = 0;
            while (null != tprocess&& tprocess.IsAlive&& cycle < 10)
            {
                Thread.Sleep(10);
                cycle++;


            }
            if (connect!= null) connect.CloseConnect();
        }
        public void StartThread()
        {
            Threadstart = true;
            tprocess = new Thread(ProcessThread);
            tprocess.IsBackground = true;
            tprocess.Start();
        }

        public virtual string MakeRequest()
        {
            return "";
        }
        public virtual string MakeResult()
        {
            return "";
        }

        protected virtual void LoadDataTable()
        {
            string sql = "select sampleid,sampleda,createtime,isrequested,orderstring from " + Instrumentname + "_order ";
            dt_instrument_order = Database.DatabaseMYSQL.GetDataTable(sql, Instrumentname + "_order");
            
        }

        protected virtual void Init()
        {

        }


        public virtual string Request(string SampleID)
        {
            return "";
        }
        public virtual string Result(string SampleID, DataGridView DGV_result)
        {
            return "";
        }


       
    }
}
