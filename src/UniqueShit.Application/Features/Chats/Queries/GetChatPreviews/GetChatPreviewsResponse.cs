namespace UniqueShit.Application.Features.Chats.Queries.GetChatPreviews
{
    public sealed record GetChatPreviewsResponse(
        Guid Id, 
        string DisplayName, 
        string LastMessage, 
        DateTimeOffset SentAt,
        bool IsRead);
}
