using UniqueShit.Chatting;

namespace UniqueShit.Domain.Chatting.Repositories
{
    public interface IChatRepository
    {
        Task<Chat?> GetAsync(Guid id);

        void Add(Chat chat);

        void Update(Chat chat);
    }
}
