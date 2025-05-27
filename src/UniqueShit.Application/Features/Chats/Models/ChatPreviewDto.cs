namespace UniqueShit.Chatting.Models
{
    public sealed class ChatPreviewDto
    {
        public Guid UserId { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public string LastMessage { get; set; } = string.Empty;

        public DateTimeOffset SentAt { get; set; }

        public bool IsRead { get; set; }
    }
}
