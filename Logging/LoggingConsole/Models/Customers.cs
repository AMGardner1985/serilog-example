using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingConsoleDemo.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalPurchases { get; set; }
        public decimal TotalReturns { get; set; }
    }
    
}
