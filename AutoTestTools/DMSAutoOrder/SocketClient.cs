using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace DMSAutoOrder
{
    class SocketClient :Connect
    {
        private string SendMessage = "",RecieveMessage = "";
        public TcpClient client = new TcpClient();
        private IPEndPoint ipendpoint;
        private Boolean ConnectStatue = false,CloesConnect = false;
        private Thread clientthread;
        private NetworkStream TcpStream;
        private Int32 TotalNum = 0;
        private string ConnectInfo,Ipadressport,log,LogFile,LogFolder;
        


        public SocketClient(string IpAddress,Int32 PortNo)
        {
            Ipadressport = IpAddress + ":" + PortNo;
            this.ipendpoint = new IPEndPoint(IPAddress.Parse(IpAddress),PortNo);
            this.clientthread = new Thread(new ThreadStart(ClientThread));
            this.clientthread.IsBackground = true;

            this.LogFolder = GlobalValue.LogPath + "\\CPORT" + PortNo + "\\";
            GlobalValue.CreateFolder(this.LogFolder);
            this.LogFile = LogFolder + DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".TXT";

            


            clientthread.Start();
            ;
        }

        public void ResetClient()
        {
            client.Close();
        }
        public string GetConnectInfo()
        {
            return ConnectInfo;
        }
        public string connectinfo
        {
            get
            {
                return this.ConnectInfo;
            }
        }
        public string LOGTEXT
        {
            get
            {
                string temp = this.log;
                this.log = "";
                return temp;

            }
        }
        public Int32 totalnum
        {
            get
            {
                return this.TotalNum;
            }
        }
        public Boolean Canwrite
        {
            get
            {
                try
                {
                    return TcpStream.CanWrite;
                }
                catch(Exception e)
                {
                    Console.WriteLine("SocketClient" + e.Message);
                    return false;
                }
            }
        }

        //数据自动接收线程
        private void ClientThread()
        {
            
            while (!this.CloesConnect)
            {
                //自动检测是否连接，如果没有连接则自动重连
                //设置接收发送超时


                try
                {
                    while (!this.client.Connected && !this.CloesConnect)
                    {
                        try
                        {
                            ConnectInfo = "Try to Connect to " + this.Ipadressport;
                            this.client = new TcpClient();
                            client.ReceiveTimeout = 5;
                            client.SendTimeout = 5;
                            this.client.Connect(this.ipendpoint);

                        }
                        catch
                        {
                            //this.ConnectStatue = this.client.Connected;
                            Thread.Sleep(20);
                            if (this.client == null) break;
                            continue;
                        }
                        this.ConnectStatue = this.client.Connected;

                        if (this.ConnectStatue == true)
                        {
                            this.TcpStream = this.client.GetStream();
                            ConnectInfo = "Connect to" + this.client.Client.LocalEndPoint.ToString();
                        }
                    }
                }
                catch
                {
                    ConnectInfo = "Connect Close!";
                }
                TcpWork();
                this.client.Close();

            }
            this.client.Close();
            ConnectInfo = "Connect Close!";
        }
        //自动读取数据
        private void TcpWork()
        {
            Int32 bytereads = 0;
            if (!this.ConnectStatue) return ;
            byte[] MessageByte = new byte[4096];
            
            while (!this.CloesConnect)
            {
                //读过程
                Boolean isonline = IsOnline();
                if (isonline && this.TcpStream.CanRead && this.TcpStream.DataAvailable)
                {
                    bytereads = 0;
                    try
                    {

                        //blocks until a client sends a message

                        bytereads = this.TcpStream.Read(MessageByte, 0, 4096);
                    }
                    catch
                    {
                        if (isonline)
                        {
                            continue;
                        }
                        //读取失败后重新检测是否连接
                        this.TcpStream.Close();
                        return;
                    }
                    //message has successfully been received
                    ASCIIEncoding encoder = new ASCIIEncoding();

                    // Convert the Bytes received to a string and display it on the Server Screen
                    string msg = encoder.GetString(MessageByte, 0, bytereads);
                    RecieveMessage += msg;
                    this.TotalNum += bytereads;


                    if (this.SendMessage != "")
                    {
                        TcpSend(this.SendMessage);
                        this.SendMessage = "";
                    }
                }


                if (IsOnline() == false)
                {
                    this.TcpStream.Close();
                    return ;
                }
                if (this.CloesConnect == true)
                {
                    this.TcpStream.Close();
                    return ;
                }
                
                
                
                Thread.Sleep(10);

            }
            
            return ;
        }
        public string TcpRead()
        {
            string Stemp = this.RecieveMessage;
            this.RecieveMessage = "";
            return Stemp;
        }
        //发送成功返回0，否则返回-1
        public int TcpSend(string message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            if (message != null && message != "")
            {
                byte[] sendmessage = encoder.GetBytes(message);

                try
                {
                    this.TcpStream.Write(sendmessage, 0, sendmessage.Length);
                    this.TcpStream.Flush();

                }
                catch
                {

                    return -1;
                }
            }
            return 0;
        }

        public string StrSend
        {
            set
            {
                SendMessage = value;
            }
        }
        public string StrRead
        {
            get
            {
                string Strtemp = RecieveMessage;
                RecieveMessage = "";
                return Strtemp;
            }
        }
        public Boolean BConnectStatue
        {
            get
            {
                return this.client.Connected;
            }
        }
        private Boolean IsOnline()
        {
            Boolean isonline;
            try
            {
                isonline = !((this.client.Client.Poll(1000, SelectMode.SelectRead) && (this.client.Client.Available == 0)) || !this.client.Client.Connected);
            }
            catch
            {
                isonline = false;
            }
            Thread.Sleep(1);
            return isonline;
        }
        public void TCPClose()
        {
            
            this.CloesConnect = true;
            Thread.Sleep(10);
            try
            {
                this.TcpStream.Close();
                this.client.Close();
            }
            catch
            {
                return;
            }
        }
        public void LOG( string msg,string type)
        {
            msg = DateTime.Today.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " " + type + " : " + msg;
            GlobalValue.WriteLog(msg, LogFile);
            this.log += GlobalValue.ReplaceASCII(msg) + GlobalValue.CR + GlobalValue.LF;
            
        }


        


    }
}
