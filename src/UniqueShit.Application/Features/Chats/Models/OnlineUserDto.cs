namespace UniqueShit.Chatting.Models
{
    public sealed class OnlineUserDto
    {
        public Guid Id { get; set; }

        public string ConnectionId { get; set; } = default!;

        public string DisplayName { get; set; } = default!;
    }
}
