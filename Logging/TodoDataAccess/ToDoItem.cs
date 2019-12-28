using Logging.Data.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace TodoDataAccess
{
    public class ToDoItem
    {
        public int Id { get; set; }        
        public string Item { get; set; }
        public bool Completed { get; set; }
    }

    public class ToDoDbContext : DbContext
    {        
        public ToDoDbContext(string connectionString) : base(connectionString)
        {
            DbInterception.Add(new LoggerEfInterceptor());
        }
        public ToDoDbContext()
        {
            DbInterception.Add(new LoggerEfInterceptor());
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
