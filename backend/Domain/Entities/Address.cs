using System;
using Domain.Entities.CoreEntities;

namespace Domain.Entities
{
    /// <summary>
    /// Address of Entities, 
    /// 
    /// Type and TypeId determines the entities eg Type = User, TypeId = "Id of the user"
    /// 
    /// </summary>
    public class Address
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid TypeId { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public State State { get; set; }
        public int StateId { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public bool Global { get; set; }
    }
}

