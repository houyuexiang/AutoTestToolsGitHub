﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Data;

namespace AutoTestTools.ClassLib
{
    class GlobalValue
    {
        public static string ENQ = chr(5), ACK = chr(6), STX = chr(2), ETX = chr(3), EOT = chr(4), LF = chr(10), CR = chr(13), NAK = chr(21), ETB = chr(23), FS = chr(28), GS = chr(29);
        private static string[] ASCII = new string[9] { ENQ, ACK, STX, ETX, ETB, LF, CR, EOT, NAK };
        private static List<string> list = ASCII.ToList();
        public static string LogPath = System.Environment.CurrentDirectory + "\\LOG\\";
        public static Int32 DBV = 2;
        public static string StartSampleno = "1", EndSampleno = "100", CurrentSampleno;
        public static int InstrumentRequestInterval;
        public static List<string> testlist = new List<string>();
        public static CommunityControl CC;

        private static string chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
        public static int CreateFolder(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                return 0;
            }
            else
            {
                try
                {
                    info.Create();
                    return 0;
                }
                catch
                {
                    return -1;
                }
            }
        }
        public static void WriteLog(string log, string logfile)
        {
            try
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Close();
            }
            catch(Exception e)
            {
                System.Console.WriteLine("Globalvalue" + e.Message);
                return;
            }
        }
        public static string ReplaceASCII(string msg)
        {
            string Stemp, Sreplace;
            int pos;
            Stemp = msg;
            foreach (string a in list)
            {
                pos = msg.IndexOf(a);
                if (pos > 0)
                {
                    Sreplace = TransASCII(a);
                    Stemp = Stemp.Replace(a, Sreplace);
                }
            }
            return Stemp;

        }

        public static string GetSetting(string settingname)
        {
            string sql = "select settingname,settingvalue from setting where settingname ='" + settingname + "'";
            DataTable dt_set = Database.DatabaseMYSQL.GetDataTable(sql, "setting");
            if (dt_set.Rows.Count > 0)
            {
                return dt_set.Rows[0][1].ToString();
            }
            else
            {
                return "";
            }
        }
        public static void SaveSetting(string settingname ,string settingvalue)
        {
            string sql = "select settingname,settingvalue from setting where settingname ='" + settingname + "'";
            DataTable dt_set = Database.DatabaseMYSQL.GetDataTable(sql, "setting");
            if (dt_set.Rows.Count > 0)
            {
                sql = "update setting set settingvalue = '" + settingvalue + "' where settingname = '" + settingname + "'";
                Database.DatabaseMYSQL.ExecuteSQL(sql);
            }
            else
            {
                sql = "insert into setting(settingname,settingvalue) values('" + settingname  + "','" + settingvalue +  "')";
                Database.DatabaseMYSQL.ExecuteSQL(sql);
            }
        }

        public static void Init()
        {
            StartSampleno = GetSetting("StartSampleno");
            EndSampleno = GetSetting("EndSampleno");
            CurrentSampleno = GetSetting("CurrentSampleno");
            int second;
            string tmp;
            try
            {
                tmp = GetSetting("InstrumentRequestInterval");
                second = Convert.ToInt32(tmp);
            }
            catch
            {
                second = 600;
            }
            InstrumentRequestInterval = second;
            
            string[] stestlist = GetSetting("testlist").Split('|');
            testlist = stestlist.ToList<string>();
            
        }

        //public static int GetSecondSpan(DateTime dt_datetime)
        //{
        //    DateTime dt_now = DateTime.Now;
        //    return (dt_now.Subtract(dt_datetime).TotalSeconds)
        //}

        private static string TransASCII(string ascii)
        {
            string Sreturn = "";
            if (ascii == ENQ)
            {
                Sreturn = "<ENQ>";
            }
            else if (ascii == ACK)
            {
                Sreturn = "<ACK>";
            }
            else if (ascii == STX)
            {
                Sreturn = "<STX>";
            }
            else if (ascii == ETX)
            {
                Sreturn = "<ETX>";
            }
            else if (ascii == ETB)
            {
                Sreturn = "<ETB>";
            }
            else if (ascii == LF)
            {
                Sreturn = "<LF>";
            }
            else if (ascii == CR)
            {
                Sreturn = "<CR>";
            }
            else if (ascii == EOT)
            {
                Sreturn = "<EOT>";
            }
            else if (ascii == NAK)
            {
                Sreturn = "<NAK>";
            }
            return Sreturn;
        }
    }
}
