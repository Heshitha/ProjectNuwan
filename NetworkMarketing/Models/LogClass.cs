using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace NetworkMarketing.Models
{
    public static class LogClass
    {
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(ConfigurationManager.AppSettings["LogFilePath"].ToString(), true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + ", " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }

        public static void WriteErrorLog(string errorMessege)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(ConfigurationManager.AppSettings["LogFilePath"].ToString(), true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + errorMessege);
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }

    }
}