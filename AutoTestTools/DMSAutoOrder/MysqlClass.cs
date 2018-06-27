using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;
using System.Configuration;

namespace DMSAutoOrder
{
    static class MysqlClass
    {
        public static string connectstring = "server=127.0.0.1;user id=root;password=root;database=sys";
        public static string DBIP, DBUserName, DBPassword;
        public static string DBname;
        public static MySqlCommand dbcommand;

        public static MySqlConnection ConnectDb()
        {
            MySqlConnection dbconnect;
            
            try
            {
                dbconnect = new MySqlConnection("server =" + DBIP + ";user id=" + DBUserName + ";password=" + DBPassword + ";database=" + DBname);
                dbconnect.Open();
            }
            catch (Exception e)
            {
                GlobalValue.WriteLog(e.Message + "\r\n", "SQLERRORLOADTABLE.log");
                return null;
            }
            

            return dbconnect;
        }

        public static MySqlConnection GetNewConnect(string DBname)
        {
            MySqlConnection dbnconnect;
            Boolean success = false;
            int timeout = 0;
            while (!success)
            {
                try
                {


                    dbnconnect = new MySqlConnection("server =" + DBIP + ";user id=" + DBUserName + ";password=" + DBPassword + ";database=" + DBname);

                    dbnconnect.Open();
                    dbnconnect.ChangeDatabase(DBname);
                    success = true;
                    return dbnconnect;
                }
                catch (Exception e)
                {

                    timeout++;
                    if (timeout > 10)
                    {
                        GlobalValue.WriteLog(e.Message + "\r\n", "SQLERRORLOADTABLE.log");
                        return null;
                    }


                }
            }
            return null;


        }

        public static DataTable GetDataTable(string sql, string tablename, Boolean ShowErrorMessage = false)
        {

            MySqlConnection MSC = GetNewConnect(DBname);
            DataTable dt = new DataTable(tablename);//= new DataTable(tablename);
            DataSet ds = new DataSet(tablename);
            Boolean success = false;
            int trycycle = 0;
            while (!success)
            {
                try
                {
                    if (MSC == null)
                    {
                        MSC = GetNewConnect(DBname);
                    }
                    MySqlDataAdapter MSDA = new MySqlDataAdapter(sql, MSC);
                    int i = MSDA.Fill(ds, tablename);
                    dt = ds.Tables[tablename];
                    success = true;
                    ds.Dispose();
                    MSC.Close();
                    MSC.Dispose();
                    MSDA.Dispose();
                }
                catch (Exception e)
                {
                    if (ShowErrorMessage)
                    {
                        DialogResult result = MessageBox.Show(e.Message + "\r\n" + sql, "Error");
                    }
                    trycycle++;
                    if (trycycle > 10)
                    {
                        GlobalValue.WriteLog(e.Message + "\r\n" + sql, "SQLERRORLOADTABLE.log");
                        Thread.Sleep(5);
                        //if (MSC != null)
                        //{
                        //    MSC.Close();
                        //    MSC.Dispose();
                        //}
                        return dt;
                    }
                }
            }




            return dt;
        }
        public static int ExecuteSQL(string sql, Boolean ShowErrormessage = false)
        {
            MySqlConnection MSC = GetNewConnect(DBname); ;
            Boolean success = false;
            int trycycle = 0;
            while (!success)
            {
                try
                {
                    if (MSC == null)
                    {
                        MSC = GetNewConnect(DBname);
                    }

                    dbcommand = new MySqlCommand(sql, MSC);
                    dbcommand.ExecuteNonQuery();
                    success = true;
                    MSC.Close();
                    MSC.Dispose();
                    //dbcommand.BeginExecuteNonQuery
                }
                catch (Exception e)
                {
                    if (ShowErrormessage)
                    {
                        DialogResult result = MessageBox.Show(e.Message + "\r\n" + sql, "SQLError");
                    }

                    trycycle++;
                    if (trycycle > 10)
                    {
                        GlobalValue.WriteLog(e.Message + "\r\n" + sql, "SQLERROR.log");
                        //if (MSC!=null)
                        //{
                        //    MSC.Close();
                        //    MSC.Dispose();
                        //}

                        return -1;
                    }


                }

            }


            return 0;
        }

    }
}
