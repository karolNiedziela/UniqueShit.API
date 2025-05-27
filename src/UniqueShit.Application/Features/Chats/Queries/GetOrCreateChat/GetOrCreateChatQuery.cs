using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Chats.Queries.GetOrCreateChat
{
    public sealed record GetOrCreateChatQuery(Guid ReceiverId, string DisplayName) : IQuery<ChatResponse>;    
}
