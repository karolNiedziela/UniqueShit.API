using UniqueShit.Domain.Chatting;
using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Chatting
{
    public sealed class ChatMessage
    {
        public Guid Id { get; private set; }

        public string Content { get; private set; } = string.Empty;

        public Guid SenderId { get; private set; }

        public AppUser Sender { get; private set; } = default!;

        public DateTimeOffset SentAt { get; private set; }

        public bool IsRead { get; private set; }

        public Guid PrivateChatId { get; private set; }

        public PrivateChat PrivateChat { get; private set; } = default!;


        private ChatMessage() { }

        public ChatMessage(Guid id, Guid privateChatId, Guid senderId, string content, DateTimeOffset sentAt, bool isRead = false)
        {
            Id = id;
            PrivateChatId = privateChatId;
            SenderId = senderId;
            Content = content;
            SentAt = sentAt;
            IsRead = isRead;
        }

        public void MarkAsRead() => IsRead = true;
    }
}
