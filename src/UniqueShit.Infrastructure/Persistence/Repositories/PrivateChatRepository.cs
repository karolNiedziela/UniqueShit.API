using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Chatting;
using UniqueShit.Domain.Chatting.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class PrivateChatRepository : IPrivateChatRepository
    {
        private readonly DbSet<PrivateChat> _privateChats;

        public PrivateChatRepository(IDbContext context)
        {
            _privateChats = context.Set<PrivateChat>();
        }

        public async Task<PrivateChat?> GetAsync(Guid id)
            => await _privateChats.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<PrivateChat?> GetByUserIdsAsync(Guid user1Id, Guid user2Id)
        {
            List<Guid> orderedUserIds = user1Id.CompareTo(user2Id) <= 0
                ? [user1Id, user2Id]
                : [user2Id, user1Id];

            return await _privateChats
                .FirstOrDefaultAsync(x => x.User1Id == orderedUserIds[0] && x.User2Id == orderedUserIds[1]);
        }

        public void Add(PrivateChat privateChat)
            => _privateChats.Add(privateChat);

        public void Update(PrivateChat privateChat)
            => _privateChats.Update(privateChat);
    }
}
