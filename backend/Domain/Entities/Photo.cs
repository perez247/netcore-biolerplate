using System;
using Domain.Entities.CoreEntities;

namespace Domain.Entities
{
    public class Photo
    {
        // Id,Url,Description,DateAdded,ProblemBetaId,PublicId
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Type { get; set; }
        public PhotoType PhotoType { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
        public string PublicId { get; set; }
        public Photo()
        {
            DateCreated = DateTime.Now;
        }
    }
}