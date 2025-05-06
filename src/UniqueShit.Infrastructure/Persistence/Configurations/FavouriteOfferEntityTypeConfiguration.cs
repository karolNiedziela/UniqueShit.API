using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.FavouriteOffers;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class FavouriteOfferEntityTypeConfiguration : IEntityTypeConfiguration<FavouriteOffer>
    {
        public void Configure(EntityTypeBuilder<FavouriteOffer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedOnUtc).IsRequired();

            builder.HasOne(x => x.SaleOffer)
                .WithMany()
                .HasForeignKey(x => x.SaleOfferId);

            builder.HasOne(x => x.AppUser)
                .WithMany()
                .HasForeignKey(x => x.AppUserId);
        }
    }
}
