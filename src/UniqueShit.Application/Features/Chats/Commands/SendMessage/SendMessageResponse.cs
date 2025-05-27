namespace UniqueShit.Application.Features.Chats.Commands.SendMessage
{
    public sealed record SendMessageResponse(Guid Id, Guid ChatId, Guid SenderId, Guid ReceiverId, string Content, DateTimeOffset SentAt, bool IsRead, bool IsMe = true);
}
