using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Chats.Commands.SendMessage
{
    public sealed record SendMessageCommand(Guid ChatId, string Content) : ICommand<Result<SendMessageResponse>>;
}
