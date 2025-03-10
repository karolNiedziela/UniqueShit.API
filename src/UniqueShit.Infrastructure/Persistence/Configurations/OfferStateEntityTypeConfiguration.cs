using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class OfferStateEntityTypeConfiguration : IEntityTypeConfiguration<OfferState>
    {
        public void Configure(EntityTypeBuilder<OfferState> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();

            builder.HasData(OfferState.List);
        }
    }
}
