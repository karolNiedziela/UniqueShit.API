using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enitities;

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

            builder.Property(x => x.City).HasMaxLength(128).IsRequired();

            builder.HasIndex(x => x.ADObjectId);
        }
    }
}
