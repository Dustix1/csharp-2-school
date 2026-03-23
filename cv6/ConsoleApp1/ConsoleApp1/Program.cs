using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDb db = new MyDb();
            db.Customer.Add(new Customer()
            {
                Name = "Jana",
                Address = "Klimkovice"
            });
            db.SaveChanges();

            db.Order.Add(new Order()
            {
                Price = 2000,
                Product = "Banány",
                Customer = new Customer()
                {
                    Name = "Karel",
                    Address = null
                }
            });
             db.SaveChanges();

            foreach(Order o in db.Order.Include(x => x.Customer))
            {
                Console.WriteLine(o.Product + " - " + o.Customer.Name);
            }

            foreach (Customer customer in db.Customer.Where(x => x.Id < 5).OrderByDescending(x => x.Name))
            {
                Console.WriteLine(customer.Id + " | " + customer.Name + " | " + customer.Address);
            }

            Customer zuzana = db.Customer.FirstOrDefault(x => x.Name == "Zuzana");
            zuzana.Address = "Aš";
            db.SaveChanges();

            int count = db.Customer.Where(x => x.Name.StartsWith("Zu")).Count();
            Console.WriteLine(count);


        }
    }
}
