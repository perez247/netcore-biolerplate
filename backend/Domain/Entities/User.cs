using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {

        public virtual UserDetail UserDetail { get; set; }
        public bool Deleted { get; set; }
        public bool AgreeToTermsAndCondition { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual Collection Collection { get; set; }
        
        public User()
        {
            // Problems = new HashSet<Problem>();
            // Projects = new HashSet<Project>();
            // Votes = new HashSet<Vote>();
            // Ideas = new HashSet<Idea>();
            UserRoles = new HashSet<UserRole>();
        }
    }
}