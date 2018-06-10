using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMSAutoOrder
{
    
    public class DMSConnector
    {
        public string IPaddr, PortNo;
        public Boolean ConnectMode;
        Connect connect;
        ASTM astm;
        SocketClient client;
        SocketServer server;

        public void MakeASTMConnect()
        {
            if (ConnectMode)
            {
                server = new SocketServer();
                server.initport(Convert.ToInt32(PortNo));
                connect = server;
            }
            else
            {
                client = new SocketClient(IPaddr, Convert.ToInt32(PortNo));
                connect = client;
            }
            astm = new ASTM(connect);
        }
        public string SendBuffer
        {
            set
            {
                astm.sendbuffer = value;
            }
        }
        public void StopConnector()
        {
            connect.TCPClose();
            server = null;
            client = null;
            connect = null;
        }

    }
}
