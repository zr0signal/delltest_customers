using System.Data.Entity;
using DellTest.Customers.Service.Models;

namespace DellTest.Customers.Service
{
    public class CustomerServiceContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}