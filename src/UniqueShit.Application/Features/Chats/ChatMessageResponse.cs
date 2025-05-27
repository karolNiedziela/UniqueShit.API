namespace UniqueShit.Application.Features.Chats
{
    public sealed record ChatMessageResponse(
        Guid Id,
        Guid SenderId,
        string Content,
        DateTimeOffset SentAt,
        bool IsRead
    );
}
