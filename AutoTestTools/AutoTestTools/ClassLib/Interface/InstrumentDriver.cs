using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AutoTestTools.ClassLib
{
    interface IFInstrumentDriver
    {
        string GetInstrumentName();
        TCPConnect GetConnect();
        Thread GetThread();
        void StartThread();
        void StopThread();
        string GetInstrumentType();
        int InitInstrument(string InstrumentName,string ConnectType,string IPaddress,string Portno, string InstrumentType);
        string Request(string SampleID);
        string Result(string SampleID, DataGridView DGV_result);
    }
}
