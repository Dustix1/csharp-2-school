using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
    }

    class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
    }

    internal class MyDb : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source pičo nevím vole nejede to asi idk").LogTo(x => Console.Write(x), Microsoft.Extensions.Logging.LogLevel.Warning);
        }
    }
}
