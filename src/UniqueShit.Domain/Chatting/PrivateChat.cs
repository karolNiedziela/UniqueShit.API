using UniqueShit.Chatting;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Domain.Chatting
{
    public sealed class PrivateChat
    {
        public Guid Id { get; private set; }

        public AppUser User1 { get; private set; } = default!;

        public Guid User1Id { get; private set; }

        public AppUser User2 { get; private set; } = default!;

        public Guid User2Id { get; private set; }

        public List<ChatMessage> Messages { get; private set; } = [];

        private PrivateChat() { }

        private PrivateChat(Guid id, Guid user1Id, Guid user2Id)
        {
            Id = id;
            User1Id = user1Id;
            User2Id = user2Id;
        }

        public static PrivateChat Create(Guid id, Guid user1Id, Guid user2Id)
        {
            Guid[] userIds = user1Id.CompareTo(user2Id) <= 0
                ? [user1Id, user2Id]
                : [user2Id, user1Id];

            return new PrivateChat(id, userIds[0], userIds[1]);
        }

        public void AddMessage(ChatMessage message) => Messages.Add(message);
    }
}
