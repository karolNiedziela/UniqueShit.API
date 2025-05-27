namespace UniqueShit.Application.Features.Chats
{
    public sealed record ChatResponse(Guid Id, string DisplayName, List<ChatMessageResponse> Messages);
}
