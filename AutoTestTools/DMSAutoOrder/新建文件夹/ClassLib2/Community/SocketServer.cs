using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;


namespace AutoTestTools.ClassLib
{
    class SocketServer :Connect
    {


        public NetworkStream tcpstream;
        public Boolean portinuse;
        //public int clientnum;
        private TcpListener listener1;
        private Thread listenserthread1;
        private Thread clientthread;
        public string tcpmessages;
        public Boolean tcpconnect;
        private Boolean closeport1 = false;
        //public string sendbuffer;
        public long totalnums;
        public string connectinfo = "";
        private string log,LogFile,LogFolder;
        public TcpClient tcpclient1;


        private void checkport(int port)
        {
            this.portinuse = false;
            IPGlobalProperties ipglobal = IPGlobalProperties.GetIPGlobalProperties();

            IPEndPoint[] ipEndPoints = ipglobal.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    this.portinuse = true;
                    connectinfo = "Port Used By Other App";
                    break;
                }
            }
        }

        public void ResetClient()
        {
            if (tcpclient1!= null)
            {
                tcpclient1.Client.Close();
            }
        }
        public string GetConnectInfo()
        {
            return connectinfo;
        }
        public Boolean initport(int port)
        {
            checkport(port);
            if (!portinuse)
            {
                this.listener1 = new TcpListener(IPAddress.Any, port);

                this.listenserthread1 = new Thread(new ThreadStart(openport));
                this.listenserthread1.IsBackground = true;

                this.LogFolder = GlobalValue.LogPath + "\\SPORT" + port + "\\";
                GlobalValue.CreateFolder(this.LogFolder);
                this.LogFile = LogFolder + DateTime.Today.ToString("yyyyMMdd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".TXT";
               



                listenserthread1.Start();
            }
            return true;
        }
        private void openport()
        {
            listener1.ExclusiveAddressUse = true;

            tcpclient1 = new TcpClient();
            while (!closeport1)
            {
                //打开监听

                try
                {
                    connectinfo = "Listening";
                    this.listener1.Start();
                }
                catch
                {
                    connectinfo = "Open Port Filed";
                    return;
                }
                if (closeport1 == true)
                {
                    connectinfo = "Port Closed";
                    tcpclient1.Close();
                    return;
                }
                try
                {
                    tcpconnect = tcpclient1.Connected;
                    tcpclient1 = listener1.AcceptTcpClient();

                    connectinfo = "Connect to " + tcpclient1.Client.RemoteEndPoint.ToString();
                    //connectinfo = tcpclient1.Client.RemoteEndPoint.Port.ToString();

                    this.clientthread = new Thread(new ParameterizedThreadStart(tcpread));
                    this.clientthread.IsBackground = true;
                    clientthread.Start(tcpclient1);


                    //连接后关闭监听

                    do
                    {
                        tcpconnect = tcpclient1.Connected;
                        this.listener1.Stop();
                        Thread.Sleep(1);

                    } while (tcpclient1.Connected == true);

                }
                catch
                {
                    tcpclient1.Close();
                    connectinfo = "Port Close";
                    return;
                }
                //}

            }
            connectinfo = "Port Close";

        }

        private void tcpread(object client)
        {

            TcpClient tcpclient = (TcpClient)client;
            tcpclient.Client.SendTimeout = 5;
            tcpclient.Client.ReceiveTimeout = 5;
            tcpstream = tcpclient.GetStream();


            byte[] message = new byte[4096];
            int bytesRead;

            while (!closeport1)
            {
                //读过程
                bytesRead = 0;
                Boolean isonline = IsOnline(tcpclient);
                if (isonline && tcpstream.CanRead && tcpstream.DataAvailable)
                {
                    try
                    {
                        //blocks until a client sends a message
                        bytesRead = tcpstream.Read(message, 0, 4096);
                    }
                    catch
                    {
                        //a socket error has occured

                        //return;
                    }

                    //if (bytesRead == 0)
                    //{
                    //    break;
                    //}
                    
                    //message has successfully been received
                    ASCIIEncoding encoder = new ASCIIEncoding();

                    // Convert the Bytes received to a string and display it on the Server Screen
                    string msg = encoder.GetString(message, 0, bytesRead);
                    tcpmessages += msg;
                    totalnums += bytesRead;

                }

                if (isonline == false)
                {
                    tcpstream.Close();
                    tcpclient.Close();
                    return;
                }
                if (closeport1 == true)
                {
                    tcpstream.Close();
                    tcpclient.Close();
                    return;
                }
                


            }
            tcpclient.Close();
            return;
        }

        //发送函数
        public int TcpSend(string message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            if (message != null && message != "")
            {
                byte[] sendmessage = encoder.GetBytes(message);

                try
                {
                    tcpstream.Write(sendmessage, 0, sendmessage.Length);
                    tcpstream.Flush();
                }
                catch
                {
                    return -1;
                }
            }
            return 0;


        }
        public string TcpRead()
        {
            //byte[] message = new byte[4096];
            //int bytesRead;
            //string msg = "";
            //ASCIIEncoding encoder = new ASCIIEncoding();
            //do
            //{
            //    try
            //    {
            //        bytesRead = tcpstream.Read(message, 0, 4096);
            //    }
            //    catch
            //    {
            //        return "";
            //    }
            //    msg += encoder.GetString(message, 0, bytesRead);
            //    totalnums += bytesRead;
            //} while (bytesRead != 0);
            //return msg;
            string Stemp = this.tcpmessages;
            this.tcpmessages = "";
            return Stemp;
        }

        public void TCPClose()
        {

            try
            {
                if (tcpstream != null)
                {
                    tcpstream.Close();
                }


                closeport1 = true;
                listener1.Stop();
            }
            catch
            {

            }
            connectinfo = "Port Close";
        }
        public void tcpport(int portnums)
        {
            initport(portnums);
            return;
        }


        private Boolean IsOnline(TcpClient tcpclient)
        {
            Boolean isonline;
            try
            {
                isonline = !((tcpclient.Client.Poll(1000, SelectMode.SelectRead) && (tcpclient.Client.Available == 0)) || !tcpclient.Client.Connected);
            }
            catch
            {
                isonline = false;
            }
            Thread.Sleep(1);
            return isonline;
        }
        public Boolean Canwrite
        {
            get
            {
                try
                {
                    return tcpstream.CanWrite;
                }
                catch
                {
                    return false;
                }
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
        public void LOG(string msg, string type)
        {
            msg = DateTime.Today.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " " + type + " : " + msg;
            GlobalValue.WriteLog(msg, LogFile);
            this.log += GlobalValue.ReplaceASCII(msg) + GlobalValue.CR + GlobalValue.LF;
            
        }

        
    }
}
