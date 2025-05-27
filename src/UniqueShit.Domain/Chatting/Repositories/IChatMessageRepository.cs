using UniqueShit.Chatting;

namespace UniqueShit.Domain.Chatting.Repositories
{
    public interface IChatMessageRepository
    {
        void Add(ChatMessage chatMessage);

        //Task<IEnumerable<ChatPreviewDto>> GetUserChatPreviewsAsync(Guid userId);
    }
}
