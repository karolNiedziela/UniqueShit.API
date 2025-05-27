namespace UniqueShit.Domain.Chatting.Repositories
{
    public interface IChatUserRepository
    {
        Task<Guid> GetChatId(List<Guid> chatUsers);
    }
}
