using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class PaymentTypeEntityTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasData(PaymentType.List);
        }
    }
}
