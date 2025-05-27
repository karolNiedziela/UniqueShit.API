using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueShit.Domain.Chatting;

namespace UniqueShit.Infrastructure.Persistence.Configurations.Chats
{
    internal sealed class PrivateChatEntityTypeConfiguration : IEntityTypeConfiguration<PrivateChat>
    {
        public void Configure(EntityTypeBuilder<PrivateChat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Messages)
                .WithOne(x => x.PrivateChat)
                .HasForeignKey(x => x.PrivateChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User1)
              .WithMany()
              .HasForeignKey(x => x.User1Id)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User2)
                .WithMany()
                .HasForeignKey(x => x.User2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
