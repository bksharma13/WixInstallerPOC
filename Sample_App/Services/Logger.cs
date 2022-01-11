using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Sample_App.Services
{
    public class Logger
    {
        static string connectionstring = string.Empty;

        static Logger()
        {
            connectionstring = ConfigurationManager.AppSettings["logfilepath"].ToString();
        }

        public static void WriteLog(string message)
        {
            File.AppendAllText(connectionstring, DateTime.Now.ToString() + " -: " + message + Environment.NewLine);
        }
    }
}
