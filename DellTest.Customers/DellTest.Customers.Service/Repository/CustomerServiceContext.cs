using System.Data.Entity;
using DellTest.Customers.Service.Models;

namespace DellTest.Customers.Service.Repository
{
    public class CustomerServiceContext : DbContext, ICustomerServiceContext
    {
        public DbSet<Customer> Customers { get; set; }

        public void MarkAsModified(IEntity entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}