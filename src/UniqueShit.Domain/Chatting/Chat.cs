namespace UniqueShit.Chatting
{
    public sealed class Chat
    {
        public Guid Id { get; private set; }

        public List<ChatUser> Users { get; private set; } = [];

        public List<ChatMessage> Messages { get; private set; } = [];

        private Chat() { }

        public Chat(Guid id)
        {
            Id = id;
        }

        public void AddUsers(List<ChatUser> users) => Users.AddRange(users);

        public void AddMessage(ChatMessage message) => Messages.Add(message);
    }
}
