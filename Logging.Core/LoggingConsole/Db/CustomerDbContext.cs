using LoggingConsoleDemo.Models;
using System.Data.Entity;

namespace LoggingConsoleDemo
{

    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(): base()
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
