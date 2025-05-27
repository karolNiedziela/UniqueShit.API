namespace UniqueShit.Domain.Chatting.Repositories
{
    public interface IPrivateChatRepository
    {
        Task<PrivateChat?> GetAsync(Guid id);

        Task<PrivateChat?> GetByUserIdsAsync(Guid user1Id, Guid user2Id);

        void Add(PrivateChat privateChat);

        void Update(PrivateChat privateChat);
    }
}
