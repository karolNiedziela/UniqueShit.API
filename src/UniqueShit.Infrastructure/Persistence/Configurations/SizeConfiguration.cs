using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value).HasMaxLength(50).IsRequired();

            builder.HasOne<ProductCategory>()
                .WithMany()
                .HasForeignKey(x => x.ProductCategoryId);
        }
    }
}
