using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoTestTools.ClassLib.InstrumentDrivers
{
    class LIS_RESULT:InstrumentBase
    {
        private string FS = GlobalValue.FS, GS = GlobalValue.GS;
        public ASTM LISASTM;
        protected override void LoadDataTable()
        {
            //string sql = "select seq,sampleid,sampleda,createtime,isrequested,isresulted,orderstring from " + Instrumentname + "_order where isrequested = 0 or isresulted = 0 limit 1";
            //string sql = "select sampleid,sampleda,createtime,isrequested,isresulted,orderstring from " + Instrumentname + "_order where isrequested = 0  limit 5";
            string sql = "select settingname,settingvalue from setting ";
            dt_instrument_order = Database.DatabaseMYSQL.GetDataTable(sql, Instrumentname);

        }

        protected override void Init()
        {
            LISASTM = new ASTM(connect.connect);
        }
        public override string MakeResult()
        {
            string t = LISASTM.receivebuffer;
            t = "";
            return t;
        }
    }
}
