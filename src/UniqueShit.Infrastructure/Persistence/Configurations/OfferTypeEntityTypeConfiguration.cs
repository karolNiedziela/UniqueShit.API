using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class OfferTypeEntityTypeConfiguration : IEntityTypeConfiguration<OfferType>
    {
        public void Configure(EntityTypeBuilder<OfferType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();

            builder.HasData(OfferType.List);
        }
    }
}
