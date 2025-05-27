using Microsoft.AspNetCore.Mvc;
using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Chats.Queries.GetChat
{
    public sealed record GetChatQuery(
        Guid ChatId,
        int PageNumber
        ) : IQuery<ChatResponse?>;
}
