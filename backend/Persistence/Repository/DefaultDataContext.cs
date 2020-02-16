using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.CoreEntities;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Triggers;

namespace Persistence.Repository
{
        public class DefaultDataContext : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

        // Entities to add to the database
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Photo> Photos { get; set; }        
        public DbSet<UserDetail> UserDetails { get; set; }

        // Core Details
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<EcoEntity> EcoEntities { get; set; }
        public DbSet<Ico> Icos { get; set; }
        public DbSet<UnSDGGoal> UnSDGGoals { get; set; }
        public DbSet<PhotoType> PhotoTypes { get; set; }


        public DefaultDataContext(DbContextOptions<DefaultDataContext> options)
        : base(options) {}
 
        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<UserRole>(userRole =>{
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.ApplyConfigurationsFromAssembly(typeof(DefaultDataContext).Assembly);

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Fire trigger on deleting User
            Triggers<User>.Deleting += async (entity) => {
                await UserTriggers.OnDeletingUser(entity, this);
            };

            // Fire trigger on deleting Collections 
            Triggers<Collection>.Deleting += async (entity) => {
                await CollectionTrigger.OnDeletingCollection(entity, this);
            };


            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);

        }

    }
}