using System;
using System.Data.Entity;
using DellTest.Customers.Service.Models;

namespace DellTest.Customers.Service
{
    public interface ICustomerServiceContext : IDisposable
    {
        DbSet<Customer> Customers { get; }

        int SaveChanges();

        void MarkAsModified(IEntity entity);
    }
}