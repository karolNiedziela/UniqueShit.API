using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.Enumerations;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Infrastructure.Persistence.Configurations.Offers
{
    internal abstract class OfferEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Offer
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

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

            builder.HasOne<OfferState>()
             .WithMany()
             .HasForeignKey(x => x.OfferStateId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.CreatedOnUtc).IsRequired();

            builder.Property(x => x.ExpiredAtUtc).IsRequired();

            builder.HasOne(x => x.Model)
              .WithMany()
              .HasForeignKey(x => x.ModelId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.AppUser)
              .WithMany()
              .HasForeignKey(x => x.AppUserId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
