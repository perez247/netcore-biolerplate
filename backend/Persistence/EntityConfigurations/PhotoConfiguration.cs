using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(entity => entity.Url)
                .HasMaxLength(1000);

            builder.Property(entity => entity.PublicId)
                .HasMaxLength(1000);

            builder.Property(entity => entity.Type)
                .HasMaxLength(50);
        }
    }
}