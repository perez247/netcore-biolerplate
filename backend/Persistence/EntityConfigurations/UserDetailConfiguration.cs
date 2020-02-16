using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.Property(entity => entity.AboutMe)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(entity => entity.FirstName)
                .IsRequired(true)
                .HasMaxLength(100);
            
            builder.Property(entity => entity.LastName)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(entity => entity.Gender)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Ignore(entity => entity.Address);
            builder.Ignore(entity => entity.LocalAddress);
            builder.Ignore(entity => entity.Contact);
        }
    }
}