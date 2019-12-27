using Logging.Core;
using LoggingConsoleDemo;
using LoggingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            // Entity Framework
            var customerDbContext = new CustomerDbContext();
            try
            {
                var name = new SqlParameter("@Name", "waytoolongforitsowngood");
                var totalPurchaes = new SqlParameter("@TotalPurchases", 12000);
                var totalReturns = new SqlParameter("@TotalReturns", 100.50M);
                customerDbContext.Database.ExecuteSqlCommand("Exec dbo.CreateNewCustomer @Name, @TotalPurchases, @TotalReturns",
                        name, totalPurchaes, totalReturns);

            }
            catch (Exception ex)
            {
                var entityFrameworkDetail = GetLogDetail("", ex);
                Logger.WriteError(entityFrameworkDetail);
            }
            logDetail = GetLogDetail("used logging console", null);
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
