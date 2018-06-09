using System.Data.Entity;
using DellTest.Customers.Service.Models;
using DellTest.Customers.Service.Repository;

namespace DellTest.Customers.Service.Tests.Mock.EntityFramework
{
    public class TestCustomerServiceContext : ICustomerServiceContext
    {
        public TestCustomerServiceContext()
        {
            Customers = new TestCustomerDbSet();
        }

        public DbSet<Customer> Customers { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(IEntity item)
        {
        }

        public void Dispose()
        {
        }
    }
}