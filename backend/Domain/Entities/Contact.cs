using System;

namespace Domain.Entities
{
    /// <summary>
    /// Contact of an Entity
    /// </summary>
    public class Contact
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Type { get; set; }
        public string Phone_1 { get; set; }
        public string Phone_2 { get; set; }
        public string Email_1 { get; set; }
        public string Email_2 { get; set; }
    }
}