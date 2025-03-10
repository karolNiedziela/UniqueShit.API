using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class OfferEntityTypeConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Topic, topicBuilder =>
            {
                topicBuilder.Property(x => x.Value)
                .HasMaxLength(Topic.MaxLength)
                .HasColumnName("Topic")
                .IsRequired();
            });

            builder.OwnsOne(x => x.Description, descriptionBuilder =>
            {
                descriptionBuilder.Property(x => x.Value)
                .HasMaxLength(Description.MaxLength)
                .HasColumnName("Description")
                .IsRequired();
            });

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

            builder.Property(x => x.CreatedOnUtc).IsRequired();

            builder.Property(x => x.ExpiredAtUtc).IsRequired();

            builder.HasOne<OfferType>()
                .WithMany()
                .HasForeignKey(x => x.OfferTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Size>()
                .WithMany()
                .HasForeignKey(x => x.SizeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<ProductCategory>()
                .WithMany()
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Manufacturer)
                .WithMany()
                .HasForeignKey(x => x.ManufacturerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<OfferState>()
                .WithMany()
                .HasForeignKey(x => x.OfferStateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Colours)
             .WithMany()
              .UsingEntity(
                 "OfferColour",
                 l => l.HasOne(typeof(Colour)).WithMany().HasForeignKey("ColourId").HasPrincipalKey(nameof(Colour.Id)),
                 r => r.HasOne(typeof(Offer)).WithMany().HasForeignKey("OfferId").HasPrincipalKey(nameof(Offer.Id)),
                 j => j.HasKey("ColourId", "OfferId"));
        }
    }
}
