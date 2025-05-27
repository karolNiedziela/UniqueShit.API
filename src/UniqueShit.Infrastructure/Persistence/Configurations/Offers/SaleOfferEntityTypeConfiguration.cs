using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.Enumerations;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Infrastructure.Persistence.Configurations.Offers
{
    internal sealed class SaleOfferEntityTypeConfiguration : OfferEntityTypeConfiguration<SaleOffer>
    {
        public override void Configure(EntityTypeBuilder<SaleOffer> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(x => x.Price, priceBuilder =>
            {
                priceBuilder.Property(x => x.Amount)
                .HasColumnType("decimal(6,2)")
                .HasColumnName("Price")
                .IsRequired();
                priceBuilder.Property(x => x.Currency)
                .HasMaxLength(Money.CurrencyMaxLength)
                .HasColumnName("Currency")
                .IsRequired();
            });

            builder.HasOne<ItemCondition>()
              .WithMany()
              .HasForeignKey(x => x.ItemConditionId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Size)
                .WithMany()
                .HasForeignKey(x => x.SizeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<DeliveryType>()
              .WithMany()
              .HasForeignKey(x => x.DeliveryTypeId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<PaymentType>()
                .WithMany()
                .HasForeignKey(x => x.PaymentTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Colours)
              .WithMany()
              .UsingEntity(
                  "SaleOfferColour",
                  l => l.HasOne(typeof(Colour)).WithMany().HasForeignKey("ColourId"),
                  r => r.HasOne(typeof(SaleOffer)).WithMany().HasForeignKey("SaleOfferId"));
        }
    }
}
