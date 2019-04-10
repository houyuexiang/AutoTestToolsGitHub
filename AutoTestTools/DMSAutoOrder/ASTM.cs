using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace DMSAutoOrder
{
    class ASTM
    {
        public Connect TcpConnect;
        private string SendBuffer = "",ReceiveBuffer = "";
        private int SendBufferCount = 0,Sendindex = 0;
        private string[] SendBufferArray = new string[100];
        private string Receivemsg = "";
        private Thread TransThread;
        private int timeout;
        Boolean STOP = false;
        Boolean BAddACK = false;

        string LastRec = "", LastSend = "";
        Boolean BRec = false, BSend = false;

        string ACK = GlobalValue.ACK, ENQ = GlobalValue.ENQ, EOT = GlobalValue.EOT, STX = GlobalValue.STX,
            ETX = GlobalValue.ETX, CR = GlobalValue.CR, LF = GlobalValue.LF, NAK = GlobalValue.NAK,ETB = GlobalValue.ETB,FS = GlobalValue.FS,GS = GlobalValue.GS;
        
        

        public ASTM(Connect connect)
        {
            TcpConnect = connect;
            this.STOP = false;
            this.TransThread = new Thread(new ThreadStart(ASTMThread));
            this.TransThread.IsBackground = true;
            this.TransThread.Start();
        }
        public string sendbuffer
        {
            set
            {
                SendBuffer += value;
            }
            get
            {
                return SendBuffer;
            }
        }
        public string receivebuffer
        {
            get
            {
                string temp = "";
                temp = ReceiveBuffer;
                ReceiveBuffer = "";
                return temp;
            }
        }
        public void Reset()
        {
            LastRec = "";
            LastSend = "";
            BRec = false;

            //if (Sendindex != 0)
            //{
            //TcpConnect.ResetClient();
            
                Send(ENQ);
                LastSend = ENQ;
                BSend = true;
            //}
            //else
            //{

            //    BSend = false ;
            //}
            
            Sendindex = 0;
        }
        public void Stop()
        {
            this.STOP = true;
        }
        private void ASTMThread()
        {
            while (!this.STOP || !SendComplete())
            {
                Process();
                Thread.Sleep(1);
            }

        }

        public Boolean SendComplete()
        {
            if (SendBuffer == "" && BSend == false && SendBufferCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Process()
        {
            string recievestr;
            #region process
            try
            {
                Receive();
                if (Receivemsg == "" || Receivemsg == null)
                {
                    //检测无数据传输后是否需要发送数据
                    if (BRec == false && (SendBuffer != "" || SendBufferCount > 0) && BSend == false)
                    {
                        if (Receive() == "")
                        {
                            int Startpos, Endpos;
                            string temp, tempd;
                            Send(ENQ);
                            LastSend = ENQ;
                            BSend = true;
                            if (SendBufferCount > 0)
                            {
                                LOG("SENDSTART", "Process");
                                return;
                            }

                            Sendindex = 0;

                            Startpos = SendBuffer.IndexOf(FS);
                            Endpos = SendBuffer.IndexOf(GS);
                            temp = SendBuffer.Substring(Startpos + 1, Endpos - 1);
                            SendBuffer = SendBuffer.Substring(Endpos + 1);
                            Startpos = temp.IndexOf("[");
                            Endpos = temp.IndexOf("]");

                            while (Startpos >= 0 && Endpos >= 0)
                            {
                                tempd = temp.Substring(Startpos + 1, Endpos - 1);
                                temp = temp.Substring(Endpos + 1);
                                int astmcount = 0;
                                //tempd = MakeAstm(tempd);
                                if (tempd != "")
                                {
                                    string astmtmpf = "", astmtmp = "";
                                    //添加fn
                                    astmtmp = tempd;
                                    while (astmtmp.Length > 240)
                                    {
                                        astmtmp = ((SendBufferCount + 1) % 8).ToString() + astmtmp;
                                        astmtmpf = astmtmp.Substring(0, 241);
                                        astmtmp = astmtmp.Substring(241);
                                        SendBufferArray[SendBufferCount] = MakeAstm(astmtmpf + ETB) + CR + LF;
                                        SendBufferCount++;
                                    }


                                    astmtmp = ((SendBufferCount + 1) % 8).ToString() + astmtmp;
                                    //添加计算校验
                                    SendBufferArray[SendBufferCount] = MakeAstm(astmtmp + ETX) + CR + LF;
                                    SendBufferCount++;
                                    astmcount++;

                                }
                                Startpos = temp.IndexOf("[");
                                Endpos = temp.IndexOf("]");
                            }
                            SendBufferArray[SendBufferCount] = EOT;
                            SendBufferCount++;
                            LOG("SENDSTART", "Process");
                            timeout = 0;
                        }
                    }

                    if (BSend == true)
                    {

                        Thread.Sleep(100);
                        timeout++;
                        if (timeout >= 10)
                        {
                            if (LastSend == ENQ)
                            {
                                Reset();
                            }
                            else
                            {
                                Receivemsg = ACK + Receivemsg;
                                LOG("ADD ACK", "ADDACK");
                            }

                            //Reset();
                            //Send(NAK);
                            //LastSend = NAK;
                            Thread.Sleep(10);
                            timeout = 0;

                        }
                    }
                    if (BRec == true)
                    {
                        Thread.Sleep(10);
                        timeout++;
                        if (timeout >= 10)
                        {
                            if (BAddACK)
                            {
                                BRec = false;
                                BAddACK = false;
                                return;
                            }
                            BAddACK = true;
                            Send(ACK);
                            LOG("Resend ACK", "ReSendACK");
                            LastSend = ACK;
                            Thread.Sleep(10);
                            timeout = 0;

                        }
                    }

                    return;
                }
                timeout = 0;
                recievestr = Receivemsg.Substring(0, 1);
                //Receivemsg.Substring(0, 1);

                //发送完毕
                if (LastSend == EOT)
                {
                    //LOG("SENDEND", "Process");
                    BSend = false;
                }
                //

                //开始接收
                if (recievestr == ENQ)
                {

                    if (!BSend)
                    {

                        LOG("RECSTART", "Process");
                        Send(ACK);
                        LastSend = ACK;
                        Receivemsg = Receivemsg.Substring(1);
                        LastRec = ENQ;
                        BRec = true;
                        return;
                    }
                    else
                    {
                        Send(ENQ);
                        LastSend = ENQ;
                        LOG("SENDSTART", "Process");
                        Receivemsg = Receivemsg.Substring(1);
                        //LastRec = ENQ;
                        BRec = false;
                        return;
                    }
                }
                //发送数据
                else if (recievestr == ACK)
                {
                    Receivemsg = Receivemsg.Substring(1);
                    if (Sendindex == SendBufferCount) return;
                    if (SendBufferArray[Sendindex] != "" && SendBufferArray[Sendindex] != null)
                    {
                        Send(SendBufferArray[Sendindex]);
                        LastSend = SendBufferArray[Sendindex];
                        if (SendBufferArray[Sendindex] == EOT)
                        {
                            BSend = false;
                            LOG("SENDEND", "Process");
                            LOG(SendBuffer + ":" + Sendindex + "/" + SendBufferCount, "SendBuffer");
                            BRec = false;
                        }
                        Sendindex++;
                        if (LastSend == EOT)
                        {
                            SendBufferCount = 0;
                        }
                        LastRec = ACK;

                    }
                }
                //数据接收完毕
                else if (recievestr == EOT)
                {
                    ReceiveBuffer += GS;
                    LastRec = EOT;
                    BRec = false;
                    Receivemsg = Receivemsg.Substring(1);
                    LOG("RECEND", "Process");
                    BSend = false;
                }
                //重新发送数据
                else if (recievestr == NAK)
                {
                    Send(LastSend);
                    //Send(ENQ);
                    //LastSend = ENQ;
                    if (Sendindex > 0)
                    {
                        Sendindex = Sendindex - 1;
                    }

                    LastRec = NAK;
                    BSend = true;
                    BRec = false;
                    Receivemsg = Receivemsg.Substring(1);
                }
                else
                {
                    //接收数据
                    int Startpos, Endpos;





                    //处理接收到ETB的情况
                    //*[msgETB

                    Startpos = Receivemsg.IndexOf(ETB);
                    if (Startpos >= 0)
                    {
                        Endpos = Receivemsg.IndexOf(STX, Startpos);
                        while (Startpos >= 0 && Endpos > 0 && Endpos > Startpos)
                        {
                            Receivemsg = Receivemsg.Substring(0, Startpos - 1) + Receivemsg.Substring(Endpos + 1);
                            Startpos = Receivemsg.IndexOf(ETB);
                            if (Startpos >= 0)
                            {
                                Endpos = Receivemsg.IndexOf(STX, Startpos);
                            }

                        }
                    }

                    //

                    Endpos = Receivemsg.IndexOf(ETX);
                    Startpos = Receivemsg.IndexOf(STX);

                    while (Endpos >= 0 && Startpos >= 0)
                    {

                        if (LastRec == ENQ)
                        {
                            ReceiveBuffer += FS + "[" + Receivemsg.Substring(Startpos + 1, Endpos - 2) + "]";
                        }
                        else
                        {
                            ReceiveBuffer += "[" + Receivemsg.Substring(Startpos + 1, Endpos - 2) + "]";
                        }
                        LastRec = Receivemsg.Substring(Startpos + 1, Endpos - 2);
                        Endpos = Receivemsg.IndexOf(CR + LF);
                        Receivemsg = Receivemsg.Substring(Endpos + 2);
                        Endpos = Receivemsg.IndexOf(ETX);
                        Startpos = Receivemsg.IndexOf(STX);
                    }




                    //处理接收一半的情况
                    if (Endpos < 0 && Startpos >= 0)
                    {
                        Send(ACK);
                        LastSend = ACK;
                    }
                    else
                    {
                        Send(ACK);
                        LastSend = ACK;
                    }


                }

                int pos = 0;
                //垃圾数据处理
                if (BSend == true && BRec == false)
                {

                    pos = Receivemsg.IndexOf(ACK);
                    if (pos < 0)
                    {
                        Receivemsg = "";
                    }
                    else if (pos == 0)
                    {

                    }
                    else
                    {
                        Receivemsg.Substring(pos);
                    }
                }
                else if (BRec == true && BSend == false)
                {
                    if (Receivemsg.IndexOf(STX) < 0 && Receivemsg.IndexOf(ETX) < 0 && Receivemsg.IndexOf(LF) < 0 && Receivemsg.IndexOf(CR) < 0 && Receivemsg.IndexOf(EOT) < 0 && Receivemsg.IndexOf(ACK) < 0)
                    {
                        Receivemsg = "";
                    }
                    //if (Receivemsg.IndexOf(EOT) >= 0&& Receivemsg.IndexOf(LF) < 0&& Receivemsg.IndexOf(CR) < 0)
                    //{

                    //}
                }
                else
                {
                    pos = Receivemsg.IndexOf(ENQ);
                    if (pos < 0)
                    {
                        Receivemsg = "";
                        return;
                    }
                }
            }
            catch(Exception e)
            {
                GlobalValue.WriteLog(e.Message + "\r\n", "SystemError.log");
            }
                #endregion
            
        }
        private void Send(string msg)
        {
            try
            {
                //等待直到退出或可发送
                while (!TcpConnect.Canwrite)
                {
                    if (STOP) return;
                    Thread.Sleep(10);
                }
                TcpConnect.TcpSend(msg);
                LOG(msg, "S");
            }
            catch (Exception e)
            {
                GlobalValue.WriteLog(e.Message + "\r\n", "SystemError.log");
            }
            

        }
        private string Receive()
        {
            try
            {
                string tmp = "";
                tmp = TcpConnect.TcpRead();
                if (tmp == "" || tmp == null)
                {
                    return "";
                }
                Receivemsg += tmp;
                LOG(tmp, "R");
                //LastRec = tmp;

                return tmp;
            }
            catch(Exception e)
            {
                GlobalValue.WriteLog(e.Message + "\r\n", "SystemError.log");
                return "";
            }
            
        }

        public void LOG(string log,string type)
        {
            TcpConnect.LOG(log, type);
        }

        #region astm checksum 
        private string MakeAstm(string msg)
        {
            string msgtmp;
            msgtmp = msg;
            string Stemp = msgtmp, C1, C2;
            Int32 len, sum = 0, c1, c2;
            len = Stemp.Length;
            for (int i = 0; i < len; i++)
            {
                sum += ChartoAscii(Stemp[i].ToString());

            }
            sum = sum % 256;
            c1 = sum / 16;
            c2 = Convert.ToInt32((Convert.ToDecimal(sum) / 16 - (sum / 16)) * 16);
            C1 = IntToStr(c1);
            C2 = IntToStr(c2);
            Stemp += C1 + C2;
            Stemp = STX + Stemp;
            return Stemp;
        }


        private int ChartoAscii(string chr)
        {
            if (chr.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(chr)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

            
        }
        private string IntToStr(int oct)
        {
            switch (oct)
            {
                case 10:
                    return "A";

                case 11:
                    return "B";

                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
            }
            return oct.ToString();

        }
        #endregion




    }
}
