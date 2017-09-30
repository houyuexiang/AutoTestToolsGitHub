using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoTestTools.ClassLib.InstrumentDrivers
{
    class Advia2400: InstrumentBase
    {
        string Instrumentname,InstrumentType;
        string ConnectType, IPaddress;
        int Portno;
        TCPConnect connect;

        






        #region SingleSample
        public new string Request(string SampleID)
        {
            return "[Q 0101010" + SampleID.PadRight(13, Convert.ToChar(" ")) + " ]";
        }


        public new string Result(string SampleID, DataGridView DGV_result)
        {
            int gridcount = DGV_result.Rows.Count, testnum = 0;
            string item, result, flag, resultstring = "", sendstr;

            for (int i = 0; i < gridcount; i++)
            {
                if (DGV_result.Rows[i] != null)
                {
                    item = (string)DGV_result.Rows[i].Cells[0].Value;
                    result = (string)DGV_result.Rows[i].Cells[1].Value;
                    flag = (string)DGV_result.Rows[i].Cells[2].Value;
                    if (item != null && item != "" && result != null)
                    {
                        resultstring += item.Substring(0, 3) + "M";
                        if (result.Length > 8) result = result.Substring(0, 8);
                        resultstring += result.PadLeft(8, Convert.ToChar(" "));
                        if (flag == null) flag = "";
                        if (flag.Length > 3) flag = flag.Substring(0, 3);
                        resultstring += flag.PadRight(3, Convert.ToChar(" "));
                        testnum++;
                    }
                }
            }
            string datetime = DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);// + DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            sendstr = "[R 0101" + Convert.ToString(testnum).PadLeft(3, Convert.ToChar("0")) + datetime + "N1" + SampleID.PadRight(13, Convert.ToChar(" "));
            sendstr += "".PadRight(7, Convert.ToChar(" ")) + "".PadRight(16, Convert.ToChar(" ")) + "".PadRight(16, Convert.ToChar(" ")) + "M000";
            sendstr += DateTime.Today.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " 1.011" + resultstring + " ]";
            return sendstr;
        }
        #endregion

        


    }

}
