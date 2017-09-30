using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace AutoTestTools.Tools
{
    static class  RegeditSet
    {
        public static void SetRegeditKey(string dbpath)
        {
            string regeditpath;
            if (Environment.Is64BitOperatingSystem == true)
            {
                regeditpath = "SOFTWARE\\WOW6432Node\\ODBC\\ODBC.INI";
            }
            else
            {
                regeditpath = "SOFTWARE\\ODBC\\ODBC.INI";
            }
            RegistryKey rk;
            rk = Registry.LocalMachine.OpenSubKey(regeditpath + "\\Auto", true);
            if (rk == null)
            {
                rk = Registry.LocalMachine.CreateSubKey(regeditpath + "\\Auto");
            }
            rk.SetValue("Database", dbpath);
            rk.SetValue("Description", "Created by Auto val test tools");
            rk.SetValue("Driver", "C:\\WINDOWS\\system32\\sqlite3odbc.dll");
            rk.SetValue("LongNames", "1");
            rk.SetValue("ShortNames", "1");
            rk.SetValue("SyncPragma", "NORMAL");
            rk.SetValue("NoTXN", "0");
            rk.SetValue("NoWCHAR", "0");
            rk.SetValue("StepAPI", "0");
            rk.SetValue("Timeout", "10");
            rk.Close();

            rk = Registry.LocalMachine.OpenSubKey(regeditpath + "\\ODBC Data Sources", true);
            if (rk == null)
            {
                rk = Registry.LocalMachine.CreateSubKey(regeditpath + "\\ODBC Data Sources");
            }
            rk.SetValue("Auto", "SQLite3 ODBC Driver");
            rk.Close();

        }
    }
}
