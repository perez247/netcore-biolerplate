using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// A class/table that represent the list of entities it has.
    /// Its more like a one to many relationship
    /// </summary>
    public class Collection
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid TypeId { get; set; }
        public virtual  ICollection<Photo> Photos { get; set; }

        public Collection()
        {
            Photos = new HashSet<Photo>();
        }
    }
}