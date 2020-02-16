using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.Property(entity => entity.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Ignore(entity => entity.Photos);
        }
    }
}