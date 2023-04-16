using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DMSAutoOrder;


namespace Service_DMSAssist
{
    public partial class Service_DMSAssist : ServiceBase
    {
        public Service_DMSAssist()
        {
            InitializeComponent();
        }
        
        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\LOG\\";
        
        protected override void OnStart(string[] args)
        {
            GlobalValue.LogPath = LogPath;
            GlobalValue.wlog.Source = "Service_DMSAssist";
            GlobalValue.wlog.WriteEntry("DMS_Assist:Start",EventLogEntryType.Information);
            GlobalValue.wlog.WriteEntry("DMS_Assist Work Path:" + AppDomain.CurrentDomain.BaseDirectory, EventLogEntryType.Information);
            DMSProcessor.Start();
        }

        protected override void OnStop()
        {
           
            DMSProcessor.Stop();

        }
    }
}
