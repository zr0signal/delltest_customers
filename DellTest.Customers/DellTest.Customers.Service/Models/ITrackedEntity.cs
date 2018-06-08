using System;

namespace DellTest.Customers.Service.Models
{
    public interface ITrackedEntity : IEntity
    {
        DateTime DateCreated { get; set; }

        DateTime DateUpdated { get; set; }

        DateTime? DateDeleted { get; set; }
    }
}
