using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(entity => entity.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(entity => entity.Phone_1)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(entity => entity.Phone_2)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(entity => entity.Email_1)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(entity => entity.Email_2)
                .IsRequired(false)
                .HasMaxLength(500);
        }
    }
}