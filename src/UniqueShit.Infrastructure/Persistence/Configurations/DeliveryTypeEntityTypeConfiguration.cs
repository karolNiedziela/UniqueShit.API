using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class DeliveryTypeEntityTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasData(DeliveryType.List);
        }
    }
}
