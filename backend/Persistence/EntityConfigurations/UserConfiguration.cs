using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Ignore property
            builder.Ignore(entity => entity.Collection);

            // Entity Relationship

            // User <-> UserDetails
            builder.HasOne(entity => entity.UserDetail)
                .WithOne(entity => entity.User)
                .OnDelete(DeleteBehavior.Cascade);

            // User <-> Problems
            // builder.HasMany(entity => entity.Problems)
            //     .WithOne(entity => entity.User)
            //     .OnDelete(DeleteBehavior.Cascade);

            // // User <-> Projects
            // builder.HasMany(entity => entity.Projects)
            //     .WithOne(entity => entity.User)
            //     .OnDelete(DeleteBehavior.Cascade);

            // // User <-> Ideas
            // builder.HasMany(entity => entity.Ideas)
            //     .WithOne(entity => entity.User)
            //     .OnDelete(DeleteBehavior.ClientSetNull);

            // // User <-> Projects
            // builder.HasMany(entity => entity.Votes)
            //     .WithOne(entity => entity.User)
            //     .OnDelete(DeleteBehavior.ClientSetNull);

            // User <-> UserDetails
            // builder.HasOne(entity => entity.UserDetail)
            //     .WithOne(entity => entity.)
        }
    }
}