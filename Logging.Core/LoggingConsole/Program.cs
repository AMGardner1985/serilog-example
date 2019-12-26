using Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logDetail = GetLogDetail("starting application", null);
            Logger.WriteDiagnostic(logDetail);

            var tracker = new PerfTracker("LoggerConsole_Executure", "", logDetail.UserName, 
                                    logDetail.Location, logDetail.Product, logDetail.Layer);

            try
            {
                var ex = new Exception("Something bad has happened!");
                ex.Data.Add("input param", "nothing to see here");
                throw ex;
            }
            catch (Exception ex)
            {
                logDetail = GetLogDetail("", ex);
                Logger.WriteError(logDetail);
            }

            logDetail = GetLogDetail("used flogging console", null); //new flog detail
            Logger.WriteUsage(logDetail);

            logDetail = GetLogDetail("stopping app", null);
            Logger.WriteDiagnostic(logDetail);

            tracker.Stop();
        }

        private static LogDetail GetLogDetail(string message, Exception ex) {
            return new LogDetail
            {
                Product = "Demo",
                Location = "LogginConsole", //this application or project
                Layer = "Job", // unattended executable invoked (could be job / scheduled etc / endpoint
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = message,
                Exception = ex
            };
        }
    }
}
