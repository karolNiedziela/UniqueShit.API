namespace UniqueShit.Chatting.Models
{
    public sealed class AppUserDto
    {
        public required int Id { get; set; }

        public required string DisplayName { get; set; }

        public required Guid ADObjectId { get; set; }
    }
}
