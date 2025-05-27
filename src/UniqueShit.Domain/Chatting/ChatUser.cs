using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Chatting
{
    public sealed class ChatUser
    {
        public Guid UserId { get; private set; }

        public AppUser AppUser { get; private set; } = default!;

        public Guid ChatId { get; private set; }

        public Chat Chat { get; private set; } = default!;

        private ChatUser() { }
        
        public ChatUser(Guid userId, Guid chatId)
        {
            UserId = userId;
            ChatId = chatId;
        }
    }
}
