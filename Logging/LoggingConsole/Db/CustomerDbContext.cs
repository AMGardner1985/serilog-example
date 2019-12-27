using Logging.Data.EntityFramework;
using LoggingConsoleDemo.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace LoggingConsoleDemo
{

    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(): base()
        {
            DbInterception.Add(new LoggerEfInterceptor());
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
