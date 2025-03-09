using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class ManufacturerEntityTypeConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
        }
    }
}
