using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.ValueObjects;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.ADObjectId).IsClustered(false);
            builder.HasIndex(x => x.Id).IsClustered(true);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Id).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(x => x.DisplayName).HasMaxLength(256).IsRequired();

            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

            builder.Property(x => x.City).HasMaxLength(128).IsRequired(false);

            builder.Property(x => x.PhoneNumber)
               .HasColumnName("PhoneNumber")
               .HasMaxLength(12)
               .HasConversion(
               phoneNumber => phoneNumber == null ? null : phoneNumber.Value,
               value => PhoneNumber.Create(value).Value);

            builder.Property(x => x.AboutMe).HasMaxLength(512).IsRequired(false);

            builder.HasIndex(x => x.ADObjectId);
        }
    }
}
