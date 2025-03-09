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

            builder.ComplexProperty(x => x.Price, priceBuilder =>
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

            builder.HasOne<Model>()
                .WithMany()
                .HasForeignKey(x => x.ModelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
