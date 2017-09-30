using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AutoTestTools.ClassLib
{
    class TCPConnect
    {
        string Connecttype, Hostipaddress, Portno;
        Boolean Connectstatue,Stop;
        public SocketClient client;
        public SocketServer Server;
        private Thread statuethread;
        public Connect connect;
        public String ConnectType
        {
            get
            {
                return Connecttype;
            }
            set
            {
                Connecttype = value;
            }
        }
        public Boolean ConnectStatue
        {
            get
            {
                return Connectstatue;
            }
        }
        
        public string HostIpaddress
        {
            get
            {
                return Hostipaddress;
            }
            set
            {
                Hostipaddress = value;
            }
        }
        
        public string PortNo
        {
            get
            {
                return Portno;
            }
            set
            {
                Portno = value;
            }
        }

        public int TCPConnectInit()
        {
            this.Stop = false;
            if (this.Connecttype == "S")
            {
                this.Server = new SocketServer();
                if (this.Server.initport(Convert.ToInt32(this.Portno)))
                {
                    this.Connectstatue = true;
                }
                else
                {
                    this.Connectstatue = false;
                }
                connect = this.Server;

            }
            else
            {
                this.client = new SocketClient(this.Hostipaddress, Convert.ToInt32(this.Portno));
                connect = this.client;
            }
            this.statuethread = new Thread(new ThreadStart(ConnectStatueRefreshThread));
            this.statuethread.IsBackground = true;
            statuethread.Start();
            
            return 0;
        }
        public void CloseConnect()
        {
            this.Stop = true;
            if (this.Connecttype == "S")
            {
                
                this.Server.TCPClose();
                this.Connectstatue = false;
            }
            else
            {
                this.client.TCPClose();
                this.Connectstatue = false;
            }
        }
        public void ConnectStatueRefreshThread()
        {
            while(!Stop)
            {
                if (this.Connecttype == "S")
                {
                    this.Connectstatue = this.Server.tcpconnect;
                }
                else
                {
                    this.Connectstatue = this.client.BConnectStatue;
                }
                Thread.Sleep(10);
            }
        }

    }
}
