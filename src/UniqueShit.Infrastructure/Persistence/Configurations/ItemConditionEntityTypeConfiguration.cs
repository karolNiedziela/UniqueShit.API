using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Infrastructure.Persistence.Configurations
{
    internal sealed class ItemConditionEntityTypeConfiguration : IEntityTypeConfiguration<ItemCondition>
    {
        public void Configure(EntityTypeBuilder<ItemCondition> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasData(ItemCondition.List);
        }
    }
}
