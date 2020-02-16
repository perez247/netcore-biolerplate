using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // builder.Property(entity => entity.CountryId)
            //     .IsRequired(false);
            
            // builder.Property(entity => entity.StateId)
            //     .IsRequired(false);

            builder.Property(entity => entity.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(entity => entity.Street)
                .IsRequired(false)
                .HasMaxLength(1000);
            
            builder.Property(entity => entity.PostCode)
                .IsRequired(false)
                .HasMaxLength(100);
        }
    }
}