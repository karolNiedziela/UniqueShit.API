using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class ColourEntityTypeConfiguration : IEntityTypeConfiguration<Colour>
    {
        public void Configure(EntityTypeBuilder<Colour> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

            builder.HasData(Colour.List);
        }
    }
}
