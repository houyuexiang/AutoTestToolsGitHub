using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace AutoTestTools.ClassLib
{
    class DataBaseSQLite
    {
        public SQLiteConnection SQLConnect = null;
        string logfile = Directory.GetCurrentDirectory() + "\\UpdateErrMessage.txt";
        public DataBaseSQLite(string DataBaseName)
        {
            SQLConnect = new SQLiteConnection("Data Source = "  + DataBaseName);
            SQLConnect.Open();
            InitDataBase();


        }
        private void  InitDataBase()
        {
            CheckDBVersion();
        }
        private void CheckDBVersion()
        {
            string sql = "select *  from TAT_Dictonary where ktype='DBV' and Kname='DatabaseVersion'";
            SQLiteCommand comm = null;
            SQLiteDataReader reader = null;


            Int32 DBversion = 0;
            try
            {
                comm = new SQLiteCommand(sql, SQLConnect);
                reader = comm.ExecuteReader();
                reader.Read();
               if (reader.HasRows)
               {
                  DBversion = Convert.ToInt16( reader.GetString(2));
                  if (DBversion < GlobalValue.DBV)
                  {
                      UpdateDatabase(DBversion);
                   }
                    

                }
                    
            }
            catch
            {
                UpdateDatabase(DBversion);
            }
            if (reader != null)
            {
                reader.Close();
            }
            if (comm != null)
            {
                comm.Dispose();
            }
            

        }
        private int UpdateDatabase(int DBV)
        {
            
            
            string S_read = "",S_tmp;
            int dbversion = 0;
            string filename = Directory.GetCurrentDirectory() + "\\DatabaseUpdate.txt",sql;
            StreamReader SR = new StreamReader(@filename);
            SQLiteCommand sqlcomm = new SQLiteCommand(SQLConnect);

            S_read = SR.ReadToEnd();
            SR.Close();

            S_read = S_read.Replace("\r\n", " ");
            
            
            while (S_read.IndexOf("}") > 0)
            {
                S_tmp = S_read.Substring(0, S_read.IndexOf("}") + 1);
                S_read = S_read.Substring(S_read.IndexOf("}") + 1);
                try
                {
                   
                    dbversion = Convert.ToInt16(S_tmp.Substring(S_tmp.IndexOf("[") + 1, S_tmp.IndexOf("]") - S_tmp.IndexOf("[") - 1).Trim());
                }
                catch
                {
                    return -1;
                }
                if (dbversion <= DBV) continue;
                int s = S_tmp.IndexOf("{") + 1;
                int e = S_tmp.IndexOf("}") - s - 1;
                S_tmp = S_tmp.Substring(s, e);
                while (S_tmp.IndexOf(";") > 0)
                {
                    sql = S_tmp.Substring(0, S_tmp.IndexOf(";"));
                    S_tmp = S_tmp.Substring(S_tmp.IndexOf(";") + 1);
                    sqlcomm.CommandText = sql ;
                    try
                    {
                        sqlcomm.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        GlobalValue.WriteLog(DateTime.Now.ToString() + ":SQL" + sql + ":" +  ex.Message.ToString(),logfile);
                        continue;
                    }
                }


            }
            sql = "update TAT_Dictonary set kvalue = '" + dbversion + "' where ktype='DBV' and Kname='DatabaseVersion'";
            sqlcomm.CommandText = sql;
            sqlcomm.ExecuteNonQuery();

            

            return 0;
        }

        public int GetSelectValue(string sql)
        {
            SQLiteCommand sqlcom = new SQLiteCommand(sql,SQLConnect);
            SQLiteDataReader reader = sqlcom.ExecuteReader();
            int count = 0;
            reader.Read();
            try
            {
                if (reader.HasRows)
                {
                    count = reader.GetInt32(0);
                    
                }
            }
            catch(Exception ex)
            {
                GlobalValue.WriteLog(DateTime.Now.ToString() + ":SQL" + sql + ":" + ex.Message.ToString(), logfile);
                count = 0;
            }
            reader.Close();
            sqlcom.Dispose();
            return count;
        }
        public int ExecuteSQL(string sql)
        {
            SQLiteCommand  sqlcomm = new SQLiteCommand(SQLConnect);
            try
            {
                sqlcomm.CommandText = sql;
                sqlcomm.ExecuteNonQuery();
                
            }
            catch(Exception ex)
            {
                GlobalValue.WriteLog(DateTime.Now.ToString() + ":SQL" + sql + ":" + ex.Message.ToString(), logfile);
                return -1;
            }
            sqlcomm.Dispose();
            return 0;
        }
        //public TATtime GetTATtime(string sampleno,string sampleda ,int seq)
        //{
        //    TATtime tattime;
        //    string sql = "select creation,orders,inlabbed,finish from tat_sample where instrument = 'APTIO' and sampleno = '" + sampleno + "' and sampleda = '" + sampleda + "' and seq =" + seq;
        //    SQLiteCommand sqlcomm = new SQLiteCommand(sql, SQLConnect);
        //    SQLiteDataReader reader = sqlcomm.ExecuteReader();
        //    reader.Read();
        //    if (reader.HasRows)
        //    {
        //        tattime.creation = reader[0].ToString();
        //        tattime.order = reader[1].ToString();
        //        tattime.inlabbed = reader[2].ToString();
        //        tattime.finish = reader[3].ToString();
        //    }
        //    else
        //    {
        //        tattime.creation = "";
        //        tattime.order = "";
        //        tattime.inlabbed = "";
        //        tattime.finish = "";
        //    }
        //    reader.Close();
        //    sqlcomm.Dispose();
        //    return tattime;
            
        //}

        public void Close()
        {
            if (SQLConnect != null)
            {
                SQLConnect.Close();
            }

        }
    }
}
