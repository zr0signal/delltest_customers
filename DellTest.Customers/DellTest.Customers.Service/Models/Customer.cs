using System;
using System.ComponentModel.DataAnnotations;

namespace DellTest.Customers.Service.Models
{
    public enum CustomerState
    {
        Pending,
        Active,
        Inactive,
        Deleted
    }

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public CustomerState State { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool IsActive { get; set; }
    }
}