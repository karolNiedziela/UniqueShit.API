using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class ModelEntityTypeConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasOne(x => x.ProductCategory)
                .WithMany()
                .HasForeignKey(x => x.ProductCategoryId);

            builder.HasOne(x => x.Brand)
                .WithMany()
                .HasForeignKey(x => x.BrandId);

            builder.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.AppUserId);
        }
    }
}
