using System;

namespace MicroProjectApplication.Models
{
    // US-001 / SVC-002: Customer (id, name, city, createdAt, updatedAt)
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
